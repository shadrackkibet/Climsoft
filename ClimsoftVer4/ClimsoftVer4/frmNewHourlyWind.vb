﻿Public Class frmNewHourlyWind
    Private bFirstLoad As Boolean = True

    Private Sub frmNewHourlyWind_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitaliseDialog()
            bFirstLoad = False
        End If
    End Sub

    Private Sub InitaliseDialog()
        'TODO 
        'If by default the selector is to be sorted by name
        'then this can be removed
        ucrStationSelector.SortByStationName()

        ucrDay.setYearAndMonthLink(ucrYearSelector, ucrMonth)

        ucrHourlyWind.SetSpeedDigits(Val(txtSpeedDigits.Text))
        ucrHourlyWind.SetDirectionDigits(Val(txtDirectionDigits.Text))
        ucrHourlyWind.SetDirectionValidation(112)
        ucrHourlyWind.SetSpeedValidation(111)

        AssignLinkToKeyField(ucrHourlyWind)

        'TO CORRECTLY SORT THE RECORDS IN THE NAVIGATION IN SEQUENCE OF
        'OF HOW THEY WERE SAVED THE entryDatetime NEEDS
        'TO BE INCLUDED. CURRENTLY ITS NOT IN OUR MODEL

        'ucrNavigation.SetTableNameAndFields("form_hourlywind", (New List(Of String)({"stationId", "yyyy", "mm", "dd", "entryDatetime"})))
        ucrNavigation.SetTableNameAndFields("form_hourlywind", (New List(Of String)({"stationId", "yyyy", "mm", "dd"})))
        ucrNavigation.SetKeyControls("stationId", ucrStationSelector)
        ucrNavigation.SetKeyControls("yyyy", ucrYearSelector)
        ucrNavigation.SetKeyControls("mm", ucrMonth)
        ucrNavigation.SetKeyControls("dd", ucrDay)


        'THIS WILL WORK ONCE WE INCLUDE THE entryDatetime AS A FIELD FOR ucrNavigation
        'ucrNavigation.SetSortBy("entryDatetime")
        ucrHourlyWind.SetLinkedNavigation(ucrNavigation)
        ucrNavigation.PopulateControl()

        SaveEnable()

    End Sub

    Private Sub AssignLinkToKeyField(ucrControl As ucrBaseDataLink)
        ucrControl.AddLinkedControlFilters(ucrStationSelector, "stationId", "==", strLinkedFieldName:="stationId", bForceValuesAsString:=True)
        ucrControl.AddLinkedControlFilters(ucrYearSelector, "yyyy", "==", strLinkedFieldName:="Year", bForceValuesAsString:=False)
        ucrControl.AddLinkedControlFilters(ucrMonth, "mm", "==", strLinkedFieldName:="MonthId", bForceValuesAsString:=False)
        ucrControl.AddLinkedControlFilters(ucrDay, "dd", "==", strLinkedFieldName:="day", bForceValuesAsString:=False)
    End Sub

    Private Sub btnHourSelection_Click(sender As Object, e As EventArgs) Handles btnHourSelection.Click
        If btnHourSelection.Text = "Enable all hours" Then
            ucrHourlyWind.SetHourSelection(True)
            btnHourSelection.Text = "Enable synoptic hours only"
        Else
            ucrHourlyWind.SetHourSelection(False)
            btnHourSelection.Text = "Enable all hours"
        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Dim dctSequencerFields As New Dictionary(Of String, List(Of String))

        btnAddNew.Enabled = False
        btnClear.Enabled = True
        btnDelete.Enabled = False
        btnUpdate.Enabled = False
        btnSave.Enabled = True
        'ucrNavigation.SetControlsForNewRecord()

        'change the sequencer
        If ucrYearSelector.isLeapYear Then
            txtSequencer.Text = "seq_month_day_leap_yr"
        Else
            txtSequencer.Text = "seq_month_day"
        End If

        ' temporary until we know how to get all fields from table without specifying names
        dctSequencerFields.Add("mm", New List(Of String)({"mm"}))
        dctSequencerFields.Add("dd", New List(Of String)({"dd"}))

        ucrNavigation.NewSequencerRecord(strSequencer:=txtSequencer.Text, dctFields:=dctSequencerFields, lstDateIncrementControls:=New List(Of ucrDataLinkCombobox)({ucrMonth, ucrDay}), ucrYear:=ucrYearSelector)

        ucrHourlyWind.ucrDirectionSpeedFlag0.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'Check if header information is complete. If the header information is complete and there is at least on obs value then,
            'carry out the next actions, otherwise bring up message showing that there is insufficient data
            If (Not ucrHourlyWind.IsDirectionValuesEmpty) And Strings.Len(ucrStationSelector.GetValue) > 0 And Strings.Len(ucrYearSelector.GetValue) > 0 And Strings.Len(ucrMonth.GetValue) And Strings.Len(ucrDay.GetValue) > 0 Then

                'TODO
                'Check valid station
                'Check valid year
                'Check valid month
                'Check valid Day
                'Check future date
                'MsgBox("Evaluated observation date [ " & DateSerial(yyyy, mm, dd) & "]. Dates greater than today not accepted!", MsgBoxStyle.Critical)

                'Then Do QC Checks. 
                'based on upper & lower limit for wind direction 
                If Not ucrHourlyWind.QcForDirection() Then
                    Exit Sub
                End If
                'based on upper & lower limit for wind speed 
                If Not ucrHourlyWind.CheckQcForSpeed() Then
                    Exit Sub
                End If

                'check total if its required
                If Not ucrHourlyWind.checkTotal() Then
                    Exit Sub
                End If

                'then go ahead and save to database
                If MessageBox.Show("Do you want to continue and commit to database table?", "Save Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ucrHourlyWind.SaveRecord()
                    ucrNavigation.ResetControls()
                    ucrNavigation.GoToNewRecord()
                    SaveEnable()
                    MessageBox.Show("New record added to database table!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Incomplete header information and insufficient observation data!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("New Record has NOT been added to database table. Error: " & ex.Message, "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If MessageBox.Show("Are you sure you want to update this record?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ucrHourlyWind.SaveRecord()
                MessageBox.Show("Record updated successfully!", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Record has NOT been updated. Error: " & ex.Message, "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ucrHourlyWind.DeleteRecord()
                ucrNavigation.RemoveRecord()
                SaveEnable()
                MessageBox.Show("Record has been deleted", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Record has NOT been deleted. Error: " & ex.Message, "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'SAMUEL IS DOING THIS AND I'M NOT SURE WHY BUT I DID IT TO HAVE
        'A SIMILAR IMPLEMENTATION, THE CHECKING OF HEADER INFORMATION
        'COULD BE REMOVED IF ITS NOT NECESSARY
        'Check if header information is complete. If the header information is complete and there is at least on obs value then,
        'carry out the next actions, otherwise bring up message showing that there is insufficient data
        'If (Not ucrHourlyWind.IsDirectionValuesEmpty) AndAlso Strings.Len(ucrStationSelector.GetValue) > 0 AndAlso Strings.Len(ucrYearSelector.GetValue) > 0 AndAlso Strings.Len(ucrMonth.GetValue) AndAlso Strings.Len(ucrDay.GetValue) > 0 Then
        ucrNavigation.ResetControls()
        ucrNavigation.MoveFirst()
        SaveEnable()
        'Else
        'MessageBox.Show("Incomplete header information and insufficient observation data!", "Clear Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub

    'This is from Samuel's code
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Help.ShowHelp(Me, Application.StartupPath & "\climsoft4.chm", "keyentryoperations.htm#form_synopticRA1")
    End Sub

    'This is from Samuel's code
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Dim viewRecords As New dataEntryGlobalRoutines
        Dim sql, userName As String
        userName = frmLogin.txtUsername.Text
        dsSourceTableName = "form_hourlywind"
        If userGroup = "ClimsoftOperator" Or userGroup = "ClimsoftRainfall" Then
            sql = "SELECT * FROM form_hourlywind where signature ='" & userName & "' ORDER by stationId,yyyy,mm,dd;"
        Else
            sql = "SELECT * FROM form_hourlywind ORDER by stationId,yyyy,mm,dd;"
        End If
        viewRecords.viewTableRecords(sql)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        'TODO
    End Sub

    Private Sub SaveEnable()
        btnAddNew.Enabled = True
        btnSave.Enabled = False
        btnClear.Enabled = False
        If ucrNavigation.iMaxRows > 0 Then
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
        End If
    End Sub

End Class
