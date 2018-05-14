﻿Public Class ucrElementSelector
    Private strElementTableName As String = "obselements"
    Private strElementName As String = "elementName"
    Private strElementID As String = "elementId"
    Private strIDsAndElements As String = "ids_elements"

    Public Overrides Sub PopulateControl()
        MyBase.PopulateControl()
        If dtbRecords.Rows.Count > 0 Then
            cboValues.ValueMember = strElementID
            'TODO 
            'what if there were no records on the first load. 
            'Then there are records later
            If bFirstLoad Then
                SetViewTypeAsElements()
            End If
        Else
            cboValues.DataSource = Nothing
        End If
    End Sub

    Public Sub SetViewTypeAsElements()
        SetViewType(strElementName)
    End Sub

    Public Sub SetViewTypeAsIDs()
        SetViewType(strElementID)
    End Sub

    Public Sub SetViewTypeAsIDsAndElements()
        SetViewType(strIDsAndElements)
    End Sub

    Private Sub SortByID()
        SortBy(strElementID)
        cmsElementSortByID.Checked = True
        cmsElementSortyByName.Checked = False
    End Sub

    Private Sub SortByElementName()
        SortBy(strElementName)
        cmsElementSortByID.Checked = False
        cmsElementSortyByName.Checked = True
    End Sub

    Protected Overrides Sub ucrComboBoxSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dct As New Dictionary(Of String, List(Of String))
        If bFirstLoad Then
            'SortByStationName()
            SetTableName(strElementTableName)
            dct.Add(strElementName, New List(Of String)({strElementName}))
            dct.Add(strElementID, New List(Of String)({strElementID}))
            dct.Add(strIDsAndElements, New List(Of String)({strElementID, strElementName}))
            SetFields(dct)
            PopulateControl()
            cboValues.ContextMenuStrip = cmsElement
            SetComboBoxSelectorProperties()
            bFirstLoad = False
        End If
    End Sub

    Private Sub cmsElementsNames_Click(sender As Object, e As EventArgs) Handles cmsElementsNames.Click
        SetViewTypeAsElements()
    End Sub

    Private Sub cmsElementIDs_Click(sender As Object, e As EventArgs) Handles cmsElementIDs.Click
        SetViewTypeAsIDs()
    End Sub

    Private Sub cmsElemntIDName_Click(sender As Object, e As EventArgs) Handles cmsElemntIDName.Click
        SetViewTypeAsIDsAndElements()
    End Sub

    Private Sub tsmSortByID_Click(sender As Object, e As EventArgs) Handles cmsElementSortByID.Click
        SortByID()
    End Sub

    Private Sub cmsElementSortyByName_Click(sender As Object, e As EventArgs) Handles cmsElementSortyByName.Click
        SortByElementName()
    End Sub

    Private Sub cmsElementsFilter_Click(sender As Object, e As EventArgs) Handles cmsElementsFilter.Click
        ' TODOD SetDataTable() in sdgFilter needs to be created
        'sdgFilter.SetDataTable(dtbElements)
        sdgFilter.ShowDialog()
        PopulateControl()
    End Sub
End Class

