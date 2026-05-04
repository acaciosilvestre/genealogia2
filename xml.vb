Imports System.IO
Imports System.Net.Http
Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions

Imports System.Xml

Module XML
    Public xmlloaded    'True se arquivo for carregado
    'Entrada:  arquivo .ged
    'Saída:    Membros.xml  e Familias.xml

    Dim inp
    'inpxmlDoc será montado para servir de entrada para o segundo arquivo (Familias.xml)
    Dim inpxmlDoc
    Dim membros

    Dim inst
    Dim head

    ' Adiciona um comentário ao documento XML
    Dim com
    Dim pessoa = 0

    Dim linha, cod, Att, namedNodeMap
    Dim outxmlDoc
    Dim familias, familia, newAtt, meuId
    Dim objNode, marido, esposa, crianca, individuo, nome, sex, nascimento, batismo, morte, sepultamento
    Dim nasclocal, batlocal, mortelocal, seplocal
    Dim FAM = 0
    'Dim INDI, BIRT, CHRS, DEAT, BURI
    'Dim rubrica = ""

    Sub initXML()
        'startFS2(My.Settings.defaultInputFile)

        If Form1.rbascii.Checked Then
            inpEnc = "ascii"
            outEnc = "ascii"
        Else
            inpEnc = "utf-8"
            outEnc = "utf-8"
        End If


        inpxmlDoc = CreateObject("Msxml2.DOMDocument.6.0")
        membros = inpxmlDoc.CreateElement("Membros")            'Elemento raiz
        inpxmlDoc.appendChild(membros)
        inst = inpxmlDoc.createProcessingInstruction("xml", "version='1.0' encoding='" & inpEnc & "'")
        inpxmlDoc.insertBefore(inst, membros)
        com = inpxmlDoc.createComment(Date.Today() & vbTab & TimeOfDay())
        inpxmlDoc.insertBefore(com, membros)

    End Sub

    Sub stopXML()
        inpxmlDoc.save("Membros.xml")
        outxmlDoc.save("Familias.xml")
        inpxmlDoc = Nothing
        membros = Nothing
        inst = Nothing
        com = Nothing
        inp = Nothing
    End Sub

    Sub token(tk_number As Integer, tk As String)

        Static level, xref_id, tag, line_value, evento

        Select Case (tk_number)

            Case EnumToken.TK_EOL

                Select Case (tag)
                    Case "SOUR"
                        If head Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & line_value
                        Else

                        End If


                    Case "VERS", "CHAR"     'gedcom file
                        If head Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & line_value
                        End If

                    Case "NAME"
                        If head Then        'gedcom file
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & line_value

                        Else
                            Dim match As Match
                            match = Regex.Match(xref_id, "\d+")
                            pessoa = match.Value

                            'pessoa = pessoa + 1
                            individuo = inpxmlDoc.CreateElement("Individuo")

                            newAtt = inpxmlDoc.createAttribute("id")            'Cria um atributo
                            newAtt.value = pessoa                               'Dá um valor
                            namedNodeMap = individuo.attributes                 'Obtém atributos do nó
                            namedNodeMap.setNamedItem(newAtt)                   'Insere novo atributo

                            membros.appendChild(individuo)

                            nome = inpxmlDoc.CreateElement("Nome")
                            nome.text = Replace(line_value, "/", "")
                            individuo.appendChild(nome)
                            Form1.ToolStripStatusLabel1.Text = nome.text
                        End If

                    Case "SEX"

                        sex = inpxmlDoc.CreateElement("Sex")
                        sex.text = line_value
                        individuo.appendChild(sex)

                    Case "DATE"
                        Select Case (evento)
                            Case "BIRT"
                                nascimento = inpxmlDoc.CreateElement("Nascimento")
                                nascimento.text = line_value
                                individuo.appendChild(nascimento)

                            Case "DEAT"
                                morte = inpxmlDoc.CreateElement("Morte")
                                morte.text = line_value
                                individuo.appendChild(morte)
                            Case "CHR"
                                batismo = inpxmlDoc.CreateElement("Batismo")
                                batismo.text = line_value
                                individuo.appendChild(batismo)

                            Case "MARR"
                                objNode = outxmlDoc.selectSingleNode("//Familias/Familia[@id='" & FAM & "']")
                                Dim dataCasamento = outxmlDoc.CreateElement("DataDoCasamento")                      'child
                                dataCasamento.text = line_value
                                objNode.appendChild(dataCasamento)
                                objNode = Nothing
                                'MsgBox("MARR: " & line_value)

                            Case "BURI"
                                sepultamento = inpxmlDoc.CreateElement("Sepultamento")
                                sepultamento.text = line_value
                                individuo.appendChild(sepultamento)

                        End Select

                    Case "PLAC"
                        Select Case (evento)
                            Case "BIRT"
                                nasclocal = inpxmlDoc.CreateElement("NascimentoLocal")
                                nasclocal.text = line_value
                                individuo.appendChild(nasclocal)

                            Case "CHR"
                                batlocal = inpxmlDoc.CreateElement("BatismoLocal")
                                batlocal.text = line_value
                                individuo.appendChild(batlocal)

                            Case "DEAT"
                                mortelocal = inpxmlDoc.CreateElement("MorteLocal")
                                mortelocal.text = line_value
                                individuo.appendChild(mortelocal)

                            Case "BURI"
                                seplocal = inpxmlDoc.CreateElement("SepultamentoLocal")
                                seplocal.text = line_value
                                individuo.appendChild(seplocal)

                            Case "MARR"
                                objNode = outxmlDoc.selectSingleNode("//Familias/Familia[@id='" & FAM & "']")
                                Dim localCasamento = outxmlDoc.CreateElement("LocalDoCasamento")                      'child
                                localCasamento.text = line_value
                                objNode.appendChild(localCasamento)
                                objNode = Nothing

                        End Select


                    Case "CHAN"
                        line_value = ""
                        tag = ""
                        evento = ""
                        tk = ""
                        xref_id = ""

                    Case "FAMC"
                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        newAtt = inpxmlDoc.createAttribute("famc")                        'Cria um atributo
                        newAtt.value = match.Value                                            'Dá um valor
                        namedNodeMap = individuo.attributes                                'Obtém atributos do nó
                        namedNodeMap.setNamedItem(newAtt)

                    Case "_FSFTID"
                        'MsgBox(xref_id)
                        Dim fsftid = inpxmlDoc.CreateElement("FSFTID")
                        fsftid.text = xref_id
                        individuo.appendChild(fsftid)

                    Case "FAMS"
                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        newAtt = inpxmlDoc.createAttribute("fams")                        'Cria um atributo
                        newAtt.value = match.Value                                            'Dá um valor
                        namedNodeMap = individuo.attributes                                'Obtém atributos do nó
                        namedNodeMap.setNamedItem(newAtt)

                    Case "FILE"
                        If head Then
                        Else
                            'SOURCE
                            'MsgBox(nome.text & " " & line_value)
                        End If


                    Case "FAM"
                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        FAM = match.Value

                        If FAM = 1 Then
                            Att = inpxmlDoc.createAttribute("xmlns:myNS")           'Cria um atributo
                            Att.value = "uri:ged2xml2"                  'Dá um valor
                            namedNodeMap = membros.attributes           'Obtém atributos do nó
                            namedNodeMap.setNamedItem(Att)              'Insere novo atributo

                            'inpxmlDoc.Save("Membros2.xml")

                            outxmlDoc = CreateObject("Msxml2.DOMDocument.6.0")
                            familias = outxmlDoc.CreateElement("Familias")      'Elemento raiz
                            outxmlDoc.appendChild(familias)
                            inst = outxmlDoc.createProcessingInstruction("xml", "version='1.0' encoding='" & outEnc & "'")
                            outxmlDoc.insertBefore(inst, familias)

                            ' Adiciona um comentário ao documento XML
                            com = outxmlDoc.createComment(Date.Today() & vbTab & TimeOfDay)
                            outxmlDoc.insertBefore(com, familias)

                            familia = outxmlDoc.CreateElement("Familia")       'child
                            newAtt = outxmlDoc.createAttribute("id")            'Cria um atributo
                            newAtt.value = FAM                  'Dá um valor
                            namedNodeMap = familia.attributes           'Obtém atributos do nó
                            namedNodeMap.setNamedItem(newAtt)           'Insere novo atributo
                            familias.appendChild(familia)


                        Else
                            familia = outxmlDoc.CreateElement("Familia")        'child
                            newAtt = outxmlDoc.createAttribute("id")            'Cria um atributo
                            newAtt.value = FAM                  'Dá um valor
                            namedNodeMap = familia.attributes           'Obtém atributos do nó
                            namedNodeMap.setNamedItem(newAtt)           'Insere novo atributo
                            familias.appendChild(familia)

                        End If


                    Case "HUSB"
                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        meuId = match.Value

                        objNode = inpxmlDoc.selectSingleNode("//Individuo[@id='" & meuId & "']/Nome")
                        marido = outxmlDoc.CreateElement("Marido")                      'child
                        newAtt = outxmlDoc.createAttribute("id")                        'Cria um atributo
                        newAtt.value = meuId                                            'Dá um valor
                        namedNodeMap = marido.attributes                                'Obtém atributos do nó
                        namedNodeMap.setNamedItem(newAtt)
                        familia.appendChild(marido)

                        Dim nmarido = outxmlDoc.CreateElement("Nome")
                        nmarido.text = objNode.text
                        marido.appendChild(nmarido)

                        Dim fmarido = outxmlDoc.CreateElement("Familia")
                        fmarido.text = FAM
                        marido.appendChild(fmarido)

                        objNode = Nothing


                    Case "WIFE"
                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        meuId = match.Value

                        objNode = inpxmlDoc.selectSingleNode("//Individuo[@id='" & meuId & "']/Nome")
                        esposa = outxmlDoc.CreateElement("Esposa")                  'child
                        newAtt = outxmlDoc.createAttribute("id")                    'Cria um atributo
                        newAtt.value = meuId                                   'Dá um valor
                        namedNodeMap = esposa.attributes                        'Obtém atributos do nó
                        namedNodeMap.setNamedItem(newAtt)                       'Insere novo atributo
                        familia.appendChild(esposa)

                        Dim nesposa = outxmlDoc.CreateElement("Nome")
                        nesposa.text = objNode.text
                        esposa.appendChild(nesposa)

                        Dim fesposa = outxmlDoc.CreateElement("Familia")
                        fesposa.text = FAM
                        esposa.appendchild(fesposa)

                        objNode = Nothing

                    Case "CHIL"

                        Dim match As Match
                        match = Regex.Match(xref_id, "\d+")
                        meuId = match.Value

                        objNode = inpxmlDoc.selectSingleNode("//Individuo[@id='" & meuId & "']/Nome")
                        crianca = outxmlDoc.CreateElement("Crianca")                'child
                        newAtt = outxmlDoc.createAttribute("id")                    'Cria um atributo
                        newAtt.value = meuId                                   'Dá um valor
                        namedNodeMap = crianca.attributes                       'Obtém atributos do nó
                        namedNodeMap.setNamedItem(newAtt)                       'Insere novo atributo
                        familia.appendChild(crianca)

                        Dim ncrianca = outxmlDoc.CreateElement("Nome")
                        ncrianca.text = objNode.text
                        crianca.appendchild(ncrianca)

                        Dim fcrianca = outxmlDoc.CreateElement("Familia")
                        fcrianca.text = FAM
                        crianca.appendchild(fcrianca)

                        objNode = Nothing
                End Select



            Case EnumToken.TK_UM
                level = tk

            Case EnumToken.TK_DOIS

                Select Case (Trim(level))
                    Case EnumLevel.LEVEL_0
                        If tk = "HEAD" Then
                            head = True
                        End If
                        'ref 
                        xref_id = tk
                    Case EnumLevel.LEVEL_1  'token2 level1
                        tag = tk
                        Select Case (tag)
                            Case "SOUR", "CHAR"

                            Case "NAME", "CHIL"
                                'NAME
                                'CHIL

                            Case "SEX"
                            'SEX
                            Case "BIRT", "CHR", "DEAT", "BURI", "MARR"
                                evento = tag
                                'BIRT
                                'CHR
                                'DEAT
                                'BURI

                            Case "FAMS", "FAMC", "CHAN"
                                evento = tag

                                'FAMS
                                'FAMC
                                'SOUR
                                'CHAN
                        End Select

                    Case EnumLevel.LEVEL_2  'token2 level2
                        tag = tk
                        Select Case (tag)
                            Case "DATE", "PLAC"
                                'MsgBox(evento & tag)
                                'DATE
                                'PLACE

                                'OBJE
                            Case "VERS"

                            Case "OBJE"

                            Case "SOUR"
                        End Select
                        'OBJE

                    Case EnumLevel.LEVEL_3  'token2 level3
                        tag = tk
                        'FORM
                        'FILE
                        Select Case (tag)
                            Case "FORM"
                            Case "FILE"
                            Case "OBJE"
                        End Select

                    Case EnumLevel.LEVEL_4
                        tag = tk
                        Select Case (tag)
                            Case "FORM"
                            Case "FILE"
                                'line_value = tk
                        End Select

                End Select   '463

            Case EnumToken.TK_TRES
                Select Case (level)
                    Case EnumLevel.LEVEL_0
                        Select Case (tk)
                            Case "INDI"
                                head = False
                        End Select
                        'INDI
                        'FAM
                        'MsgBox(xref_id)
                        tag = tk
                        If tk = "FAM" Then
                            evento = ""
                        End If

                    Case EnumLevel.LEVEL_1

                        Select Case (tag)
                            Case "NAME", "SEX"
                                line_value = tk

                            Case "SOUR"
                                xref_id = tk

                            Case "CHAR"
                                line_value = tk

                            Case "_FSFTID"
                                xref_id = tk

                            Case Else
                                xref_id = tk
                        End Select
                        'fams
                        'famc
                        'sour
                        'chil
                        'sex

                    Case EnumLevel.LEVEL_2
                        'MsgBox(evento & tag & " = " & tk)
                        'mês (nascimento, batismo, casamento,morte)
                        'lugar (nascimento, batismo, casamento,morte)
                        Select Case (tag)
                            Case "SOUR"
                                xref_id = tk
                            Case Else
                                line_value = tk
                        End Select


                    Case EnumLevel.LEVEL_3
                        line_value = tk

                    Case EnumLevel.LEVEL_4
                        line_value = tk
                End Select

            Case EnumToken.TK_QUATRO, EnumToken.TK_CINCO, EnumToken.TK_SEIS

                Select Case (level)
                    Case EnumLevel.LEVEL_1
                        'nome2,3,...
                        line_value = line_value & " " & tk
                    Case EnumLevel.LEVEL_2
                        'dia (nascimento, batismo, casamento,morte)
                        'lugar (nascimento, batismo, casamento,morte)
                        'MsgBox(evento & tag & " = " & tk)
                        line_value = line_value & " " & tk

                End Select

            Case > EnumToken.TK_SEIS
                line_value = line_value & " " & tk
        End Select '456


    End Sub

    Sub processaXML()
        startFS2(My.Settings.defaultInputFile)
        Dim l, c, p As String
        Dim i As Integer
        initXML()
        i = 0
        pessoa = 0

        For Each l In fileReader
            For Each c In l
                If (c <> " ") Then
                    p = p & c
                Else
                    i = i + 1
                    token(i, p)
                    p = ""
                End If

            Next
            i = i + 1
            token(i, p)
            p = ""
            i = 0
            token(-1, "")
        Next
        stopXML()
        stopFS2()

    End Sub
    Public ldxmlFile
    Sub LoadXML(xmlFile)
        ldxmlFile = CreateObject("Msxml2.DOMDocument.6.0")
        ldxmlFile.async = False
        If ldxmlFile.load(xmlFile) Then
            Form1.RichTextBox1.Text = ldxmlFile.xml
            xmlloaded = True
        Else
            xmlloaded = False
            MsgBox(ldxmlFile.parseError.reason)
        End If
	End Sub
    Function xpath(xpathquery)
        If xmlloaded Then
            Try
                Dim obNode = ldxmlFile.selectSingleNode(xpathquery)
                If IsNothing(obNode) Then
                    xpath = Nothing
                Else

                    If Form1.RadioButtonXML.Checked Then
                        xpath = obNode.xml
                    Else
                        xpath = obNode.text
                    End If
                End If
            Catch ex As Exception
                MsgBox("Erro: " & ex.Message)
            End Try
        Else
            MsgBox("Nenhum xml carregado. ")
        End If
    End Function

    Sub xmlns()
        Dim xmldoc As New XmlDocument()
        xmldoc.Load("Familias.xml")
        Dim nsmgr As New XmlNamespaceManager(xmldoc.NameTable)
        nsmgr.AddNamespace("myNS", "uri:ged2xml2")
        nsmgr.AddNamespace("f", "family")
        Dim node As XmlNode = xmldoc.SelectSingleNode("//Crianca[@id='1']/Familia", nsmgr)
        Dim fami = node.InnerXml
        node = xmldoc.SelectSingleNode("//Familia[@id='" & fami & "']/Marido", nsmgr)
        MsgBox(node.InnerXml)
        'xmldoc.Load("Membros.xml")
        'node = xmldoc.SelectSingleNode("//Individuo[@id='1444']", nsmgr)
        'MsgBox(node.InnerXml)

    End Sub

End Module
