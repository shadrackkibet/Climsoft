﻿Imports System.Linq.Dynamic

Public Class ucrFormDaily2
    'Boolean to check if control is loading for first time
    Private bFirstLoad As Boolean = True
    'sets table name assocaited with this user control
    Private strTableName As String = "form_daily2"
    'These store field names for value, flag and period
    Private strValueFieldName As String = "day"
    Private strFlagFieldName As String = "flag"
    Private strPeriodFieldName As String = "period"
    Private strTotalFieldName As String = "total"
    'These store instances of linked controls
    Private ucrLinkedMonth As ucrMonth
    Private ucrLinkedYear As ucrYearSelector
    Private ucrLinkedUnits As New Dictionary(Of String, ucrDataLinkCombobox)
    'Stores fields for the value flag and period
    Private lstFields As New List(Of String)
    'Stores the record assocaited with this control
    Public fd2Record As form_daily2
    'Boolean to check if record is updating
    Public bUpdating As Boolean = False
    'Stores the list of containing for the  ucrValueFlagPeriod  controls
    Private lstValueFlagPeriodControls As List(Of ucrValueFlagPeriod)
    Private lstTextboxControls As List(Of ucrTextBox)
    'Stores Linked navigation control
    Private ucrLinkedNavigation As ucrNavigation
    'stores a list containing all fields of this control
    Private lstAllFields As New List(Of String)
    Private ElementId As Integer

    ''' <summary>
    ''' Sets the values of the controls to the coresponding record values in the database with the current key
    ''' </summary>
    Public Overrides Sub PopulateControl()
        Dim ctrVFP As New ucrValueFlagPeriod
        Dim ctrTotal As New ucrTextBox
        Dim clsCurrentFilter As TableFilter

        If Not bFirstLoad Then
            MyBase.PopulateControl()
            If fd2Record Is Nothing Then
                clsCurrentFilter = GetLinkedControlsFilter()
                Try
                    Dim y = clsDataConnection.db.form_daily2.Where(clsCurrentFilter.GetLinqExpression())
                    If y.Count() = 1 Then
                        fd2Record = y.FirstOrDefault()
                        bUpdating = True
                    Else
                        fd2Record = New form_daily2
                        bUpdating = False
                    End If
                Catch ex As Exception
                    'TODO Is this correct?
                    fd2Record = New form_daily2
                    bUpdating = False
                End Try
                'This is determined by the current user not set from the form
                fd2Record.signature = frmLogin.txtUsername.Text
            End If
            For Each ucrVFP As ucrValueFlagPeriod In lstValueFlagPeriodControls
                ucrVFP.SetValue(New List(Of Object)({GetValue(strValueFieldName & ucrVFP.Tag), GetValue(strFlagFieldName & ucrVFP.Tag), GetValue(strPeriodFieldName & ucrVFP.Tag)}))
            Next
            For Each ucrText As ucrTextBox In lstTextboxControls
                ucrText.SetValue(GetValue(strTotalFieldName))
            Next
        End If
    End Sub

    Private Sub ucrFormDaily2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ctr As Control
        Dim ctrVFP As New ucrValueFlagPeriod
        Dim ctrTotal As New ucrTextBox

        If bFirstLoad Then
            lstValueFlagPeriodControls = New List(Of ucrValueFlagPeriod)
            lstTextboxControls = New List(Of ucrTextBox)
            For Each ctr In Me.Controls
                If TypeOf ctr Is ucrValueFlagPeriod Then
                    lstValueFlagPeriodControls.Add(ctr)
                    ctrVFP = DirectCast(ctr, ucrValueFlagPeriod)
                    lstFields.Add(strValueFieldName & ctrVFP.Tag)
                    lstFields.Add(strFlagFieldName & ctrVFP.Tag)
                    lstFields.Add(strPeriodFieldName & ctrVFP.Tag)
                    ctrVFP.SetTableNameAndValueFlagPeriodFields(strTableName, strValueFieldName:=strValueFieldName & ctrVFP.Tag, strFlagFieldName:=strFlagFieldName & ctrVFP.Tag, strPeriodFieldName:=strPeriodFieldName & ctrVFP.Tag)

                    AddHandler ctrVFP.ucrValue.evtValueChanged, AddressOf InnerControlValueChanged
                    AddHandler ctrVFP.ucrFlag.evtValueChanged, AddressOf InnerControlValueChanged
                    AddHandler ctrVFP.ucrPeriod.evtValueChanged, AddressOf InnerControlValueChanged
                    AddHandler ctrVFP.evtGoToNextVFPControl, AddressOf GoToNextVFPControl
                ElseIf TypeOf ctr Is ucrTextBox Then
                    lstTextboxControls.Add(ctr)
                    ctrTotal = ctr
                    ctrTotal.SetTableName(strTableName)
                    ctrTotal.SetField(strTotalFieldName)
                    lstFields.Add(strTotalFieldName)
                    AddHandler ctrTotal.evtValueChanged, AddressOf InnerControlValueChanged
                End If
            Next
            SetTableName(strTableName)
            SetFields(lstFields)

            ' This list is used for uploading to observation table so all fields needed.
            lstAllFields.AddRange(lstFields)
            'TODO "entryDatetime" should be here as well once entity model has been updated.
            lstAllFields.AddRange({"stationId", "elementId", "yyyy", "mm", "hh", "signature", "temperatureUnits", "precipUnits", "cloudHeightUnits", "visUnits"})
            bFirstLoad = False
            EnableDaysofMonth()
        End If
    End Sub
    ''' <summary>
    ''' Sets the linked navigation control
    ''' </summary>
    ''' <param name="ucrNewNavigation"></param>
    Public Sub SetLinkedNavigation(ucrNewNavigation As ucrNavigation)
        ucrLinkedNavigation = ucrNewNavigation
    End Sub


    Public Overrides Sub AddLinkedControlFilters(ucrLinkedDataControl As ucrBaseDataLink, tblFilter As TableFilter, Optional strFieldName As String = "")
        Dim ctrVFP As New ucrValueFlagPeriod
        Dim ctrTotal As New ucrTextBox

        MyBase.AddLinkedControlFilters(ucrLinkedDataControl, tblFilter, strFieldName)
        If Not lstFields.Contains(tblFilter.GetField) Then
            lstFields.Add(tblFilter.GetField)
            SetFields(lstFields)
        End If

    End Sub

    Private Sub InnerControlValueChanged(sender As Object, e As EventArgs)
        Dim ctr As ucrTextBox

        If TypeOf sender Is ucrTextBox Then
            ctr = DirectCast(sender, ucrTextBox)
            CallByName(fd2Record, ctr.GetField, CallType.Set, ctr.GetValue)
        End If
    End Sub

    Private Sub GoToNextVFPControl(sender As Object, e As EventArgs)
        Dim ctr As Control
        Dim ctrVFP As New ucrValueFlagPeriod

        If TypeOf sender Is ucrValueFlagPeriod Then
            ctrVFP = sender
            For Each ctr In Me.Controls
                If TypeOf ctr Is ucrValueFlagPeriod Then
                    If ctr.Tag = ctrVFP.Tag + 1 Then
                        If ctr.Enabled Then
                            ctr.Focus()
                        End If
                    End If
                End If
            Next
        End If

    End Sub

    Protected Overrides Sub LinkedControls_evtValueChanged()
        'need an if statement that checks for changes 
        fd2Record = Nothing
        MyBase.LinkedControls_evtValueChanged()
        EnableDaysofMonth()

        'Dim ctr As Control
        'Dim ctrVFP As ucrDataLinkCombobox
        'Dim ctrTotal As New ucrTextBox
        'For Each ctr In Me.Controls
        '    If TypeOf ctr Is ucrValueFlagPeriod Then
        '        ctrVFP = ctr
        '        CallByName(fd2Record, strValueFieldName & ctrVFP.Tag, CallType.Set, ctrVFP.ucrValue.GetValue)
        '        CallByName(fd2Record, strFlagFieldName & ctrVFP.Tag, CallType.Set, ctrVFP.ucrFlag.GetValue)
        '        CallByName(fd2Record, strPeriodFieldName & ctrVFP.Tag, CallType.Set, ctrVFP.ucrPeriod.GetValue)
        '    ElseIf TypeOf ctr Is ucrTextBox Then
        '        ctrTotal = ctr
        '        CallByName(fd2Record, strTotalFieldName, CallType.Set, ctrTotal.GetValue)
        '    End If

        'Next

        For Each kvpTemp As KeyValuePair(Of ucrBaseDataLink, KeyValuePair(Of String, TableFilter)) In dctLinkedControlsFilters
            CallByName(fd2Record, kvpTemp.Value.Value.GetField(), CallType.Set, kvpTemp.Key.GetValue)
        Next
        ucrLinkedNavigation.UpdateNavigationByKeyControls()
        SetValueUpperAndLowerLimitsValidation()
    End Sub
    ''' <summary>
    ''' Enables the day of month fields equivalent to the days of that month
    ''' </summary>

    Private Sub EnableDaysofMonth()

        Dim iMonthLength As Integer

        If ucrLinkedYear Is Nothing OrElse ucrLinkedMonth Is Nothing Then
            iMonthLength = 31
        Else
            iMonthLength = DateTime.DaysInMonth(ucrLinkedYear.GetValue, ucrLinkedMonth.GetValue())
        End If

        For Each ctrVFP As ucrValueFlagPeriod In {ucrValueFlagPeriod29, ucrValueFlagPeriod30, ucrValueFlagPeriod31}
            If ctrVFP.Tag <= iMonthLength Then
                ctrVFP.Enabled = True
            Else
                ctrVFP.Enabled = False
            End If
        Next
    End Sub
    ''' <summary>
    ''' Sets the linked year and month controls
    ''' </summary>
    ''' <param name="ucrYearControl"></param>
    ''' <param name="ucrMonthControl"></param>
    Public Sub SetYearAndMonthLink(ucrYearControl As ucrYearSelector, ucrMonthControl As ucrMonth)
        ucrLinkedYear = ucrYearControl
        ucrLinkedMonth = ucrMonthControl
    End Sub
    ''' <summary>
    ''' Sets the  filed name and the control for the liked units
    ''' </summary>
    ''' <param name="strFieldName"></param>
    ''' <param name="ucrComboBox"></param>
    Public Sub AddUnitslink(strFieldName As String, ucrComboBox As ucrDataLinkCombobox)

        If ucrLinkedUnits.ContainsKey(strFieldName) Then
            ucrLinkedUnits.Add(strFieldName, ucrComboBox)
        Else
            MessageBox.Show("Developer error: This field is already linked.", caption:="Developer error")
        End If

        If Not lstFields.Contains(strFieldName) Then
            lstFields.Add(strFieldName)
            SetFields(lstFields)
            PopulateControl()
        End If

        AddHandler ucrComboBox.evtValueChanged, AddressOf LinkedControls_evtValueChanged

    End Sub
    ''' <summary>
    ''' Clears all the text In the ucrValueFlagPeriod controls 
    ''' </summary>
    Public Overrides Sub Clear()
        Dim ctr As Control
        Dim ctrTotal As ucrTextBox
        Dim ctrVFP As New ucrValueFlagPeriod
        For Each ctr In Me.Controls
            If TypeOf ctr Is ucrValueFlagPeriod Then
                ctrVFP = ctr
                ctrVFP.Clear()
            ElseIf TypeOf ctr Is ucrTextBox Then
                ctrTotal = ctr
                ctrTotal.Clear()
            End If
        Next
    End Sub
    ''' <summary>
    ''' Checks if total for current element is required
    ''' Checks if the computed total is same as the user entered total.
    ''' </summary>
    Public Sub checkTotal()
        'Check total if required from obselements table from qcTotalRequired field
        Dim clsDataDefinition As DataCall
        Dim dtbl As DataTable
        Dim elemTotal As Integer = 0
        Dim expectedTotal As Integer
        Dim ctr As Control
        Dim ctrVFP As New ucrValueFlagPeriod

        clsDataDefinition = New DataCall
        clsDataDefinition.SetTableName("obselements")
        clsDataDefinition.SetFields(New List(Of String)({"qcTotalRequired"}))
        clsDataDefinition.SetFilter("elementId", "=", ElementId, bIsPositiveCondition:=True, bForceValuesAsString:=False)

        dtbl = clsDataDefinition.GetDataTable()
        If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
            If dtbl.Rows(0).Item("qcTotalRequired") = 1 Then
                expectedTotal = Val(ucrInputTotal.GetValue)
                For Each ctr In Me.Controls
                    If TypeOf ctr Is ucrValueFlagPeriod Then
                        ctrVFP = ctr
                        elemTotal = elemTotal + Val(ctrVFP.ucrValue.GetValue)
                    End If
                Next
                If elemTotal <> expectedTotal Then
                    MessageBox.Show("Value in [Total] textbox is different from that calculated by computer!", caption:="Error in total")
                    ucrInputTotal.GetFocus()
                    ucrInputTotal.SetBackColor(Color.Cyan)
                End If
            End If
        End If
    End Sub

    Private Sub ucrInputTotal_Leave(sender As Object, e As EventArgs) Handles ucrInputTotal.Leave
        checkTotal()
    End Sub

    Public Sub UploadAllRecords()
        Dim clsAllRecordsCall As New DataCall
        Dim dtbAllRecords As DataTable
        Dim rcdObservationInitial As observationinitial
        Dim strCurrTag As String
        Dim dtObsDateTime As Date
        Dim lElementID As Long
        Dim iPeriod As Integer

        clsAllRecordsCall.SetTableName("form_daily2")
        clsAllRecordsCall.SetFields(lstAllFields)
        dtbAllRecords = clsAllRecordsCall.GetDataTable()

        For Each row As DataRow In dtbAllRecords.Rows
            For i As Integer = 1 To 31
                rcdObservationInitial = Nothing
                rcdObservationInitial = New observationinitial
                If i < 10 Then
                    strCurrTag = "0" & i
                Else
                    strCurrTag = i
                End If
                If Not IsDBNull(row.Item("day" & strCurrTag)) AndAlso Strings.Len(row.Item("day" & strCurrTag)) > 0 Then
                    rcdObservationInitial.recordedFrom = row.Item("stationId")
                    If Long.TryParse(row.Item("elementId"), lElementID) Then
                        rcdObservationInitial.describedBy = lElementID
                    Else
                        Exit Sub
                    End If
                    Try
                        dtObsDateTime = New Date(year:=row.Item("yyyy"), month:=row.Item("mm"), day:=i, hour:=row.Item("hh"), minute:=0, second:=0)
                        rcdObservationInitial.obsDatetime = dtObsDateTime
                    Catch ex As Exception

                    End Try
                    rcdObservationInitial.obsLevel = "surface"
                    rcdObservationInitial.obsValue = row.Item("day" & strCurrTag)
                    rcdObservationInitial.flag = row.Item("flag" & strCurrTag)
                    If Integer.TryParse(row.Item("period" & strCurrTag), iPeriod) Then
                        rcdObservationInitial.period = iPeriod
                    End If
                    rcdObservationInitial.qcStatus = 0
                    rcdObservationInitial.acquisitionType = 1
                    rcdObservationInitial.dataForm = "form_daily2"
                    If Not IsDBNull(row.Item("signature")) Then
                        rcdObservationInitial.capturedBy = row.Item("signature")
                    End If
                    If Not IsDBNull(row.Item("temperatureUnits")) Then
                        rcdObservationInitial.temperatureUnits = row.Item("temperatureUnits")
                    End If
                    If Not IsDBNull(row.Item("precipUnits")) Then
                        rcdObservationInitial.precipitationUnits = row.Item("precipUnits")
                    End If
                    If Not IsDBNull(row.Item("cloudHeightUnits")) Then
                        rcdObservationInitial.cloudHeightUnits = row.Item("cloudHeightUnits")
                    End If
                    If Not IsDBNull(row.Item("visUnits")) Then
                        rcdObservationInitial.visUnits = row.Item("visUnits")
                    End If
                    'clsDataConnection.db.observationinitials.Add(rcdObservationInitial)
                End If
            Next
        Next
        clsDataConnection.SaveUpdate()
    End Sub
    ''' <summary>
    ''' Sets upper and lower limits validation curent element
    ''' </summary>
    Public Sub SetValueUpperAndLowerLimitsValidation()
        Dim ucrVFP As ucrValueFlagPeriod
        Dim clsDataDefinition As DataCall
        Dim dtbl As DataTable
        clsDataDefinition = New DataCall

        clsDataDefinition.SetTableName("obselements")
        clsDataDefinition.SetFields(New List(Of String)({"lowerLimit", "upperLimit"}))

        For Each ucrKeyControl As ucrBaseDataLink In dctLinkedControlsFilters.Keys
            If TypeOf ucrKeyControl Is ucrElementSelector Then
                ElementId = Val(ucrKeyControl.GetValue)
                Exit For
            End If
        Next
        clsDataDefinition.SetFilter("elementId", "=", ElementId, bIsPositiveCondition:=True, bForceValuesAsString:=False)

        dtbl = clsDataDefinition.GetDataTable()
        If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
            For Each ctr As Control In Me.Controls
                If TypeOf ctr Is ucrValueFlagPeriod Then
                    ucrVFP = DirectCast(ctr, ucrValueFlagPeriod)
                    If dtbl.Rows(0).Item("lowerLimit") <> "" Then
                        ucrVFP.SetElementValueValidation(iLowerLimit:=Val(dtbl.Rows(0).Item("lowerLimit")))
                    End If
                    If dtbl.Rows(0).Item("upperLimit") <> "" Then
                        ucrVFP.SetElementValueValidation(iUpperLimit:=Val(dtbl.Rows(0).Item("upperLimit")))
                    End If
                End If
            Next
        End If
    End Sub
End Class
