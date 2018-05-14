﻿Public Class ucrDataLinkCombobox
    Protected bFirstLoad As Boolean = True
    Public bFillFromDataBase As Boolean = True

    Public Overrides Sub PopulateControl()
        If bFillFromDataBase Then
            MyBase.PopulateControl()
        End If
        cboValues.DataSource = dtbRecords
    End Sub

    Public Sub SetPossibleValues(dtbNewRecords As DataTable)
        dtbRecords = dtbNewRecords
        bFillFromDataBase = False
    End Sub

    Public Sub SetPossibleValues(strColumnName As String, tColumnType As Type, lstValues As IEnumerable(Of Object))
        SetPossibleValues(New DataTable)
        dtbRecords.Columns.Add(strColumnName, tColumnType)
        For Each objVal In lstValues
            dtbRecords.Rows.Add(objVal)
        Next
    End Sub
    'NOT SURE ABOUT THIS BUT I SUSPECT IT NEEDS TO BE OVERRIDEN 
    'ALTERNATIVELY WE COULD OVERRIDE THE sub UpdateDataTable() 
    Protected Overrides Sub LinkedControls_evtValueChanged()
        If Not IsNothing(clsDataDefinition) Then
            Dim objData As List(Of String)
            Dim strDbValue As String
            objData = clsDataDefinition.GetValues(GetLinkedControlsFilter())
            If objData.Count > 0 Then
                strDbValue = objData(0)
                For index As Integer = 0 To dtbRecords.Rows.Count - 1
                    If dtbRecords.Rows(index).Field(Of String)(0) = strDbValue Then
                        cboValues.SelectedIndex = index
                    End If
                Next
            End If
        End If
    End Sub

    Public Overrides Sub SetValue(objNewValue As Object)
        Dim strCol As String

        MyBase.SetValue(objNewValue)

        strCol = cboValues.ValueMember

        For Each rTemp As DataRow In dtbRecords.Rows
            'Calling ToString to prevent invalid casting
            If rTemp.Item(strCol).ToString = objNewValue.ToString Then
                cboValues.SelectedValue = objNewValue
                Exit Sub
            End If
        Next
        cboValues.Text = objNewValue
        'TODO possibly want this as well?
        'cboValues.SelectedIndex = -1
    End Sub

    Public Overrides Function ValidateValue() As Boolean
        Return cboValues.Items.Contains(cboValues.Text)
    End Function

    ' If the text in the textbox portion does not match any of the items in the dropdown then GetValue will always return cboValue.Text
    ' regardless of strFieldName
    ' If the text matches an item then the value from strFieldName for the selected item will be returned.
    ' When strFieldName is not specified cboValue.SelectedValue will be returned.
    Public Overrides Function GetValue(Optional strFieldName As String = "") As Object
        Dim strCol As String

        strCol = cboValues.DisplayMember
        For Each rTemp As DataRow In dtbRecords.Rows
            If rTemp(strCol) = cboValues.Text Then
                If strFieldName = "" Then
                    Return cboValues.SelectedValue
                Else
                    Return rTemp(strFieldName)
                End If
            End If
        Next
        Return cboValues.Text
    End Function

    Public Sub SetViewType(strViewType As String)
        If dtbRecords.Columns.Contains(strViewType) Then
            cboValues.DisplayMember = strViewType
        End If
    End Sub

    ''' <summary>
    ''' Sorts the datatable in ascending order based on the c
    ''' column specified as the parameter. This in turn sorts
    ''' the combobox values
    ''' </summary>
    ''' <param name="strCol"></param>
    Public Sub SortBy(strCol As String)
        If dtbRecords IsNot Nothing Then
            'Datatable Sorting affects cboValues.SelectedValue
            'thus SuppressChange And retain previous cboValues.SelectedValue 
            Dim prevSelected = GetValue()
            bSuppressChangedEvents = True
            dtbRecords.DefaultView.Sort = strCol & " ASC"
            SetValue(prevSelected)
            bSuppressChangedEvents = False
        End If
    End Sub

    Protected Sub SetComboBoxSelectorProperties(Optional comboBoxStyle As ComboBoxStyle = ComboBoxStyle.DropDown, Optional autoCompletSource As AutoCompleteSource = AutoCompleteSource.ListItems, Optional autoCompleteMode As AutoCompleteMode = AutoCompleteMode.SuggestAppend)
        cboValues.DropDownStyle = comboBoxStyle
        cboValues.AutoCompleteSource = autoCompletSource
        cboValues.AutoCompleteMode = autoCompleteMode
    End Sub

    Protected Overridable Sub ucrComboBoxSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        If bFirstLoad Then
            PopulateControl()
            bFirstLoad = False
        End If
    End Sub

    Private Sub cboValues_KeyDown(sender As Object, e As KeyEventArgs) Handles cboValues.KeyDown
        OnevtKeyDown(sender, e)
    End Sub

    Private Sub cboValues_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboValues.SelectedValueChanged
        OnevtValueChanged(sender, e)
    End Sub

    Private Sub cboValues_TextChanged(sender As Object, e As EventArgs) Handles cboValues.TextChanged
        OnevtTextChanged(sender, e)
    End Sub

    Private Sub cboValues_Leave(sender As Object, e As EventArgs) Handles cboValues.Leave
        SetValue(cboValues.Text)
    End Sub
End Class
