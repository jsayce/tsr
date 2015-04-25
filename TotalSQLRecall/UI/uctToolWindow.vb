Imports System.Windows.Forms
Imports System.IO

Public Class uctToolWindow

#Region " Declarations "

    ' dataset
    Private dsTSR As dstTotalSQLRecall

    ' constants
    Private Const COLUMN_QUERYID As Integer = 0
    Private Const COLUMN_RAWSQL As Integer = 1
    Private Const COLUMN_SQL As Integer = 2
    Private Const COLUMN_EXECUTION As Integer = 3
    Private Const COLUMN_LENGTH As Integer = 4
    Private Const FILE_PREVIOUSQUERIES As String = "RecentQueries.xml"

    ' events
    Public Event InsertCurrentSQL(ByVal SQL As String)
    Public Event OpenNewSQL(ByVal SQL As String)

    ' used to store the current row of the context menu
    Private _CurrentRowIndex As Integer

#End Region

#Region " Properties "

    ''' <summary>
    ''' The dataset containing recent queries
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property QueryData() As dstTotalSQLRecall
        Get
            Return dsTSR
        End Get
        Set(ByVal value As dstTotalSQLRecall)

            clsExceptions.LogMessage("Updating QueryData", clsExceptions.LogLevel.Level3)

            ' set the dataset
            dsTSR = value

        End Set
    End Property

#End Region

#Region " Constructor "

    ''' <summary>
    ''' sets the properties of the control from the stored settings
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' this call is required by the Windows Form Designer.
        InitializeComponent()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)
        clsExceptions.LogMessage("Creating TSR window", clsExceptions.LogLevel.Level1)

        ' store new row height as default
        dgvSQLGrid.RowTemplate.Height = My.Settings.RowHeight

        ' set the column widths
        dgvSQLGrid.Columns(COLUMN_SQL).Width = My.Settings.ColumnWidthSQL
        dgvSQLGrid.Columns(COLUMN_EXECUTION).Width = My.Settings.ColumnWidthExecution
        dgvSQLGrid.Columns(COLUMN_LENGTH).Width = My.Settings.ColumnWidthLength

        ' instanciate dataset
        dsTSR = New dstTotalSQLRecall

        Dim strFilepath As String = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), FILE_PREVIOUSQUERIES)

        ' check whether the file exists
        If File.Exists(strFilepath) Then

            clsExceptions.LogMessage("Loading " & strFilepath, clsExceptions.LogLevel.Level3)

            Dim stm As StreamReader
            Try

                ' open the file
                stm = New StreamReader(strFilepath, True)

                ' read the xml into the dataset
                dsTSR.ReadXml(stm)

                ' close the stream
                stm.Close()

            Catch ex As Exception
                Throw
            Finally
                If Not IsNothing(stm) Then
                    stm.Dispose()
                End If
            End Try

        Else
            ' no file exists
            clsExceptions.LogMessage(strFilepath & " does not exist", clsExceptions.LogLevel.Level3)
        End If

        ' set up tooltips
        ttpToolTipProvider.SetToolTip(cboFilter, "Filter the SQL list by typing or choosing text here")
        ttpToolTipProvider.SetToolTip(butClearSearch, "Clear the filter and display all SQL")

        clsExceptions.LogMessage("End of TSR control constructor", clsExceptions.LogLevel.Level3)

    End Sub

#End Region

#Region " Control Events "

#Region " DataGridView - Drag & Drop Events "

    ''' <summary>
    ''' Handles the drag/drop functionality of the datagrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' This handling of drag and drop is more complicated than the usual way it's done in datagrids
    ''' (using mousedown) as there seemed to be conflict between the context menu and the DoDragDrop 
    ''' method: using mousedown to handle drag and drop, the second left click after a context menu 
    ''' show would fail to select the cell - no mousedown would fire at all.
    ''' 
    ''' This method uses mousemove to catch the drag event, with the handler only enabled between 
    ''' mousedown and mouseup, and removed if there is a mousemove with no buttons.
    ''' </remarks>
    Private Sub dgvSQLGrid_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)

        Try

            ' check if the left button is down - currently dragging
            If e.Button = Windows.Forms.MouseButtons.Left Then

                ' get the text from the SQL cell of the current row
                Dim strText As String = CStr(dgvSQLGrid.CurrentRow.Cells(COLUMN_RAWSQL).Value)

                ' check there's some text
                If Not String.IsNullOrEmpty(strText) Then
                    ' do the drag thing
                    dgvSQLGrid.DoDragDrop(strText, DragDropEffects.Copy)
                End If

            Else

                ' remove the mousemove event handler - it's definitely not a drag operation
                RemoveHandler dgvSQLGrid.CellMouseMove, AddressOf dgvSQLGrid_CellMouseMove

            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Switched drag/drop functionality on at the beginning of a drag
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>See mousemove for more details</remarks>
    Private Sub dgvSQLGrid_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSQLGrid.CellMouseDown

        Try

            ' check it's not a header
            If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                ' check left button is down
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    ' add the mousemove handler to enable drag/drop
                    AddHandler dgvSQLGrid.CellMouseMove, AddressOf dgvSQLGrid_CellMouseMove
                End If
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Switches drag/drop functionality off at the end of a drag
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>See mousemove for more details</remarks>
    Private Sub dgvSQLGrid_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSQLGrid.CellMouseUp

        Try

            ' remove the mousemove handler - drag & drop is no longer required
            RemoveHandler dgvSQLGrid.CellMouseMove, AddressOf dgvSQLGrid_CellMouseMove

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " DataGridView - Other Events "

    ''' <summary>
    ''' Displays the context menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSQLGrid_CellContextMenuStripNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs) Handles dgvSQLGrid.CellContextMenuStripNeeded

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' set up on/off menu item
            If My.Settings.Enabled Then
                mnuTSREnableDisable.Text = "Stop Collecting SQL"
            Else
                mnuTSREnableDisable.Text = "Start Collecting SQL"
            End If

            ' check if it's a header cell
            If e.RowIndex = -1 Then

                ' hide top half of menu
                mnuCurrentWindow.Visible = False
                mnuNewWindow.Visible = False
                mnuCopy.Visible = False
                mnuRemove.Visible = False
                tssSeparator.Visible = False

            Else

                ' show top half of menu
                mnuCurrentWindow.Visible = True
                mnuNewWindow.Visible = True
                mnuCopy.Visible = True
                mnuRemove.Visible = True
                tssSeparator.Visible = True

                ' store the row which initiated the context menu so we know which row to process
                _CurrentRowIndex = e.RowIndex

            End If

            ' set menu
            e.ContextMenuStrip = cmsContextMenu

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Provides custom tooltip for SQL cells with the raw SQL version
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSQLGrid_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles dgvSQLGrid.CellToolTipTextNeeded

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' check this isn't a header
            If e.RowIndex > -1 Then
                ' check this is a SQL cell
                If e.ColumnIndex = 2 Then
                    ' check the raw SQL has a value
                    If Not IsNothing(dgvSQLGrid.Rows(e.RowIndex).Cells(COLUMN_RAWSQL).Value) Then
                        ' get the raw SQL
                        Dim strRawSQL As String = CType(dgvSQLGrid.Rows(e.RowIndex).Cells(COLUMN_RAWSQL).Value, String)
                        ' check raw SQL isn't too long
                        If strRawSQL.Length > My.Settings.TooltipMaxLength Then
                            ' truncate raw SQL if necessary
                            strRawSQL = strRawSQL.Substring(0, My.Settings.TooltipMaxLength) & "..."
                        End If
                        ' set tooltip
                        e.ToolTipText = strRawSQL
                    End If
                End If
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Raises the InsertCurrentSQL event using the clicked row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSQLGrid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSQLGrid.CellMouseDoubleClick

        Try

            ' check it was a left double click
            If e.Button = Windows.Forms.MouseButtons.Left Then

                ' check it wasn't a header
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then

                    clsExceptions.LogMessage("Doing insert", clsExceptions.LogLevel.Level3)

                    ' raise insert event
                    RaiseEvent InsertCurrentSQL(CStr(dgvSQLGrid.Rows(e.RowIndex).Cells(COLUMN_RAWSQL).Value))

                End If
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Puts a cleaned version of the SQL into the display cell
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSQLGrid_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvSQLGrid.CellFormatting

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' only interested in the SQL column
            If e.ColumnIndex = COLUMN_SQL Then
                ' only need to do any work if there's data
                If e.Value IsNot Nothing Then
                    ' replace tabs with spaces as datagridview cell can't display tabs
                    e.Value = CType(e.Value, String).Replace(vbTab, " ")
                End If
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Changes the height of all rows and stores new height
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSQLGrid_RowHeightChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvSQLGrid.RowHeightChanged

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' cycle through all rows
            For Each dgr As System.Windows.Forms.DataGridViewRow In dgvSQLGrid.Rows
                ' set the height to match the current row
                dgr.Height = e.Row.Height
            Next

            ' store new row height as default (used when the grid is refreshed)
            dgvSQLGrid.RowTemplate.Height = e.Row.Height

            ' update setting
            My.Settings.RowHeight = e.Row.Height

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " ComboBox/Button Events "

    ''' <summary>
    ''' Catches the enter key and filters the list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Setting the handled property here doesn't seem to stop the event bubbling up</remarks>
    Private Sub cboFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboFilter.KeyDown

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            If e.KeyCode = Keys.Enter Then
                HandleSQLSearch()
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Catches the enter key and kills the event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Filtering the list doesn't seem to work in this event</remarks>
    Private Sub cboFilter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboFilter.KeyPress

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
                e.Handled = True
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Filters the SQL list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboFilter_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFilter.LostFocus

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            HandleSQLSearch()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Filters the SQL list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFilter.SelectedIndexChanged

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            HandleSQLSearch()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clears the search criteria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butClearSearch.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' clear the text from the filter combo
            cboFilter.Text = String.Empty

            ' fire the search
            HandleSQLSearch()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " Menu Events "

    ''' <summary>
    ''' Raises the InsertCurrentSQL event using the clicked row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuCurrentWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCurrentWindow.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            RaiseEvent InsertCurrentSQL(CStr(dgvSQLGrid.Rows(_CurrentRowIndex).Cells(COLUMN_RAWSQL).Value))

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Raises the OpenNewSQL event using the clicked row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuNewWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewWindow.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            RaiseEvent OpenNewSQL(CStr(dgvSQLGrid.Rows(_CurrentRowIndex).Cells(COLUMN_RAWSQL).Value))

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Puts the clicked item onto the clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCopy.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            Clipboard.SetText(CStr(dgvSQLGrid.Rows(_CurrentRowIndex).Cells(COLUMN_RAWSQL).Value))

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Removes the clicked item from the cache
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemove.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' find the row in the dataset
            Dim drs As DataRow() = dsTSR.RecentQueries.Select("QueryID=" & CStr(dgvSQLGrid.Rows(_CurrentRowIndex).Cells(COLUMN_QUERYID).Value))

            ' check row was located
            If drs.Length = 1 Then

                ' delete the row
                drs(0).Delete()

                ' refresh the grid
                UpdateSQL()

            Else
                ' row was not found - shouldn't happen
                MsgBox("there's been some kind of a problem")
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Reverses the current state of the TSR Enabled flag
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuTSREnableDisable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTSREnableDisable.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' invert the current setting
            If My.Settings.Enabled = True Then
                My.Settings.Enabled = False
            Else
                My.Settings.Enabled = True
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Shows the options form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnuOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptions.Click

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            Dim Options As New frmOptions
            If Options.ShowDialog() = DialogResult.OK Then
                ' options were updated so may affect the grid - update it
                UpdateSQL()
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Saves the SQL and the application settings
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveSettings()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        clsExceptions.LogMessage("Storing Settings", clsExceptions.LogLevel.Level1)

        Dim stm As StreamWriter
        Try

            ' save the SQL
            Dim strFilepath As String = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), FILE_PREVIOUSQUERIES)
            stm = New StreamWriter(strFilepath, False)
            stm.Write(dsTSR.GetXml)
            stm.Flush()
            stm.Close()

        Catch ex As Exception
            Throw
        Finally
            If Not IsNothing(stm) Then
                stm.Dispose()
            End If
        End Try

        ' save col widths and row height from TSR window
        My.Settings.ColumnWidthSQL = dgvSQLGrid.Columns(COLUMN_SQL).Width
        My.Settings.ColumnWidthExecution = dgvSQLGrid.Columns(COLUMN_EXECUTION).Width
        My.Settings.ColumnWidthLength = dgvSQLGrid.Columns(COLUMN_LENGTH).Width

        ' todo - save the sort col and order

        ' save settings
        My.Settings.Save()

    End Sub

    ''' <summary>
    ''' Checks the cache size, performs the search and repopulates the grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateSQL()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)
        clsExceptions.LogMessage("Refreshing SQL", clsExceptions.LogLevel.Level3)

        ' set font
        dgvSQLGrid.Font = My.Settings.GridFont

        ' set dateformat
        dgvSQLGrid.Columns(COLUMN_EXECUTION).DefaultCellStyle.Format = My.Settings.GridDateFormat

        ' check that dataset doesn't exceed current cache limit
        CheckCacheSize()

        ' store sort column (if there is one)
        Dim colSort As DataGridViewColumn
        Dim dirSort As System.ComponentModel.ListSortDirection
        If Not IsNothing(dgvSQLGrid.SortedColumn) Then
            ' store sorted colum
            colSort = dgvSQLGrid.SortedColumn
            ' store order
            Select Case dgvSQLGrid.SortOrder
                Case SortOrder.Ascending, SortOrder.None
                    dirSort = System.ComponentModel.ListSortDirection.Ascending
                Case SortOrder.Descending
                    dirSort = System.ComponentModel.ListSortDirection.Descending
            End Select
        Else
            ' default to reverse chronological
            colSort = dgvSQLGrid.Columns(COLUMN_EXECUTION)
            dirSort = System.ComponentModel.ListSortDirection.Descending
        End If

        ' limit dataset to queries matching the current search string
        Dim dv As New DataView(dsTSR.RecentQueries, "SQL like '%" & cboFilter.Text & "%'", String.Empty, DataViewRowState.CurrentRows)
        dsbTSRBindingSource.DataSource = dv

        ' sort the grid
        dgvSQLGrid.Sort(colSort, dirSort)

        ' check first two columns are still invisible
        ' (apparently changing the datasource can affect this!)
        dgvSQLGrid.Columns(COLUMN_QUERYID).Visible = False
        dgvSQLGrid.Columns(COLUMN_RAWSQL).Visible = False

    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Deals with a search intiated by the combo box
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleSQLSearch()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        ' check whether there was a search string
        If cboFilter.Text.Length > 0 Then

            ' check whether the combo list already contains the search string
            If Not cboFilter.Items.Contains(cboFilter.Text) Then

                ' add the new search to the recent items
                cboFilter.Items.Add(cboFilter.Text)

                ' resort the combo box
                cboFilter.Sorted = False
                cboFilter.Sorted = True

            End If
        End If

        ' repopulate the list
        UpdateSQL()

    End Sub

    ''' <summary>
    ''' Checks whether the cache is over the limit and removes oldest entries if it is
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCacheSize()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        ' check whether current cache size is being reduced
        If My.Settings.CacheSize < dsTSR.RecentQueries.Rows.Count Then

            ' initialise counter
            Dim intRowNumber As Integer = 1

            ' cycle through rows (newest to oldest)
            For Each dr As dstTotalSQLRecall.RecentQueriesRow In dsTSR.RecentQueries.Select("", "QueryID DESC")

                If intRowNumber > My.Settings.CacheSize Then
                    ' chop off the queries beyond the new size limit
                    dr.Delete()
                End If

                ' increment counter
                intRowNumber += 1

            Next

        End If

    End Sub

#End Region

End Class
