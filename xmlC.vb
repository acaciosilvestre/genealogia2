Public Class xmlClass
    'Private Field
    Private PrivateField As String
    Public rec As xmlClass
    Private xmlDoc

    Public Property ClassName() As String
        Get
            ' Gets the property value.
            Return PrivateField
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            PrivateField = Value
        End Set

    End Property

    Public Function captalize()
        rec = New xmlClass("teste.xml")
        captalize = rec.GetRootElement
    End Function
    Public Function GetRootElement()
        GetRootElement = xmlDoc.documentElement.nodename
    End Function


    'Constructor
    Public Sub New(ByVal xmlFile As String)
        ' Set the property value.
        Me.PrivateField = xmlFile
        xmlDoc = CreateObject("Msxml2.DOMDocument.6.0")
        Try
            xmlDoc.load(xmlFile)
        Catch ex As Exception
            Debug.WriteLine("xmlC load error: " & ex.Message)
            xmlDoc = Nothing
        End Try
    End Sub
End Class
