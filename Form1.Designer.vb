<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Button1 = New Button()
        ImageList1 = New ImageList(components)
        DataGridView1 = New DataGridView()
        txtQuery = New TextBox()
        MenuStrip1 = New MenuStrip()
        ArquivoToolStripMenuItem = New ToolStripMenuItem()
        AbrirToolStripMenuItem = New ToolStripMenuItem()
        BancoDeDadosToolStripMenuItem = New ToolStripMenuItem()
        CriarToolStripMenuItem = New ToolStripMenuItem()
        AbrirToolStripMenuItem1 = New ToolStripMenuItem()
        AtualizarToolStripMenuItem = New ToolStripMenuItem()
        ConexãoToolStripMenuItem = New ToolStripMenuItem()
        ConfigurarToolStripMenuItem = New ToolStripMenuItem()
        ConectarToolStripMenuItem = New ToolStripMenuItem()
        FecharToolStripMenuItem = New ToolStripMenuItem()
        PesquisarToolStripMenuItem = New ToolStripMenuItem()
        IDToolStripMenuItem = New ToolStripMenuItem()
        IgualAToolStripMenuItem = New ToolStripMenuItem()
        MenorQueToolStripMenuItem = New ToolStripMenuItem()
        MaiorQueToolStripMenuItem = New ToolStripMenuItem()
        EntreToolStripMenuItem = New ToolStripMenuItem()
        NomeParcialToolStripMenuItem = New ToolStripMenuItem()
        NomeExatoToolStripMenuItem = New ToolStripMenuItem()
        PesquisaParcialNoCampoAtualToolStripMenuItem = New ToolStripMenuItem()
        PesquisaExataNoCampoAtualToolStripMenuItem = New ToolStripMenuItem()
        FamilySearchToolStripMenuItem = New ToolStripMenuItem()
        GoogleToolStripMenuItem = New ToolStripMenuItem()
        ProjResgateToolStripMenuItem = New ToolStripMenuItem()
        XMLToolStripMenuItem = New ToolStripMenuItem()
        ExtrairToolStripMenuItem = New ToolStripMenuItem()
        CarregarToolStripMenuItem = New ToolStripMenuItem()
        FontesToolStripMenuItem = New ToolStripMenuItem()
        IncluirToolStripMenuItem = New ToolStripMenuItem()
        ExibirToolStripMenuItem1 = New ToolStripMenuItem()
        PesquisarToolStripMenuItem1 = New ToolStripMenuItem()
        CToolStripMenuItem = New ToolStripMenuItem()
        ExibirToolStripMenuItem = New ToolStripMenuItem()
        OpenFileDialog1 = New OpenFileDialog()
        RichTextBox1 = New RichTextBox()
        Button2 = New Button()
        RadioButtonXML = New RadioButton()
        RadioButtonTXT = New RadioButton()
        RichTextBox2 = New RichTextBox()
        PictureBox1 = New PictureBox()
        Button3 = New Button()
        Button4 = New Button()
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        TreeView1 = New TreeView()
        GroupBox1 = New GroupBox()
        OpenFileDialog2 = New OpenFileDialog()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        DataGridView2 = New DataGridView()
        rbascii = New RadioButton()
        rbutf8 = New RadioButton()
        GroupBox2 = New GroupBox()
        GroupBox3 = New GroupBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.ImageList = ImageList1
        Button1.Location = New Point(30, 647)
        Button1.Name = "Button1"
        Button1.Size = New Size(97, 37)
        Button1.TabIndex = 0
        Button1.Text = "Pesquisar"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "db.jpg")
        ImageList1.Images.SetKeyName(1, "off64.png")
        ImageList1.Images.SetKeyName(2, "on64.png")
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(3, 12)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(1335, 272)
        DataGridView1.TabIndex = 1
        ' 
        ' txtQuery
        ' 
        txtQuery.Location = New Point(26, 67)
        txtQuery.Name = "txtQuery"
        txtQuery.Size = New Size(769, 27)
        txtQuery.TabIndex = 2
        txtQuery.Text = "SELECT * FROM Membros WHERE ID<5;"
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {ArquivoToolStripMenuItem, BancoDeDadosToolStripMenuItem, ConexãoToolStripMenuItem, PesquisarToolStripMenuItem, XMLToolStripMenuItem, FontesToolStripMenuItem, CToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(6, 3, 0, 3)
        MenuStrip1.Size = New Size(1924, 30)
        MenuStrip1.TabIndex = 3
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' ArquivoToolStripMenuItem
        ' 
        ArquivoToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AbrirToolStripMenuItem})
        ArquivoToolStripMenuItem.Name = "ArquivoToolStripMenuItem"
        ArquivoToolStripMenuItem.Size = New Size(75, 24)
        ArquivoToolStripMenuItem.Text = "Arquivo"
        ' 
        ' AbrirToolStripMenuItem
        ' 
        AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        AbrirToolStripMenuItem.Size = New Size(125, 26)
        AbrirToolStripMenuItem.Text = "Abrir"
        ' 
        ' BancoDeDadosToolStripMenuItem
        ' 
        BancoDeDadosToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CriarToolStripMenuItem, AbrirToolStripMenuItem1, AtualizarToolStripMenuItem})
        BancoDeDadosToolStripMenuItem.Name = "BancoDeDadosToolStripMenuItem"
        BancoDeDadosToolStripMenuItem.Size = New Size(132, 24)
        BancoDeDadosToolStripMenuItem.Text = "Banco de Dados"
        ' 
        ' CriarToolStripMenuItem
        ' 
        CriarToolStripMenuItem.Name = "CriarToolStripMenuItem"
        CriarToolStripMenuItem.Size = New Size(151, 26)
        CriarToolStripMenuItem.Text = "Criar"
        ' 
        ' AbrirToolStripMenuItem1
        ' 
        AbrirToolStripMenuItem1.Name = "AbrirToolStripMenuItem1"
        AbrirToolStripMenuItem1.Size = New Size(151, 26)
        AbrirToolStripMenuItem1.Text = "Abrir"
        ' 
        ' AtualizarToolStripMenuItem
        ' 
        AtualizarToolStripMenuItem.Name = "AtualizarToolStripMenuItem"
        AtualizarToolStripMenuItem.Size = New Size(151, 26)
        AtualizarToolStripMenuItem.Text = "Atualizar"
        ' 
        ' ConexãoToolStripMenuItem
        ' 
        ConexãoToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ConfigurarToolStripMenuItem, ConectarToolStripMenuItem, FecharToolStripMenuItem})
        ConexãoToolStripMenuItem.Name = "ConexãoToolStripMenuItem"
        ConexãoToolStripMenuItem.Size = New Size(81, 24)
        ConexãoToolStripMenuItem.Text = "Conexão"
        ' 
        ' ConfigurarToolStripMenuItem
        ' 
        ConfigurarToolStripMenuItem.Name = "ConfigurarToolStripMenuItem"
        ConfigurarToolStripMenuItem.Size = New Size(162, 26)
        ConfigurarToolStripMenuItem.Text = "Configurar"
        ' 
        ' ConectarToolStripMenuItem
        ' 
        ConectarToolStripMenuItem.Name = "ConectarToolStripMenuItem"
        ConectarToolStripMenuItem.Size = New Size(162, 26)
        ConectarToolStripMenuItem.Text = "Conectar"
        ' 
        ' FecharToolStripMenuItem
        ' 
        FecharToolStripMenuItem.Name = "FecharToolStripMenuItem"
        FecharToolStripMenuItem.Size = New Size(162, 26)
        FecharToolStripMenuItem.Text = "Fechar"
        ' 
        ' PesquisarToolStripMenuItem
        ' 
        PesquisarToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IDToolStripMenuItem, NomeParcialToolStripMenuItem, NomeExatoToolStripMenuItem, PesquisaParcialNoCampoAtualToolStripMenuItem, PesquisaExataNoCampoAtualToolStripMenuItem, FamilySearchToolStripMenuItem, GoogleToolStripMenuItem, ProjResgateToolStripMenuItem})
        PesquisarToolStripMenuItem.Name = "PesquisarToolStripMenuItem"
        PesquisarToolStripMenuItem.Size = New Size(84, 24)
        PesquisarToolStripMenuItem.Text = "Pesquisar"
        ' 
        ' IDToolStripMenuItem
        ' 
        IDToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IgualAToolStripMenuItem, MenorQueToolStripMenuItem, MaiorQueToolStripMenuItem, EntreToolStripMenuItem})
        IDToolStripMenuItem.Name = "IDToolStripMenuItem"
        IDToolStripMenuItem.Size = New Size(307, 26)
        IDToolStripMenuItem.Text = "ID"
        ' 
        ' IgualAToolStripMenuItem
        ' 
        IgualAToolStripMenuItem.Name = "IgualAToolStripMenuItem"
        IgualAToolStripMenuItem.Size = New Size(164, 26)
        IgualAToolStripMenuItem.Text = "igual a"
        ' 
        ' MenorQueToolStripMenuItem
        ' 
        MenorQueToolStripMenuItem.Name = "MenorQueToolStripMenuItem"
        MenorQueToolStripMenuItem.Size = New Size(164, 26)
        MenorQueToolStripMenuItem.Text = "menor que"
        ' 
        ' MaiorQueToolStripMenuItem
        ' 
        MaiorQueToolStripMenuItem.Name = "MaiorQueToolStripMenuItem"
        MaiorQueToolStripMenuItem.Size = New Size(164, 26)
        MaiorQueToolStripMenuItem.Text = "maior que"
        ' 
        ' EntreToolStripMenuItem
        ' 
        EntreToolStripMenuItem.Name = "EntreToolStripMenuItem"
        EntreToolStripMenuItem.Size = New Size(164, 26)
        EntreToolStripMenuItem.Text = "entre"
        ' 
        ' NomeParcialToolStripMenuItem
        ' 
        NomeParcialToolStripMenuItem.Name = "NomeParcialToolStripMenuItem"
        NomeParcialToolStripMenuItem.Size = New Size(307, 26)
        NomeParcialToolStripMenuItem.Text = "Nome Parcial"
        ' 
        ' NomeExatoToolStripMenuItem
        ' 
        NomeExatoToolStripMenuItem.Name = "NomeExatoToolStripMenuItem"
        NomeExatoToolStripMenuItem.Size = New Size(307, 26)
        NomeExatoToolStripMenuItem.Text = "Nome Exato"
        ' 
        ' PesquisaParcialNoCampoAtualToolStripMenuItem
        ' 
        PesquisaParcialNoCampoAtualToolStripMenuItem.Name = "PesquisaParcialNoCampoAtualToolStripMenuItem"
        PesquisaParcialNoCampoAtualToolStripMenuItem.Size = New Size(307, 26)
        PesquisaParcialNoCampoAtualToolStripMenuItem.Text = "Pesquisa Parcial no Campo Atual"
        ' 
        ' PesquisaExataNoCampoAtualToolStripMenuItem
        ' 
        PesquisaExataNoCampoAtualToolStripMenuItem.Name = "PesquisaExataNoCampoAtualToolStripMenuItem"
        PesquisaExataNoCampoAtualToolStripMenuItem.Size = New Size(307, 26)
        PesquisaExataNoCampoAtualToolStripMenuItem.Text = "Pesquisa Exata no Campo Atual"
        ' 
        ' FamilySearchToolStripMenuItem
        ' 
        FamilySearchToolStripMenuItem.Name = "FamilySearchToolStripMenuItem"
        FamilySearchToolStripMenuItem.Size = New Size(307, 26)
        FamilySearchToolStripMenuItem.Text = "Family Search"
        ' 
        ' GoogleToolStripMenuItem
        ' 
        GoogleToolStripMenuItem.Name = "GoogleToolStripMenuItem"
        GoogleToolStripMenuItem.Size = New Size(307, 26)
        GoogleToolStripMenuItem.Text = "Google"
        ' 
        ' ProjResgateToolStripMenuItem
        ' 
        ProjResgateToolStripMenuItem.Name = "ProjResgateToolStripMenuItem"
        ProjResgateToolStripMenuItem.Size = New Size(307, 26)
        ProjResgateToolStripMenuItem.Text = "Proj Resgate"
        ' 
        ' XMLToolStripMenuItem
        ' 
        XMLToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExtrairToolStripMenuItem, CarregarToolStripMenuItem})
        XMLToolStripMenuItem.Name = "XMLToolStripMenuItem"
        XMLToolStripMenuItem.Size = New Size(52, 24)
        XMLToolStripMenuItem.Text = "XML"
        ' 
        ' ExtrairToolStripMenuItem
        ' 
        ExtrairToolStripMenuItem.Name = "ExtrairToolStripMenuItem"
        ExtrairToolStripMenuItem.Size = New Size(149, 26)
        ExtrairToolStripMenuItem.Text = "Extrair"
        ' 
        ' CarregarToolStripMenuItem
        ' 
        CarregarToolStripMenuItem.Name = "CarregarToolStripMenuItem"
        CarregarToolStripMenuItem.Size = New Size(149, 26)
        CarregarToolStripMenuItem.Text = "Carregar"
        ' 
        ' FontesToolStripMenuItem
        ' 
        FontesToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IncluirToolStripMenuItem, ExibirToolStripMenuItem1, PesquisarToolStripMenuItem1})
        FontesToolStripMenuItem.Name = "FontesToolStripMenuItem"
        FontesToolStripMenuItem.Size = New Size(66, 24)
        FontesToolStripMenuItem.Text = "Fontes"
        ' 
        ' IncluirToolStripMenuItem
        ' 
        IncluirToolStripMenuItem.Name = "IncluirToolStripMenuItem"
        IncluirToolStripMenuItem.Size = New Size(155, 26)
        IncluirToolStripMenuItem.Text = "incluir"
        ' 
        ' ExibirToolStripMenuItem1
        ' 
        ExibirToolStripMenuItem1.Name = "ExibirToolStripMenuItem1"
        ExibirToolStripMenuItem1.Size = New Size(155, 26)
        ExibirToolStripMenuItem1.Text = "exibir"
        ' 
        ' PesquisarToolStripMenuItem1
        ' 
        PesquisarToolStripMenuItem1.Name = "PesquisarToolStripMenuItem1"
        PesquisarToolStripMenuItem1.Size = New Size(155, 26)
        PesquisarToolStripMenuItem1.Text = "pesquisar"
        ' 
        ' CToolStripMenuItem
        ' 
        CToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExibirToolStripMenuItem})
        CToolStripMenuItem.Name = "CToolStripMenuItem"
        CToolStripMenuItem.Size = New Size(118, 24)
        CToolStripMenuItem.Text = "Configurações"
        ' 
        ' ExibirToolStripMenuItem
        ' 
        ExibirToolStripMenuItem.Name = "ExibirToolStripMenuItem"
        ExibirToolStripMenuItem.Size = New Size(129, 26)
        ExibirToolStripMenuItem.Text = "Exibir"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "*.ged"
        OpenFileDialog1.RestoreDirectory = True
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Location = New Point(26, 435)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(625, 206)
        RichTextBox1.TabIndex = 5
        RichTextBox1.Text = ""
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(127, 647)
        Button2.Name = "Button2"
        Button2.Size = New Size(96, 37)
        Button2.TabIndex = 6
        Button2.Text = "xpath"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' RadioButtonXML
        ' 
        RadioButtonXML.AutoSize = True
        RadioButtonXML.Location = New Point(21, 26)
        RadioButtonXML.Name = "RadioButtonXML"
        RadioButtonXML.Size = New Size(54, 24)
        RadioButtonXML.TabIndex = 8
        RadioButtonXML.Text = "xml"
        RadioButtonXML.UseVisualStyleBackColor = True
        ' 
        ' RadioButtonTXT
        ' 
        RadioButtonTXT.AutoSize = True
        RadioButtonTXT.Location = New Point(80, 26)
        RadioButtonTXT.Name = "RadioButtonTXT"
        RadioButtonTXT.Size = New Size(55, 24)
        RadioButtonTXT.TabIndex = 9
        RadioButtonTXT.Text = "text"
        RadioButtonTXT.UseVisualStyleBackColor = True
        ' 
        ' RichTextBox2
        ' 
        RichTextBox2.Location = New Point(749, 435)
        RichTextBox2.Name = "RichTextBox2"
        RichTextBox2.Size = New Size(625, 206)
        RichTextBox2.TabIndex = 10
        RichTextBox2.Text = ""
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(1330, 33)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(35, 33)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(230, 647)
        Button3.Name = "Button3"
        Button3.Size = New Size(94, 37)
        Button3.TabIndex = 14
        Button3.Text = "xsd"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(347, 651)
        Button4.Name = "Button4"
        Button4.Size = New Size(94, 29)
        Button4.TabIndex = 15
        Button4.Text = "Button4"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel1, ToolStripStatusLabel2})
        StatusStrip1.Location = New Point(0, 697)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1924, 22)
        StatusStrip1.TabIndex = 16
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(0, 16)
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(0, 16)
        ' 
        ' TreeView1
        ' 
        TreeView1.Location = New Point(21, 30)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(415, 514)
        TreeView1.TabIndex = 17
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TreeView1)
        GroupBox1.Location = New Point(1426, 73)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(454, 568)
        GroupBox1.TabIndex = 18
        GroupBox1.TabStop = False
        GroupBox1.Text = "Árvore"
        ' 
        ' OpenFileDialog2
        ' 
        OpenFileDialog2.FileName = "OpenFileDialog2"
        OpenFileDialog2.RestoreDirectory = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(30, 99)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1359, 320)
        TabControl1.TabIndex = 19
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(DataGridView1)
        TabPage1.Location = New Point(4, 29)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1351, 287)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Membros"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(DataGridView2)
        TabPage2.Location = New Point(4, 29)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1351, 287)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Fontes"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(6, 21)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersWidth = 51
        DataGridView2.Size = New Size(1334, 260)
        DataGridView2.TabIndex = 0
        ' 
        ' rbascii
        ' 
        rbascii.AutoSize = True
        rbascii.Location = New Point(16, 26)
        rbascii.Name = "rbascii"
        rbascii.Size = New Size(59, 24)
        rbascii.TabIndex = 20
        rbascii.TabStop = True
        rbascii.Text = "ascii"
        rbascii.UseVisualStyleBackColor = True
        ' 
        ' rbutf8
        ' 
        rbutf8.AutoSize = True
        rbutf8.Location = New Point(81, 26)
        rbutf8.Name = "rbutf8"
        rbutf8.Size = New Size(62, 24)
        rbutf8.TabIndex = 21
        rbutf8.TabStop = True
        rbutf8.Text = "utf-8"
        rbutf8.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(rbascii)
        GroupBox2.Controls.Add(rbutf8)
        GroupBox2.Location = New Point(811, 36)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(149, 61)
        GroupBox2.TabIndex = 22
        GroupBox2.TabStop = False
        GroupBox2.Text = "Codificação:"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(RadioButtonXML)
        GroupBox3.Controls.Add(RadioButtonTXT)
        GroupBox3.Location = New Point(983, 33)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(146, 64)
        GroupBox3.TabIndex = 23
        GroupBox3.TabStop = False
        GroupBox3.Text = "xpath: saída"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 719)
        Controls.Add(GroupBox3)
        Controls.Add(GroupBox2)
        Controls.Add(TabControl1)
        Controls.Add(GroupBox1)
        Controls.Add(StatusStrip1)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(PictureBox1)
        Controls.Add(RichTextBox2)
        Controls.Add(Button2)
        Controls.Add(RichTextBox1)
        Controls.Add(txtQuery)
        Controls.Add(Button1)
        Controls.Add(MenuStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "genealogia"
        WindowState = FormWindowState.Maximized
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtQuery As TextBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArquivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BancoDeDadosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CriarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbrirToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ConexãoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfigurarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConectarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents XMLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtrairToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FecharToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CarregarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents RadioButtonXML As RadioButton
    Friend WithEvents RadioButtonTXT As RadioButton
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents CToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExibirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PesquisarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NomeParcialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NomeExatoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PesquisaParcialNoCampoAtualToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PesquisaExataNoCampoAtualToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button4 As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents FontesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IncluirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents ExibirToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PesquisarToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents IgualAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenorQueToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaiorQueToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EntreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FamilySearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents rbascii As RadioButton
    Friend WithEvents rbutf8 As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GoogleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AtualizarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProjResgateToolStripMenuItem As ToolStripMenuItem

End Class
