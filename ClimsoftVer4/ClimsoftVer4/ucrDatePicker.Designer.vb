﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucrDatePicker
    Inherits ClimsoftVer4.ucrValueView

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.dtbRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDate
        '
        Me.txtDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDate.Location = New System.Drawing.Point(0, 0)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(182, 20)
        Me.txtDate.TabIndex = 59
        '
        'dtpDate
        '
        Me.dtpDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(182, 0)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(20, 20)
        Me.dtpDate.TabIndex = 58
        '
        'ucrDatePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.dtpDate)
        Me.Name = "ucrDatePicker"
        Me.Size = New System.Drawing.Size(202, 21)
        CType(Me.dtbRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDate As TextBox
    Friend WithEvents dtpDate As DateTimePicker
End Class
