Imports System.Configuration
Imports System.Data
Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Reflection
Imports System.Reflection.Metadata
Imports System.Runtime.Intrinsics
Imports System.Security.Cryptography
Imports System.Windows.Forms.LinkLabel
Imports System.Windows.Forms.VisualStyles
Imports System.Xml
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Form1
    Dim connectionString As String
    Dim conn, rs, r
    Dim connected = False
    Dim strQuery As String
    Public loadingxml = False, populatingDGV As Boolean



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not connected Then
            MsgBox("Conecte-se para iniciar sessão!")
        Else

            Dim qry, parametro
            qry = ""

            Select Case (sender.ToString)
                Case "igual a"
                    parametro = InputBox("Digite o ID: ", sender.ToString)
                    If IsNumeric(parametro) Then
                        Dim id = CInt(parametro)
                        qry = "SELECT * FROM Membros WHERE ID = " & id & " ;"
                    End If

                Case "menor que"
                    parametro = InputBox("Digite o ID: ", sender.ToString)
                    If IsNumeric(parametro) Then
                        Dim id = CInt(parametro)
                        qry = "SELECT * FROM Membros WHERE ID <= " & id & " ;"
                    End If

                Case "maior que"
                    parametro = InputBox("Digite o ID: ", sender.ToString)
                    If IsNumeric(parametro) Then
                        Dim id = CInt(parametro)
                        qry = "SELECT * FROM Membros WHERE ID >= " & id & " ;"
                    End If

                Case "Nome Parcial"
                    parametro = InputBox("Digite o Nome: ", sender.ToString)
                    If (parametro <> "" And (Not IsNumeric(parametro))) Then
                        Dim enparam = Utils.EscapeSql(parametro)
                        qry = "SELECT ID,Nome,Nascimento,LocalNascimento,Falecimento,LocalFalecimento,Batismo,LocalBatismo,Sepultamento,LocalSepultamento FROM Membros GROUP BY ID,Nome,Nascimento,LocalNascimento,Falecimento,LocalFalecimento,Batismo,LocalBatismo,Sepultamento,LocalSepultamento HAVING Nome LIKE ('%" & enparam & "%');"
                    End If

                Case "Nome Exato"
                    parametro = InputBox("Digite o Nome: ", sender.ToString)
                    If (parametro <> "" And (Not IsNumeric(parametro))) Then
                        Dim enparam = Utils.EscapeSql(parametro)
                        qry = "SELECT * FROM Membros WHERE Nome = '" & enparam & "' ;"
                    End If

                Case "Pesquisa Parcial no Campo Atual"
                    parametro = InputBox("Digite o valor: ", sender.ToString)
                    If (parametro <> "") Then
                        Dim enparam = Utils.EscapeSql(parametro)
                        Dim colIdx = DataGridView1.CurrentCell.ColumnIndex
                        If colIdx >= 0 And colIdx <= UBound(Campos) Then
                            Dim colName = Campos(colIdx)
                            qry = "SELECT ID,Nome,Nascimento,LocalNascimento,Falecimento,LocalFalecimento,Batismo,LocalBatismo,Sepultamento,LocalSepultamento FROM Membros GROUP BY ID,Nome,Nascimento,LocalNascimento,Falecimento,LocalFalecimento,Batismo,LocalBatismo,Sepultamento,LocalSepultamento HAVING " & colName & " LIKE ('%" & enparam & "%');"
                        Else
                            qry = ""
                        End If
                    End If

                Case "Pesquisa Exata no Campo Atual"

                    parametro = InputBox("Digite o valor: ", sender.ToString)
                    If (parametro <> "") Then
                        Dim enparam = Utils.EscapeSql(parametro)
                        Dim colIdx2 = DataGridView1.CurrentCell.ColumnIndex
                        If colIdx2 >= 0 And colIdx2 <= UBound(Campos) Then
                            Dim colName2 = Campos(colIdx2)
                            qry = "SELECT * FROM Membros WHERE " & colName2 & " = '" & enparam & "' ;"
                        Else
                            qry = ""
                        End If
                    End If

                Case Else
                    qry = txtQuery.Text
            End Select

            PopulateDGV(DataGridView1, qry)
            DirectCast(MenuStrip1.Items(3), ToolStripMenuItem).DropDownItems(3).Enabled = True
            DirectCast(MenuStrip1.Items(3), ToolStripMenuItem).DropDownItems(4).Enabled = True
        End If
    End Sub

    Sub PopulateDGV(data_grid As DataGridView, qry As String)
        If (qry <> "") Then
            populatingDGV = True
            Try
                rs = conn.execute(qry)

                Static Dim i

                With data_grid
                    .Rows.Clear()
                    .Columns.Clear()

                    If Not (rs Is Nothing) Then
                        For Each r In rs.fields
                            .Columns.Add(r.name, r.name)
                        Next

                        i = 0

                        While Not rs.eof()
                            .Rows.Add()
                            For Each r In rs.fields
                                .Rows(i).Cells(r.name).Value = r.value
                            Next
                            i = i + 1
                            rs.movenext()
                        End While
                        If .Columns.Count > 1 Then
                            .Columns(1).Width = 400
                        End If
                        .Refresh()
                    Else
                        ToolStripStatusLabel1.Text = "Consulta retornou vazio ou falhou."
                    End If

                End With
            Catch ex As Exception
                ToolStripStatusLabel1.Text = "Erro ao executar query: " & ex.Message
                Debug.WriteLine("PopulateDGV error: " & ex.Message)
            End Try
            populatingDGV = False
        End If
    End Sub

    Sub ShowStatus()
        ToolStripStatusLabel1.Text = "Pronto: Banco de Dados: " & My.Settings.DBName
        ToolStripStatusLabel2.Text = "Arquivo: " & My.Settings.defaultInputFile
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "genealogia " & Application.ProductVersion.ToString
        Me.RadioButtonXML.Checked = My.Settings.radiobuttonxmlv
        If Not RadioButtonXML.Checked Then
            RadioButtonTXT.Checked = True
        End If

        Me.rbutf8.Checked = My.Settings.radiobuttonutf8
        If Not rbutf8.Checked Then
            rbascii.Checked = True
        End If

        Campos(0) = "ID"
        Campos(1) = "Nome"
        Campos(2) = "Nascimento"
        Campos(3) = "LocalNascimento"
        Campos(4) = "Falecimento"
        Campos(5) = "LocalFalecimento"
        Campos(6) = "Batismo"
        Campos(7) = "LocalBatismo"
        Campos(8) = "Sepultamento"
        Campos(9) = "LocalSepultamento"

        ShowStatus()


    End Sub

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click
        'MsgBox(My.Settings.defaultInputFile)
        Dim ofd As New OpenFileDialog()

        ' Configurações opcionais
        ofd.InitialDirectory = My.Settings.gedcomFolder
        ofd.Filter = "Arquivos GEDCOM|*.ged"
        ofd.Title = "Selecione um arquivo"

        ' Mostra a janela
        If ofd.ShowDialog() = DialogResult.OK Then
            ' Se o usuário clicou em "Abrir"
            If Not String.IsNullOrEmpty(ofd.FileName) Then
                My.Settings.defaultInputFile = ofd.FileName
                StartFS(ofd.FileName)
            End If
        End If
        My.Settings.defaultInputFile = ofd.FileName
        ShowStatus()
    End Sub

    Private Sub CriarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CriarToolStripMenuItem.Click
        Dim ib
        'Verifica se banco de dados existe
        Inicializa()
        If fs.FileExists(My.Settings.DBName) Then
            DataBaseExists = True
            ib = InputBox("Banco de Dados já Existe!!!.Dejesa recriá-lo?", "genealogia", "yes")
            If ib = "yes" Then
                Dim file2del = CreateObject("Scripting.FileSystemObject")
                Try
                    file2del.deletefile(DataBaseName, True)
                    createDataBase(DataBaseName)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                file2del = Nothing
            End If
        Else
            DataBaseExists = False
            createDataBase(DataBaseName)
        End If

    End Sub

    Private Sub ExtrairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtrairToolStripMenuItem.Click
        processaXML()
    End Sub

    Private Sub FecharToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FecharToolStripMenuItem.Click
        conn.close()
        If conn.state = 0 Then
            'Dim myimg = New Bitmap("C:\genealogia - Copia (7)\My Project\off64.png")
            'PictureBox1.Image = myimg
            PictureBox1.Image = My.Resources.off64
            ShowStatus()
        End If
        rs = Nothing
        conn = Nothing
        connected = False
    End Sub

    Private Sub CarregarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CarregarToolStripMenuItem.Click

        txtQuery.Text = My.Settings.xmlFolder
        Dim ofd As New OpenFileDialog()

        ' Configurações opcionais
        ofd.InitialDirectory = My.Settings.xmlFolder
        ofd.Filter = "Arquivos XML|*.xml"
        ofd.Title = "Selecione um arquivo"

        ' Mostra a janela
        If ofd.ShowDialog() = DialogResult.OK Then
            ' Se o usuário clicou em "Abrir"
            If Not String.IsNullOrEmpty(ofd.FileName) Then
                loadingxml = True
                LoadXML(ofd.FileName)
            Else
                loadingxml = False

            End If

            '        OpenFileDialog1.FileName = "*.xml"
            '               If My.Settings.xmlFolder <> "" Then
            '              OpenFileDialog1.InitialDirectory = My.Settings.xmlFolder
            '         End If
            '            If OpenFileDialog1.ShowDialog() = 1 Then
            'My.Settings.xmlFolder = OpenFileDialog1.InitialDirectory
            'My.Settings.Save()
            '           loadingxml = True
            '          LoadXML(OpenFileDialog1.FileName)
            '         Else
            '        loadingxml = False
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RichTextBox2.Text = xpath(txtQuery.Text)
    End Sub

    Private Sub ConfigurarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurarToolStripMenuItem.Click
        Dim ib
        ib = InputBox("Banco de Dados: ", "BANCO DE DADOS", My.Settings.DBName)
        If Trim(ib) <> "" Then
            My.Settings.DBName = ib
            My.Settings.Save()
            Inicializa()
        End If
        ShowStatus()
    End Sub

    Private Sub RadioButtonXML_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonXML.CheckedChanged
        With Events
            If RadioButtonXML.Checked Then
                My.Settings.radiobuttonxmlv = True
            Else
                My.Settings.radiobuttonxmlv = False
            End If
        End With
        My.Settings.Save()
    End Sub



    Private Sub ConectarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConectarToolStripMenuItem.Click
        connectionString = My.Settings.connectionString & My.Settings.DBName
        conn = CreateObject("adodb.connection")
        rs = CreateObject("adodb.recordset")
        conn.Open(connectionString)
        If conn.state = 1 Then
            'Dim myimg = New Bitmap("C:\genealogia - Copia (7)\My Project\on64.png")
            'PictureBox1.Image = myimg
            PictureBox1.Image = My.Resources.on64
            connected = True
            ShowStatus()
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Not connected Then
            ConectarToolStripMenuItem_Click(PictureBox1, e)
        Else
            FecharToolStripMenuItem_Click(PictureBox1, e)
        End If
    End Sub

    Private Sub RadioButtonTXT_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTXT.CheckedChanged
        With Events
            If RadioButtonTXT.Checked Then
                My.Settings.radiobuttonxmlv = False
            Else
                My.Settings.radiobuttonxmlv = True
            End If
        End With
        My.Settings.Save()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        XmlSchemaCreateExample("teste")
    End Sub

    Private Sub CToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CToolStripMenuItem.Click

    End Sub

    Private Sub ExibirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExibirToolStripMenuItem.Click
    End Sub

    Private Sub AbrirToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem1.Click
        OpenFileDialog1.FileName = "*.accdb"

        If OpenFileDialog1.ShowDialog() = 1 Then
            My.Settings.DBName = OpenFileDialog1.FileName
        End If
        ShowStatus()
    End Sub

    Private Sub IDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IDToolStripMenuItem.Click
        Button1_Click(IDToolStripMenuItem, e)
    End Sub

    Private Sub NomeParcialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NomeParcialToolStripMenuItem.Click
        Button1_Click(NomeParcialToolStripMenuItem, e)
    End Sub

    Private Sub NomeExatoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NomeExatoToolStripMenuItem.Click
        Button1_Click(NomeExatoToolStripMenuItem, e)
    End Sub

    Private Sub PesquisaParcialNoCampoAtualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PesquisaParcialNoCampoAtualToolStripMenuItem.Click
        Button1_Click(PesquisaParcialNoCampoAtualToolStripMenuItem, e)
    End Sub

    Private Sub PesquisaExataNoCampoAtualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PesquisaExataNoCampoAtualToolStripMenuItem.Click
        Button1_Click(PesquisaExataNoCampoAtualToolStripMenuItem, e)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        If Not loadingxml Then
            If (e.ColumnIndex = 1) Then
                Dim id = DataGridView1.CurrentRow.Cells(0).Value
                PopularTvw(id)
            End If
        Else
            loadingxml = False
        End If
    End Sub
    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Dim qry As String
        Dim en = Utils.EscapeSql(e.Node.Text)
        qry = "SELECT * FROM Membros WHERE Nome = '" & en & "' ;"
        PopulateDGV(DataGridView1, qry)
        TabControl1.SelectedTab = TabPage1

    End Sub
    Sub PopularTvw(id As Integer)

        TreeView1.Nodes.Clear()
        Dim ind As New Individuo(id)
        ind.SetIndividuo()

        Dim tindividuo As TreeNode = TreeView1.Nodes.Add(ind.nome)

        If (ind.iddoPai <> 0) Then
            Dim indPai = New Individuo(ind.iddoPai)
            indPai.SetIndividuo()
            Dim tPai As TreeNode = tindividuo.Nodes.Insert(0, indPai.nome)
            tPai.Nodes.Insert(0, indPai.nomedoPai)
            tPai.Nodes.Insert(1, indPai.nomedaMae)

        End If

        If (ind.iddaMae <> 0) Then
            Dim indMae = New Individuo(ind.iddaMae)
            indMae.SetIndividuo()
            Dim tMae As TreeNode = tindividuo.Nodes.Insert(1, indMae.nome)
            tMae.Nodes.Insert(0, indMae.nomedoPai)
            tMae.Nodes.Insert(1, indMae.nomedaMae)

        End If



        If (ind.idConjuge <> 0) Then
            Dim conj As New Individuo(ind.idConjuge)
            conj.SetIndividuo()

            Dim tconjuge As TreeNode = TreeView1.Nodes.Add(ind.conjuge)

            If (conj.iddoPai <> 0) Then
                Dim indPaiConj = New Individuo(conj.iddoPai)
                indPaiConj.SetIndividuo()
                Dim tPaiConj As TreeNode = tconjuge.Nodes.Insert(0, indPaiConj.nome)
                tPaiConj.Nodes.Insert(0, indPaiConj.nomedoPai)
                tPaiConj.Nodes.Insert(1, indPaiConj.nomedaMae)

            End If

            If (conj.iddaMae <> 0) Then
                Dim indMaeConj = New Individuo(conj.iddaMae)
                indMaeConj.SetIndividuo()
                Dim tMaeConj As TreeNode = tconjuge.Nodes.Insert(1, indMaeConj.nome)
                tMaeConj.Nodes.Insert(0, indMaeConj.nomedoPai)
                tMaeConj.Nodes.Insert(1, indMaeConj.nomedaMae)

            End If
        End If

        If IsNothing(ind.casamento(0, 0)) Then
        Else
            Dim casamento1 As TreeNode = TreeView1.Nodes.Add("Casamento")
            casamento1.Nodes.Insert(0, ind.casamento(0, 0))
            casamento1.Nodes.Insert(1, ind.casamento(0, 1))
        End If

        Dim tfilhos As TreeNode = TreeView1.Nodes.Add("filhos")
        Dim filho As String
        Dim x As Short
        Dim fim As Boolean

        x = 1
        fim = False
        While (Not fim)
            filho = ind.getChild(ind.ParentFamily, x)
            If (filho <> "") Then
                tfilhos.Nodes.Insert(x, filho)
                x = x + 1
            Else
                fim = True
            End If
        End While

        If (ind.idConjuge2 <> 0) Then
            Dim conj2 As New Individuo(ind.idConjuge2)
            conj2.SetIndividuo()

            Dim tconjuge2 As TreeNode = TreeView1.Nodes.Add(ind.conjuge2)

            If (conj2.iddoPai <> 0) Then
                Dim indPaiConj2 = New Individuo(conj2.iddoPai)
                indPaiConj2.SetIndividuo()
                Dim tPaiConj2 As TreeNode = tconjuge2.Nodes.Insert(0, indPaiConj2.nome)
                tPaiConj2.Nodes.Insert(0, indPaiConj2.nomedoPai)
                tPaiConj2.Nodes.Insert(1, indPaiConj2.nomedaMae)

            End If

            If (conj2.iddaMae <> 0) Then
                Dim indMaeConj2 = New Individuo(conj2.iddaMae)
                indMaeConj2.SetIndividuo()
                Dim tMaeConj2 As TreeNode = tconjuge2.Nodes.Insert(1, indMaeConj2.nome)
                tMaeConj2.Nodes.Insert(0, indMaeConj2.nomedoPai)
                tMaeConj2.Nodes.Insert(1, indMaeConj2.nomedaMae)

            End If

            If IsNothing(ind.casamento(1, 0)) Then
            Else
                Dim casamento2 As TreeNode = TreeView1.Nodes.Add("Casamento")
                casamento2.Nodes.Insert(0, ind.casamento(1, 0))
                casamento2.Nodes.Insert(1, ind.casamento(1, 1))
            End If


            Dim tfilhos2 As TreeNode = TreeView1.Nodes.Add("filhos")
            x = 1
            fim = False
            While (Not fim)
                filho = ind.getChild(ind.ParentFamily2, x)
                If (filho <> "") Then
                    tfilhos2.Nodes.Insert(x, filho)
                    x = x + 1
                Else
                    fim = True
                End If
            End While
        End If

        If (ind.idConjuge3 <> 0) Then
            Dim conj3 As New Individuo(ind.idConjuge3)
            conj3.SetIndividuo()

            Dim tconjuge3 As TreeNode = TreeView1.Nodes.Add(ind.conjuge3)

            If (conj3.iddoPai <> 0) Then
                Dim indPaiConj3 = New Individuo(conj3.iddoPai)
                indPaiConj3.SetIndividuo()
                Dim tPaiConj3 As TreeNode = tconjuge3.Nodes.Insert(0, indPaiConj3.nome)
                tPaiConj3.Nodes.Insert(0, indPaiConj3.nomedoPai)
                tPaiConj3.Nodes.Insert(1, indPaiConj3.nomedaMae)

            End If

            If (conj3.iddaMae <> 0) Then
                Dim indMaeConj3 = New Individuo(conj3.iddaMae)
                indMaeConj3.SetIndividuo()
                Dim tMaeConj3 As TreeNode = tconjuge3.Nodes.Insert(1, indMaeConj3.nome)
                tMaeConj3.Nodes.Insert(0, indMaeConj3.nomedoPai)
                tMaeConj3.Nodes.Insert(1, indMaeConj3.nomedaMae)

            End If

            If IsNothing(ind.casamento(2, 0)) Then
            Else
                Dim casamento3 As TreeNode = TreeView1.Nodes.Add("Casamento")
                casamento3.Nodes.Insert(0, ind.casamento(2, 0))
                casamento3.Nodes.Insert(1, ind.casamento(2, 1))
            End If


            Dim tfilhos3 As TreeNode = TreeView1.Nodes.Add("filhos")
            x = 1
            fim = False
            While (Not fim)
                filho = ind.getChild(ind.ParentFamily3, x)
                If (filho <> "") Then
                    tfilhos3.Nodes.Insert(x, filho)
                    x = x + 1
                Else
                    fim = True
                End If
            End While
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        startFS2(My.Settings.defaultInputFile)
        Dim lin As String
        For Each lin In fileReader
            MsgBox(lin)
        Next
    End Sub

    '    Private Sub IncluirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncluirToolStripMenuItem.Click
    '   End Sub

    Private Sub ExibirToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExibirToolStripMenuItem1.Click
        If Not connected Then
            MsgBox("Conecte-se para iniciar sessão!")
        Else
            Dim qry = "SELECT * FROM Fontes"
            PopulateDGV(DataGridView2, qry)
            TabControl1.SelectedTab = TabPage2

        End If
    End Sub

    Private Sub PesquisarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PesquisarToolStripMenuItem1.Click
        If Not connected Then
            MsgBox("Conecte-se para iniciar sessão!")
        Else
            Dim parametro = InputBox("Digite o valor a ser pesquisado: ", sender.ToString)
            If (parametro <> "") Then
                Dim enparam = Utils.EscapeSql(parametro)
                Dim qry = "SELECT ID,Local,IID FROM Fontes GROUP BY ID, Local,IID HAVING Local Like ('%" & enparam & "%');"
                PopulateDGV(DataGridView2, qry)
            End If
            TabControl1.SelectedTab = TabPage2

        End If
    End Sub

    Private Sub DataGridView2_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentDoubleClick
        Static done = False

        If Not done Then
            Dim p As New ProcessStartInfo
            p.UseShellExecute = True
            p.FileName = DataGridView2.CurrentRow.Cells(1).Value
            System.Diagnostics.Process.Start(p)
            done = True
        Else
            done = False
        End If
    End Sub
    Private Sub Datagridview2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If Not populatingDGV Then
            Dim field = DataGridView2.Columns(DataGridView2.CurrentCell.ColumnIndex).HeaderText
            Dim value = Utils.EscapeSql(If(DataGridView2.CurrentCell.Value, ""))
            Dim id = CInt(DataGridView2.CurrentRow.Cells(0).Value)
            Try
                conn.execute("UPDATE Fontes SET [" & field & "] = '" & value & "' WHERE ID = " & id)
            Catch ex As Exception
                Debug.WriteLine("Datagridview2 update error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Datagridview1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If Not populatingDGV Then
            Dim colIdx = DataGridView1.CurrentCell.ColumnIndex
            If colIdx >= 0 And colIdx <= UBound(Campos) Then
                Dim colName = Campos(colIdx)
                Dim value = Utils.EscapeSql(If(DataGridView1.CurrentCell.Value, ""))
                Dim id = CInt(DataGridView1.CurrentRow.Cells(0).Value)
                Dim qry = "UPDATE Membros SET [" & colName & "] = '" & value & "' WHERE ID = " & id
                Try
                    conn.execute(qry)
                Catch ex As Exception
                    Debug.WriteLine("Datagridview1 update error: " & ex.Message)
                End Try
            End If
        End If

    End Sub
    Private Sub IgualAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgualAToolStripMenuItem.Click
        Button1_Click(IgualAToolStripMenuItem, e)
    End Sub

    Private Sub MenorQueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenorQueToolStripMenuItem.Click
        Button1_Click(MenorQueToolStripMenuItem, e)
    End Sub

    Private Sub MaiorQueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaiorQueToolStripMenuItem.Click
        Button1_Click(MaiorQueToolStripMenuItem, e)
    End Sub

    Private Sub FamilySearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FamilySearchToolStripMenuItem.Click
        Dim person As New Individuo(DataGridView1.CurrentRow.Cells(0).Value)
        person.SetIndividuo()
        Static done = False

        If Not done Then
            Dim p As New ProcessStartInfo
            p.UseShellExecute = True
            p.FileName = "https://www.familysearch.org/tree/person/sources/" & person.fsftid
            System.Diagnostics.Process.Start(p)
            done = True
        Else
            done = False
        End If
        person = Nothing
    End Sub

    Private Sub rbascii_CheckedChanged(sender As Object, e As EventArgs) Handles rbascii.CheckedChanged
        With Events
            If rbutf8.Checked Then
                My.Settings.radiobuttonutf8 = True
            Else
                My.Settings.radiobuttonutf8 = False
            End If
        End With
        My.Settings.Save()

    End Sub

    Private Sub GoogleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoogleToolStripMenuItem.Click
        Dim person As New Individuo(DataGridView1.CurrentRow.Cells(0).Value)
        person.SetIndividuo()
        Static done = False

        If Not done Then
            Dim p As New ProcessStartInfo
            p.UseShellExecute = True
            p.FileName = "https://www.google.com.br/search?q=" & Chr(34) & person.nome & Chr(34)
            System.Diagnostics.Process.Start(p)
            done = True
        Else
            done = False
        End If
        person = Nothing

    End Sub

    Private Sub AtualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtualizarToolStripMenuItem.Click
        If Not connected Then
            Inicializa()
            'Dim tbl As New Table
            Dim cat = CreateObject("ADOX.Catalog")

            ' Open the Catalog.  
            cat.ActiveConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & My.Settings.DBName & ";"
            Dim db = CreateObject("DAO.DBEngine.120")
            IO.conn = db.OpenDatabase(My.Settings.DBName)

            cat.Tables.Delete("Membros")
            ' Aqui você pode adicionar tabelas, consultas, etc. ao banco de dados.
            IO.conn.Execute("CREATE TABLE Membros")
            For Each field In Campos
                IO.conn.Execute("ALTER TABLE Membros ADD COLUMN " & field & ";")
            Next
            db.Begintrans
            startFS2(My.Settings.defaultInputFile)
            PopulateDB()
            stopFS2()
            db.Committrans
            IO.conn.close
            IO.conn = Nothing
            db = Nothing
            cat = Nothing
        Else
            MsgBox("Descontecte-se para atualizar.")
        End If
    End Sub

    Private Sub ProjResgateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProjResgateToolStripMenuItem.Click
        Dim person As New Individuo(DataGridView1.CurrentRow.Cells(0).Value)
        person.SetIndividuo()
        Static done = False

        If Not done Then
            Dim p As New ProcessStartInfo
            p.UseShellExecute = True
            p.FileName = "https://resgate.bn.gov.br/docreader/DocReader.aspx?bib=011_MG&pesq=" & Chr(34) & person.nome & Chr(34)
            System.Diagnostics.Process.Start(p)
            done = True
        Else
            done = False
        End If
        person = Nothing
    End Sub

    Private Sub PastaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PastaToolStripMenuItem.Click
        If Not connected Then
            MsgBox("Conecte-se para iniciar sessão!")
        Else

            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim i = 0
                Try
                    conn.Execute("CREATE TABLE Fontes")
                    conn.Execute("ALTER TABLE Fontes ADD COLUMN ID Number;")
                    conn.Execute("ALTER TABLE Fontes ADD COLUMN Local Text")
                    conn.Execute("ALTER TABLE Fontes ADD COLUMN IID Text")

                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(FolderBrowserDialog1.SelectedPath)

                        Dim nomeArquivo As String = "fontes\" & Path.GetFileName(foundFile)

                        i = i + 1
                        conn.Execute("INSERT INTO Fontes(ID,Local,IID) VALUES ('" & i & "','" & nomeArquivo & "','')")
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            End If
        End If
    End Sub

    Private Sub ArquivoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ArquivoToolStripMenuItem1.Click
        If Not connected Then
            MsgBox("Conecte-se para iniciar sessão!")
        Else

            Dim ofd As New OpenFileDialog()

            ' Configurações opcionais
            'ofd.Filter = "Arquivos de texto|*.txt|Todos os arquivos|*.*"
            ofd.Title = "Selecione um arquivo"

            ' Mostra a janela
            If ofd.ShowDialog() = DialogResult.OK Then
                ' Se o usuário clicou em "Abrir"
                If Not String.IsNullOrEmpty(ofd.FileName) Then
                    ' Define a consulta SQL ordenando pelo ID de forma decrescente
                    rs = conn.execute("SELECT * FROM Fontes;")

                    Static Dim i

                    With DataGridView2
                        .Rows.Clear()
                        .Columns.Clear()

                        For Each r In rs.fields
                            .Columns.Add(r.name, r.name)
                        Next

                        i = 0

                        While Not rs.eof()
                            .Rows.Add()
                            For Each r In rs.fields
                                .Rows(i).Cells(r.name).Value = r.value
                            Next
                            i = i + 1
                            rs.movenext()
                        End While

                        .Refresh()
                    End With
                    i = i + 1
                    conn.Execute("INSERT INTO Fontes(ID,Local,IID) VALUES ('" & i & "','" & ofd.FileName & "','')")
                    DataGridView2.Refresh()
                End If
            Else
                ' Se o usuário cancelou
                MessageBox.Show("Nenhum arquivo foi selecionado.")
            End If

        End If
    End Sub


End Class

