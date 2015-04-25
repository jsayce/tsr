<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uctToolWindow
    Inherits System.Windows.Forms.UserControl

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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uctToolWindow))
        Me.dgvSQLGrid = New System.Windows.Forms.DataGridView
        Me.colQueryID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRawSQL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSQL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colExecution = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLength = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dsbTSRBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsTotalSQLRecall = New SauceCode.TotalSQLRecall.dstTotalSQLRecall
        Me.cboFilter = New System.Windows.Forms.ComboBox
        Me.cmsContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCurrentWindow = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNewWindow = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRemove = New System.Windows.Forms.ToolStripMenuItem
        Me.tssSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.mnuTSREnableDisable = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.ttpToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.butClearSearch = New System.Windows.Forms.Button
        CType(Me.dgvSQLGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsbTSRBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsTotalSQLRecall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvSQLGrid
        '
        Me.dgvSQLGrid.AllowDrop = True
        Me.dgvSQLGrid.AllowUserToAddRows = False
        Me.dgvSQLGrid.AllowUserToDeleteRows = False
        Me.dgvSQLGrid.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvSQLGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSQLGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSQLGrid.AutoGenerateColumns = False
        Me.dgvSQLGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.dgvSQLGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSQLGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colQueryID, Me.colRawSQL, Me.colSQL, Me.colExecution, Me.colLength})
        Me.dgvSQLGrid.DataMember = "RecentQueries"
        Me.dgvSQLGrid.DataSource = Me.dsbTSRBindingSource
        Me.dgvSQLGrid.Location = New System.Drawing.Point(0, 21)
        Me.dgvSQLGrid.MultiSelect = False
        Me.dgvSQLGrid.Name = "dgvSQLGrid"
        Me.dgvSQLGrid.ReadOnly = True
        Me.dgvSQLGrid.RowHeadersWidth = 20
        Me.dgvSQLGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvSQLGrid.RowTemplate.Height = 50
        Me.dgvSQLGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvSQLGrid.Size = New System.Drawing.Size(150, 129)
        Me.dgvSQLGrid.StandardTab = True
        Me.dgvSQLGrid.TabIndex = 2
        '
        'colQueryID
        '
        Me.colQueryID.DataPropertyName = "QueryID"
        Me.colQueryID.HeaderText = "QueryID"
        Me.colQueryID.Name = "colQueryID"
        Me.colQueryID.ReadOnly = True
        Me.colQueryID.Visible = False
        Me.colQueryID.Width = 14
        '
        'colRawSQL
        '
        Me.colRawSQL.DataPropertyName = "SQL"
        Me.colRawSQL.HeaderText = "Raw SQL"
        Me.colRawSQL.Name = "colRawSQL"
        Me.colRawSQL.ReadOnly = True
        Me.colRawSQL.Visible = False
        Me.colRawSQL.Width = 15
        '
        'colSQL
        '
        Me.colSQL.DataPropertyName = "SQL"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSQL.DefaultCellStyle = DataGridViewCellStyle2
        Me.colSQL.HeaderText = "SQL"
        Me.colSQL.MaxInputLength = 9999999
        Me.colSQL.Name = "colSQL"
        Me.colSQL.ReadOnly = True
        Me.colSQL.Width = 53
        '
        'colExecution
        '
        Me.colExecution.DataPropertyName = "ExecutionDate"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.colExecution.DefaultCellStyle = DataGridViewCellStyle3
        Me.colExecution.HeaderText = "Date"
        Me.colExecution.Name = "colExecution"
        Me.colExecution.ReadOnly = True
        Me.colExecution.Width = 29
        '
        'colLength
        '
        Me.colLength.DataPropertyName = "ExecutionLength"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.colLength.DefaultCellStyle = DataGridViewCellStyle4
        Me.colLength.HeaderText = "Length (s)"
        Me.colLength.Name = "colLength"
        Me.colLength.ReadOnly = True
        Me.colLength.Visible = False
        Me.colLength.Width = 28
        '
        'dsbTSRBindingSource
        '
        Me.dsbTSRBindingSource.DataSource = Me.dsTotalSQLRecall
        Me.dsbTSRBindingSource.Position = 0
        '
        'dsTotalSQLRecall
        '
        Me.dsTotalSQLRecall.DataSetName = "dstTotalSQLRecall"
        Me.dsTotalSQLRecall.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboFilter
        '
        Me.cboFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {""})
        Me.cboFilter.Location = New System.Drawing.Point(0, 0)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(130, 21)
        Me.cboFilter.TabIndex = 0
        '
        'cmsContextMenu
        '
        Me.cmsContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCurrentWindow, Me.mnuNewWindow, Me.mnuCopy, Me.mnuRemove, Me.tssSeparator, Me.mnuTSREnableDisable, Me.mnuOptions})
        Me.cmsContextMenu.Name = "ContextMenuStrip1"
        Me.cmsContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.cmsContextMenu.Size = New System.Drawing.Size(236, 142)
        '
        'mnuCurrentWindow
        '
        Me.mnuCurrentWindow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.mnuCurrentWindow.Name = "mnuCurrentWindow"
        Me.mnuCurrentWindow.Size = New System.Drawing.Size(235, 22)
        Me.mnuCurrentWindow.Text = "Insert into current window"
        '
        'mnuNewWindow
        '
        Me.mnuNewWindow.Name = "mnuNewWindow"
        Me.mnuNewWindow.Size = New System.Drawing.Size(235, 22)
        Me.mnuNewWindow.Text = "Open in new window"
        '
        'mnuCopy
        '
        Me.mnuCopy.Name = "mnuCopy"
        Me.mnuCopy.Size = New System.Drawing.Size(235, 22)
        Me.mnuCopy.Text = "Copy SQL"
        '
        'mnuRemove
        '
        Me.mnuRemove.Name = "mnuRemove"
        Me.mnuRemove.Size = New System.Drawing.Size(235, 22)
        Me.mnuRemove.Text = "Remove"
        '
        'tssSeparator
        '
        Me.tssSeparator.Name = "tssSeparator"
        Me.tssSeparator.Size = New System.Drawing.Size(232, 6)
        '
        'mnuTSREnableDisable
        '
        Me.mnuTSREnableDisable.Name = "mnuTSREnableDisable"
        Me.mnuTSREnableDisable.Size = New System.Drawing.Size(235, 22)
        Me.mnuTSREnableDisable.Text = "Stop Collecting SQL"
        '
        'mnuOptions
        '
        Me.mnuOptions.Name = "mnuOptions"
        Me.mnuOptions.Size = New System.Drawing.Size(235, 22)
        Me.mnuOptions.Text = "Options..."
        '
        'butClearSearch
        '
        Me.butClearSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butClearSearch.Image = CType(resources.GetObject("butClearSearch.Image"), System.Drawing.Image)
        Me.butClearSearch.Location = New System.Drawing.Point(129, 0)
        Me.butClearSearch.Name = "butClearSearch"
        Me.butClearSearch.Size = New System.Drawing.Size(21, 21)
        Me.butClearSearch.TabIndex = 1
        Me.butClearSearch.UseVisualStyleBackColor = True
        '
        'uctToolWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.butClearSearch)
        Me.Controls.Add(Me.cboFilter)
        Me.Controls.Add(Me.dgvSQLGrid)
        Me.Name = "uctToolWindow"
        CType(Me.dgvSQLGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsbTSRBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsTotalSQLRecall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSQLGrid As System.Windows.Forms.DataGridView
    Friend WithEvents dsbTSRBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsTotalSQLRecall As dstTotalSQLRecall
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents cmsContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuCurrentWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTSREnableDisable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ttpToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents butClearSearch As System.Windows.Forms.Button
    Friend WithEvents colQueryID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRawSQL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSQL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExecution As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLength As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
