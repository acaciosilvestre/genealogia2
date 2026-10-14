
Module DataBase
	Sub createDataBase(DataBasePath)
		Dim cat, field, db

		cat = Nothing
		db = Nothing

		Try
			cat = CreateObject("ADOX.Catalog")
			cat.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & DataBasePath & ";")
			db = CreateObject("DAO.DBEngine.120")
			db.Begintrans
			conn = db.OpenDatabase(DataBasePath)
			' Aqui você pode adicionar tabelas, consultas, etc. ao banco de dados.
			conn.Execute("CREATE TABLE " & TableName)
			For Each field In Campos
				conn.Execute("ALTER TABLE " & TableName & " ADD COLUMN " & field & ";")
			Next

			startFS2(InputFileName)
			PopulateDB()
			stopFS2()
			db.Committrans
		Catch ex As Exception
			' Registre ou trate o erro conforme apropriado
			Debug.WriteLine("createDataBase error: " & ex.Message)
			Try
				db.Rollback
			Catch
			End Try
		Finally
			If Not conn Is Nothing Then
				Try
					conn.close
				Catch
				End Try
			End If
			conn = Nothing
			db = Nothing
			cat = Nothing
		End Try

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
						Campos(i) = .selectSingleNode("//Fields/FDName[" & (i + 1) & "]").text _
							& " " & .selectSingleNode("//Fields/FDType[" & (i + 1) & "]").text
					Next
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
