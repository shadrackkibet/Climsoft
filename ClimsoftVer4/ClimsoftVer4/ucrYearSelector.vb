﻿Public Class ucrYearSelector
    'Private strYearsTableName As String = "years"
    Private strYear As String = "Year"
    Private strShortYear As String = "ShortYear"

    Public Overrides Sub PopulateControl()
        'MyBase.PopulateControl()

        Dim endYear As Integer = DateTime.Now.Year   'DateAndTime.Year(DateTime.Today)

        dtbRecords = New DataTable
        dtbRecords.Columns.Add(strYear, GetType(Integer))
        dtbRecords.Columns.Add(strShortYear, GetType(Integer))

        For i As Integer = endYear To endYear - 5 Step -1
            dtbRecords.Rows.Add(i, CInt(Strings.Right(i, 2)))
        Next

        cboValues.DataSource = dtbRecords
        dtbRecords.DefaultView.Sort = strYear & " DESC"
        cboValues.ValueMember = strYear
        If bFirstLoad Then
            SetViewTypeAsYear()
        End If

    End Sub

    Public Function isLeapYear() As Boolean
        Return DateTime.IsLeapYear(GetValue)
    End Function

    Public Overrides Function ValidateValue() As Boolean
        Dim bValid As Boolean = False
        Dim strCol As String

        bValid = MyBase.ValidateValue

        If Not bValid Then
            strCol = cboValues.DisplayMember
            If strCol = strYear Then
                If cboValues.Text.Length = 4 AndAlso Val(cboValues.Text) <= DateTime.Now.Year Then
                    bValid = True
                End If
            ElseIf strCol = strShortYear
                'TODO
                'check validity of short years
            End If
        End If

        SetBackColor(If(bValid, Color.White, Color.Red))
        Return bValid
    End Function

    Private Sub ucrYearSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        cboValues.ContextMenuStrip = cmsYear
    End Sub

    Public Sub SetViewTypeAsYear()
        SetDisplayMember(strYear)
    End Sub

    Public Sub SetViewTypeAsShortYear()
        SetDisplayMember(strShortYear)
    End Sub

    Private Sub cmsYearViewLongYear_Click(sender As Object, e As EventArgs) Handles cmsYearViewLongYear.Click
        SetViewTypeAsYear()
        cmsYearViewShortYear.Checked = False
        cmsYearViewLongYear.Checked = True
    End Sub

    Private Sub cmsYearViewShortYear_Click(sender As Object, e As EventArgs) Handles cmsYearViewShortYear.Click
        SetViewTypeAsShortYear()
        cmsYearViewShortYear.Checked = True
        cmsYearViewLongYear.Checked = False
    End Sub
End Class
