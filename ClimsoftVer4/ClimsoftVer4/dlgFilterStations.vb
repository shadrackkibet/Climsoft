Imports System.ComponentModel

Public Class dlgFilterStations
    Private dtbStations As DataTable

    Private Sub dlgFilterStations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboStationProperties.Items.Clear()
        For Each column As DataColumn In dtbStations.Columns
            cboStationProperties.Items.Add(column.ColumnName)
        Next
        cboStationProperties.SelectedIndex = 0
    End Sub

    Public Sub SetDataTable(dtbNewStations As DataTable)
        dtbStations = dtbNewStations
    End Sub

    Private Sub cboStationProperties_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStationProperties.SelectedIndexChanged
        Dim view As New DataView(dtbStations)
        Dim dtbTemp As DataTable

        CheckedListBox1.Items.Clear()
        'Gets unique values from column
        dtbTemp = view.ToTable(True, cboStationProperties.Text)
        For i As Integer = 0 To dtbTemp.Rows.Count - 1
            CheckedListBox1.Items.Add(dtbTemp.Rows(i).Item(0))
            CheckedListBox1.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub SetFilter()
        Dim strSelectedItems As String
        Dim bFirst As Boolean = True

        If CheckedListBox1.CheckedItems.Count > 0 Then
            strSelectedItems = "("
            For Each strTemp As String In CheckedListBox1.CheckedItems
                If Not bFirst Then
                    strSelectedItems = strSelectedItems & ", "
                End If
                strSelectedItems = strSelectedItems & Chr(39) & strTemp & Chr(39)
                bFirst = False
            Next
            strSelectedItems = strSelectedItems & ")"
            dtbStations.DefaultView.RowFilter() = cboStationProperties.Text & " IN " & strSelectedItems
        Else
            dtbStations.DefaultView.RowFilter() = ""
        End If
    End Sub

    Private Sub dlgFilterStations_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        SetFilter()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, False)
        Next
    End Sub
End Class