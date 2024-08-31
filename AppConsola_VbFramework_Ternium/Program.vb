Imports System

Module Program
    Sub Main()
        Dim configService As New ConfigManager("config.xml")
        Dim logger As New LogManager(configService.GetLogFilePath())
        Dim userInputService As New UserInput()
        Dim dbService As New UsuarioRepository()

        Dim app As New UserApplication(configService, logger, userInputService, dbService)
        app.Run()
    End Sub
End Module
