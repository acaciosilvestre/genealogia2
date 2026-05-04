Imports System.Xml

Public Class Individuo
    Private id As Integer
    Public fsftid As String
    Public nome As String
    Private sexo As String
    Public childFamily, ParentFamily, ParentFamily2, ParentFamily3
    Public nomedoPai As String
    Public nomedaMae As String
    Public conjuge, conjuge2, conjuge3 As String
    Public iddoPai, iddaMae, idConjuge, idConjuge2, idConjuge3 As Integer
    Public casamento(2, 2) As String

    Public Property IndividuoID() As String
        Get
            ' Gets the property value.
            Return id
        End Get
        Set(ByVal Value As String)
            ' Sets the property value.
            id = Value
        End Set
    End Property

    Public Sub New(ByVal id As Integer)
        ' Set the property value.
        Me.id = id
    End Sub

    Public Sub SetIndividuo()
        LoadXML("Membros.xml")
        nome = xpath("//Membros/Individuo[@id='" & Me.id & "']/Nome")
        sexo = xpath("//Membros/Individuo[@id='" & Me.id & "']/Sex")
        fsftid = xpath("//Membros/Individuo[" & Me.id & "]/FSFTID")
        LoadXML("Familias.xml")

        'PAIS
        childFamily = xpath("//Familias/Familia/Crianca[@id='" & Me.id & "']/Familia")

        If (IsNothing(childFamily)) Then
            nomedoPai = "Desconhecido"
            nomedaMae = "Desconhecida"
        Else
            nomedoPai = xpath("//Familias/Familia[@id='" & childFamily & "']/Marido/Nome")
            nomedaMae = xpath("//Familias/Familia[@id='" & childFamily & "']/Esposa/Nome")
            If (IsNothing(nomedoPai)) Then
                nomedoPai = "Desconhecido"
            Else
                iddoPai = xpath("//Familias/Familia[@id='" & childFamily & "']/Marido/@id")
            End If

            If (IsNothing(nomedaMae)) Then
                nomedaMae = "Desconhecida"
            Else
                iddaMae = xpath("//Familias/Familia[@id='" & childFamily & "']/Esposa/@id")
            End If
        End If

        'CONJUGE
        Dim papel, papel_conjuge As String
        papel = ""
        papel_conjuge = ""

        Select Case (sexo)
            Case "M"
                papel = "Marido"
                papel_conjuge = "Esposa"
            Case "F"
                papel = "Esposa"
                papel_conjuge = "Marido"
        End Select

        ParentFamily = xpath("//Familias/Familia/" & papel & "[@id='" & Me.id & "']/Familia")
        If IsNothing(ParentFamily) Then
            conjuge = "Desconhecido/Inexistente"
            idConjuge = 0
        Else
            conjuge = xpath("//Familias/Familia[@id='" & ParentFamily & "']/" & papel_conjuge & "/Nome")
            idConjuge = xpath("//Familias/Familia[@id='" & ParentFamily & "']/" & papel_conjuge & "/@id")
            casamento(0, 0) = xpath("//Familias/Familia[" & ParentFamily & "]/DataDoCasamento")
            casamento(0, 1) = xpath("//Familias/Familia[" & ParentFamily & "]/LocalDoCasamento")
        End If

        ParentFamily2 = xpath("//Familias/Familia[@id>' " & ParentFamily & " ']/" & papel & "[@id='" & Me.id & "']/Familia")
        If IsNothing(ParentFamily2) Then
            conjuge2 = "Desconhecido/Inexistente"
            idConjuge2 = 0
        Else
            conjuge2 = xpath("//Familias/Familia[@id='" & ParentFamily2 & "']/" & papel_conjuge & "/Nome")
            idConjuge2 = xpath("//Familias/Familia[@id='" & ParentFamily2 & "']/" & papel_conjuge & "/@id")
            casamento(1, 0) = xpath("//Familias/Familia[" & ParentFamily2 & "]/DataDoCasamento")
            casamento(1, 1) = xpath("//Familias/Familia[" & ParentFamily2 & "]/LocalDoCasamento")

        End If

        ParentFamily3 = xpath("//Familias/Familia[@id>' " & ParentFamily2 & " ']/" & papel & "[@id='" & Me.id & "']/Familia")
        If IsNothing(ParentFamily3) Then
            conjuge3 = "Desconhecido/Inexistente"
            idConjuge3 = 0
        Else
            conjuge3 = xpath("//Familias/Familia[@id='" & ParentFamily3 & "']/" & papel_conjuge & "/Nome")
            idConjuge3 = xpath("//Familias/Familia[@id='" & ParentFamily3 & "']/" & papel_conjuge & "/@id")
            casamento(2, 0) = xpath("//Familias/Familia[" & ParentFamily3 & "]/DataDoCasamento")
            casamento(2, 1) = xpath("//Familias/Familia[" & ParentFamily3 & "]/LocalDoCasamento")

        End If

    End Sub
    Public Function getChild(familia As Integer, i As Integer) As String
        Dim crianca As String
        crianca = xpath("//Familias/Familia[@id='" & familia & "']/Crianca[" & i & "]/Nome")
        If IsNothing(crianca) Then
            getChild = ""
        Else
            getChild = crianca
        End If

    End Function
End Class
