Public Class ConfigManager
    Implements IConfigManager

    Private ReadOnly _config As XDocument

    Public Sub New(configFilePath As String)
        _config = XDocument.Load(configFilePath)
    End Sub

    Public Function GetConnectionString() As String Implements IConfigManager.GetConnectionString
        Dim element = _config.Root.Element("connectionStrings")?.Element("add")
        If element Is Nothing OrElse String.IsNullOrEmpty(element.Attribute("connectionString")?.Value) Then
            Throw New InvalidOperationException("Cadena de conexión no configurada en el archivo XML.")
        End If
        Return element.Attribute("connectionString")?.Value
    End Function

    Public Function GetLogFilePath() As String Implements IConfigManager.GetLogFilePath
        Dim element = _config.Root.Element("appSettings")?.Element("add")
        If element Is Nothing OrElse String.IsNullOrEmpty(element.Attribute("value")?.Value) Then
            Throw New InvalidOperationException("Ruta del archivo de log no configurada en el archivo XML.")
        End If
        Return element.Attribute("value")?.Value
    End Function

End Class
