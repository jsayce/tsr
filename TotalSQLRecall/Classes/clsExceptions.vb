Imports System.IO

Public Class clsExceptions

    Public Enum LogLevel As Byte
        Level1 = 1
        Level2 = 2
        Level3 = 3
    End Enum

    ''' <summary>
    ''' stores exception details when verbose switch is set
    ''' </summary>
    ''' <param name="Ex"></param>
    ''' <remarks></remarks>
    Public Shared Sub HandleException(ByVal Ex As Exception)

        ' store the details to the application log
        WriteToFile(Ex.Message & " - " & Ex.StackTrace)

    End Sub

    ''' <summary>
    ''' Logs info messages to help with debugging
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <param name="Level"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMessage(ByVal Message As String, ByVal Level As LogLevel)

        ' check how verbose we're being at the moment
        If Level <= My.Settings.LogFileLevel Then
            WriteToFile(Message)
        End If

    End Sub

    ''' <summary>
    ''' Performs actual logging for exceptions and info messages
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Shared Sub WriteToFile(ByVal Message As String)

        ' open the log stream
        ' todo - test we can do this on vista
        Dim stm As New StreamWriter(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), "TSR.log"), True)

        ' store the message
        stm.WriteLine(Now & ": " & Message)

        ' finalise the stream
        stm.Flush()
        stm.Close()
        stm.Dispose()

    End Sub

End Class
