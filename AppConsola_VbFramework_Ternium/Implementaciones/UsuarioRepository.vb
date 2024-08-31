Imports System.Data
Imports System.Data.SqlClient

Public Class UsuarioRepository
    Implements IUsuarioRepository

    Public Sub InsertUser(conn As SqlConnection, nombre As String, apellido As String, edad As Integer, correo As String, hobbies As String, activo As Boolean, creadoPor As String) Implements IUsuarioRepository.InsertUser
        Using cmd As New SqlCommand("InsertarUsuario", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Nombre", nombre)
            cmd.Parameters.AddWithValue("@Apellido", apellido)
            cmd.Parameters.AddWithValue("@Edad", edad)
            cmd.Parameters.AddWithValue("@Correo", correo)
            cmd.Parameters.AddWithValue("@Hobbies", hobbies)
            cmd.Parameters.AddWithValue("@Activo", activo)
            cmd.Parameters.AddWithValue("@UsuarioCreacion", creadoPor)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub GetUsersByAge(conn As SqlConnection, edad As Integer, logger As ILogManager) Implements IUsuarioRepository.GetUsersByAge
        logger.WriteSectionTitle("Resultados de usuarios de acuerdo a la edad ingresada")

        Using cmd As New SqlCommand("ObtenerUsuariosPorEdad", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Edad", edad)

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim logEntry As String = $"{DateTime.Now}: {String.Concat(reader.GetValue(1), "|", reader.GetValue(2), "|", reader.GetValue(3), "|", reader.GetValue(4), "|", reader.GetValue(5))}"
                    logger.Log(logEntry)
                End While
            End Using
        End Using
    End Sub

    Public Sub GetUsersCreatedLast2Hours(conn As SqlConnection, logger As ILogManager) Implements IUsuarioRepository.GetUsersCreatedLast2Hours
        logger.WriteSectionTitle("Usuarios Creados en las Últimas 2 Horas")

        Using cmd As New SqlCommand("ObtUsuarioCreadosUltimas2Horas", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim logEntry As String = $"{DateTime.Now}: {String.Concat(reader.GetValue(1), "|", reader.GetValue(2), "|", reader.GetValue(3), "|", reader.GetValue(4), "|", reader.GetValue(5))}"
                    logger.Log(logEntry)
                End While
            End Using
        End Using
    End Sub
End Class
