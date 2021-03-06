Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80
Imports Microsoft.SqlServer.Management.UI.VSIntegration
Imports System.IO

Public Class Connect
    Implements IDTExtensibility2
    Implements IDTCommandTarget

#Region " Declarations "

    ' standard extensibility bits and bobs
    Private _DTE2 As DTE2
    Private _DTE As EnvDTE.DTE
    Private _addInInstance As AddIn
    Private _CommandEvents As CommandEvents
    Private m_toolWindow As Window = Nothing

    ' commandbar stuff
    Private _CommandBarControl As CommandBarControl
    Private Const COMMAND_NAME As String = "TotalSQLRecall"
    Private Const COMMAND_TEXT As String = "T&otal SQL Recall"

    ' the TSR control
    Private WithEvents m_control As uctToolWindow

    ' used to time queries
    Private m_start As Date

#End Region

#Region " Constructor "

    '''<summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
    Public Sub New()

    End Sub

#End Region

#Region " IDTExtensibility2 Interface Methods "

    '''<summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
    '''<param name='application'>Root object of the host application.</param>
    '''<param name='connectMode'>Describes how the Add-in is being loaded.</param>
    '''<param name='addInInst'>Object representing this Add-in.</param>
    '''<remarks></remarks>
    Public Sub OnConnection(ByVal application As Object, ByVal connectMode As ext_ConnectMode, ByVal addInInst As Object, ByRef custom As Array) Implements IDTExtensibility2.OnConnection

        Try

            ' check log file verbosity level
            SetLogFileLevel()

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)
            clsExceptions.LogMessage("Starting Up TSR Add-in", clsExceptions.LogLevel.Level1)

            ' get extensibility objects
            _DTE2 = CType(application, DTE2)
            _DTE = CType(application, EnvDTE.DTE)
            _addInInstance = CType(addInInst, AddIn)

            ' get the Query.Execute command
            _CommandEvents = _DTE.Events.CommandEvents("{52692960-56BC-4989-B5D3-94C47A513E8D}", 1)

            ' add event handlers for the events
            AddHandler _CommandEvents.BeforeExecute, AddressOf OnBeforeExecute
            AddHandler _CommandEvents.AfterExecute, AddressOf OnAfterExecute

            Select Case connectMode

                Case ext_ConnectMode.ext_cm_Startup
                    ' The add-in was marked to load on startup
                    ' Do nothing at this point because the IDE may not be fully initialized
                    ' VS will call OnStartupComplete when ready

                Case ext_ConnectMode.ext_cm_AfterStartup
                    ' The add-in was loaded after startup
                    ' Initialize it in the same way that when is loaded on startup calling manually this method:
                    OnStartupComplete(Nothing)

            End Select

            ' add TSR window
            CreateTSRWindow()

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    '''<summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
    '''<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnDisconnection(ByVal disconnectMode As ext_DisconnectMode, ByRef custom As Array) Implements IDTExtensibility2.OnDisconnection

        Try

            ' check whether the control in the tools menu is there
            If Not (_CommandBarControl Is Nothing) Then
                ' delete the menu item
                _CommandBarControl.Delete()
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    '''<summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnAddInsUpdate(ByRef custom As Array) Implements IDTExtensibility2.OnAddInsUpdate
    End Sub

    '''<summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnStartupComplete(ByRef custom As Array) Implements IDTExtensibility2.OnStartupComplete

        Try

            Dim myCommand As Command = Nothing
            Dim commandBars As CommandBars
            Dim toolsCommandBar As CommandBar

            ' try to retrieve the command, just in case it was already created
            Try
                myCommand = _DTE.Commands.Item(_addInInstance.ProgID & "." & COMMAND_NAME)
            Catch
                ' this just means the command wasn't found
            End Try

            ' add the command if it does not exist
            If myCommand Is Nothing Then
                myCommand = _DTE.Commands.AddNamedCommand(_addInInstance, COMMAND_NAME, COMMAND_TEXT, "Shows the Total SQL Recall tool window", True, 0, Nothing, vsCommandStatus.vsCommandStatusSupported Or vsCommandStatus.vsCommandStatusEnabled)
            End If

            ' retrieve the collection of commandbars
            commandBars = DirectCast(_DTE.CommandBars, CommandBars)

            ' get the tools commandbar
            toolsCommandBar = commandBars.Item("View")

            ' add a button to the tools menu
            _CommandBarControl = DirectCast(myCommand.AddControl(toolsCommandBar, toolsCommandBar.Controls.Count + 1), CommandBarControl)
            _CommandBarControl.Caption = COMMAND_TEXT

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    '''<summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnBeginShutdown(ByRef custom As Array) Implements IDTExtensibility2.OnBeginShutdown

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' save settings
            m_control.SaveSettings()

            clsExceptions.LogMessage("Game over - TSR Closing down", clsExceptions.LogLevel.Level1)

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " IDTCommandTarget Interface Methods "

    '''<summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
    '''<param name='commandName'>The name of the command to execute.</param>
    '''<param name='executeOption'>Describes how the command should be run.</param>
    '''<param name='varIn'>Parameters passed from the caller to the command handler.</param>
    '''<param name='varOut'>Parameters passed from the command handler to the caller.</param>
    '''<param name='handled'>Informs the caller if the command was handled or not.</param>
    '''<remarks></remarks>
    Public Sub Exec(ByVal commandName As String, ByVal executeOption As EnvDTE.vsCommandExecOption, ByRef varIn As Object, ByRef varOut As Object, ByRef handled As Boolean) Implements EnvDTE.IDTCommandTarget.Exec

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            handled = False
            If executeOption = vsCommandExecOption.vsCommandExecOptionDoDefault Then
                If commandName = _addInInstance.ProgID & "." & COMMAND_NAME Then
                    ' show addin
                    m_toolWindow.Visible = True
                    ' set flag to show command has been handled
                    handled = True
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    '''<summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
    '''<param name='commandName'>The name of the command to determine state for.</param>
    '''<param name='neededText'>Text that is needed for the command.</param>
    '''<param name='status'>The state of the command in the user interface.</param>
    '''<param name='commandText'>Text requested by the neededText parameter.</param>
    '''<remarks></remarks>
    Public Sub QueryStatus(ByVal commandName As String, ByVal neededText As EnvDTE.vsCommandStatusTextWanted, ByRef status As EnvDTE.vsCommandStatus, ByRef commandText As Object) Implements EnvDTE.IDTCommandTarget.QueryStatus

        Try
            If neededText = vsCommandStatusTextWanted.vsCommandStatusTextWantedNone Then
                If commandName = _addInInstance.ProgID & "." & COMMAND_NAME Then
                    status = CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
                Else
                    status = vsCommandStatus.vsCommandStatusUnsupported
                End If
            End If
        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " Delegates for DTE Events "

    ''' <summary>
    ''' Handles the BeforeExecute event: starts a timer
    ''' </summary>
    ''' <param name="GUID"></param>
    ''' <param name="ID"></param>
    ''' <param name="CustomIn"></param>
    ''' <param name="CustomOut"></param>
    ''' <param name="CancelDefault"></param>
    ''' <remarks></remarks>
    Private Sub OnBeforeExecute(ByVal GUID As String, ByVal ID As Integer, ByVal CustomIn As Object, ByVal CustomOut As Object, ByRef CancelDefault As Boolean)

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' record start time
            m_start = Now

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Handles the AfterExecute event: stops timer, stores SQL, refreshes window
    ''' </summary>
    ''' <param name="GUID"></param>
    ''' <param name="ID"></param>
    ''' <param name="CustomIn"></param>
    ''' <param name="CustomOut"></param>
    ''' <remarks></remarks>
    Private Sub OnAfterExecute(ByVal GUID As String, ByVal ID As Integer, ByVal CustomIn As Object, ByVal CustomOut As Object)

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' check if currently enabled
            If My.Settings.Enabled Then

                '--------------------------------
                ' 1. Work out how long query took
                '--------------------------------

                Dim lngQueryTime As Long = DateDiff(DateInterval.Second, m_start, Now)

                '--------------------------------
                ' 2. Get the Query
                '--------------------------------

                Dim doc As EnvDTE.TextDocument = CType(_DTE.ActiveDocument.Object(String.Empty), EnvDTE.TextDocument)
                Dim strQuery As String = doc.Selection.Text

                If String.IsNullOrEmpty(strQuery) Then
                    ' user fired off whole page, not selection, so need to grab all text

                    Try

                        ' cancel screen updating while we do the selection
                        _DTE.SuppressUI = True

                        ' store current position
                        Dim CursorPosition As EnvDTE.EditPoint
                        CursorPosition = doc.CreateEditPoint(doc.Selection.ActivePoint)

                        ' select entire doc
                        doc.Selection.SelectAll()

                        ' store selection
                        strQuery = doc.Selection.Text

                        ' unselect text
                        doc.Selection.Collapse()

                        ' put cursor back where it was
                        doc.Selection.MoveToPoint(CursorPosition)

                    Catch ex As Exception
                        Throw
                    Finally

                        ' always re-enable screen updating
                        _DTE.SuppressUI = False

                    End Try

                End If

                '--------------------------------
                ' 3. Add query to dataset
                '--------------------------------

                ' remove any leading or trailing whitespace
                Dim strTrimmedQuery As String = strQuery.Trim()

                ' check query isn't too big
                If (Not My.Settings.LimitSQLSize) OrElse strTrimmedQuery.Length <= My.Settings.SQLSize Then

                    Dim dr As dstTotalSQLRecall.RecentQueriesRow = m_control.QueryData.RecentQueries.NewRecentQueriesRow
                    dr.ExecutionDate = m_start
                    dr.ExecutionLength = lngQueryTime
                    dr.SQL = strTrimmedQuery
                    m_control.QueryData.RecentQueries.AddRecentQueriesRow(dr)

                    '--------------------------------
                    ' 4. Repopulate the window
                    '--------------------------------

                    m_control.UpdateSQL()

                End If

            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " TSR Events "

    ''' <summary>
    ''' Inserts SQL into current window at cursor point
    ''' </summary>
    ''' <param name="SQL"></param>
    ''' <remarks></remarks>
    Private Sub m_control_InsertCurrentSQL(ByVal SQL As String) Handles m_control.InsertCurrentSQL

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' get the active document
            Dim x As Object = _DTE.ActiveDocument

            ' check that the active document was found (will be nothing if there is no editor open)
            If Not IsNothing(x) Then

                ' get text document from active document
                Dim doc As EnvDTE.TextDocument = CType(_DTE.ActiveDocument.Object(String.Empty), EnvDTE.TextDocument)

                ' get cursor position
                Dim editPoint As EnvDTE.EditPoint = doc.CreateEditPoint(doc.Selection.ActivePoint)

                ' insert SQL at cursor
                editPoint.Insert(SQL)

            End If

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Inserts SQL into new window
    ''' </summary>
    ''' <param name="SQL"></param>
    ''' <remarks></remarks>
    Private Sub m_control_OpenNewSQL(ByVal SQL As String) Handles m_control.OpenNewSQL

        Try

            ' standard level 2 logging message
            clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

            ' create new document
            ServiceCache.ScriptFactory.CreateNewBlankScript(Microsoft.SqlServer.Management.UI.VSIntegration.Editors.ScriptType.Sql)

            ' insert SQL
            m_control_InsertCurrentSQL(SQL)

        Catch ex As Exception
            clsExceptions.HandleException(ex)
        End Try

    End Sub

#End Region

#Region " Private methods "

    ''' <summary>
    ''' Sets up the SSMS Tool Window for TSR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTSRWindow()

        ' standard level 2 logging message
        clsExceptions.LogMessage("Starting " & MethodBase.GetCurrentMethod().Name, clsExceptions.LogLevel.Level2)

        clsExceptions.LogMessage("Creating window", clsExceptions.LogLevel.Level1)

        ' get windows2 interface
        Dim MyWindow As Windows2 = CType(_DTE2.Windows, Windows2)

        ' get current assembly
        Dim asm As Assembly = System.Reflection.Assembly.GetExecutingAssembly

        ' create the window
        Dim MyControl As Object = Nothing
        m_toolWindow = MyWindow.CreateToolWindow2(_addInInstance, asm.Location, "SauceCode.TotalSQLRecall.uctToolWindow", "Total SQL Recall", "{84A3675C-CDA0-4c8b-858F-8F00BEACF199}", MyControl)

        ' set the TSR icon
        m_toolWindow.SetTabPicture(My.Resources.TSR.GetHbitmap)

        ' cast the returned object to a SSMSTestWindow user control
        m_control = CType(MyControl, uctToolWindow)

        ' populate the list of queries
        m_control.UpdateSQL()

    End Sub

    ''' <summary>
    ''' Checks whether log file initiator is present and sets logging level
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLogFileLevel()

        Dim stm As StreamReader

        Try

            ' set log file initiator path
            Dim strLogFileInitiatorPath As String = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), "LogLevel.txt")

            ' check if file exists
            If File.Exists(strLogFileInitiatorPath) Then

                ' read file
                stm = New StreamReader(strLogFileInitiatorPath, True)
                Dim strContents As String = stm.ReadToEnd

                ' check valid location
                If strContents.Length = 1 AndAlso IsNumeric(strContents) Then

                    ' get log level from file
                    Select Case CInt(strContents)
                        Case 1
                            My.Settings.LogFileLevel = 1
                        Case 2
                            My.Settings.LogFileLevel = 2
                        Case 3
                            My.Settings.LogFileLevel = 3
                        Case Else
                            ' invalid number so we're not logging
                            My.Settings.LogFileLevel = 0
                    End Select

                Else
                    ' invalid file content so we're not logging
                    My.Settings.LogFileLevel = 0
                End If

            Else
                ' no log file initiator so we're not logging
                My.Settings.LogFileLevel = 0
            End If

        Catch ex As Exception
            ' something went wrong so, you guessed it, we're not logging
            My.Settings.LogFileLevel = 0
            Throw
        Finally

            If Not IsNothing(stm) Then
                stm.Close()
                stm.Dispose()
            End If

        End Try

    End Sub

#End Region

End Class
