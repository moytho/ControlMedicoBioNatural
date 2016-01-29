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
 Public Class WSIngresomedicina
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function mostrarEnReporte(ByVal estado As Boolean, ByVal correlativo As Integer) As String
        Dim SQLConsulta As String = ""
            SQLConsulta = "update Tratamiento set mostrarEnReporte='" & estado & "' where correlativo=" & correlativo & ""
        
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                Return "Datos correctamente"
            Catch ex As SqlException
                Return "ERROR: " + ex.Message
            End Try
        End Using
        End Function

    <WebMethod()> _
    Public Function datosProductoPorLetra(ByVal letra As String) As List(Of Medicina)
        Dim SQLConsulta As String = ""
        If letra = "1" Then
            SQLConsulta = "select correlativo,CodT,Descripcion,mostrarEnReporte from tratamiento where idtipotratamiento=2 and ISNUMERIC(SUBSTRING(LTRIM(CodT), 1, 1))=1"

        Else
            SQLConsulta = "select correlativo,CodT,Descripcion,mostrarEnReporte from tratamiento where idtipotratamiento=2 and CodT like '" & letra & "%'"
        End If
        'SQLConsulta = ""
        'MsgBox(SQLConsulta)
        Dim result As List(Of Medicina) = New List(Of Medicina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New Medicina
                    Elemento.correlativo = reader("correlativo")
                    Elemento.descripcion = IIf(reader("Descripcion") Is DBNull.Value, "", reader("Descripcion"))
                    Elemento.mostrarEnReporte = IIf(reader("mostrarEnReporte") Is DBNull.Value, False, reader("mostrarEnReporte"))
                    Elemento.CodT = reader("CodT")
                    'Elemento.descripcion = reader("Cod")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New Medicina
                Elemento.CodT = 0
                'Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod(True)> _
    Public Function reporteMedicinaAnterior(ByVal tiporeporte As String, ByVal cantidad As Integer) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = ""

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                'actualizamos la existencia de la medicina
                '=================== C R E A C I O N   D E L   P  D  F ========================

                Dim SqlConsultaMedicina As String = "select CodM idmedicina,Descripcion medicina,Exis existencia from Medicina order by existencia"
                Dim TablaMedicamentos As New DataTable
                comando.CommandText = SqlConsultaMedicina
                Dim reader As SqlDataReader = comando.ExecuteReader

                TablaMedicamentos.Load(reader)

                If TablaMedicamentos.Rows.Count > 0 Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 1, 1, 4, 2)
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

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("EXISTENCIA DE MEDICAMENTOS", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)


                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(3)

                    Encabezado.TotalWidth = 500
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({20, 60, 20})

                    Dim Celdan As PdfPCell = New PdfPCell
                    Celdan = New PdfPCell(New Paragraph("CODIGO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("MEDICAMENTO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("EXISTENCIA", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    For fila = 0 To TablaMedicamentos.Rows.Count - 1


                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("idmedicina").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("medicina").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("existencia").ToString, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                    Next

                    doc.Add(Encabezado)




                    doc.Add(New Paragraph(" "))

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
    Public Function reporteMedicina(ByVal tiporeporte As String, ByVal cantidad As String) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = ""
        Dim sqlInsertaOrden As String = ""
        If tiporeporte = 2 Then
            sqlInsertaOrden = "order by existencia desc"
        ElseIf tiporeporte = 1 Then
            sqlInsertaOrden = "order by existencia asc"
        ElseIf tiporeporte = 3 Then
            sqlInsertaOrden = "order by CodT"
        End If
        Dim sqlInsertaTop As String = ""
        If cantidad <> "Todos" Then
            sqlInsertaTop = " Top " + cantidad.ToString
        End If
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                'actualizamos la existencia de la medicina
                '=================== C R E A C I O N   D E L   P  D  F ========================

                Dim SqlConsultaMedicina As String = "select " + sqlInsertaTop + "CodT idmedicina,Descripcion medicina,existencia existencia from Tratamiento where idtipotratamiento=2 and (TratamientoPadre is null OR TratamientoPadre='') and mostrarEnReporte=1 " + sqlInsertaOrden
                Dim TablaMedicamentos As New DataTable
                comando.CommandText = SqlConsultaMedicina
                Dim reader As SqlDataReader = comando.ExecuteReader

                TablaMedicamentos.Load(reader)

                If TablaMedicamentos.Rows.Count > 0 Then

                    Dim doc As Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 1, 1, 4, 2)
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

                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    Dim datoDocumento As New Chunk("EXISTENCIA DE MEDICAMENTOS", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoDocumento As New Paragraph(datoDocumento)
                    ParrafoDocumento.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoDocumento)

                    Dim fechaImpresion1 As New Chunk("FECHA DE IMPRESION: " + Date.Now.ToString, FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD))
                    Dim ParrafoFechaImpresion1 As New Paragraph(fechaImpresion1)
                    ParrafoFechaImpresion1.Alignment = Element.ALIGN_CENTER
                    doc.Add(ParrafoFechaImpresion1)


                    doc.Add(New Paragraph("͏͏", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))


                    '==========CREAMOS UNA TABLA PARA MOSTRAR LOS DATOS DEL PACIENTE=============
                    Dim Encabezado As PdfPTable = New PdfPTable(3)

                    Encabezado.TotalWidth = 500
                    Encabezado.LockedWidth = True
                    Encabezado.SetWidths({20, 60, 20})

                    Dim Celdan As PdfPCell = New PdfPCell
                    Celdan = New PdfPCell(New Paragraph("CODIGO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("MEDICAMENTO", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    Celdan = New PdfPCell(New Paragraph("EXISTENCIA", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                    Celdan.BorderWidth = 1
                    Encabezado.AddCell(Celdan)

                    For fila = 0 To TablaMedicamentos.Rows.Count - 1


                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("idmedicina").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("medicina").ToString, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                        Celdan = New PdfPCell(New Paragraph(TablaMedicamentos.Rows(fila).Item("existencia").ToString, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)))
                        Celdan.BorderWidth = 0
                        Encabezado.AddCell(Celdan)

                    Next

                    doc.Add(Encabezado)

                    doc.Add(ParrafoFechaImpresion1)


                    doc.Add(New Paragraph(" "))

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
    Public Function GuardarIngresoMedicina(ByVal arregloEncabezado As List(Of ClaseEncabezado), ByVal arregloDetalle As List(Of ClaseDetalle)) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into compra " +
            "(factura,serie,nitproveedor,fecha,idusuario,total) " +
            "values " +
            "(" & arregloEncabezado(0).factura & ",'" & arregloEncabezado(0).serie & "'," +
            "'" & arregloEncabezado(0).nitproveedor & "','" & arregloEncabezado(0).fecha & "'," +
            "'" & arregloEncabezado(0).idusuario & "'," & arregloEncabezado(0).total & ") "

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion

            Try

                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'SQLInserta = "SET LANGUAGE spanish"
                'comando.CommandText = SQLInserta
                'comando.ExecuteNonQuery()

                'actualizamos la existencia de la medicina
                For i As Integer = 0 To arregloDetalle.Count - 1

                    SQLInserta = "insert into compradetalle " +
                    " (factura,serie,nitproveedor,idmedicina,cantidad,preciounitario,subtotal) " +
                    "values " +
                    "(" & arregloEncabezado(0).factura & ",'" & arregloEncabezado(0).serie & "'," +
                    "'" & arregloEncabezado(0).nitproveedor & "','" & arregloDetalle(i).idmedicina & "'," +
                    "" & arregloDetalle(i).cantidad & "," & arregloDetalle(i).preciounitario & "," & arregloDetalle(i).subtotal & ")"

                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()

                    SQLInserta = "update Tratamiento set Existencia=Existencia+" & Val(arregloDetalle(i).cantidad) & " where CodT='" & arregloDetalle(i).idmedicina & "'"

                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()

                Next

                transaccion.Commit()
                Return "Datos creados correctamente"
                

            Catch ex As SqlException
                transaccion.Rollback()

                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function datosproveedor() As List(Of claseproveedor)
        Dim SQLConsulta As String = "select Nit,Nombre from proveedores where estatus=1 order by Nombre"
        'MsgBox(SQLConsulta)
        Dim result As List(Of claseproveedor) = New List(Of claseproveedor)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New claseproveedor

                    Elemento.Nit = reader("Nit")
                    Elemento.Nombre = reader("Nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New claseproveedor
                Elemento.Nit = 0
                Elemento.Nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class claseproveedor
        Public Nit As String

        Public Nombre As String
        Public mensaje As String

    End Class

    <WebMethod()> _
    Public Function datosunidadmedida() As List(Of claseunidad)
        Dim sqlconsulta As String = "select idunidad,descripcion from unidadmedida  where estatus=1 order by descripcion"
        'msgbox(sqlconsulta)
        Dim result As list(Of claseunidad) = New list(Of claseunidad)()
        Using connection As New sqlconnection(system.configuration.configurationmanager.connectionstrings("strconexion").connectionstring)
            Try
                connection.open()
                Dim cmd As New sqlcommand(sqlconsulta, connection)
                Dim reader As sqldatareader = cmd.executereader()
                While (reader.read())
                    Dim elemento As New claseunidad

                    elemento.idunidad = reader("idunidad")
                    elemento.descripcion = reader("descripcion")
                    result.add(elemento)
                End While

                reader.close()
                cmd.dispose()
            Catch ex As sqlexception
                Dim elemento As New claseunidad

                elemento.idunidad = 0
                elemento.descripcion = "sin datos " + ex.message
                result.add(elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class claseunidad
        Public idunidad As Integer

        Public descripcion As String
        Public mensaje As String



    End Class

    <WebMethod()> _
    Public Function datosproducto() As List(Of claseproducto)
        Dim SQLConsulta As String = "select CodT,Descripcion from Tratamiento where idtipotratamiento=2 order by CodT"
        'MsgBox(SQLConsulta)
        Dim result As List(Of claseproducto) = New List(Of claseproducto)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New claseproducto

                    Elemento.idproducto = reader("CodT")
                    'Elemento.descripcion = reader("Cod")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New claseproducto
                Elemento.idproducto = 0
                'Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Function NumeroAleatorio() As Integer
        Dim LimiteMinimo As Integer = 0
        Dim LimiteMaximo As Integer = 10
        Dim Semilla As Integer = CInt(Date.Now.Millisecond)
        Dim NumberRandom As New Random(Semilla)
        Dim Aleatorio As Integer = NumberRandom.Next(LimiteMinimo, LimiteMaximo)
        Return Aleatorio
    End Function

    Public Class claseproducto
        Public idproducto As String
        'Public descripcion As String
        'Public mensaje As String
    End Class

    Public Class Medicina
        Public CodT As String
        Public descripcion As String
        Public correlativo As Integer
        Public mostrarEnReporte As Boolean
    End Class

    Public Class ClaseEncabezado
        Public idcompra As Integer
        Public factura As Integer
        Public serie As String
        Public nitproveedor As String
        Public fecha As String
        Public idusuario As String
        Public total As Double
    End Class

    Public Class ClaseDetalle
        Public idcompradetalle As Integer
        Public factura As Integer
        Public serie As String
        Public nitproveedor As String
        Public idmedicina As String
        Public idunidadmedida As Integer
        Public cantidad As Integer
        Public preciounitario As Double
        Public subtotal As Double
    End Class

End Class


