﻿Public Class ucrStationSelector
    Private strStationsTableName As String = "stations"
    Private strStationName As String = "stationName"
    Private strStationID As String = "stationId"
    Private strIDsAndStations As String = "ids_stations"

    Public Overrides Sub PopulateControl()
        MyBase.PopulateControl()
        If dtbRecords.Rows.Count > 0 Then
            'May need ValueMember to be different in different instances e.g. if station name is needed as return value
            'Done. It is now possible to pass the field name into get value. This comment can now be deleted
            cboValues.ValueMember = strStationID
            'TODO 
            'what if there were no records on the first load. 
            'Then there are records later
            If bFirstLoad Then
                SetViewTypeAsStations()
            End If
        Else
            cboValues.DataSource = Nothing
        End If
    End Sub

    Public Sub SetViewTypeAsStations()
        SetDisplayMember(strStationName)
    End Sub

    Public Sub SetViewTypeAsIDs()
        SetDisplayMember(strStationID)
    End Sub

    Public Sub SetViewTypeAsIDsAndStations()
        SetDisplayMember(strIDsAndStations)
    End Sub

    Public Sub SortByID()
        SortBy(strStationID)
        cmsStationSortByID.Checked = True
        cmsStationSortyByName.Checked = False
    End Sub

    Public Sub SortByStationName()
        SortBy(strStationName)
        cmsStationSortByID.Checked = False
        cmsStationSortyByName.Checked = True
    End Sub

    Protected Overrides Sub ucrComboBoxSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dct As Dictionary(Of String, List(Of String))
        If bFirstLoad Then
            'SortByStationName()
            dct = New Dictionary(Of String, List(Of String))
            dct.Add(strStationID, New List(Of String)({strStationID}))
            dct.Add(strStationName, New List(Of String)({strStationName}))
            dct.Add(strIDsAndStations, New List(Of String)({strStationID, strStationName}))
            SetTableNameAndFields(strStationsTableName, dct)
            PopulateControl()
            cboValues.ContextMenuStrip = cmsStation
            SetComboBoxSelectorProperties()
            bFirstLoad = False
        End If
    End Sub

    Private Sub cmsStationName_Click(sender As Object, e As EventArgs) Handles cmsStationNames.Click
        SetViewTypeAsStations()
    End Sub

    Private Sub cmsStationIDs_Click(sender As Object, e As EventArgs) Handles cmsStationIDs.Click
        SetViewTypeAsIDs()
    End Sub

    Private Sub cmsStationIDAndStation_Click(sender As Object, e As EventArgs) Handles cmsStationIDAndStation.Click
        SetViewTypeAsIDsAndStations()
    End Sub

    Private Sub cmsStationSortByID_Click(sender As Object, e As EventArgs) Handles cmsStationSortByID.Click
        SortByID()
    End Sub

    Private Sub cmsStationSortyByName_Click(sender As Object, e As EventArgs) Handles cmsStationSortyByName.Click
        SortByStationName()
    End Sub

    Private Sub cmsFilterStations_Click(sender As Object, e As EventArgs) Handles cmsFilterStations.Click
        ' TODOD SetDataTable() in sdgFilter needs to be created
        'sdgFilter.SetDataTable(dtbStations)
        sdgFilter.ShowDialog()
        PopulateControl()
    End Sub
End Class