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


Public Class frmChangeOwnPassword
    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim connStr As String, Sql As String
    Dim objCmd As MySql.Data.MySqlClient.MySqlCommand
    Dim msgNotYetImplemented As String
    Dim msgWrongPasswordConfirmation As String
    Dim msgPasswordTooShort As String
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmChangeOwnPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        msgNotYetImplemented = "Not yet implemented!"
        msgWrongPasswordConfirmation = "Wrong confirmation of password!"
        msgPasswordTooShort = "Password length must be >=6 characters!"
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        connStr = frmLogin.txtusrpwd.Text
        conn.ConnectionString = connStr
        'Open connection to database
        conn.Open()
        If Strings.Len(txtNewPassword.Text) >= 6 Then
            If txtNewPassword.Text = txtConfirmPassword.Text Then
                Try
                    'Set new password
                    Sql = "SET PASSWORD  = PASSWORD('" & txtNewPassword.Text & "');"
                    objCmd = New MySql.Data.MySqlClient.MySqlCommand(Sql, conn)
                    objCmd.ExecuteNonQuery()
                    MsgBox("Your new password has been set!", MsgBoxStyle.Information)
                Catch ex As Exception
                    ''Dispaly Exception error message 
                    MsgBox(ex.Message)
                    conn.Close()
                End Try
            Else
                MsgBox(msgWrongPasswordConfirmation, MsgBoxStyle.Information)
            End If
        Else
            MsgBox(msgPasswordTooShort, MsgBoxStyle.Information)
        End If
        'Close connection
        conn.Close()
    End Sub

    Private Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPassword.TextChanged
        If Strings.Len(txtConfirmPassword.Text) > 0 And Strings.Len(txtNewPassword.Text) > 0 Then
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Help.ShowHelp(Me, Application.StartupPath & "\climsoft4.chm", "changepassword.htm#ownpassword")
    End Sub
End Class