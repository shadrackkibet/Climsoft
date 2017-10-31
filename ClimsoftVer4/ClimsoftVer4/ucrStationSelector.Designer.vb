<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucrStationSelector
    Inherits ClimsoftVer4.ucrBaseRecord

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cboStations = New System.Windows.Forms.ComboBox()
        Me.cmsStationOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmStationNames = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmIDs = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmIDsAndStations = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmFilterStations = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSortByID = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSortByStationName = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsStationOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboStations
        '
        Me.cboStations.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStations.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStations.ContextMenuStrip = Me.cmsStationOptions
        Me.cboStations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboStations.FormattingEnabled = True
        Me.cboStations.Location = New System.Drawing.Point(0, 0)
        Me.cboStations.Name = "cboStations"
        Me.cboStations.Size = New System.Drawing.Size(200, 21)
        Me.cboStations.TabIndex = 0
        '
        'cmsStationOptions
        '
        Me.cmsStationOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmStationNames, Me.tsmIDs, Me.tsmIDsAndStations, Me.ToolStripSeparator1, Me.tsmFilterStations, Me.tsmSortByID, Me.tsmSortByStationName})
        Me.cmsStationOptions.Name = "cmsStationOptions"
        Me.cmsStationOptions.Size = New System.Drawing.Size(187, 142)
        '
        'tsmStationNames
        '
        Me.tsmStationNames.Name = "tsmStationNames"
        Me.tsmStationNames.Size = New System.Drawing.Size(202, 22)
        Me.tsmStationNames.Text = "Station Names"
        '
        'tsmIDs
        '
        Me.tsmIDs.Name = "tsmIDs"
        Me.tsmIDs.Size = New System.Drawing.Size(202, 22)
        Me.tsmIDs.Text = "IDs"
        '
        'tsmIDsAndStations
        '
        Me.tsmIDsAndStations.Name = "tsmIDsAndStations"
        Me.tsmIDsAndStations.Size = New System.Drawing.Size(202, 22)
        Me.tsmIDsAndStations.Text = "Stations and IDs"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(199, 6)
        '
        'tsmFilterStations
        '
        Me.tsmFilterStations.Name = "tsmFilterStations"
        Me.tsmFilterStations.Size = New System.Drawing.Size(202, 22)
        Me.tsmFilterStations.Text = "Filter Stations..."
        '
        'tsmSortByID
        '
        Me.tsmSortByID.Name = "tsmSortByID"
        Me.tsmSortByID.Size = New System.Drawing.Size(186, 22)
        Me.tsmSortByID.Text = "Sort by ID"
        '
        'tsmSortByStationName
        '
        Me.tsmSortByStationName.Name = "tsmSortByStationName"
        Me.tsmSortByStationName.Size = New System.Drawing.Size(186, 22)
        Me.tsmSortByStationName.Text = "Sort by Station Name"
        '
        'ucrStationSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cboStations)
        Me.Name = "ucrStationSelector"
        Me.Size = New System.Drawing.Size(200, 21)
        Me.cmsStationOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cboStations As ComboBox
    Friend WithEvents cmsStationOptions As ContextMenuStrip
    Friend WithEvents tsmStationNames As ToolStripMenuItem
    Friend WithEvents tsmIDs As ToolStripMenuItem
    Friend WithEvents tsmIDsAndStations As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmFilterStations As ToolStripMenuItem
    Friend WithEvents tsmSortByID As ToolStripMenuItem
    Friend WithEvents tsmSortByStationName As ToolStripMenuItem
End Class
