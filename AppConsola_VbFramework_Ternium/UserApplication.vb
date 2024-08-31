Imports System.Data.SqlClient

Public Class UserApplication
    Private ReadOnly _configService As IConfigManager
    Private ReadOnly _logger As ILogManager
    Private ReadOnly _userInputService As IUserInput
    Private ReadOnly _dbService As IUsuarioRepository

    Public Sub New(configService As IConfigManager, logger As ILogManager, userInputService As IUserInput, dbService As IUsuarioRepository)
        _configService = configService
        _logger = logger
        _userInputService = userInputService
        _dbService = dbService
    End Sub

    Public Sub Run()
        Try
            Dim connectionString = _configService.GetConnectionString()
            Dim logFilePath = _configService.GetLogFilePath()
            _logger.Log("Iniciando la aplicación")

            Using conn As New SqlConnection(connectionString)
                conn.Open()

                Dim nombre = _userInputService.GetUserInput("Ingrese el Nombre del Usuario:")
                Dim apellido = _userInputService.GetUserInput("Ingrese el Apellido del Usuario:")
                Dim edad = _userInputService.GetIntegerInput("Ingrese la Edad del Usuario:")
                Dim correo = _userInputService.GetUserInput("Ingrese el Correo del Usuario:")
                Dim hobbies = _userInputService.GetUserInput("Ingrese los Hobbies del Usuario (separados por guiones):")
                Dim activo = _userInputService.GetUserInput("¿Está el Usuario Activo? (S/N):").ToUpper() = "S"
                Dim creadoPor = _userInputService.GetUserInput("Nombre de quien crea el Usuario:")

                Try
                    _dbService.InsertUser(conn, nombre, apellido, edad, correo, hobbies, activo, creadoPor)
                Catch ex As SqlException
                    _logger.LogError($"Error al ejecutar el procedimiento InsertarUsuario: {ex.Message}")
                End Try

                Dim edadParaConsulta = _userInputService.GetIntegerInput("Ingrese la Edad para obtener usuarios:")

                Try
                    _dbService.GetUsersByAge(conn, edadParaConsulta, _logger)
                Catch ex As SqlException
                    _logger.LogError($"Error al ejecutar el procedimiento ObtenerUsuariosPorEdad: {ex.Message}")
                End Try

                Try
                    _dbService.GetUsersCreatedLast2Hours(conn, _logger)
                Catch ex As SqlException
                    _logger.LogError($"Error al ejecutar el procedimiento ObtUsuarioCreadosUltimas2Horas: {ex.Message}")
                End Try
            End Using
        Catch ex As Exception
            _logger.LogError($"Error en el proceso principal: {ex.Message}")
        End Try
    End Sub
End Class
