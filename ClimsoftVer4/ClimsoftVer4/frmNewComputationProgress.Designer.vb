﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNewComputationProgress
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.backgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtResultMessage = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(10, 6)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(65, 13)
        Me.lblHeader.TabIndex = 10
        Me.lblHeader.Text = "header label"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(249, 27)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(29, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "x"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.ForeColor = System.Drawing.Color.Black
        Me.lblProgress.Location = New System.Drawing.Point(10, 35)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(47, 13)
        Me.lblProgress.TabIndex = 8
        Me.lblProgress.Text = "progress"
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(7, 55)
        Me.progressBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(271, 18)
        Me.progressBar.TabIndex = 7
        '
        'backgroundWorker
        '
        Me.backgroundWorker.WorkerReportsProgress = True
        Me.backgroundWorker.WorkerSupportsCancellation = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(239, 112)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(56, 20)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtResultMessage
        '
        Me.txtResultMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtResultMessage.ForeColor = System.Drawing.Color.Red
        Me.txtResultMessage.Location = New System.Drawing.Point(7, 27)
        Me.txtResultMessage.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtResultMessage.Multiline = True
        Me.txtResultMessage.Name = "txtResultMessage"
        Me.txtResultMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResultMessage.Size = New System.Drawing.Size(289, 79)
        Me.txtResultMessage.TabIndex = 15
        '
        'frmNewComputationProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 138)
        Me.Controls.Add(Me.txtResultMessage)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.progressBar)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmNewComputationProgress"
        Me.Text = "Progress"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblHeader As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblProgress As Label
    Friend WithEvents progressBar As ProgressBar
    Friend WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnClose As Button
    Friend WithEvents txtResultMessage As TextBox
End Class