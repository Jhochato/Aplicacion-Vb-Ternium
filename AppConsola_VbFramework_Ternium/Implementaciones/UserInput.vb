Public Class UserInput
    Implements IUserInput

    Public Function GetUserInput(prompt As String) As String Implements IUserInput.GetUserInput
        Console.WriteLine(prompt)
        Return Console.ReadLine()
    End Function

    Public Function GetIntegerInput(prompt As String) As Integer Implements IUserInput.GetIntegerInput
        Dim result As Integer
        Do
            Console.WriteLine(prompt)
            Dim input = Console.ReadLine()
            If Integer.TryParse(input, result) Then
                Exit Do
            End If
            Console.WriteLine("Entrada no válida. Por favor ingrese un número entero.")
        Loop
        Return result
    End Function
End Class
