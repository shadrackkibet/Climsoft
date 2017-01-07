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
Imports Microsoft.Win32
Imports System.Security.Principal


Module CommonModules
    Public tabNext As Boolean
    Public regKeyName As String
    Public regKeyValue As String
    Public dsLanguageTable As New DataSet
    Public daLanguageTable As New MySql.Data.MySqlClient.MySqlDataAdapter
    Public languageTableSQL As String
    Public dsReg As New DataSet
    Public daReg As New MySql.Data.MySqlClient.MySqlDataAdapter
    Public regSQL As String
    Public dsClimsoftUserRoles As New DataSet
    Public daClimsoftUserRoles As New MySql.Data.MySqlClient.MySqlDataAdapter
    Public rolesSQL As String
    Public userGroup As String
    Public dsSourceTableName As String
    Public connStrRemoteSvr As String
    Public remoteSvr As String
    Public msgKeyentryFormsListUpdated As String
    Public msgStationInformationNotFound As String

    Public Function isAdministrator()
        isAdministrator = New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Function ViewFile(flname As String) As Boolean
        Try
            ViewFile = True
            System.Diagnostics.Process.Start(flname)
        Catch ex As Exception
            ViewFile = False
        End Try
    End Function

    Function GetWRplotPath(ByRef WRplotPath As String) As Boolean
        On Error GoTo errhandler
        Dim objRegKey As RegistryKey
        Dim KeyStringW32, KeyStringW64 As String

        GetWRplotPath = True
        'Try
        KeyStringW32 = "SOFTWARE\Lakes Environmental Software\WRPlot View"
        KeyStringW64 = "SOFTWARE\Wow6432Node\Lakes Environmental Software\WRPlot View"

        objRegKey = Registry.LocalMachine.OpenSubKey(KeyStringW32)
        If Len(objRegKey.Name) = 0 Then
            objRegKey = Registry.LocalMachine.OpenSubKey(KeyStringW64)
        End If

        WRplotPath = objRegKey.GetValue("Install_Folder")

        If WRplotPath = "" Then GetWRplotPath = False

        'Catch ex As Exception

        Exit Function
errhandler:
        If Err.Number = 91 Then Resume Next
        MsgBox(Err.Number & " " & Err.Description)
        GetWRplotPath = False
        'End Try

    End Function
End Module
