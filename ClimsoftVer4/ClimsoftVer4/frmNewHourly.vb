﻿Public Class frmNewHourly
    Private bFirstLoad As Boolean = True
    Dim selectAllHours As Boolean

    Private Sub frmNewHourly_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitaliseDialog()
            bFirstLoad = False
        End If
        selectAllHours = False
    End Sub

    Private Sub InitaliseDialog()
        txtSequencer.Text = "seq_month_day"
        ucrDay.setYearAndMonthLink(ucrYearSelector, ucrMonth)
        ucrHourly.SetKeyControls(ucrStationSelector, ucrElementSelector, ucrYearSelector, ucrMonth, ucrDay, ucrHourlyNavigation)
        ucrHourlyNavigation.PopulateControl()
        SaveEnable()
    End Sub

    Private Sub cmdAssignSameValue_Click(sender As Object, e As EventArgs) Handles cmdAssignSameValue.Click
        Dim ctl As Control
        Dim ctrltemp As ucrValueFlagPeriod

        'Adds values to only enabled controls of the ucrHourly
        For Each ctl In ucrHourly.Controls
            If TypeOf ctl Is ucrValueFlagPeriod Then
                ctrltemp = ctl
                If ctrltemp.ucrValue.Enabled Then
                    ctrltemp.ucrValue.SetValue(ucrInputValue.GetValue())
                End If
            End If
        Next
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        Try
            If Not ValidateValues() Then
                Exit Sub
            End If

            'Confirm if you want to continue and save data from key-entry form to database table
            If MessageBox.Show("Do you want to continue and commit to database table?", "Save Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ucrHourly.SaveRecord()
                ucrHourlyNavigation.ResetControls()
                ucrHourlyNavigation.GoToNewRecord()
                SaveEnable()
                MessageBox.Show("New record added to database table!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Record not Saved", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("New Record has NOT been added to database table. Error: " & ex.Message, "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If Not ValidateValues() Then
                Exit Sub
            End If

            If MessageBox.Show("Are you sure you want to update this record?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ucrHourly.SaveRecord()
                MessageBox.Show("Record updated successfully!", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Record has NOT been updated. Error: " & ex.Message, "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                ucrHourly.DeleteRecord()
                ucrHourlyNavigation.RemoveRecord()
                SaveEnable()
                MessageBox.Show("Record has been deleted", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            ucrHourlyNavigation.MoveFirst()
        Catch ex As Exception
            MessageBox.Show("Record has NOT been deleted. Error: " & ex.Message, "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ucrHourlyNavigation.ResetControls()
        ucrHourlyNavigation.MoveFirst()
        SaveEnable()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim viewRecords As New dataEntryGlobalRoutines
        Dim sql, userName As String
        dsSourceTableName = "form_hourly"
        userName = frmLogin.txtUsername.Text
        If userGroup = "ClimsoftOperator" Or userGroup = "ClimsoftRainfall" Then
            sql = "SELECT * FROM form_hourly where signature ='" & userName & "' ORDER by stationId,elementId,yyyy,mm,dd;"
        Else
            sql = "SELECT * FROM form_hourly ORDER by stationId,elementId,yyyy,mm,dd;"
        End If
        viewRecords.viewTableRecords(sql)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub



    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Dim dctSequencerFields As New Dictionary(Of String, List(Of String))

        btnAddNew.Enabled = False
        btnClear.Enabled = True
        btnDelete.Enabled = False
        btnUpdate.Enabled = False
        btnCommit.Enabled = True

        If ucrYearSelector.isLeapYear Then
            txtSequencer.Text = "seq_month_day_leap_yr"
        Else
            txtSequencer.Text = "seq_month_day"
        End If

        dctSequencerFields.Add("mm", New List(Of String)({"mm"}))
        dctSequencerFields.Add("dd", New List(Of String)({"dd"}))
        ucrHourlyNavigation.NewSequencerRecord(strSequencer:=txtSequencer.Text, dctFields:=dctSequencerFields, lstDateIncrementControls:=New List(Of ucrDataLinkCombobox)({ucrDay, ucrMonth}), ucrYear:=ucrYearSelector)

        ucrHourly.UcrValueFlagPeriod0.Focus()
        ucrHourlyNavigation.MoveLast()
    End Sub

    Private Sub SaveEnable()
        btnAddNew.Enabled = True
        btnCommit.Enabled = False
        btnClear.Enabled = False
        If ucrHourlyNavigation.iMaxRows > 0 Then
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
        Else
            btnDelete.Enabled = False
            btnUpdate.Enabled = False
        End If
    End Sub

    'Changes the date entry fields betwen synoptc hours and all hours
    Private Sub btnHourSelection_Click(sender As Object, e As EventArgs) Handles btnHourSelection.Click

        Dim ctrVFP As ucrValueFlagPeriod
        If selectAllHours Then
            selectAllHours = False
            btnHourSelection.Text = "Enable synoptic hours only"
            For Each ctr As Control In ucrHourly.Controls
                If TypeOf ctr Is ucrValueFlagPeriod Then
                    ctrVFP = DirectCast(ctr, ucrValueFlagPeriod)
                    ctrVFP.Enabled = True
                    ctrVFP.SetBackColor(Color.White)
                End If
            Next
        Else
            selectAllHours = True
            btnHourSelection.Text = "Enable all hours"
            Dim clsDataDefinition As DataCall
            Dim dtbl As DataTable
            Dim iTagVal As Integer
            Dim row As DataRow
            clsDataDefinition = New DataCall
            clsDataDefinition.SetTableName("form_hourly_time_selection")
            clsDataDefinition.SetFields(New List(Of String)({"hh", "hh_selection"}))
            dtbl = clsDataDefinition.GetDataTable()
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For Each ctr As Control In ucrHourly.Controls
                    If TypeOf ctr Is ucrValueFlagPeriod Then
                        ctrVFP = DirectCast(ctr, ucrValueFlagPeriod)
                        iTagVal = Val(Strings.Right(ctrVFP.Tag, 2))
                        row = dtbl.Select("hh = '" & iTagVal & "' AND hh_selection = '0'").FirstOrDefault()
                        If row IsNot Nothing Then
                            ctrVFP.Enabled = False
                            ctrVFP.SetBackColor(Color.LightYellow)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Help.ShowHelp(Me, Application.StartupPath & "\climsoft4.chm", "keyentryoperations.htm#form_hourly")
    End Sub

    Private Function ValidateValues() As Boolean
        If Not ucrStationSelector.ValidateValue Then
            MsgBox("Invalid Station", MsgBoxStyle.Exclamation)
            Return False
        End If

        If Not ucrElementSelector.ValidateValue Then
            MsgBox("Invalid Element", MsgBoxStyle.Exclamation)
                Return False
            End If

            If Not ucrMonth.ValidateValue Then
            MsgBox("Invalid Element", MsgBoxStyle.Exclamation)
            Return False
        End If

        If Not ucrYearSelector.ValidateValue Then
            MsgBox("Invalid Year", MsgBoxStyle.Exclamation)
            Return False
        End If

        If Not ucrDay.ValidateValue Then
            MsgBox("Invalid Day", MsgBoxStyle.Exclamation)
            Return False
        End If

        'Check if all values are empty. There should be atleast one observation value
        If ucrHourly.IsValuesEmpty() Then
            MessageBox.Show("Insufficient observation data!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        'check if all values are valid
        If Not ucrHourly.IsValuesValid Then
            Return False
        End If

        'check computed total vs input total
        If Not ucrHourly.checkTotal Then
            Return False
        End If

        Return True
    End Function

    Private Sub AllControls_KeyDown(sender As Object, e As KeyEventArgs) Handles ucrYearSelector.evtKeyDown, ucrStationSelector.evtKeyDown, ucrMonth.evtKeyDown, ucrHourly.evtKeyDown, ucrElementSelector.evtKeyDown, ucrDay.evtKeyDown
        If e.KeyCode = Keys.Enter Then
            Me.SelectNextControl(sender, True, True, True, True)
        End If
    End Sub
End Class