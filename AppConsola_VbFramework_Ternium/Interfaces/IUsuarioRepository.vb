Imports System.Data.SqlClient
Public Interface IUsuarioRepository
    Sub InsertUser(conn As SqlConnection, nombre As String, apellido As String, edad As Integer, correo As String, hobbies As String, activo As Boolean, creadoPor As String)
    Sub GetUsersByAge(conn As SqlConnection, edad As Integer, logger As ILogManager)
    Sub GetUsersCreatedLast2Hours(conn As SqlConnection, logger As ILogManager)

End Interface
