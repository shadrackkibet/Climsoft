Public Class ucrStationSelector
    Private bFirstLoad As Boolean = True
    Private strTypeStations As String = "stationName"
    Private strTypeIDs As String = "stationId"
    Private strTypeIDsAndStations As String = "stations_ids"
    Private dtbStations As New DataTable

    Private Sub PopulateStationList()
        If dtbStations IsNot Nothing Then
            cboStations.DataSource = dtbStations.DefaultView.ToTable()
            ' May need ValueMember to be different in different instances e.g. if station name is needed as return value
            cboStations.ValueMember = strTypeIDs
            If bFirstLoad Then
                SetViewTypeAsStations()
            End If
        End If
    End Sub

    Private Sub SetViewType(strViewType As String)
        tsmStationNames.Checked = False
        tsmIDs.Checked = False
        tsmIDsAndStations.Checked = False
        Select Case strViewType
            Case strTypeStations
                tsmStationNames.Checked = True
                cboStations.DisplayMember = strTypeStations
            Case strTypeIDs
                tsmIDs.Checked = True
                cboStations.DisplayMember = strTypeIDs
            Case strTypeIDsAndStations
                tsmIDsAndStations.Checked = True
                cboStations.DisplayMember = strTypeIDsAndStations
        End Select
    End Sub

    Public Sub SetViewTypeAsStations()
        SetViewType(strTypeStations)
    End Sub

    Public Sub SetViewTypeAsIDs()
        SetViewType(strTypeIDs)
    End Sub

    Public Sub SetViewTypeAsStationsAndIDs()
        SetViewType(strTypeIDsAndStations)
    End Sub

    Private Sub ucrStationSelector_Load(sender As Object, e As EventArgs) Handles Me.Load
        If bFirstLoad Then
            InitialiseStationDataTable()
            SortByID()
            PopulateStationList()
            bFirstLoad = False
        End If
    End Sub

    Private Sub InitialiseStationDataTable()
        Dim dataAdapter As New MySql.Data.MySqlClient.MySqlDataAdapter
        Dim ds As New DataSet
        Dim item As New ListViewItem
        Dim sql As String

        Try
            sql = "SELECT * FROM station;"
            dataAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, frmLogin.txtusrpwd.Text)
            dataAdapter.Fill(ds, "station")
            dtbStations = ds.Tables("station")
            dtbStations.Columns.Add(strTypeIDsAndStations, GetType(String))
            For i = 0 To dtbStations.Rows.Count - 1
                dtbStations.Rows(i).Item(strTypeIDsAndStations) = dtbStations.Rows(i).Item(strTypeIDs) & " " & dtbStations.Rows(i).Item(strTypeStations)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            dtbStations = Nothing
        End Try
    End Sub

    Private Sub tsmStations_Click(sender As Object, e As EventArgs) Handles tsmStationNames.Click
        SetViewTypeAsStations()
    End Sub

    Private Sub tsmIDs_Click(sender As Object, e As EventArgs) Handles tsmIDs.Click
        SetViewTypeAsIDs()
    End Sub

    Private Sub tsmStationsAndIDs_Click(sender As Object, e As EventArgs) Handles tsmIDsAndStations.Click
        SetViewTypeAsStationsAndIDs()
    End Sub

    Private Sub tsmSortByID_Click(sender As Object, e As EventArgs) Handles tsmSortByID.Click
        SortByID()
    End Sub

    Private Sub SortByID()
        dtbStations.DefaultView.Sort = strTypeIDs & " ASC"
        tsmSortByID.Checked = True
        tsmSortByStationName.Checked = False
        PopulateStationList()
    End Sub

    Private Sub tsmSortByStationName_Click(sender As Object, e As EventArgs) Handles tsmSortByStationName.Click
        SortByStationName()
    End Sub

    Private Sub SortByStationName()
        dtbStations.DefaultView.Sort = strTypeStations & " ASC"
        tsmSortByID.Checked = False
        tsmSortByStationName.Checked = True
        PopulateStationList()
    End Sub
End Class
