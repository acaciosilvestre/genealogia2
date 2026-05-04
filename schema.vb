Imports System
Imports System.Configuration
Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Module xml_schema
    Sub XmlSchemaCreateExample(args As String)
        Dim schema As New XmlSchema
        Dim FirstNameElement = New Schema.XmlSchemaElement
        FirstNameElement.Name = "FirstName"

        '*************************************************
        Dim LastNameElement = New Schema.XmlSchemaElement
        LastNameElement.Name = "LastName"
        '*************************************************

        Dim idAttr = New XmlSchemaAttribute
        idAttr.Name = "CustomerID"
        idAttr.Use = XmlSchemaUse.Required


        '*****************************************************
        Dim lastNameType As New Schema.XmlSchemaSimpleType
        lastNameType.Name = "LastNameType"
        Dim lastNameRestriction As New XmlSchemaSimpleTypeRestriction
        lastNameRestriction.BaseTypeName = New XmlQualifiedName()

        'teste
        'im strbasetype = New XmlSchemaSimpleType
        'astNameRestriction.BaseType = strbasetype

        Dim maxLength = New XmlSchemaMaxLengthFacet
        maxLength.Value = "20"
        lastNameRestriction.Facets.Add(maxLength)
        lastNameType.Content = lastNameRestriction

        '*****************************************************


        FirstNameElement.SchemaTypeName = New XmlQualifiedName()
        '
        LastNameElement.SchemaTypeName = New XmlQualifiedName()

        idAttr.SchemaTypeName = New XmlQualifiedName()


        Dim customerElement = New Schema.XmlSchemaElement
        customerElement.Name = "Customer"

        Dim customerType = New XmlSchemaComplexType
        Dim sequence = New XmlSchemaSequence
        sequence.Items.Add(FirstNameElement)
        sequence.Items.Add(LastNameElement)
        customerType.Particle = sequence
        customerType.Attributes.Add(idAttr)
        customerElement.SchemaType = customerType

        Dim customerSchema = New XmlSchema
        customerSchema.TargetNamespace = "http://www.tempuri.org"
        customerSchema.Items.Add(customerElement)
        'ustomerSchema.Items.Add(lastNameType)
        Dim schemaSet = New XmlSchemaSet

        schemaSet.Add(customerSchema)
        schemaSet.Compile()

        For Each schema In schemaSet.Schemas
            customerSchema = schema

        Next

        Dim sw = New StreamWriter("esquema.xsd")
        sw.AutoFlush = True
        Console.SetOut(sw)
        customerSchema.Write(Console.Out)
        Console.Out.WriteLine()
    End Sub

    Function vcb(sender As Object, args As ValidationEventArgs)
        MsgBox(args.Message)
        vcb = 0
    End Function
End Module
