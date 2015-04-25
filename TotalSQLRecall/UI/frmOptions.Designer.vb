<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.gbxStatus = New System.Windows.Forms.GroupBox
        Me.chkTSRStatus = New System.Windows.Forms.CheckBox
        Me.gbxCache = New System.Windows.Forms.GroupBox
        Me.lblBytes = New System.Windows.Forms.Label
        Me.nudSQLSize = New System.Windows.Forms.NumericUpDown
        Me.rbtSQLLimit = New System.Windows.Forms.RadioButton
        Me.rbtStoreAllSQL = New System.Windows.Forms.RadioButton
        Me.nudNumberOfQueries = New System.Windows.Forms.NumericUpDown
        Me.lblNumberOfQueries = New System.Windows.Forms.Label
        Me.gbxDisplay = New System.Windows.Forms.GroupBox
        Me.lblCharacters = New System.Windows.Forms.Label
        Me.nudTooltipSize = New System.Windows.Forms.NumericUpDown
        Me.lblTooltipSize = New System.Windows.Forms.Label
        Me.butChangeFont = New System.Windows.Forms.Button
        Me.lblFontDetails = New System.Windows.Forms.Label
        Me.lblDateExample = New System.Windows.Forms.Label
        Me.txtDateFormat = New System.Windows.Forms.TextBox
        Me.lblFont = New System.Windows.Forms.Label
        Me.lblDateFormat = New System.Windows.Forms.Label
        Me.fdlGridFont = New System.Windows.Forms.FontDialog
        Me.butCancel = New System.Windows.Forms.Button
        Me.butOK = New System.Windows.Forms.Button
        Me.gbxStatus.SuspendLayout()
        Me.gbxCache.SuspendLayout()
        CType(Me.nudSQLSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudNumberOfQueries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxDisplay.SuspendLayout()
        CType(Me.nudTooltipSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbxStatus
        '
        Me.gbxStatus.Controls.Add(Me.chkTSRStatus)
        Me.gbxStatus.Location = New System.Drawing.Point(12, 12)
        Me.gbxStatus.Name = "gbxStatus"
        Me.gbxStatus.Size = New System.Drawing.Size(399, 52)
        Me.gbxStatus.TabIndex = 0
        Me.gbxStatus.TabStop = False
        Me.gbxStatus.Text = "Total SQL Recall Status"
        '
        'chkTSRStatus
        '
        Me.chkTSRStatus.AutoSize = True
        Me.chkTSRStatus.Checked = True
        Me.chkTSRStatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTSRStatus.Location = New System.Drawing.Point(9, 19)
        Me.chkTSRStatus.Name = "chkTSRStatus"
        Me.chkTSRStatus.Size = New System.Drawing.Size(135, 17)
        Me.chkTSRStatus.TabIndex = 7
        Me.chkTSRStatus.Text = "Store new SQL queries"
        Me.chkTSRStatus.UseVisualStyleBackColor = True
        '
        'gbxCache
        '
        Me.gbxCache.Controls.Add(Me.lblBytes)
        Me.gbxCache.Controls.Add(Me.nudSQLSize)
        Me.gbxCache.Controls.Add(Me.rbtSQLLimit)
        Me.gbxCache.Controls.Add(Me.rbtStoreAllSQL)
        Me.gbxCache.Controls.Add(Me.nudNumberOfQueries)
        Me.gbxCache.Controls.Add(Me.lblNumberOfQueries)
        Me.gbxCache.Location = New System.Drawing.Point(12, 70)
        Me.gbxCache.Name = "gbxCache"
        Me.gbxCache.Size = New System.Drawing.Size(399, 98)
        Me.gbxCache.TabIndex = 1
        Me.gbxCache.TabStop = False
        Me.gbxCache.Text = "SQL Cache Setup"
        '
        'lblBytes
        '
        Me.lblBytes.AutoSize = True
        Me.lblBytes.Location = New System.Drawing.Point(231, 70)
        Me.lblBytes.Name = "lblBytes"
        Me.lblBytes.Size = New System.Drawing.Size(32, 13)
        Me.lblBytes.TabIndex = 12
        Me.lblBytes.Text = "bytes"
        '
        'nudSQLSize
        '
        Me.nudSQLSize.Location = New System.Drawing.Point(143, 68)
        Me.nudSQLSize.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudSQLSize.Name = "nudSQLSize"
        Me.nudSQLSize.Size = New System.Drawing.Size(82, 20)
        Me.nudSQLSize.TabIndex = 11
        Me.nudSQLSize.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'rbtSQLLimit
        '
        Me.rbtSQLLimit.AutoSize = True
        Me.rbtSQLLimit.Location = New System.Drawing.Point(9, 68)
        Me.rbtSQLLimit.Name = "rbtSQLLimit"
        Me.rbtSQLLimit.Size = New System.Drawing.Size(126, 17)
        Me.rbtSQLLimit.TabIndex = 10
        Me.rbtSQLLimit.Text = "Store only SQL under"
        Me.rbtSQLLimit.UseVisualStyleBackColor = True
        '
        'rbtStoreAllSQL
        '
        Me.rbtStoreAllSQL.AutoSize = True
        Me.rbtStoreAllSQL.Checked = True
        Me.rbtStoreAllSQL.Location = New System.Drawing.Point(9, 45)
        Me.rbtStoreAllSQL.Name = "rbtStoreAllSQL"
        Me.rbtStoreAllSQL.Size = New System.Drawing.Size(87, 17)
        Me.rbtStoreAllSQL.TabIndex = 9
        Me.rbtStoreAllSQL.TabStop = True
        Me.rbtStoreAllSQL.Text = "Store all SQL"
        Me.rbtStoreAllSQL.UseVisualStyleBackColor = True
        '
        'nudNumberOfQueries
        '
        Me.nudNumberOfQueries.Location = New System.Drawing.Point(143, 14)
        Me.nudNumberOfQueries.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudNumberOfQueries.Name = "nudNumberOfQueries"
        Me.nudNumberOfQueries.Size = New System.Drawing.Size(82, 20)
        Me.nudNumberOfQueries.TabIndex = 8
        Me.nudNumberOfQueries.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'lblNumberOfQueries
        '
        Me.lblNumberOfQueries.AutoSize = True
        Me.lblNumberOfQueries.Location = New System.Drawing.Point(6, 16)
        Me.lblNumberOfQueries.Name = "lblNumberOfQueries"
        Me.lblNumberOfQueries.Size = New System.Drawing.Size(131, 13)
        Me.lblNumberOfQueries.TabIndex = 7
        Me.lblNumberOfQueries.Text = "Number of queries to store"
        '
        'gbxDisplay
        '
        Me.gbxDisplay.Controls.Add(Me.lblCharacters)
        Me.gbxDisplay.Controls.Add(Me.nudTooltipSize)
        Me.gbxDisplay.Controls.Add(Me.lblTooltipSize)
        Me.gbxDisplay.Controls.Add(Me.butChangeFont)
        Me.gbxDisplay.Controls.Add(Me.lblFontDetails)
        Me.gbxDisplay.Controls.Add(Me.lblDateExample)
        Me.gbxDisplay.Controls.Add(Me.txtDateFormat)
        Me.gbxDisplay.Controls.Add(Me.lblFont)
        Me.gbxDisplay.Controls.Add(Me.lblDateFormat)
        Me.gbxDisplay.Location = New System.Drawing.Point(13, 175)
        Me.gbxDisplay.Name = "gbxDisplay"
        Me.gbxDisplay.Size = New System.Drawing.Size(398, 92)
        Me.gbxDisplay.TabIndex = 2
        Me.gbxDisplay.TabStop = False
        Me.gbxDisplay.Text = "Display"
        '
        'lblCharacters
        '
        Me.lblCharacters.AutoSize = True
        Me.lblCharacters.Location = New System.Drawing.Point(230, 61)
        Me.lblCharacters.Name = "lblCharacters"
        Me.lblCharacters.Size = New System.Drawing.Size(57, 13)
        Me.lblCharacters.TabIndex = 13
        Me.lblCharacters.Text = "characters"
        '
        'nudTooltipSize
        '
        Me.nudTooltipSize.Location = New System.Drawing.Point(142, 59)
        Me.nudTooltipSize.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudTooltipSize.Name = "nudTooltipSize"
        Me.nudTooltipSize.Size = New System.Drawing.Size(82, 20)
        Me.nudTooltipSize.TabIndex = 10
        Me.nudTooltipSize.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'lblTooltipSize
        '
        Me.lblTooltipSize.AutoSize = True
        Me.lblTooltipSize.Location = New System.Drawing.Point(5, 61)
        Me.lblTooltipSize.Name = "lblTooltipSize"
        Me.lblTooltipSize.Size = New System.Drawing.Size(100, 13)
        Me.lblTooltipSize.TabIndex = 9
        Me.lblTooltipSize.Text = "SQL tooltip size limit"
        '
        'butChangeFont
        '
        Me.butChangeFont.Location = New System.Drawing.Point(317, 34)
        Me.butChangeFont.Name = "butChangeFont"
        Me.butChangeFont.Size = New System.Drawing.Size(75, 23)
        Me.butChangeFont.TabIndex = 5
        Me.butChangeFont.Text = "Change..."
        Me.butChangeFont.UseVisualStyleBackColor = True
        '
        'lblFontDetails
        '
        Me.lblFontDetails.AutoSize = True
        Me.lblFontDetails.Location = New System.Drawing.Point(139, 39)
        Me.lblFontDetails.Name = "lblFontDetails"
        Me.lblFontDetails.Size = New System.Drawing.Size(47, 13)
        Me.lblFontDetails.TabIndex = 4
        Me.lblFontDetails.Text = "Grid font"
        '
        'lblDateExample
        '
        Me.lblDateExample.Location = New System.Drawing.Point(268, 16)
        Me.lblDateExample.Name = "lblDateExample"
        Me.lblDateExample.Size = New System.Drawing.Size(124, 13)
        Me.lblDateExample.TabIndex = 3
        Me.lblDateExample.Text = "23 Oct 1977"
        Me.lblDateExample.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDateFormat
        '
        Me.txtDateFormat.Location = New System.Drawing.Point(142, 13)
        Me.txtDateFormat.Name = "txtDateFormat"
        Me.txtDateFormat.Size = New System.Drawing.Size(120, 20)
        Me.txtDateFormat.TabIndex = 2
        Me.txtDateFormat.Text = "dd MMM yyyy HH:mm"
        '
        'lblFont
        '
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(6, 39)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(47, 13)
        Me.lblFont.TabIndex = 1
        Me.lblFont.Text = "Grid font"
        '
        'lblDateFormat
        '
        Me.lblDateFormat.AutoSize = True
        Me.lblDateFormat.Location = New System.Drawing.Point(5, 16)
        Me.lblDateFormat.Name = "lblDateFormat"
        Me.lblDateFormat.Size = New System.Drawing.Size(65, 13)
        Me.lblDateFormat.TabIndex = 0
        Me.lblDateFormat.Text = "Date Format"
        '
        'butCancel
        '
        Me.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.butCancel.Location = New System.Drawing.Point(338, 273)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(75, 23)
        Me.butCancel.TabIndex = 3
        Me.butCancel.Text = "Cancel"
        Me.butCancel.UseVisualStyleBackColor = True
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(257, 273)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(75, 23)
        Me.butOK.TabIndex = 4
        Me.butOK.Text = "OK"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AcceptButton = Me.butOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.butCancel
        Me.ClientSize = New System.Drawing.Size(425, 306)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.gbxDisplay)
        Me.Controls.Add(Me.gbxCache)
        Me.Controls.Add(Me.gbxStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.Text = "Total SQL Recall Options"
        Me.gbxStatus.ResumeLayout(False)
        Me.gbxStatus.PerformLayout()
        Me.gbxCache.ResumeLayout(False)
        Me.gbxCache.PerformLayout()
        CType(Me.nudSQLSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudNumberOfQueries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxDisplay.ResumeLayout(False)
        Me.gbxDisplay.PerformLayout()
        CType(Me.nudTooltipSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbxStatus As System.Windows.Forms.GroupBox
    Friend WithEvents chkTSRStatus As System.Windows.Forms.CheckBox
    Friend WithEvents gbxCache As System.Windows.Forms.GroupBox
    Friend WithEvents lblBytes As System.Windows.Forms.Label
    Friend WithEvents nudSQLSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents rbtSQLLimit As System.Windows.Forms.RadioButton
    Friend WithEvents rbtStoreAllSQL As System.Windows.Forms.RadioButton
    Friend WithEvents nudNumberOfQueries As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblNumberOfQueries As System.Windows.Forms.Label
    Friend WithEvents gbxDisplay As System.Windows.Forms.GroupBox
    Friend WithEvents lblDateFormat As System.Windows.Forms.Label
    Friend WithEvents fdlGridFont As System.Windows.Forms.FontDialog
    Friend WithEvents lblFontDetails As System.Windows.Forms.Label
    Friend WithEvents lblDateExample As System.Windows.Forms.Label
    Friend WithEvents txtDateFormat As System.Windows.Forms.TextBox
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents butChangeFont As System.Windows.Forms.Button
    Friend WithEvents butCancel As System.Windows.Forms.Button
    Friend WithEvents butOK As System.Windows.Forms.Button
    Friend WithEvents lblCharacters As System.Windows.Forms.Label
    Friend WithEvents nudTooltipSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblTooltipSize As System.Windows.Forms.Label
End Class
