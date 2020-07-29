﻿' CLIMSOFT - Climate Database Management System
' Copyright (C) 2017
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.

Public Class frmKeyEntry

    Private Sub frmKeyEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dataCall As New DataCall
            Dim dataTable As DataTable
            lstViewForms.Items.Clear()
            'set the database name and columns, set the key field for updating, then add the retrieved data to the listview
            dataCall.SetTableNameAndFields("data_forms", {"form_name", "description"})
            dataCall.SetFilter("selected", "=", 1)
            dataTable = dataCall.GetDataTable()
            For Each row As DataRow In dataTable.Rows
                lstViewForms.Items.Add(New ListViewItem({row.Item("form_name"), row.Item("description")}))
            Next

            If lstViewForms.Items.Count = 0 Then
                Exit Sub
            End If

            'if there are records, then adjust the height of the listview. Done this way because of climsoft operators
            If lstViewForms.Items.Count > 0 Then
                lstViewForms.Height = ((lstViewForms.Items.Count + 1) * lstViewForms.Items.Item(0).Bounds.Height) + 30
            End If

        Catch ex As Exception
            MessageBox.Show("Error : " & ex.Message)
        End Try

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles lstViewForms.DoubleClick
        OpenSelectedForm()
    End Sub

    Private Sub lstViewForms_KeyDown(sender As Object, e As KeyEventArgs) Handles lstViewForms.KeyDown
        If e.KeyCode = Keys.Enter Then
            OpenSelectedForm()
        End If
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        OpenSelectedForm()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Help.ShowHelp(Me, Application.StartupPath & "\climsoft4.chm", "keyentryoperations.htm")
    End Sub

    Private Sub OpenSelectedForm()
        If lstViewForms.SelectedItems.Count > 0 Then
            Select Case lstViewForms.SelectedItems.Item(0).Text
                Case "formSynoptic2RA1"
                    frmNewSynopticRA1.Show()
                Case "form_daily1"
                    formDaily1.Show()
                Case "form_daily2"
                    frmNewFormDaily2.Show()
                Case "form_hourly"
                    frmNewHourly.Show()
                Case "form_monthly"
                    frmNewMonthly.Show()
                Case "form_upperair1"
                    form_upperair1.Show()
                Case "form_hourlywind"
                    frmNewHourlyWind.Show()
                Case "form_agro1"
                    form_agro1.Show()
                Case "form_synoptic2_caribbean"
                    formSynopticCaribbean.Show()
            End Select
        End If
    End Sub

    Private Sub FontDialog1_Apply(sender As Object, e As EventArgs)

    End Sub
End Class