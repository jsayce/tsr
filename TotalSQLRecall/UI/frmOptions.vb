Public Class frmOptions

#Region " Form Events "

    ''' <summary>
    ''' Sets up initial option settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOptions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        ' set status checkbox
        chkTSRStatus.Checked = My.Settings.Enabled

        ' set number of queries
        nudNumberOfQueries.Value = My.Settings.CacheSize

        ' set unlimited/limited, size and enable
        rbtStoreAllSQL.Checked = Not My.Settings.LimitSQLSize
        rbtSQLLimit.Checked = My.Settings.LimitSQLSize
        nudSQLSize.Value = My.Settings.SQLSize
        SetUpSQLSizeLimit()
        nudTooltipSize.Value = My.Settings.TooltipMaxLength

        ' set date format
        txtDateFormat.Text = My.Settings.GridDateFormat
        SetUpDateFormat()

        ' set font dialog control to current font
        fdlGridFont.Font = My.Settings.GridFont
        SetUpFont()

    End Sub

    ''' <summary>
    ''' Launches the font picker dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butChangeFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butChangeFont.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            Dim fdr As Windows.Forms.DialogResult = fdlGridFont.ShowDialog
            SetUpFont()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Enables/disables the bytes UpDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtSQLLimit.CheckedChanged, rbtStoreAllSQL.CheckedChanged

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)
            SetUpSQLSizeLimit()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Closes the form without saving the changes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' send cancel (no need to refresh SQL)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

            Me.Close()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Saves the changes and closes the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' store new settings
            My.Settings.Enabled = chkTSRStatus.Checked
            My.Settings.CacheSize = CInt(nudNumberOfQueries.Value)
            My.Settings.LimitSQLSize = rbtSQLLimit.Checked
            My.Settings.SQLSize = CInt(nudSQLSize.Value)
            My.Settings.GridDateFormat = txtDateFormat.Text
            My.Settings.GridFont = fdlGridFont.Font
            My.Settings.TooltipMaxLength = CInt(nudTooltipSize.Value)

            ' send OK (refresh SQL)
            Me.DialogResult = Windows.Forms.DialogResult.OK

            Me.Close()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    Private Sub txtDateFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDateFormat.TextChanged

        Try

            SetUpDateFormat()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Enables/disables the bytes Up/Down
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpSQLSizeLimit()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        If rbtStoreAllSQL.Checked Then
            nudSQLSize.Enabled = False
            nudSQLSize.Value = 0
        Else
            nudSQLSize.Enabled = True
        End If

    End Sub

    Private Sub SetUpFont()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        ' set up font label
        lblFontDetails.Text = fdlGridFont.Font.FontFamily.Name & " (" & fdlGridFont.Font.SizeInPoints & ")"

    End Sub

    Private Sub SetUpDateFormat()

        Try
            lblDateExample.Text = Now().ToString(txtDateFormat.Text)
            butOK.Enabled = True
        Catch ex As Exception
            lblDateExample.Text = "Invalid"
            butOK.Enabled = False
        End Try

    End Sub

#End Region

End Class