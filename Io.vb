
Imports genealogia.Form1
Imports System.IO
Imports System.Runtime.Intrinsics.X86
Imports System.Text
Imports System.Text.RegularExpressions
Module IO
    Public Const kInputFileName = "C:\Users\acaci\OneDrive\Documents\NovaBase_ansi.ged"
    Const kForReading = 1
    Const kForWriting = 2
    Const kForAppending = 8
    Const kTristateUseDefault = -2  'Abre o arquivo usando o padrão do sistema.
    Const kTristateTrue = -1    'Abre o arquivo como Unicode.
    Const kTristateFalse = 0    'Abre o arquivo como ASCII.

    Public Const kTableName = "Membros"

    'Command Line
    Const kActionUnknown = 0
    Const kActionSet = 1    'New DataBase
    Const kActionGet = 2    'Query
    Const kQuery = 1
    Const kDBNew = 2
    Const kErrorSuccess = 0
    Const kErrorFailure = 1


    'Variáveis Globais
    Public InputFileName, InputFileExists, fs, a, b, stdout
    Public DataBaseName, DataBaseExists
    Public conn
    Public rs, TableName, Campos(9), connectionString
    Dim pessoa = 0, linha, nome, sex, nasc, evento, local
    Dim bloc, batismo, cloc, mort, dloc, sepulta, sloc ', fami
    Dim header As Boolean
    Public inpEnc = "ascii"
    Public outEnc = "ascii"
    Public fileReader As String()


    Public Enum EnumToken
        TK_EOL = -1
        TK_UM = 1
        TK_DOIS
        TK_TRES
        TK_QUATRO
        TK_CINCO
        TK_SEIS
    End Enum
    Public Enum EnumLevel
        LEVEL_0 = 0
        LEVEL_1
        LEVEL_2
        LEVEL_3
        LEVEL_4
        lEVEL_5
    End Enum



    Sub StopFS()
        a.Close
        a = Nothing
        fs = Nothing
    End Sub
    Sub Insere()
        'On Error Resume Next
        conn.Execute("INSERT INTO Membros(ID,Nome,Nascimento,LocalNascimento,Falecimento,LocalFalecimento,Batismo,LocalBatismo,Sepultamento,LocalSepultamento) VALUES ('" &
pessoa & "','" & nome & "','" & nasc & "','" & bloc & "','" & mort & "','" & dloc & "','" & batismo & "','" & bloc & "','" & sepulta & "','" & sloc & "')")
    End Sub


    Sub token2(tk_number As Integer, tk As String)

        Static level, xref_id, tag, line_value, evento
        Select Case (tk_number)

            Case EnumToken.TK_EOL

                Select Case (tag)
                    'GEDCOM HEADER
                    Case "TYPE"
                        If evento = "EVEN" Then
                            evento = line_value
                        End If
                    Case "TRLR"
                        Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "TRLR"
                    Case "SOUR"
                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Source: " & line_value & vbCrLf
                        End If

                    Case "VERS"
                        Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Vers: " & line_value & vbCrLf


                    Case "TIME"
                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Time: " & line_value & vbCrLf
                        End If

                    Case "FILE"
                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "File: " & line_value & vbCrLf
                        End If

                    Case "FORM"
                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Form: " & line_value & vbCrLf
                        End If

                    Case "CHAR"
                        Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Char: " & line_value & vbCrLf
                    'FIM GEDCOM HEADER

                    Case "NAME"
                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Name: " & line_value & vbCrLf
                        Else
                            nome = Replace(line_value, "/", "")
                            nome = Replace(nome, Chr(39), "")
                            Dim match As Match
                            match = Regex.Match(xref_id, "\d+")
                            pessoa = match.Value
                        End If

                    Case "SEX"

                        sex = line_value

                    Case "DATE"

                        If header Then
                            Form1.RichTextBox1.Text = Form1.RichTextBox1.Text & "Date: " & line_value & vbCrLf
                        Else

                            Select Case (evento)
                                Case "BIRT"
                                    nasc = line_value
                                Case "DEAT"
                                    mort = line_value
                                Case "CHR"
                                    batismo = line_value
                                Case "BURI"
                                    sepulta = line_value
                            End Select

                        End If

                    Case "PLAC"
                        Select Case (evento)
                            Case "BIRT"
                                bloc = Replace(line_value, Chr(39), "")
                            Case "DEAT"
                                dloc = Replace(line_value, Chr(39), "")
                            Case "CHR"
                                cloc = Replace(line_value, Chr(39), "")
                            Case "BURI"
                                sloc = Replace(line_value, Chr(39), "")
                        End Select

                    Case "CHAN"
                        line_value = ""
                        tag = ""
                        evento = ""
                        'pessoa = pessoa + 1
                        Form1.ToolStripStatusLabel1.Text = nome

                        Insere()
                        nome = ""
                        sex = ""
                        nasc = ""
                        mort = ""
                        batismo = ""
                        sepulta = ""
                        local = ""
                        evento = ""
                        bloc = ""
                        dloc = ""
                        cloc = ""
                        sloc = ""

                    Case "FAMC"
                        'MsgBox("FAMC :" & line_value)

                    Case "FAMS"
                        'MsgBox("FAMS :" & line_value)

                End Select



            Case EnumToken.TK_UM
                level = tk

            Case EnumToken.TK_DOIS

                Select Case (level)
                    Case EnumLevel.LEVEL_0
                        Select Case (tk)
                            Case "HEAD"
                                header = True
                                tag = tk
                            Case "TRLR"
                                tag = tk
                            Case Else
                                xref_id = tk
                        End Select

                    Case EnumLevel.LEVEL_1  'token2 level1
                        tag = tk
                        Select Case (tag)
                            Case "NAME"
                                'NAME


                            Case "SEX"
                            'SEX
                            Case "BIRT", "CHR", "DEAT", "BURI", "EVEN"
                                evento = tag
                                'BIRT
                                'CHR
                                'DEAT
                                'BURI

                            Case "FAMS", "FAMC", "SOUR", "CHAN"
                                'evento = tag

                                'FAMS
                                'FAMC
                                'SOUR
                                'CHAN
                            Case "CHAR"

                        End Select

                    Case EnumLevel.LEVEL_2  'token2 level2
                        tag = tk
                        Select Case (tag)
                            Case "DATE", "PLAC"
                                'NAME
                                'MsgBox(evento & tag)
                                'DATE
                                'PLACE

                                'OBJE
                            Case "TYPE"
                        End Select
                        'OBJE

                    Case EnumLevel.LEVEL_3  'token2 level3 
                        'FORM
                        'FILE
                End Select   '463

            Case EnumToken.TK_TRES
                Select Case (level)
                    Case EnumLevel.LEVEL_0
                        'INDI
                        'MsgBox(xref_id)
                        If tk = "INDI" Then
                            header = False
                            tag = ""
                        End If

                    Case EnumLevel.LEVEL_1

                        line_value = tk

                    Case EnumLevel.LEVEL_2
                        'MsgBox(evento & tag & " = " & tk)
                        'mês (nascimento, batismo, casamento,morte)
                        'lugar (nascimento, batismo, casamento,morte)

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

    Sub PopulateDB()
        'startFS2(My.Settings.defaultInputFile)
        Dim l, c, p As String
        Dim i As Integer

        p = ""
        i = 0
        pessoa = 0

        For Each l In fileReader

            For Each c In l
                If (c <> " ") Then
                    p = p & c
                Else
                    i = i + 1
                    token2(i, p)
                    p = ""
                End If

            Next
            i = i + 1
            token2(i, p)
            p = ""
            i = 0
            token2(-1, "")
        Next
        'End While
        'StopFS()

    End Sub

    Sub StartFS(InputFileName As String)
        fs = CreateObject("Scripting.FileSystemObject")
        a = fs.OpenTextFile(InputFileName, kForReading, False, kTristateFalse)
    End Sub

    Sub startFS2(InputFileName As String)

        Dim enc

        If Form1.rbascii.Checked Then
            inpEnc = "ascii"
            outEnc = "ascii"
            enc = System.Text.Encoding.UTF8

        Else
            inpEnc = "utf-8"
            outEnc = "utf-8"
            enc = System.Text.Encoding.UTF8

        End If
        fileReader = File.ReadAllLines(InputFileName, enc)
    End Sub

    Sub stopFS2()
        fileReader = Nothing
    End Sub
End Module
