
Imports System.Xml

Module DataBase
	Sub createDataBase(DataBasePath)
		Dim field, db

		'cat = CreateObject("ADOX.Catalog")
		'cat.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & DataBasePath & ";")
		'cat = Nothing

		db = CreateObject("DAO.DBEngine.120")
		db.CreateDatabase(DataBasePath, ";langid=0x0416;", 128)
		db.Begintrans
		conn = db.OpenDatabase(DataBasePath)
		' Aqui você pode adicionar tabelas, consultas, etc. ao banco de dados.
		Dim Tabelas As List(Of String) = New List(Of String) From {"Membros", "Familias", "Crianças"}
		Dim flds(3) As List(Of String)
		flds(0) = New List(Of String) From {"ID Number", "Nome Text", "Nascimento Text", "LocalNascimento Text", "Falecimento Text", "LocalFalecimento Text", "Batismo Text", "LocalBatismo Text", "Sepultamento Text", "LocalSepultamento Text", "famc Text", "fams Text"}
		flds(1) = New List(Of String) From {"FID Text", "Marido Text", "Esposa Text", "Casamento Text", "Local Text"}
		flds(2) = New List(Of String) From {"FID Text", "Criança Text"}

		For Each Tabela In Tabelas
			conn.Execute("CREATE TABLE " & Tabela)
			For Each field In flds(Tabelas.IndexOf(Tabela))
				conn.Execute("ALTER TABLE " & Tabela & " ADD COLUMN " & field & ";")
			Next
		Next

		startFS2(InputFileName)
		PopulateDB()
		stopFS2()
		db.Committrans
		conn.close
		conn = Nothing
		db = Nothing

	End Sub

	Sub Inicializa()
		'Obtém elementos para criar banco de dados do arquivo XML, ou ...
		Dim iniFile
		fs = CreateObject("Scripting.FileSystemObject")

		With My.Settings
			iniFile = .xml_config_file
			InputFileName = .defaultInputFile
			DataBaseName = .DBName
			connectionString = .connectionString & .DBName
		End With

		If fs.FileExists(iniFile) Then
			Dim oXML
			oXML = CreateObject("Msxml2.DOMDocument.6.0")
			If oXML.Load(iniFile) Then
				With oXML

					TableName = .selectSingleNode("//TableName").text
					Dim i
					For i = 0 To Campos.Count - 1
						IO.Campos(i) = .selectSingleNode("//Fields/FDName[" & (i + 1) & "]").text _
							& " " & .selectSingleNode("//Fields/FDType[" & (i + 1) & "]").text
					Next
					'MsgBox(.selectSingleNode("//TableName[2]").text)
					Dim tb = .selectSingleNode("//Tables")
                    For Each n In tb.ChildNodes
                        Dim f = n.selectSingleNode("//Fields")
						For Each f In n.ChildNodes
							'MsgBox(f.text)
						Next
					Next
					'MsgBox(.selectSingleNode("//Fields[2]/FDName[1]").text)
					'MsgBox(.selectSingleNode("//Fields[2]/FDType[1]").text)
				End With

			End If

		Else
			'... utiliza os valores embutidos
			InputFileName = My.Settings.defaultInputFile
			TableName = kTableName

		End If
		'Verifica se arquivo de entrada existe
		If fs.FileExists(InputFileName) Then
			InputFileExists = True
		Else
			InputFileExists = False
		End If

	End Sub

End Module
