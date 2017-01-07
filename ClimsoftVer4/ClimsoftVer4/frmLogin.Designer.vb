<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.txtusrpwd = New System.Windows.Forms.TextBox()
        Me.cmbDatabases = New System.Windows.Forms.ComboBox()
        Me.lblDbdetails = New System.Windows.Forms.Label()
        Me.cmdHelp = New System.Windows.Forms.Button()
        Me.lblClimsoftVersion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(209, 69)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(55, 13)
        Me.lblUsername.TabIndex = 0
        Me.lblUsername.Tag = ""
        Me.lblUsername.Text = "Username"
        Me.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(209, 110)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 2
        Me.lblPassword.Tag = ""
        Me.lblPassword.Text = "Password"
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(278, 66)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(220, 20)
        Me.txtUsername.TabIndex = 1
        Me.txtUsername.Tag = ""
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(278, 107)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(220, 20)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.Tag = ""
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(239, 187)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(67, 23)
        Me.OK.TabIndex = 4
        Me.OK.Tag = "OK"
        Me.OK.Text = "&OK"
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(342, 187)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(67, 23)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Tag = "Cancel"
        Me.Cancel.Text = "&Cancel"
        '
        'txtusrpwd
        '
        Me.txtusrpwd.Location = New System.Drawing.Point(380, 12)
        Me.txtusrpwd.Name = "txtusrpwd"
        Me.txtusrpwd.Size = New System.Drawing.Size(147, 20)
        Me.txtusrpwd.TabIndex = 6
        Me.txtusrpwd.Visible = False
        '
        'cmbDatabases
        '
        Me.cmbDatabases.BackColor = System.Drawing.SystemColors.Menu
        Me.cmbDatabases.FormattingEnabled = True
        Me.cmbDatabases.Location = New System.Drawing.Point(195, 12)
        Me.cmbDatabases.Name = "cmbDatabases"
        Me.cmbDatabases.Size = New System.Drawing.Size(164, 21)
        Me.cmbDatabases.TabIndex = 7
        Me.cmbDatabases.Visible = False
        '
        'lblDbdetails
        '
        Me.lblDbdetails.AutoSize = True
        Me.lblDbdetails.ForeColor = System.Drawing.Color.Blue
        Me.lblDbdetails.Location = New System.Drawing.Point(309, 141)
        Me.lblDbdetails.Name = "lblDbdetails"
        Me.lblDbdetails.Size = New System.Drawing.Size(158, 13)
        Me.lblDbdetails.TabIndex = 8
        Me.lblDbdetails.Tag = "Show_and_Configure_Database_Connection"
        Me.lblDbdetails.Text = "Configure Database Connection"
        '
        'cmdHelp
        '
        Me.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdHelp.Location = New System.Drawing.Point(445, 187)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(67, 23)
        Me.cmdHelp.TabIndex = 9
        Me.cmdHelp.Tag = "Help"
        Me.cmdHelp.Text = "&Help"
        '
        'lblClimsoftVersion
        '
        Me.lblClimsoftVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblClimsoftVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClimsoftVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblClimsoftVersion.Location = New System.Drawing.Point(124, 113)
        Me.lblClimsoftVersion.Name = "lblClimsoftVersion"
        Me.lblClimsoftVersion.Size = New System.Drawing.Size(47, 23)
        Me.lblClimsoftVersion.TabIndex = 10
        Me.lblClimsoftVersion.Text = "4.x"
        Me.lblClimsoftVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmLogin
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.ClimsoftVer4.My.Resources.Resources.climsoft_login
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(539, 222)
        Me.Controls.Add(Me.lblClimsoftVersion)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.lblDbdetails)
        Me.Controls.Add(Me.cmbDatabases)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtusrpwd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "Login"
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtusrpwd As System.Windows.Forms.TextBox
    Friend WithEvents cmbDatabases As System.Windows.Forms.ComboBox
    Friend WithEvents lblDbdetails As System.Windows.Forms.Label
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents lblClimsoftVersion As Label
End Class
