Module Utils
    Public Function EscapeSql(value As String) As String
        If value Is Nothing Then Return ""
        Return value.Replace("'", "''")
    End Function
End Module
