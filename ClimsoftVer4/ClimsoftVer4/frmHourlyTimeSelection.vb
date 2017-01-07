' CLIMSOFT - Climate Database Management System
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


Public Class frmHourlyTimeSelection
    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim ds As New DataSet
    Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter
    Dim sql As String

    Private Sub frmHourlyTimeSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo Err
        conn.ConnectionString = frmLogin.txtusrpwd.Text
        conn.Open()
        sql = "select * from form_hourly_time_selection"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, conn)
        da.Fill(ds, "hourlyTimeSelection")
        Me.DataGridView1.DataSource = ds.Tables(0)
        'Me.DataGridView1.
        Exit Sub
Err:
        MsgBox(Err.Description)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim cb As New MySql.Data.MySqlClient.MySqlCommandBuilder(da)
        Try
            da.Update(ds, "hourlyTimeSelection")
            MsgBox("Database table updated successfully!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        'Help.ShowHelp(Me, Application.StartupPath & "\climsoft4.chm", "hourlydata.htm")
    End Sub

End Class