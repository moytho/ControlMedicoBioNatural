Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Data
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSConsultamedica
    Inherits System.Web.Services.WebService

    <WebMethod(True)> _
    Public Function ConsultaCreaPacientesPorCodigo(ByVal usuario As String, ByVal idpaciente As String) As List(Of ClasePaciente)

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()

                Dim CodigoMedico As Integer = 0
                Dim SqlBusquedaMedico As String = "select idempleado from usuario where idusuario='" & usuario & "'"

                Dim comando As New SqlCommand(SqlBusquedaMedico, connection)
                Dim readerdoctor As SqlDataReader = comando.ExecuteReader()


                readerdoctor.Read()
                If readerdoctor.HasRows() Then
                    CodigoMedico = readerdoctor("idempleado")
                End If

                readerdoctor.Close()

                Dim SQLBusqueda As String


                SQLBusqueda = "select r.idpaciente,r.idreceta,CONVERT(varchar,r.fecha,103) fecha,p.nombre+' '+p.apellido nombrecompleto " +
                    "from recetasencabezado r INNER JOIN Pacientes p ON r.idpaciente=p.CodE " +
                    "where r.idpaciente=" & idpaciente & " and r.estado='CONFIRMADO' ORDER BY r.fecha desc"


                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombrepaciente = reader("fecha").ToString + " " + reader("nombrecompleto")
                    Elemento.idconsulta = reader("idreceta")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente

                Elemento.idpaciente = 0
                Elemento.nombrepaciente = "ERROR " + ex.Message
                Elemento.idconsulta = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod(True)> _
    Public Function ConsultaCreaPacientesTodos(ByVal cadenabusqueda As String, ByVal usuario As String) As List(Of ClasePaciente)

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()

                Dim CodigoMedico As Integer = 0
                Dim SqlBusquedaMedico As String = "select idempleado from usuario where idusuario='" & usuario & "'"

                Dim comando As New SqlCommand(SqlBusquedaMedico, connection)
                Dim readerdoctor As SqlDataReader = comando.ExecuteReader()


                readerdoctor.Read()
                If readerdoctor.HasRows() Then
                    CodigoMedico = readerdoctor("idempleado")
                End If

                readerdoctor.Close()

                Dim SQLBusqueda As String
                If cadenabusqueda.Length > 0 Then
                    cadenabusqueda = Right(cadenabusqueda, cadenabusqueda.Length - 1)
                    SQLBusqueda = "select c.idconsulta,c.idpaciente,p.Nombre+' '+p.Apellido nombrecompleto " +
                    "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                    "where CONVERT(varchar,c.fechaconsulta,102)=CONVERT(varchar,getdate(),102) and " +
                    "c.estado='PENDIENTE' and c.idpaciente NOT in (" & cadenabusqueda & ") " +
                    "ORDER BY c.idconsulta asc"

                Else
                    SQLBusqueda = "select c.idconsulta,c.idpaciente,p.Nombre+' '+p.Apellido nombrecompleto " +
                    "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                    "where CONVERT(varchar,c.fechaconsulta,102)=CONVERT(varchar,getdate(),102) and " +
                    "c.estado='PENDIENTE' ORDER BY c.idconsulta asc"
                End If

                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombrepaciente = reader("nombrecompleto")
                    Elemento.idconsulta = reader("idconsulta")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente

                Elemento.idpaciente = 0
                Elemento.nombrepaciente = "ERROR " + ex.Message
                Elemento.idconsulta = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod(True)> _
    Public Function ConsultaCreaPacientesAtendidos(ByVal usuario As String, ByVal fecha As String) As List(Of ClasePaciente)

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()

                Dim CodigoMedico As Integer = 0
                Dim SqlBusquedaMedico As String = "select idempleado from usuario where idusuario='" & usuario & "'"

                Dim comando As New SqlCommand(SqlBusquedaMedico, connection)
                Dim readerdoctor As SqlDataReader = comando.ExecuteReader()


                readerdoctor.Read()
                If readerdoctor.HasRows() Then
                    CodigoMedico = readerdoctor("idempleado")
                End If

                readerdoctor.Close()

                Dim SQLBusqueda As String

                SQLBusqueda = "select re.idreceta,re.idpaciente,p.Nombre+' '+p.Apellido nombrecompleto " +
                "from recetasencabezado re INNER JOIN Pacientes p ON re.idpaciente=p.CodE " +
                "where CONVERT(varchar,re.fecha,102)='" & fecha & "' and " +
                "re.estado='CONFIRMADO' ORDER BY re.idreceta asc"


                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombrepaciente = reader("nombrecompleto")
                    Elemento.idconsulta = reader("idreceta")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente

                Elemento.idpaciente = 0
                Elemento.nombrepaciente = "ERROR " + ex.Message
                Elemento.idconsulta = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod(True)> _
    Public Function reimprimirReceta(ByVal idpaciente As Integer, ByVal idreceta As Integer) As String

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                Dim Elemento As New ClaseConsulta

                Dim SQLInserta As String = ""

                SQLInserta = "SET LANGUAGE spanish"
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                '=================== C R E A C I O N   D E L   P  D  F ========================

                Dim SqlConsultaPaciente As String = "select r.idpaciente,dbo.fn_formato_fecha(r.proximaconsulta) as proximaconsulta,p.Nombre+' ' +p.Apellido nombrecompleto " +
                "from recetasencabezado r INNER JOIN Pacientes p ON r.idpaciente=p.CodE where idreceta=" & idreceta & ""
                Dim Paciente As Integer = 0
                Dim NombreCompleto As String = ""
                Dim ProximaConsulta As String = ""

                comando.CommandText = SqlConsultaPaciente
                Dim reader As SqlDataReader = comando.ExecuteReader

                reader.Read()
                If reader.HasRows Then
                    Paciente = reader("idpaciente")
                    NombreCompleto = reader("nombrecompleto")
                    ProximaConsulta = IIf(reader("proximaconsulta") IsNot DBNull.Value, reader("proximaconsulta"), "")
                    reader.Close()

                    'Lector.Read()
                    'If Lector.HasRows Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 15, 15, 3, 10)
                    Dim Correlativo As Integer = NumeroAleatorio()

                    Dim pd As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath("~\privado\pdf\manifiesto" + Correlativo.ToString + ".pdf"), FileMode.Create))
                    doc.AddTitle("Manifiesto")
                    doc.AddAuthor("BIO * NATURAL")
                    doc.AddCreationDate()

                    doc.Open()

                    Dim TablaDatosClinica As DataTable = cargar_datatable("select top 1 nombre,telefono,fax,direccion,correo,eslogan from empresa")
                    If TablaDatosClinica.Rows.Count > 0 Then

                        'End If
                        'For i = 0 To TablaRecetas.Rows.Count - 1
                        'EMPEZAMOS ARMANDO EL OBJETO
                        'Dim Elemento As New Recetario
                        'Elemento.idreceta = TablaRecetas.Rows(i).Item("idreceta").ToString
                        'Elemento.fecha = TablaRecetas.Rows(i).Item("fecha").ToString

                        Dim datoEmpresa As New Chunk(TablaDatosClinica.Rows(0).Item("nombre").ToString, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk(TablaDatosClinica.Rows(0).Item("direccion").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk(TablaDatosClinica.Rows(0).Item("telefono").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)

                    Else

                        Dim datoEmpresa As New Chunk("BIO * NATURAL", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk("7a Av. A 3-54 Zona 9, Ciudad de Guatemala", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk("(502) 2331-7935", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)

                    End If
                    'doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("RECETA MEDICA", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)
                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(3)

                    Encabezado.TotalWidth = 500
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({10, 60, 30})

                    Dim Celdan As PdfPCell = New PdfPCell
                    Celdan = New PdfPCell(New Paragraph("Paciente", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Nombre Completo", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Proxima Consulta", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Paciente, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(NombreCompleto, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(ProximaConsulta, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    doc.Add(Encabezado)


                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))


                    Dim datoSugerencia As New Chunk("** LE ROGAMOS TRAER SU RECETA A LA PRÓXIMA CONSULTA **", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia As New Paragraph(datoSugerencia)
                    ParrafoSugerencia.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia)

                    Dim datoSugerencia2 As New Chunk("LEA TODAS LAS INSTRUCCIONES ANTES DE COMENZAR A TOMAR SU MEDICINA", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia2 As New Paragraph(datoSugerencia2)
                    ParrafoSugerencia2.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia2)

                    Dim datoSugerencia3 As New Chunk("DEJAR 15 o 20 MINUTOS DE DIFERENCIA ENTRE CADA MEDICAMENTO, NO TOMARLOS JUNTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia3 As New Paragraph(datoSugerencia3)
                    ParrafoSugerencia3.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia3)

                    Dim datoSugerencia4 As New Chunk("TODO LO ESCRITO ES PARA USTED, HAGALO PORQUE ES PARTE DE SU TRATAMIENTO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia4 As New Paragraph(datoSugerencia4)
                    ParrafoSugerencia4.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia4)


                    Dim cell1 As PdfPCell = New PdfPCell()
                    Dim StrCodigoTratamiento As String = ""
                    Dim StrTratamiento As String = ""
                    Dim SqlConsultaReceta As String = ""



                    '################ I N D I C A C I O N E S ###############
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodT=t.CodT " +
                    "where r.CodR=" & idreceta & " and (r.CodT!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim Lector As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               INDICACIONES", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (Lector.Read())

                        StrCodigoTratamiento = IIf(Lector("codigotratamiento") IsNot DBNull.Value, Lector("codigotratamiento"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(Lector("detalletratamiento") IsNot DBNull.Value, Lector("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While
                    Lector.Close()


                    '####################### MEDICAMENTOS #########################'
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodM=t.CodT " +
                    "where r.CodR=" & idreceta & " and (r.CodM!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorMedicamentos As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               MEDICAMENTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorMedicamentos.Read())

                        StrCodigoTratamiento = IIf(LectorMedicamentos("codigotratamiento") IsNot DBNull.Value, LectorMedicamentos("codigotratamiento"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorMedicamentos("detalletratamiento") IsNot DBNull.Value, LectorMedicamentos("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorMedicamentos.Close()

                    '####################### SINTOMAS #########################'
                    SqlConsultaReceta = "select s.descripcion sintoma,s.observaciones observaciones " +
                    "from Recetas r " +
                    "INNER JOIN sintoma s ON r.idsintoma=s.idsintoma " +
                    "where r.CodR=" & idreceta & " and (s.idsintoma!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorSintomas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               SINTOMAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorSintomas.Read())

                        StrCodigoTratamiento = IIf(LectorSintomas("sintoma") IsNot DBNull.Value, LectorSintomas("sintoma"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorSintomas("observaciones") IsNot DBNull.Value, LectorSintomas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorSintomas.Close()

                    '####################### Notas #########################'
                    SqlConsultaReceta = "select " +
                    "t.CodT, t.Descripcion descripcion, t.Otros observaciones " +
                    "from tratamientosugerido ts " +
                    "INNER JOIN Tratamiento t ON ts.idtratamiento=t.CodT " +
                    "WHERE (ts.estatus=1) AND (getdate() BETWEEN ts.fechainicio and ts.fechafinal)"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorNotas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               NOTAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorNotas.Read())

                        StrCodigoTratamiento = IIf(LectorNotas("descripcion") IsNot DBNull.Value, LectorNotas("descripcion"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorNotas("observaciones") IsNot DBNull.Value, LectorNotas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorNotas.Close()

                    doc.Close()

                    transaccion.Commit()
                    Return "pdf/manifiesto" + Correlativo.ToString + ".pdf"

                Else
                    reader.Close()
                    Return "ERROR: No se obtuvieron los datos del paciente"
                End If


            Catch ex As SqlException
                transaccion.Rollback()

                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function RecetaGuardarCompleta(ByVal arregloSintoma As List(Of ClaseConsulta), ByVal arregloTratamiento As List(Of ClaseTratamiento), ByVal arregloMedicina As List(Of ClaseMedicina), ByVal usuario As String, ByVal idpaciente As String, ByVal Varproximaconsulta As String) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "INSERT INTO recetasencabezado " +
           "(idpaciente " +
           ",fecha " +
           ",iddoctor " +
           ",estado,proximaconsulta) " +
           "VALUES " +
           "(" & idpaciente & "" +
           ",getdate() " +
           ",1" +
           ",'PENDIENTE','" & Varproximaconsulta & "')"
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseConsulta

                'comando.CommandText = "SET IDENTITY_INSERT Recetas ON"
                'comando.ExecuteNonQuery()

                'EJECUTO EL QUERY DE o
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim idReceta As Integer = comando.ExecuteScalar

                SQLInserta = "SET LANGUAGE spanish"
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                Dim CodigoMedicina As String = ""
                Dim CodigoTratamiento As String = ""
                Dim CodigoSintoma As Integer = 0

                'ESTO SE EJECUTARA CUANDO EXISTAN MAS TRATAMIENTOS QUE MEDICAMENTOS A LA RECETA
                Dim NumerodeRepeticiones As Integer = 0
                NumerodeRepeticiones = burbuja(arregloTratamiento.Count, arregloMedicina.Count, arregloSintoma.Count)

                Dim obsSintomas, obsIndicaciones, obsMedicamentos As String
                For i As Integer = 0 To NumerodeRepeticiones - 1

                    If i > (arregloTratamiento.Count - 1) Then
                        CodigoTratamiento = ""
                        obsIndicaciones = ""
                    Else
                        CodigoTratamiento = arregloTratamiento(i).idtratamiento
                        obsIndicaciones = arregloTratamiento(i).observaciones
                    End If

                    If i > (arregloMedicina.Count - 1) Then
                        CodigoMedicina = ""
                        obsMedicamentos = ""
                    Else
                        'VERIFICAMOS SI EL MEDICAMENTO TIENE RELACIONADO UN CODIGO PADRE PARA REBAJARLE LA EXISTENCIA AL PADRE
                        SQLInserta = "select TratamientoPadre from Tratamiento where CodT='" & arregloMedicina(i).idmedicina & "'"
                        comando.CommandText = SQLInserta
                        Dim CodigoTratamientoPadre As String = IIf(comando.ExecuteScalar Is DBNull.Value, "", comando.ExecuteScalar)

                        'VERIFICAMOS LA CANTIDAD QUE SE LE TIENE QUE REBAJAR AL PADRE
                        SQLInserta = "select CantidadARebajar from Tratamiento where CodT='" & arregloMedicina(i).idmedicina & "'"
                        comando.CommandText = SQLInserta

                        Dim cantidadARebajar As Integer = IIf(comando.ExecuteScalar Is DBNull.Value, 0, comando.ExecuteScalar)
                        'SI EL CODIGOTRATAMIENTOPADRE RETORNA "" ES QUE NO TIENE NINGUN CODIGO DE MEDICAMENTO RELACIONADO A EL, POR LO TANTO
                        'SE REBAJA SU PROPIA EXISTENCIA
                        If CodigoTratamientoPadre = "" Then
                            SQLInserta = "update Tratamiento set existencia=(existencia-1) " +
                            "where CodT='" & arregloMedicina(i).idmedicina & "'"
                            'SI ENCONTRAMOS DATOS RELACIONADOS EJECUTAMOS EL SIGUIENTE STATEMENT
                        Else
                            SQLInserta = "update Tratamiento set existencia=existencia-" & Val(cantidadARebajar) & " where CodT='" & CodigoTratamientoPadre & "'"
                        End If

                        comando.CommandText = SQLInserta
                        comando.ExecuteNonQuery()

                        CodigoMedicina = arregloMedicina(i).idmedicina
                        obsMedicamentos = arregloMedicina(i).observaciones
                    End If

                    If i > (arregloSintoma.Count - 1) Then
                        CodigoSintoma = 0
                        obsSintomas = ""
                    Else
                        CodigoSintoma = arregloSintoma(i).idsintoma
                        obsSintomas = arregloSintoma(i).observaciones
                    End If

                    SQLInserta = "INSERT INTO dbo.Recetas " +
                    "(CodR,CodT,CodM,Cant,idsintoma,obssintomas,obsindicaciones,obsmedicamentos) " +
                    "VALUES " +
                    "(" & idReceta & ",'" & CodigoTratamiento & "','" & CodigoMedicina & "',0," & CodigoSintoma & "," +
                    "'" & obsSintomas & "','" & obsIndicaciones & "','" & obsMedicamentos & "') "

                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()


                Next

                SQLInserta = "update consulta set estado='CONFIRMADO' where idconsulta in (select c.idconsulta " +
                "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                "where idpaciente=" & idpaciente & ")"

                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                SQLInserta = "update recetasencabezado " +
                "set estado='CONFIRMADO' " +
                "WHERE idreceta=" & idReceta & " "

                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                '=================== C R E A C I O N   D E L   P  D  F ========================

                Dim SqlConsultaPaciente As String = "select r.idpaciente,dbo.fn_formato_fecha(r.proximaconsulta) as proximaconsulta,p.Nombre+' ' +p.Apellido nombrecompleto " +
                "from recetasencabezado r INNER JOIN Pacientes p ON r.idpaciente=p.CodE where idreceta=" & idReceta & ""
                Dim Paciente As Integer = 0
                Dim NombreCompleto As String = ""
                Dim ProximaConsulta As String = ""

                comando.CommandText = SqlConsultaPaciente
                Dim reader As SqlDataReader = comando.ExecuteReader

                reader.Read()
                If reader.HasRows Then
                    Paciente = reader("idpaciente")
                    NombreCompleto = reader("nombrecompleto")
                    ProximaConsulta = IIf(reader("proximaconsulta") IsNot DBNull.Value, reader("proximaconsulta"), "")
                    reader.Close()

                    'Lector.Read()
                    'If Lector.HasRows Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 15, 15, 3, 10)
                    Dim Correlativo As Integer = NumeroAleatorio()

                    Dim pd As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath("~\privado\pdf\manifiesto" + Correlativo.ToString + ".pdf"), FileMode.Create))
                    doc.AddTitle("Manifiesto")
                    doc.AddAuthor("BIO * NATURAL")
                    doc.AddCreationDate()

                    doc.Open()

                    Dim TablaDatosClinica As DataTable = cargar_datatable("select top 1 nombre,telefono,fax,direccion,correo,eslogan from empresa")
                    If TablaDatosClinica.Rows.Count > 0 Then

                        'End If
                        'For i = 0 To TablaRecetas.Rows.Count - 1
                        'EMPEZAMOS ARMANDO EL OBJETO
                        'Dim Elemento As New Recetario
                        'Elemento.idreceta = TablaRecetas.Rows(i).Item("idreceta").ToString
                        'Elemento.fecha = TablaRecetas.Rows(i).Item("fecha").ToString

                        Dim datoEmpresa As New Chunk(TablaDatosClinica.Rows(0).Item("nombre").ToString, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk(TablaDatosClinica.Rows(0).Item("direccion").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk(TablaDatosClinica.Rows(0).Item("telefono").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)

                    Else

                        Dim datoEmpresa As New Chunk("BIO * NATURAL", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk("7a Av. A 3-54 Zona 9, Ciudad de Guatemala", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk("(502) 2331-7935", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)

                    End If

                    'doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("RECETA MEDICA", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)


                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(3)

                    Encabezado.TotalWidth = 500
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({10, 60, 30})

                    Dim Celdan As PdfPCell = New PdfPCell
                    Celdan = New PdfPCell(New Paragraph("Paciente", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Nombre Completo", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Proxima Consulta", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Paciente, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(NombreCompleto, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(ProximaConsulta, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    doc.Add(Encabezado)


                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))


                    Dim datoSugerencia As New Chunk("** LE ROGAMOS TRAER SU RECETA A LA PRÓXIMA CONSULTA **", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia As New Paragraph(datoSugerencia)
                    ParrafoSugerencia.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia)

                    Dim datoSugerencia2 As New Chunk("LEA TODAS LAS INSTRUCCIONES ANTES DE COMENZAR A TOMAR SU MEDICINA", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia2 As New Paragraph(datoSugerencia2)
                    ParrafoSugerencia2.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia2)

                    Dim datoSugerencia3 As New Chunk("DEJAR 15 o 20 MINUTOS DE DIFERENCIA ENTRE CADA MEDICAMENTO, NO TOMARLOS JUNTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia3 As New Paragraph(datoSugerencia3)
                    ParrafoSugerencia3.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia3)

                    Dim datoSugerencia4 As New Chunk("TODO LO ESCRITO ES PARA USTED, HAGALO PORQUE ES PARTE DE SU TRATAMIENTO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia4 As New Paragraph(datoSugerencia4)
                    ParrafoSugerencia4.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia4)


                    Dim cell1 As PdfPCell = New PdfPCell()
                    Dim StrCodigoTratamiento As String = ""
                    Dim StrTratamiento As String = ""
                    Dim SqlConsultaReceta As String = ""

                    'FECHA DE IMPRESION
                    Dim fechaImpresion As New Chunk("Fecha creación: " + Date.Now.ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.ITALIC))
                    Dim ParrafoFechaImpresion As New Paragraph(fechaImpresion)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(fechaImpresion)

                    'doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))




                    '################ I N D I C A C I O N E S ###############
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodT=t.CodT " +
                    "where r.CodR=" & idReceta & " and (r.CodT!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim Lector As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               INDICACIONES", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (Lector.Read())

                        StrCodigoTratamiento = IIf(Lector("codigotratamiento") IsNot DBNull.Value, Lector("codigotratamiento"), "")
                        Dim CodigoTratamiento1 As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento1)

                        StrTratamiento = IIf(Lector("detalletratamiento") IsNot DBNull.Value, Lector("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While
                    Lector.Close()

                    '####################### MEDICAMENTOS #########################'
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodM=t.CodT " +
                    "where r.CodR=" & idReceta & " and (r.CodM!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorMedicamentos As SqlDataReader = comando.ExecuteReader

                    doc.Add(New Paragraph("               MEDICAMENTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorMedicamentos.Read())

                        StrCodigoTratamiento = IIf(LectorMedicamentos("codigotratamiento") IsNot DBNull.Value, LectorMedicamentos("codigotratamiento"), "")
                        Dim CodigoTratamiento1 As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento1)

                        StrTratamiento = IIf(LectorMedicamentos("detalletratamiento") IsNot DBNull.Value, LectorMedicamentos("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorMedicamentos.Close()

                    '####################### SINTOMAS #########################'
                    SqlConsultaReceta = "select s.descripcion sintoma,s.observaciones observaciones " +
                    "from Recetas r " +
                    "INNER JOIN sintoma s ON r.idsintoma=s.idsintoma " +
                    "where r.CodR=" & idReceta & " and (s.idsintoma!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorSintomas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               SINTOMAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorSintomas.Read())

                        StrCodigoTratamiento = IIf(LectorSintomas("sintoma") IsNot DBNull.Value, LectorSintomas("sintoma"), "")
                        Dim CodigoTratamiento1 As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento1)

                        StrTratamiento = IIf(LectorSintomas("observaciones") IsNot DBNull.Value, LectorSintomas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorSintomas.Close()

                    '####################### Notas #########################'
                    SqlConsultaReceta = "select " +
                    "t.CodT, t.Descripcion descripcion, t.Otros observaciones " +
                    "from tratamientosugerido ts " +
                    "INNER JOIN Tratamiento t ON ts.idtratamiento=t.CodT " +
                    "WHERE (ts.estatus=1) AND (getdate() BETWEEN ts.fechainicio and ts.fechafinal)"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorNotas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               NOTAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorNotas.Read())

                        StrCodigoTratamiento = IIf(LectorNotas("descripcion") IsNot DBNull.Value, LectorNotas("descripcion"), "")
                        Dim CodigoTratamiento1 As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento1)

                        StrTratamiento = IIf(LectorNotas("observaciones") IsNot DBNull.Value, LectorNotas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorNotas.Close()

                    Dim fechaImpresion1 As New Chunk("Fecha creación: " + Date.Now.ToString, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.ITALIC))
                    Dim ParrafoFechaImpresion1 As New Paragraph(fechaImpresion1)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(fechaImpresion1)

                    doc.Close()

                    transaccion.Commit()
                    Return "pdf/manifiesto" + Correlativo.ToString + ".pdf"

                Else
                    Return "ERROR: No se obtuvieron los datos"
                End If
            Catch ex As SqlException
                transaccion.Rollback()
                Return "ERROR: " + ex.Message
            End Try
        End Using
    End Function

    <WebMethod(True)> _
    Public Function ConfirmarReceta(ByVal arregloMedicina As List(Of ClaseMedicina), ByVal idpaciente As Integer, ByVal idreceta As Integer, ByVal numeromedicamentos As Integer, ByVal Varproximaconsulta As String) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "update recetasencabezado " +
            "set estado='CONFIRMADO' " +
            "WHERE idreceta=" & idreceta & " "

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                Dim Elemento As New ClaseConsulta
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'POR SI MODIFICARON LA FECHA DE LA PROXIMA CONSULTA
                If Varproximaconsulta <> "" Then
                    comando.CommandText = "update recetasencabezado " +
                    "set proximaconsulta='" & Varproximaconsulta & "' " +
                    "WHERE idreceta=" & idreceta & " "
                    comando.ExecuteNonQuery()

                End If

                SQLInserta = "SET LANGUAGE spanish"
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'actualizamos la existencia de la medicina
                For i As Integer = 0 To arregloMedicina.Count - 1

                    SQLInserta = "update Tratamiento set existencia=(existencia-" & arregloMedicina(i).cantidad & ") " +
                    "where CodT='" & arregloMedicina(i).idmedicina & "'"

                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()

                Next

                result.Add(Elemento)

                '=================== C R E A C I O N   D E L   P  D  F ========================

                Dim SqlConsultaPaciente As String = "select r.idpaciente,dbo.fn_formato_fecha(r.proximaconsulta) as proximaconsulta,p.Nombre+' ' +p.Apellido nombrecompleto " +
                "from recetasencabezado r INNER JOIN Pacientes p ON r.idpaciente=p.CodE where idreceta=" & idreceta & ""
                Dim Paciente As Integer = 0
                Dim NombreCompleto As String = ""
                Dim ProximaConsulta As String = ""

                comando.CommandText = SqlConsultaPaciente
                Dim reader As SqlDataReader = comando.ExecuteReader

                reader.Read()
                If reader.HasRows Then
                    Paciente = reader("idpaciente")
                    NombreCompleto = reader("nombrecompleto")
                    ProximaConsulta = IIf(reader("proximaconsulta") IsNot DBNull.Value, reader("proximaconsulta"), "")
                    reader.Close()

                    'Lector.Read()
                    'If Lector.HasRows Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 15, 15, 3, 10)
                    Dim Correlativo As Integer = NumeroAleatorio()

                    Dim pd As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath("~\privado\pdf\manifiesto" + Correlativo.ToString + ".pdf"), FileMode.Create))
                    doc.AddTitle("Manifiesto")
                    doc.AddAuthor("BIO * NATURAL")
                    doc.AddCreationDate()

                    doc.Open()



                    Dim datoEmpresa As New Chunk("BIO * NATURAL", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD))
                    Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                    ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoEmpresa)

                    Dim datoEmpresad As New Chunk("7a Av. A 3-54 Zona 9, Ciudad de Guatemala", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                    ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoEmpresad)

                    Dim datoEmpresat As New Chunk("(502) 2331-7935", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                    ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoEmpresat)

                    'doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("RECETA MEDICA", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)
                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(3)

                    Encabezado.TotalWidth = 500
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({10, 60, 30})

                    Dim Celdan As PdfPCell = New PdfPCell
                    Celdan = New PdfPCell(New Paragraph("Paciente", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Nombre Completo", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Proxima Consulta", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Paciente, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(NombreCompleto, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(ProximaConsulta, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Encabezado.AddCell(Celdan)

                    doc.Add(Encabezado)


                    doc.Add(New Paragraph("      _______________________________________________________________________________", FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))


                    Dim datoSugerencia As New Chunk("** LE ROGAMOS TRAER SU RECETA A LA PRÓXIMA CONSULTA **", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia As New Paragraph(datoSugerencia)
                    ParrafoSugerencia.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia)

                    Dim datoSugerencia2 As New Chunk("LEA TODAS LAS INSTRUCCIONES ANTES DE COMENZAR A TOMAR SU MEDICINA", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia2 As New Paragraph(datoSugerencia2)
                    ParrafoSugerencia2.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia2)

                    Dim datoSugerencia3 As New Chunk("DEJAR 15 o 20 MINUTOS DE DIFERENCIA ENTRE CADA MEDICAMENTO, NO TOMARLOS JUNTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia3 As New Paragraph(datoSugerencia3)
                    ParrafoSugerencia3.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia3)

                    Dim datoSugerencia4 As New Chunk("TODO LO ESCRITO ES PARA USTED, HAGALO PORQUE ES PARTE DE SU TRATAMIENTO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoSugerencia4 As New Paragraph(datoSugerencia4)
                    ParrafoSugerencia4.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoSugerencia4)


                    Dim cell1 As PdfPCell = New PdfPCell()
                    Dim StrCodigoTratamiento As String = ""
                    Dim StrTratamiento As String = ""
                    Dim SqlConsultaReceta As String = ""

                    '####################### MEDICAMENTOS #########################'
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodM=t.CodT " +
                    "where r.CodR=" & idreceta & " and (r.CodM!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorMedicamentos As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               MEDICAMENTOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorMedicamentos.Read())

                        StrCodigoTratamiento = IIf(LectorMedicamentos("codigotratamiento") IsNot DBNull.Value, LectorMedicamentos("codigotratamiento"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorMedicamentos("detalletratamiento") IsNot DBNull.Value, LectorMedicamentos("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorMedicamentos.Close()

                    '################ I N D I C A C I O N E S ###############
                    SqlConsultaReceta = "select t.Descripcion codigotratamiento,t.Otros detalletratamiento " +
                    "from Recetas r " +
                    "INNER JOIN Tratamiento t ON r.CodT=t.CodT " +
                    "where r.CodR=" & idreceta & " and (r.CodT!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim Lector As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               INDICACIONES", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (Lector.Read())

                        StrCodigoTratamiento = IIf(Lector("codigotratamiento") IsNot DBNull.Value, Lector("codigotratamiento"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(Lector("detalletratamiento") IsNot DBNull.Value, Lector("detalletratamiento"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While
                    Lector.Close()

                    '####################### SINTOMAS #########################'
                    SqlConsultaReceta = "select s.descripcion sintoma,s.observaciones observaciones " +
                    "from Recetas r " +
                    "INNER JOIN sintoma s ON r.idsintoma=s.idsintoma " +
                    "where r.CodR=" & idreceta & " and (s.idsintoma!='')"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorSintomas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               SINTOMAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorSintomas.Read())

                        StrCodigoTratamiento = IIf(LectorSintomas("sintoma") IsNot DBNull.Value, LectorSintomas("sintoma"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorSintomas("observaciones") IsNot DBNull.Value, LectorSintomas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorSintomas.Close()

                    '####################### Notas #########################'
                    SqlConsultaReceta = "select " +
                    "t.CodT, t.Descripcion descripcion, t.Otros observaciones " +
                    "from tratamientosugerido ts " +
                    "INNER JOIN Tratamiento t ON ts.idtratamiento=t.CodT " +
                    "WHERE (ts.estatus=1) AND (getdate() BETWEEN ts.fechainicio and ts.fechafinal)"
                    comando.CommandText = SqlConsultaReceta
                    Dim LectorNotas As SqlDataReader = comando.ExecuteReader
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Paragraph("               NOTAS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLDITALIC)))
                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    While (LectorNotas.Read())

                        StrCodigoTratamiento = IIf(LectorNotas("descripcion") IsNot DBNull.Value, LectorNotas("descripcion"), "")
                        Dim CodigoTratamiento As New Paragraph(StrCodigoTratamiento, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD))
                        doc.Add(CodigoTratamiento)

                        StrTratamiento = IIf(LectorNotas("observaciones") IsNot DBNull.Value, LectorNotas("observaciones"), "")
                        Dim DetalleTratamiento As New Paragraph(StrTratamiento, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL))
                        doc.Add(DetalleTratamiento)

                    End While

                    LectorNotas.Close()

                    doc.Close()

                    transaccion.Commit()
                    Return "pdf/manifiesto" + Correlativo.ToString + ".pdf"

                Else
                    reader.Close()
                    Return "ERROR: No se obtuvieron los datos del paciente"
                End If


            Catch ex As SqlException
                transaccion.Rollback()

                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ConsultaSintoma(ByVal busqueda As String) As List(Of Sintoma)
        Dim SQLConsulta As String = ""

        SQLConsulta = "select idsintoma,descripcion " +
        "from sintoma where descripcion like '" & busqueda & "%' order by descripcion"


        Dim result As List(Of [Sintoma]) = New List(Of Sintoma)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New Sintoma

                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.nombre = IIf(reader("descripcion") IsNot DBNull.Value, reader("descripcion"), "No se pudo cargar")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New Sintoma
                Elemento.idsintoma = SQLConsulta
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod(True)> _
    Public Function fichaPaciente(ByVal idPaciente As Integer) As String

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                Dim SQLInserta As String = "SET LANGUAGE spanish"
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                Dim SQLConsulta As String = "SELECT Apellido," +
                   "Nombre," +
                   "dbo.fn_formato_fecha(getdate()) as fechaActual," +
                   "Estado," +
                   "Direccion," +
                   "Telefono," +
                   "convert(varchar, Fech_nac, 103)  as fechanacimiento," +
                   "Ocupacion," +
                   "Nacionalidad," +
                   "DatosExtras," +
                   "correo," +
                   "nhijos," +
                   "operaciones," +
                   "recomendado," +
                   "fechacreacion," +
                   "estatus," +
                   "genero," +
                   "foto," +
                   "idempresa,alergias," +
                   "DATEDIFF(year,Fech_nac,getdate() ) + " +
                     "case " +
                     "when ( Month(getdate()) < Month(Fech_nac) Or " +
                     "(Month(getdate()) = Month(Fech_nac) And " +
                     "Day(getdate()) < day(Fech_nac))) Then -1 else 0 end " +
                     "as edad " +
                   "FROM Pacientes where CodE=" & idPaciente & ""

                '=================== C R E A C I O N   D E L   P  D  F ========================
                comando.CommandText = SQLConsulta
                Dim reader As SqlDataReader = comando.ExecuteReader()

                reader.Read()
                If reader.HasRows Then

                    Dim Elemento As New WSPaciente.ClasePaciente

                    Elemento.idpaciente = idPaciente
                    Elemento.nombre = reader("Nombre")
                    Elemento.apellido = reader("Apellido")
                    Elemento.direccion = IIf(reader("Direccion") IsNot DBNull.Value, reader("Direccion"), "N/A")
                    Elemento.telefono = IIf(reader("Telefono") IsNot DBNull.Value, reader("Telefono"), "N/A")
                    Elemento.estado = IIf(reader("estado") IsNot DBNull.Value, reader("estado"), "N/A")
                    Elemento.estatus = IIf(reader("estatus") IsNot DBNull.Value, reader("estatus"), 1)
                    Elemento.fechanacimiento = IIf(reader("fechanacimiento") IsNot DBNull.Value, reader("fechanacimiento"), "00/00/2000")
                    Elemento.datosextras = IIf(reader("DatosExtras") IsNot DBNull.Value, reader("DatosExtras"), "N/A")
                    Elemento.nacionalidad = IIf(reader("Nacionalidad") IsNot DBNull.Value, reader("Nacionalidad"), "N/A")
                    Elemento.ocupacion = IIf(reader("Ocupacion") IsNot DBNull.Value, reader("Ocupacion"), "N/A")
                    Elemento.correo = IIf(reader("correo") IsNot DBNull.Value, reader("correo"), "N/A")
                    Elemento.nhijos = IIf(reader("nhijos") IsNot DBNull.Value, reader("nhijos"), 0)
                    Elemento.operaciones = IIf(reader("operaciones") IsNot DBNull.Value, reader("operaciones"), "N/A")
                    Elemento.recomendado = IIf(reader("recomendado") IsNot DBNull.Value, reader("recomendado"), "N/A")
                    Elemento.genero = IIf(reader("genero") IsNot DBNull.Value, reader("genero"), "unknown")
                    Elemento.foto = IIf(reader("foto") IsNot DBNull.Value, reader("foto"), "unknow.jpg")
                    Elemento.idempresa = IIf(reader("idempresa") IsNot DBNull.Value, reader("idempresa"), 1)
                    Elemento.edad = IIf(reader("edad") IsNot DBNull.Value, reader("edad"), 1)
                    Elemento.alergias = IIf(reader("alergias") IsNot DBNull.Value, reader("alergias"), "N/A")

                    Dim fechaActual As String = reader("fechaActual").ToString

                    reader.Close()
                    comando.Dispose()

                    reader.Close()

                    'Lector.Read()
                    'If Lector.HasRows Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 1, 1, 0, 2)
                    Dim Correlativo As Integer = NumeroAleatorio()

                    Dim pd As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath("~\privado\pdf\manifiesto" + Correlativo.ToString + ".pdf"), FileMode.Create))
                    doc.AddTitle("Manifiesto")
                    doc.AddAuthor("BIO * NATURAL")
                    doc.AddCreationDate()

                    doc.Open()

                    If Elemento.foto = "" Then Elemento.foto = "unknow.jpg"

                    Dim pathImagen As String = Server.MapPath("~\privado\fotos\" + Elemento.foto)

                    Try
                        Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(pathImagen)
                        jpg.Alignment = iTextSharp.text.Image.LEFT_ALIGN
                        jpg.ScalePercent(30.0F)
                        jpg.SetAbsolutePosition(20, 710)

                        doc.Add(jpg)
                    Catch ex As Exception
                        Elemento.foto = "unknow.jpg"
                        pathImagen = Server.MapPath("~\privado\fotos\" + Elemento.foto)
                        Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(pathImagen)
                        jpg.Alignment = iTextSharp.text.Image.LEFT_ALIGN
                        'jpg.WidthPercentage = 10%
                        jpg.ScalePercent(30.0F)
                        jpg.SetAbsolutePosition(20, 710)

                        doc.Add(jpg)
                    End Try

                    Dim TablaDatosClinica As DataTable = cargar_datatable("select top 1 nombre,telefono,fax,direccion,correo,eslogan from empresa")
                    If TablaDatosClinica.Rows.Count > 0 Then

                        'End If
                        'For i = 0 To TablaRecetas.Rows.Count - 1
                        'EMPEZAMOS ARMANDO EL OBJETO
                        'Dim Elemento As New Recetario
                        'Elemento.idreceta = TablaRecetas.Rows(i).Item("idreceta").ToString
                        'Elemento.fecha = TablaRecetas.Rows(i).Item("fecha").ToString

                        Dim datoEmpresa As New Chunk(TablaDatosClinica.Rows(0).Item("nombre").ToString, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk(TablaDatosClinica.Rows(0).Item("direccion").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk(TablaDatosClinica.Rows(0).Item("telefono").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)

                    Else

                        Dim datoEmpresa As New Chunk("BIO * NATURAL", FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresa As New Paragraph(datoEmpresa)
                        ParrafoEmpresa.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresa)

                        Dim datoEmpresad As New Chunk("7a Av. A 3-54 Zona 9, Ciudad de Guatemala", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresad As New Paragraph(datoEmpresad)
                        ParrafoEmpresad.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresad)

                        Dim datoEmpresat As New Chunk("(502) 2331-7935", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                        Dim ParrafoEmpresat As New Paragraph(datoEmpresat)
                        ParrafoEmpresat.Alignment = Element.ALIGN_CENTER
                        doc.Add(ParrafoEmpresat)
                    End If

                    'doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("FICHA", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(8)

                    Encabezado.TotalWidth = 570
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({20, 20, 10, 20, 20, 20, 20, 30})

                    Dim Celdan As PdfPCell = New PdfPCell
                    'ZERO FILA
                    Celdan = New PdfPCell(New Paragraph(" ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.Colspan = 8
                    Celdan.BorderWidthTop = 1
                    Encabezado.AddCell(Celdan)

                    'fila ZERO
                    Celdan = New PdfPCell(New Paragraph("Paciente", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.idpaciente, FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 7
                    Encabezado.AddCell(Celdan)

                    'PRIMERA FILA
                    Celdan = New PdfPCell(New Paragraph("NOMBRES", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 3
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("APELLIDOS", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 3
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Edad", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Direccion", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    'SEGUNDA FILA

                    Celdan = New PdfPCell(New Paragraph(Elemento.nombre, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 3
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.apellido, FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 3
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.edad, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.direccion, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    'TERCERA FILA
                    Celdan = New PdfPCell(New Paragraph("Alergias", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Telefono", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Hijos", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Ocupacion", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Estado Civil", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Recomendado", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("Operaciones", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 2
                    Encabezado.AddCell(Celdan)

                    'CUARTA FILA
                    Celdan = New PdfPCell(New Paragraph(Elemento.alergias, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.telefono, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.nhijos, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.ocupacion, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.estado, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.recomendado, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(Elemento.operaciones, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.VerticalAlignment = Element.ALIGN_BOTTOM
                    Celdan.Colspan = 2
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(" ", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.Colspan = 8
                    Celdan.BorderWidthBottom = 1
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph(fechaActual, FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 0
                    Celdan.Colspan = 8
                    Encabezado.AddCell(Celdan)

                    doc.Add(Encabezado)

                    Dim cb As PdfContentByte = pd.DirectContent
                    Dim largoLinea As Integer = doc.PageSize.Height - 183
                    'lineas que dividen la hoja completa
                    'LINEAS VERTICALES
                    cb.MoveTo(doc.PageSize.Width / 3, largoLinea)
                    cb.LineTo(doc.PageSize.Width / 3, 0)
                    cb.Stroke()

                    cb.MoveTo(doc.PageSize.Width / (3 / 2), largoLinea)
                    cb.LineTo(doc.PageSize.Width / (3 / 2), 0)
                    cb.Stroke()

                    'LINEAS HORIZONTALES
                    cb.MoveTo(0, largoLinea / 3 * 2)
                    cb.LineTo(doc.PageSize.Width, largoLinea / 3 * 2)
                    cb.Stroke()

                    cb.MoveTo(0, largoLinea / 3)
                    cb.LineTo(doc.PageSize.Width, largoLinea / 3)
                    cb.Stroke()

                    'cuadritos que se muestran en el encabezado
                    'VERTICALES
                    cb.MoveTo(420, 780)
                    cb.LineTo(420, 710)
                    cb.Stroke()

                    cb.MoveTo(450, 780)
                    cb.LineTo(450, 710)
                    cb.Stroke()

                    cb.MoveTo(480, 780)
                    cb.LineTo(480, 710)
                    cb.Stroke()

                    cb.MoveTo(510, 780)
                    cb.LineTo(510, 710)
                    cb.Stroke()

                    cb.MoveTo(540, 780)
                    cb.LineTo(540, 710)
                    cb.Stroke()

                    cb.MoveTo(570, 780)
                    cb.LineTo(570, 710)
                    cb.Stroke()

                    'HORIZONTALES
                    cb.MoveTo(420, 780)
                    cb.LineTo(570, 780)
                    cb.Stroke()

                    cb.MoveTo(420, 745)
                    cb.LineTo(570, 745)
                    cb.Stroke()

                    doc.Close()

                    transaccion.Commit()
                    Return "pdf/manifiesto" + Correlativo.ToString + ".pdf"
                    'Else
                    'Lector.Close()
                    Return "ERROR: No se obtuvieron datos de los tratamientos"
                    'End If
                Else
                    reader.Close()
                    Return "ERROR: No se obtuvieron los datos del paciente"
                End If


            Catch ex As SqlException
                transaccion.Rollback()

                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    <WebMethod(True)> _
    Public Function cargaSubPartes(ByVal idparte As String, ByVal sexo As String) As List(Of SubParte)

        Dim SqlBusqueda As String = "select " +
         "pp.idparte," +
         "pp.nombre," +
         "(select count(idparte) from parte p " +
           "where p.idpartesuperior=pp.idparte) hijos " +
        "from parte pp " +
        "where pp.idpartesuperior=" & idparte & " and pp.estatus=1 "
        If sexo = "M" Then
            SqlBusqueda = SqlBusqueda + "and masculino=1 ORDER BY pp.nombre"
        Else
            SqlBusqueda = SqlBusqueda + "and femenino=1 ORDER BY pp.nombre"
        End If

        Dim result As List(Of [SubParte]) = New List(Of SubParte)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()

                
                Dim cmd As New SqlCommand(SqlBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New SubParte

                    Elemento.idsubparte = reader("idparte")
                    Elemento.nombre = reader("nombre")
                    Elemento.hijos = reader("hijos")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New SubParte
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    Public Class SubParte
        Public idsubparte As Integer
        Public nombre As String
        Public hijos As Integer
    End Class

    Public Function NumeroAleatorio() As Integer
        Dim LimiteMinimo As Integer = 0
        Dim LimiteMaximo As Integer = 10
        Dim Semilla As Integer = CInt(Date.Now.Millisecond)
        Dim NumberRandom As New Random(Semilla)
        Dim Aleatorio As Integer = NumberRandom.Next(LimiteMinimo, LimiteMaximo)
        Return Aleatorio
    End Function

    <WebMethod()> _
    Public Function ConsultaHistorial(ByVal idpaciente As String, ByVal tipoconsulta As String) As List(Of Receta)
        Dim SQLConsulta As String = "select r.idsintoma,r.CodT,r.CodM,CONVERT(VARCHAR(10), re.fecha, 103) fecha,s.descripcion " +
        "from Recetas r " +
        "INNER JOIN recetasencabezado re ON re.idreceta= r.CodR " +
        "INNER JOIN sintoma s ON r.idsintoma=s.idsintoma " +
        "where r.CodR in (select idreceta from recetasencabezado where idpaciente=" & idpaciente & ") order by re.fecha desc"

        Dim result As List(Of [Receta]) = New List(Of Receta)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New Receta

                    Elemento.idsintoma = IIf(reader("idsintoma") IsNot DBNull.Value, reader("idsintoma"), "N/A")
                    Elemento.sintoma = IIf(reader("descripcion") IsNot DBNull.Value, reader("descripcion"), "N/A")
                    Elemento.idtratamiento = IIf(reader("CodT") IsNot DBNull.Value Or reader("CodT") = "", reader("CodT"), "N/A")
                    Elemento.idmedicina = IIf(reader("CodM") IsNot DBNull.Value Or reader("CodM") = "", reader("CodM"), "")
                    Elemento.fecha = reader("fecha")

                    If Elemento.idtratamiento = "" Then Elemento.idtratamiento = "N/A"
                    If Elemento.idmedicina = "" Then Elemento.idmedicina = "N/A"
                    If Elemento.idsintoma = "" Then Elemento.idsintoma = "N/A"
                    
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New Receta
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class Recetario
        Public idreceta As Integer
        Public idpaciente As String
        Public fecha As String
        Public indicacion As List(Of Indicacion)
        Public medicamento As List(Of Medicamento)
        Public sintoma As List(Of Sintoma)
    End Class

    Public Class Indicacion
        Public idindicacion As String
        Public nombre As String
        Public observaciones As String

    End Class

    Public Class Medicamento
        Public idmedicamento As String
        Public nombre As String
        Public observaciones As String
    End Class

    Public Class Sintoma
        Public idsintoma As String
        Public nombre As String
        Public observaciones As String
    End Class

    <WebMethod()> _
    Public Function ConsultaHistorialNuevo(ByVal idpaciente As String, ByVal tipoconsulta As String) As List(Of Recetario)

        Dim result As List(Of [Recetario]) = New List(Of Recetario)()

        Dim arregloIndicaciones As List(Of Indicacion)
        Dim arregloMedicamentos As List(Of Medicamento)
        Dim arregloSintomas As List(Of Sintoma)

        Dim TablaRecetas As DataTable = cargar_datatable("select idreceta,CONVERT(varchar,fecha,103) fecha from recetasencabezado where idpaciente=" & idpaciente & "")
        If TablaRecetas.Rows.Count > 0 Then

            Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
                Try
                    connection.Open()
                    Dim SQLConsulta As String = ""
                    Dim comando As SqlCommand
                    Dim reader As SqlDataReader
                    For i = 0 To TablaRecetas.Rows.Count - 1
                        'EMPEZAMOS ARMANDO EL OBJETO
                        Dim Elemento As New Recetario
                        Elemento.idreceta = TablaRecetas.Rows(i).Item("idreceta").ToString
                        Elemento.fecha = TablaRecetas.Rows(i).Item("fecha").ToString
                        'CONSULTA TOMANDO EN CUENTA EL RESULTADO DE LA TABLARECETAS
                        SQLConsulta = "select r.CodT idindicacion," +
                            "r.CodM idmedicamento," +
                            "r.idsintoma idsintoma," +
                            "s.descripcion sintoma," +
                            "r.obssintomas," +
                            "r.obsindicaciones," +
                            "r.obsmedicamentos " +
                        "from recetas r " +
                        "LEFT JOIN sintoma s ON r.idsintoma=s.idsintoma " +
                        "where codr=" & TablaRecetas.Rows(i).Item("idreceta").ToString & ""
                        comando = New SqlCommand(SQLConsulta, connection)
                        reader = comando.ExecuteReader

                        arregloIndicaciones = New List(Of Indicacion)()
                        arregloMedicamentos = New List(Of Medicamento)()
                        arregloSintomas = New List(Of Sintoma)()

                        Dim datoIndicacion As String = ""
                        Dim datoMedicamento As String = ""
                        Dim datoSintoma As String = ""

                        While (reader.Read())

                            Dim ElementoIndicacion As New Indicacion
                            ElementoIndicacion.idindicacion = IIf(reader("idindicacion") IsNot DBNull.Value, reader("idindicacion"), "N/A")
                            If ElementoIndicacion.idindicacion = "" Then ElementoIndicacion.idindicacion = "N/A"
                            ElementoIndicacion.nombre = IIf(reader("idindicacion") IsNot DBNull.Value, reader("idindicacion"), "N/A")
                            If ElementoIndicacion.nombre = "" Then ElementoIndicacion.nombre = "N/A"
                            ElementoIndicacion.observaciones = IIf(reader("obsindicaciones") IsNot DBNull.Value, reader("obsindicaciones"), "N/A")
                            If ElementoIndicacion.observaciones = "" Then ElementoIndicacion.observaciones = "N/A"
                            arregloIndicaciones.Add(ElementoIndicacion)


                            Dim ElementoMedicamento As New Medicamento
                            ElementoMedicamento.idmedicamento = IIf(reader("idmedicamento") IsNot DBNull.Value, reader("idmedicamento"), "N/A")
                            If ElementoMedicamento.idmedicamento = "" Then ElementoMedicamento.idmedicamento = "N/A"
                            ElementoMedicamento.nombre = IIf(reader("idmedicamento") IsNot DBNull.Value, reader("idmedicamento"), "N/A")
                            If ElementoMedicamento.nombre = "" Then ElementoMedicamento.nombre = "N/A"
                            ElementoMedicamento.observaciones = IIf(reader("obsmedicamentos") IsNot DBNull.Value, reader("obsmedicamentos"), "N/A")
                            If ElementoMedicamento.observaciones = "" Then ElementoMedicamento.observaciones = "N/A"
                            arregloMedicamentos.Add(ElementoMedicamento)

                            Dim ElementoSintoma As New Sintoma
                            ElementoSintoma.idsintoma = IIf(reader("idsintoma") IsNot DBNull.Value, reader("idsintoma"), 1)
                            ElementoSintoma.nombre = IIf(reader("sintoma") IsNot DBNull.Value, reader("sintoma"), "N/A")
                            If ElementoMedicamento.nombre = "" Then ElementoMedicamento.nombre = "N/A"
                            ElementoSintoma.observaciones = IIf(reader("obssintomas") IsNot DBNull.Value, reader("obssintomas"), "N/A")
                            If ElementoSintoma.observaciones = "" Then ElementoSintoma.observaciones = "N/A"
                            arregloSintomas.Add(ElementoSintoma)

                            'result.Add(Elemento)
                        End While

                        reader.Close()
                        comando.Dispose()

                        Elemento.indicacion = arregloIndicaciones
                        Elemento.medicamento = arregloMedicamentos
                        Elemento.sintoma = arregloSintomas

                        result.Add(Elemento)
                    Next
                Catch ex As SqlException
                    Dim Elemento As New Recetario
                    result.Add(Elemento)
                    Return result
                End Try
            End Using
        End If
        Return result

    End Function

    Public Shared Function cargar_datatable(ByVal strsql As String) As System.Data.DataTable
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            'connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("EjemploConString").ConnectionString
            Dim ds As New System.Data.DataTable
            Try

                connection.Open()
                Dim comando As New SqlCommand(strsql, connection)
                Dim da As New SqlDataAdapter(comando)

                da.Fill(ds)
                comando.Dispose()
                da.Dispose()

            Catch ex As SqlException

            End Try
            Return ds
        End Using
    End Function

    <WebMethod(True)> _
    Public Function PacienteReceta(ByVal idreceta As Integer) As List(Of Receta)
        Dim SQLBusqueda As String

        SQLBusqueda = "select r.CodT idtratamiento,r.CodM idmedicina,m.existencia,CONVERT(VARCHAR,re.proximaconsulta,103) proximaconsulta " +
        "from " +
        "Recetas r " +
        "LEFT JOIN Tratamiento m ON r.CodM=m.CodT " +
         "INNER JOIN recetasencabezado re ON r.CodR=re.idreceta " +
         "where r.CodR=" & idreceta & ";"

        Dim result As List(Of [Receta]) = New List(Of Receta)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New Receta

                    Elemento.idtratamiento = IIf(reader("idtratamiento") IsNot DBNull.Value, reader("idtratamiento"), "")
                    Elemento.idmedicina = IIf(reader("idmedicina") IsNot DBNull.Value, reader("idmedicina"), "")
                    'Elemento.medicina = IIf(reader("medicina") IsNot DBNull.Value, reader("medicina"), "")
                    Elemento.existencia = IIf(reader("existencia") IsNot DBNull.Value, reader("existencia"), 0)
                    Elemento.fecha = IIf(reader("proximaconsulta") IsNot DBNull.Value, reader("proximaconsulta"), "2000/01/01")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New Receta

                Elemento.idtratamiento = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod(True)> _
    Public Function DespacharRecetas() As List(Of ClasePaciente)
        Dim SQLBusqueda As String

        SQLBusqueda = "SELECT r.idreceta,r.idpaciente,p.Nombre + ' ' + p.Apellido nombrecompleto " +
        "FROM recetasencabezado r INNER JOIN Pacientes p ON r.idpaciente=p.CodE " +
        "where r.estado='PENDIENTE' and CONVERT(varchar,r.fecha,102)=CONVERT(varchar,getdate(),102)"

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombrepaciente = reader("nombrecompleto")
                    Elemento.idconsulta = reader("idreceta")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente

                Elemento.idpaciente = 0
                Elemento.nombrepaciente = SQLBusqueda
                Elemento.idconsulta = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function RecetaGuardar(ByVal arregloSintoma As List(Of ClaseConsulta), ByVal arregloTratamiento As List(Of ClaseTratamiento), ByVal arregloMedicina As List(Of ClaseMedicina), ByVal usuario As String, ByVal idpaciente As String) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "INSERT INTO recetasencabezado " +
           "(idpaciente " +
           ",fecha " +
           ",iddoctor " +
           ",estado,proximaconsulta) " +
           "VALUES " +
           "(" & idpaciente & "" +
           ",getdate() " +
           ",1" +
           ",'PENDIENTE',dbo.fn_proxima_consulta(getdate()))"
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseConsulta

                'comando.CommandText = "SET IDENTITY_INSERT Recetas ON"
                'comando.ExecuteNonQuery()

                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim CodigoReceta As Integer = comando.ExecuteScalar

                'comando.CommandText = "SET IDENTITY_INSERT Recetas OFF"
                'comando.ExecuteNonQuery()


                Dim CodigoMedicina As String = ""
                Dim CodigoTratamiento As String = ""
                Dim CodigoSintoma As Integer = 0

                'ESTO SE EJECUTARA CUANDO EXISTAN MAS TRATAMIENTOS QUE MEDICAMENTOS A LA RECETA

                'Dim mayor As Integer = 

                'If arregloTratamiento.Count > arregloMedicina.Count And arregloMedicina.Count > arregloSintoma.Count Then
                Dim NumerodeRepeticiones As Integer = 0
                NumerodeRepeticiones = burbuja(arregloTratamiento.Count, arregloMedicina.Count, arregloSintoma.Count)
                '=================================TRATAMIENTO'
                'If arregloTratamiento.Count > arregloMedicina.Count And arregloMedicina.Count > arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                'ElseIf arregloTratamiento.Count = arregloMedicina.Count And arregloMedicina.Count > arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                'ElseIf arregloTratamiento.Count > arregloMedicina.Count And arregloMedicina.Count = arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                'ElseIf arregloTratamiento.Count > arregloSintoma.Count And arregloSintoma.Count > arregloMedicina.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                'ElseIf arregloTratamiento.Count = arregloSintoma.Count And arregloSintoma.Count > arregloMedicina.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                '    '===================================MEDICINA'
                'ElseIf arregloMedicina.Count > arregloTratamiento.Count And arregloTratamiento.Count > arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloMedicina.Count
                'ElseIf arregloMedicina.Count = arregloTratamiento.Count And arregloTratamiento.Count > arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloMedicina.Count
                'ElseIf arregloMedicina.Count > arregloTratamiento.Count And arregloTratamiento.Count = arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloMedicina.Count
                'ElseIf arregloMedicina.Count = arregloSintoma.Count And arregloSintoma.Count > arregloTratamiento.Count Then
                '    NumerodeRepeticiones = arregloMedicina.Count
                'ElseIf arregloMedicina.Count > arregloSintoma.Count And arregloSintoma.Count > arregloSintoma.Count Then
                '    NumerodeRepeticiones = arregloMedicina.Count
                '    '======================================SINTOMAS'
                'ElseIf arregloSintoma.Count > arregloMedicina.Count And arregloMedicina.Count > arregloTratamiento.Count Then
                '    NumerodeRepeticiones = arregloSintoma.Count
                'ElseIf arregloSintoma.Count = arregloMedicina.Count And arregloMedicina.Count > arregloTratamiento.Count Then
                '    NumerodeRepeticiones = arregloTratamiento.Count
                'ElseIf arregloSintoma.Count > arregloMedicina.Count And arregloMedicina.Count = arregloTratamiento.Count Then
                '    NumerodeRepeticiones = arregloSintoma.Count
                'ElseIf arregloSintoma.Count = arregloTratamiento.Count And arregloTratamiento.Count > arregloMedicina.Count Then
                '    NumerodeRepeticiones = arregloSintoma.Count
                'ElseIf arregloSintoma.Count > arregloTratamiento.Count And arregloTratamiento.Count > arregloMedicina.Count Then
                '    NumerodeRepeticiones = arregloSintoma.Count
                '    'SI SON IGUALES
                'ElseIf arregloSintoma.Count = arregloTratamiento.Count And arregloTratamiento.Count = arregloMedicina.Count Then
                '    NumerodeRepeticiones = arregloSintoma.Count
                'End If
                Dim obsSintomas, obsIndicaciones, obsMedicamentos As String
                For i As Integer = 0 To NumerodeRepeticiones - 1

                    If i > (arregloTratamiento.Count - 1) Then
                        CodigoTratamiento = ""
                        obsIndicaciones = ""
                    Else
                        CodigoTratamiento = arregloTratamiento(i).idtratamiento
                        obsIndicaciones = arregloTratamiento(i).observaciones
                    End If

                    If i > (arregloMedicina.Count - 1) Then
                        CodigoMedicina = ""
                        obsMedicamentos = ""
                    Else
                        CodigoMedicina = arregloMedicina(i).idmedicina
                        obsMedicamentos = arregloMedicina(i).observaciones
                    End If

                    If i > (arregloSintoma.Count - 1) Then
                        CodigoSintoma = 0
                        obsSintomas = ""
                    Else
                        CodigoSintoma = arregloSintoma(i).idsintoma
                        obsSintomas = arregloSintoma(i).observaciones
                    End If

                    SQLInserta = "INSERT INTO dbo.Recetas " +
                    "(CodR,CodT,CodM,Cant,idsintoma,obssintomas,obsindicaciones,obsmedicamentos) " +
                    "VALUES " +
                    "(" & CodigoReceta & ",'" & CodigoTratamiento & "','" & CodigoMedicina & "',0," & CodigoSintoma & "," +
                    "'" & obsSintomas & "','" & obsIndicaciones & "','" & obsMedicamentos & "') "

                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()


                Next

                SQLInserta = "update consulta set estado='CONFIRMADO' where idconsulta in (select c.idconsulta " +
                "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                "where idpaciente=" & idpaciente & ")"

                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()


                transaccion.Commit()
                Return "Datos guardados correctamente"

            Catch ex As SqlException
                transaccion.Rollback()
                Return "ERROR: " + ex.Message
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ConsultaGuardar(ByVal nombre As String, ByVal telefono As String, ByVal fax As String, ByVal direccion As String, ByVal correo As String, ByVal eslogan As String) As List(Of [ClaseConsulta])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into Consulta " +
            "(nombre,telefono,fax,direccion,correo,eslogan,estatus) " +
            "values " +
            "('" & nombre & "','" & telefono & "','" & fax & "'," +
            "'" & direccion & "','" & correo & "','" & eslogan & "',1)"
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseConsulta
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idsintoma = Codigo
                Elemento.descripcion = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)
                transaccion.Commit()
                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = 0
                Elemento.descripcion = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ConsultaBuscar(ByVal idsintoma As String) As List(Of [ClaseConsulta])
        Dim SQLConsulta As String = "select idsintoma,nombre,telefono,fax,direccion,correo,eslogan " +
                                    "from Consulta where idsintoma=" & idsintoma & ""
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseConsulta

                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.descripcion = reader("nombre")


                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = 0
                Elemento.descripcion = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function ConsultaActualizar(ByVal nombre As String, ByVal telefono As String, ByVal fax As String, ByVal direccion As String, ByVal correo As String, ByVal eslogan As String, ByVal idsintoma As String) As List(Of [ClaseConsulta])
        Dim SQLActualiza As String = "update Consulta set nombre='" & nombre & "',telefono='" & telefono & "'," +
            "fax='" & fax & "',direccion='" & direccion & "',correo='" & correo & "',eslogan='" & eslogan & "' where idsintoma=" & idsintoma & ""
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseConsulta

                Elemento.idsintoma = idsintoma
                Elemento.descripcion = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = idsintoma
                Elemento.descripcion = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ConsultaEliminar(ByVal idsintoma As String) As List(Of [ClaseConsulta])
        Dim SQLElimina As String = "update Consulta set estatus=0 where idsintoma=" & idsintoma & ""
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = idsintoma
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = idsintoma
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function ConsultaDatos(ByVal idsubpartedelcuerpo As String, ByVal sexo As String) As List(Of ClaseConsulta)
        Dim SQLConsulta As String = ""
        If sexo = "M" Then
            SQLConsulta = "select idsintoma,descripcion from sintoma where idpartedelcuerpo=" & idsubpartedelcuerpo & " and masculino=1 "
        Else
            SQLConsulta = "select idsintoma,descripcion from sintoma where idpartedelcuerpo=" & idsubpartedelcuerpo & " and femenino=1 "
        End If
        Dim result As List(Of [ClaseConsulta]) = New List(Of ClaseConsulta)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseConsulta

                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.descripcion = reader("descripcion")
                    'MsgBox(Elemento.descripcion)
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseConsulta
                Elemento.idsintoma = 0
                Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    'opcion 1 = indicaciones, opcion 2 = medicamentos,3 autocomplete sin distincion si es indicacion o medicamento
    <WebMethod()> _
    Public Function ConsultaTratamiento(ByVal busqueda As String, ByVal opcion As Integer) As List(Of ClaseTratamiento)
        Dim SQLConsulta As String = ""
        If opcion = 3 Then
            SQLConsulta = "select CodT idtratamiento,Descripcion descripcion,existencia  " +
            "from tratamiento where CodT like '" & busqueda & "%' order by descripcion"
        Else
            SQLConsulta = "select CodT idtratamiento,Descripcion descripcion,existencia " +
                "from tratamiento where idtipotratamiento=" & opcion & " and CodT like '" & busqueda & "%' order by descripcion"
        End If

        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseTratamiento

                    Elemento.idtratamiento = reader("idtratamiento")
                    'Elemento.nombre = "" 'reader("nombre")
                    Elemento.descripcion = IIf(reader("descripcion") IsNot DBNull.Value, reader("descripcion"), "No se pudo cargar")
                    Elemento.existencia = reader("existencia")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = SQLConsulta
                'Elemento.nombre = ""
                Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod(True)> _
    Public Function ConsultaCreaPacientes(ByVal cadenabusqueda As String, ByVal usuario As String) As List(Of ClasePaciente)

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()

                Dim CodigoMedico As Integer = 0
                Dim SqlBusquedaMedico As String = "select idempleado from usuario where idusuario='" & usuario & "'"

                Dim comando As New SqlCommand(SqlBusquedaMedico, connection)
                Dim readerdoctor As SqlDataReader = comando.ExecuteReader()


                readerdoctor.Read()
                If readerdoctor.HasRows() Then
                    CodigoMedico = readerdoctor("idempleado")
                End If

                readerdoctor.Close()

                Dim SQLBusqueda As String
                If cadenabusqueda.Length > 0 Then
                    cadenabusqueda = Right(cadenabusqueda, cadenabusqueda.Length - 1)
                    SQLBusqueda = "select c.idconsulta,c.idpaciente,p.Nombre+' '+p.Apellido nombrecompleto,c.correlativo " +
                    "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                    "where CONVERT(varchar,c.fechaconsulta,102)=CONVERT(varchar,getdate(),102) and " +
                    "c.iddoctor=" & CodigoMedico & " and c.estado='PENDIENTE' and c.idpaciente NOT in (" & cadenabusqueda & ") " +
                    "ORDER BY c.idconsulta asc"

                Else
                    SQLBusqueda = "select c.idconsulta,c.idpaciente,p.Nombre+' '+p.Apellido nombrecompleto,c.correlativo " +
                    "from consulta c INNER JOIN Pacientes p ON c.idpaciente=p.CodE " +
                    "where CONVERT(varchar,c.fechaconsulta,102)=CONVERT(varchar,getdate(),102) and " +
                    "c.iddoctor=" & CodigoMedico & " and c.estado='PENDIENTE' ORDER BY c.idconsulta asc"
                End If

                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombrepaciente = reader("nombrecompleto")
                    Elemento.idconsulta = reader("idconsulta")
                    Elemento.correlativo = reader("correlativo")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente

                Elemento.idpaciente = 0
                Elemento.nombrepaciente = "ERROR " + ex.Message
                Elemento.idconsulta = 0

                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod(True)> _
    Public Function ConsultaPacientesEnespera(ByVal contador As Integer, ByVal usuario As String) As Integer

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim CodigoMedico As Integer = 0
                Dim SqlBusquedaMedico As String = "select idempleado from usuario where idusuario='" & usuario & "'"

                Dim comando As New SqlCommand(SqlBusquedaMedico, connection)
                Dim readerdoctor As SqlDataReader = comando.ExecuteReader()


                readerdoctor.Read()
                If readerdoctor.HasRows() Then
                    CodigoMedico = readerdoctor("idempleado")
                End If

                readerdoctor.Close()

                Dim SQLBusqueda As String = "select count(idconsulta) as numeropacientes from consulta where estado='PENDIENTE' and CONVERT(varchar,fechaconsulta,102)=CONVERT(varchar,getdate(),102) and iddoctor=" & CodigoMedico & ""

                Dim cmd As New SqlCommand(SQLBusqueda, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                reader.Read()
                If reader.HasRows() Then
                    Return reader("numeropacientes")
                Else
                    Return 0
                End If
                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException

                Return -1
            End Try
        End Using

    End Function

    Public Function burbuja(ByVal n1 As Integer, ByVal n2 As Integer, ByVal n3 As Integer) As Integer
        Dim Datos(2) As Integer
        Dim Temp, a, b As Integer
        
        Datos(0) = n1
        Datos(1) = n2
        Datos(2) = n3

        For b = 0 To 1
            For a = 0 To 1

                If Datos(a) > Datos(a + 1) Then 'con solo cambiar el < se puede cambiar el orden 
                    Temp = Datos(a + 1)
                    Datos(a + 1) = Datos(a)
                    Datos(a) = Temp
                End If
            Next
        Next
        'RETORNAMOS EL NUMERO MAYOR
        Return Datos(2)
    End Function

    Public Class ClaseMedicina
        Public idmedicina As String
        Public descripcionmedicina As String
        Public aplicacionmedicina As String
        Public existenciamedicina As String
        Public proveedormedicina As String
        Public preciomedicina As String
        Public estatusmedicina As String
        Public cantidad As Integer
        Public observaciones As String
    End Class

    Public Class ClaseTratamiento
        Public idtratamiento As String
        Public nombre As String
        Public descripcion As String
        Public mensaje As String
        Public observaciones As String
        Public existencia As Integer
    End Class

    Public Class ClaseConsulta
        Public idsintoma As Integer
        Public descripcion As String
        Public mensaje As String
        Public observaciones As String
    End Class

    Public Class Receta
        Public idtratamiento As String
        Public tratamiento As String
        Public idmedicina As String
        Public medicina As String
        Public existencia As Integer
        Public idsintoma As String
        Public sintoma As String
        Public fecha As String
    End Class

    Public Class ClasePaciente
        Public idpaciente As Integer
        Public nombrepaciente As String
        Public fotopaciente As String
        Public idconsulta As Integer
        Public contador As Integer
        Public correlativo As Integer
    End Class

End Class