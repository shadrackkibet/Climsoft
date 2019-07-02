﻿Public Class ucrMetadataStationElement

    Private Sub ucrMetadataStationElement_Load(sender As Object, e As EventArgs) Handles Me.Load
        If bFirstLoad Then
            'SetUpTableEntry 
            SetUpTableEntry("stationelement")

            ucrStationSelector.SetViewTypeAsIDs()
            ucrElementSelector.SetViewTypeAsIDs()

            ucrDataLinkInstrumentID.SetTableNameAndField("instrument", "instrumentId")
            ucrDataLinkInstrumentID.PopulateControl()
            ucrDataLinkInstrumentID.SetDisplayAndValueMember("instrumentId")

            ucrDataLinkScheduleClass.SetTableNameAndField("obsscheduleclass", "scheduleClass")
            ucrDataLinkScheduleClass.PopulateControl()
            ucrDataLinkScheduleClass.SetDisplayAndValueMember("scheduleClass")

            'validations
            ucrTextHeight.SetValidationTypeAsNumeric()
            ucrDatePickerBeginDate.bValidateEmpty = True

            AddLinkedControlFilters(ucrStationSelector, ucrStationSelector.FieldName(), "=", strLinkedFieldName:="stationId", bForceValuesAsString:=True)
            AddLinkedControlFilters(ucrElementSelector, ucrElementSelector.FieldName(), "=", strLinkedFieldName:="elementId", bForceValuesAsString:=False)
            AddLinkedControlFilters(ucrDataLinkInstrumentID, ucrDataLinkInstrumentID.FieldName(), "=", strLinkedFieldName:="instrumentId", bForceValuesAsString:=True)
            AddLinkedControlFilters(ucrDatePickerBeginDate, ucrDatePickerBeginDate.FieldName(), "=", bForceValuesAsString:=True)

            'set FILTER field name used in the where clause of UPDATE and DELETE statement
            AddKeyField(ucrStationSelector.FieldName)

            'set up the navigation control
            ucrNavigationStationElement.SetTableEntry(Me)
            ucrNavigationStationElement.AddKeyControls(ucrStationSelector)
            ucrNavigationStationElement.AddKeyControls(ucrElementSelector)
            ucrNavigationStationElement.AddKeyControls(ucrDataLinkInstrumentID)
            ucrNavigationStationElement.AddKeyControls(ucrDatePickerBeginDate)

            bFirstLoad = False

            'populate the values
            ucrNavigationStationElement.PopulateControl()

        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        ucrNavigationStationElement.NewRecord()
        SaveEnable()
        ucrStationSelector.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not ValidateValue() Then
                Exit Sub
            End If

            'Confirm if you want to continue and save data from key-entry form to database table
            If MessageBox.Show("Do you want to continue and commit to database table?", "Save Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If InsertRecord() Then
                    ucrNavigationStationElement.GoToNewRecord()
                    SaveEnable()
                    MessageBox.Show("New record added to database table!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("New Record has NOT been added to database table. Error: " & ex.Message, "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If Not ValidateValue() Then
                Exit Sub
            End If

            If MessageBox.Show("Are you sure you want to update this record?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If UpdateRecord() Then
                    MessageBox.Show("Record updated successfully!", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
        MessageBox.Show("Record has NOT been updated. Error: " & ex.Message, "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not ValidateValue() Then
                Exit Sub
            End If

            If MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If DeleteRecord() Then
                    ucrNavigationStationElement.RemoveRecord()
                    SaveEnable()
                    MessageBox.Show("Record has been deleted", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Record has NOT been deleted. Error: " & ex.Message, "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Move to the first record of datatable
        ucrNavigationStationElement.MoveFirst()
        'Enable appropriate base buttons
        SaveEnable()
    End Sub

    ''' <summary>
    ''' Enables appropriately the base buttons on Delete, Save, Add New, Clear and on dialog load
    ''' </summary>
    Private Sub SaveEnable()
        btnAddNew.Enabled = True
        btnSave.Enabled = False
        btnClear.Enabled = False
        btnDelete.Enabled = False
        btnUpdate.Enabled = False

        If ucrNavigationStationElement.iCurrRow = -1 Then
            btnAddNew.Enabled = False
            btnClear.Enabled = True
            btnDelete.Enabled = False
            btnUpdate.Enabled = False
            btnSave.Enabled = True
        ElseIf ucrNavigationStationElement.iMaxRows = 0 Then
            btnAddNew.Enabled = False
            btnSave.Enabled = True
        ElseIf ucrNavigationStationElement.iMaxRows > 0 Then
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
        End If
    End Sub
End Class

