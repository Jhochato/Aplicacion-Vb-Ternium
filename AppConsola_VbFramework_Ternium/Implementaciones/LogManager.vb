Imports System.IO

Public Class LogManager
    Implements ILogManager

    Private ReadOnly _logFilePath As String

    Public Sub New(logFilePath As String)
        _logFilePath = logFilePath
    End Sub

    Public Sub Log(message As String) Implements ILogManager.Log
        Try
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}")
        Catch ex As IOException
            Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}")
        End Try
    End Sub

    Public Sub LogError(message As String) Implements ILogManager.LogError
        Log($"ERROR: {message}")
    End Sub

    Public Sub WriteSectionTitle(title As String) Implements ILogManager.WriteSectionTitle
        Log($"--- {title} ---")
    End Sub
End Class
