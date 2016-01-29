Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSPaciente
    Inherits System.Web.Services.WebService

    Public Class DatosActualizados

        Public codigopaciente As Integer
        Public nombrepaciente As String
        Public apellidopaciente As String
        Public direccionpaciente As String
        Public fechacreacionpaciente As String

        Public codigoreceta As Integer
        Public codigopacientereceta As Integer
        Public nombrepacientereceta As String
        Public apellidopacientereceta As String
        Public direccionpacientereceta As String
        Public fechareceta As String
        Public estadoreceta As String

    End Class
    <WebMethod()> _
    Public Function mostrarUltimosDatosActualizados() As DatosActualizados
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpacienteOS


        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As DatosActualizados = New DatosActualizados

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim SQLInserta As String

                SQLInserta = "select top 1 CodE,Apellido,Nombre,direccion,convert(varchar,fechacreacion,103) fechacreacion from pacientes order by code desc"
                comando.CommandText = SQLInserta

                Dim reader As SqlDataReader = comando.ExecuteReader

                While (reader.Read())
                    result.codigopaciente = reader("CodE")
                    result.nombrepaciente = reader("Nombre")
                    result.apellidopaciente = reader("Apellido")
                    result.direccionpaciente = reader("direccion")
                    result.fechacreacionpaciente = reader("fechacreacion")
                End While
                reader.Close()


                SQLInserta = "select top 1 re.idreceta,re.idpaciente,convert(varchar,re.fecha,103) fecha,re.estado,p.nombre,p.apellido,p.direccion " +
                "from recetasencabezado re INNER JOIN pacientes p ON re.idpaciente=p.CodE " +
                "order by idreceta desc"

                comando.CommandText = SQLInserta
                Dim reader2 As SqlDataReader = comando.ExecuteReader

                While (reader2.Read())

                    result.codigoreceta = reader2("idreceta")
                    result.codigopacientereceta = reader2("idpaciente")
                    result.nombrepacientereceta = reader2("Nombre")
                    result.apellidopacientereceta = reader2("Apellido")
                    result.direccionpacientereceta = reader2("direccion")
                    result.fechareceta = reader2("fecha")
                    result.estadoreceta = reader2("estado")

                End While
                reader2.Close()
                
                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                
            End Try
        End Using
        Return result
    End Function


    <WebMethod()> _
    Public Function FechaActual() As String
        Dim SQLConsulta As String = "select convert(varchar,getdate(),103) as fecha"
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim Fecha As String
                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Fecha = reader("fecha")
                    result.Add(Elemento)

                End While

                reader.Close()
                cmd.Dispose()

                Return Fecha

            Catch ex As SqlException

                Return "01/01/2014"
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ProximaConsulta() As String
        Dim SQLConsulta As String = "select convert(varchar,bionatural.dbo.fn_proxima_consulta(getdate()),103) as fecha"
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim Fecha As String
                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Fecha = reader("fecha")
                    result.Add(Elemento)

                End While

                reader.Close()
                cmd.Dispose()

                Return Fecha

            Catch ex As SqlException

                Return "01/01/2014"
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PacienteGenero(ByVal idpaciente As String, ByVal genero As String) As String
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpacienteOS

        Dim SQLActualiza As String = "update Pacientes set " +
        "genero='" & genero & "' where CodE=" & idpaciente & ""

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()



                Return "Datos guardados correctamente"

            Catch ex As SqlException


                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PacienteGuardarCita(ByVal idpaciente As String, ByVal iddoctor As String) As List(Of [ClasePaciente])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpacienteOS


        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim SQLInserta As String

                SQLInserta = "select count(idconsulta) from consulta " +
                "where CONVERT(VARCHAR,fechaconsulta,102)=CONVERT(VARCHAR,getdate(),102) and iddoctor=" & iddoctor & ""
                comando.CommandText = SQLInserta
                Dim correlativo As Integer = IIf(comando.ExecuteScalar IsNot DBNull.Value, comando.ExecuteScalar + 1, 1)

                'EJECUTO EL QUERY DE INSERCION
                SQLInserta = "insert into consulta " +
                "(idpaciente,fechacreacion,fechaconsulta,estado,iddoctor,correlativo) " +
                "values (" & idpaciente & ",getdate(),getdate(),'PENDIENTE'," & iddoctor & "," & correlativo & ")"


                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar



                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = idpaciente
                Elemento.nombre = ""
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = 0
                Elemento.nombre = ""
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PacienteGuardar(ByVal nombre As String, ByVal apellido As String, ByVal estado As String, ByVal direccion As String, _
                                    ByVal telefono As String, ByVal fechanacimiento As String, ByVal ocupacion As String, ByVal nacionalidad As String, _
                                    ByVal datosextras As String, ByVal correo As String, ByVal nhijos As String, ByVal operaciones As String, _
                                    ByVal recomendado As String, ByVal genero As String, ByVal foto As String, _
                                    ByVal idempresa As String, ByVal alergias As String) As List(Of [ClasePaciente])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpacienteOS

        Dim SQLInserta As String = "insert into Pacientes " +
        "(Apellido,Nombre,Estado,Direccion,Telefono,Fech_nac,Ocupacion,Nacionalidad,DatosExtras," +
        "correo,nhijos,operaciones,recomendado,fechacreacion,estatus,genero,foto,idempresa,alergias) " +
        "values ('" & apellido & "','" & nombre & "','" & estado & "','" & direccion & "','" & telefono & "'," +
        "'" & fechanacimiento & "','" & ocupacion & "','" & nacionalidad & "','" & datosextras & "','" & correo & "'," +
        "" & nhijos & ",'" & operaciones & "','" & recomendado & "',getdate(),1,'" & genero & "','" & foto & "'," +
        "" & idempresa & ",'" & alergias & "')"

        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try

                'GUARDAMOS LA PROFESION SI LA NECESITAMOS
                Dim sqlInserta2 As String = "select count(descripcion) from profesion where descripcion='" & ocupacion & "'"
                comando.CommandText = sqlInserta2
                Dim contador As Integer = comando.ExecuteScalar

                If contador = 0 Then

                    sqlInserta2 = "insert into profesion (descripcion,estatus) values ('" & ocupacion & "',1)"
                    comando.CommandText = sqlInserta2
                    comando.ExecuteNonQuery()

                End If
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PacienteBuscarAutocomplete(ByVal busquedapaciente As String, ByVal tipoBusqueda As String) As List(Of ClasePaciente)


        Dim SQLConsulta As String = ""
        If tipoBusqueda = 1 Then
            If Not IsNumeric(busquedapaciente) Then
                busquedapaciente = 0
            End If
            SQLConsulta = "select CodE idpaciente,(Nombre +' '+ Apellido) as nombre " +
        "from Pacientes " +
        "where CodE= '" & busquedapaciente & "'"
        ElseIf tipoBusqueda = 2 Then
            SQLConsulta = "select top 25 CodE idpaciente,(Nombre +' '+ Apellido) as nombre " +
        "from Pacientes " +
        "where Nombre like '" & busquedapaciente & "%' order by CodE desc"
        ElseIf tipoBusqueda = 3 Then
            SQLConsulta = "select top 25 CodE idpaciente,(Nombre +' '+ Apellido) as nombre " +
        "from Pacientes " +
        "where Apellido like '" & busquedapaciente & "%' order by CodE desc"
        End If
        ' MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While


                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente
                'Elemento.idpaciente = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                'Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function PacienteBuscar(ByVal idpaciente As String) As List(Of ClasePaciente)

        Dim SQLConsulta As String = "SELECT Apellido," +
      "Nombre," +
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
      "FROM Pacientes where CodE=" & idpaciente & ""

        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    Elemento.idpaciente = idpaciente
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
                    Elemento.operaciones = IIf(reader("operaciones") IsNot DBNull.Value, reader("operaciones"), "")
                    Elemento.recomendado = IIf(reader("recomendado") IsNot DBNull.Value, reader("recomendado"), "N/A")
                    Elemento.genero = IIf(reader("genero") IsNot DBNull.Value, reader("genero"), "unknown")
                    Elemento.foto = IIf(reader("foto") IsNot DBNull.Value, reader("foto"), "unknow.jpg")
                    Elemento.idempresa = IIf(reader("idempresa") IsNot DBNull.Value, reader("idempresa"), 1)
                    Elemento.edad = IIf(reader("edad") IsNot DBNull.Value, reader("edad"), 1)
                    Elemento.alergias = IIf(reader("alergias") IsNot DBNull.Value, reader("alergias"), "")
                    result.Add(Elemento)
                End While


                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function PacienteActualizar(ByVal idpaciente As String, ByVal nombre As String, ByVal apellido As String, _
                                       ByVal estado As String, ByVal direccion As String, ByVal telefono As String, _
                                       ByVal fechanacimiento As String, ByVal ocupacion As String, ByVal nacionalidad As String, _
                                       ByVal datosextras As String, ByVal correo As String, ByVal nhijos As String, _
                                       ByVal operaciones As String, ByVal recomendado As String, ByVal genero As String, _
                                       ByVal foto As String, ByVal idempresa As String) As List(Of [ClasePaciente])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpacienteOS

        Dim SQLActualiza As String = "update Pacientes set " +
        "Apellido='" & apellido & "',Nombre='" & nombre & "',Estado='" & estado & "'," +
        "Direccion='" & direccion & "',Telefono='" & telefono & "',Fech_nac='" & fechanacimiento & "'," +
        "Ocupacion='" & ocupacion & "',Nacionalidad='" & nacionalidad & "',DatosExtras='" & datosextras & "'," +
        "correo='" & correo & "',nhijos=" & Val(nhijos) & ",operaciones=" & Val(operaciones) & "," +
        "recomendado='" & recomendado & "',genero='" & genero & "' where CodE=" & idpaciente & ""

        'MsgBox(SQLActualiza)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()


                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = idpaciente
                Elemento.nombre = nombre + " " + apellido
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                Return result

            Catch ex As SqlException

                Dim Elemento As New ClasePaciente
                Elemento.idpaciente = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PacienteEliminar(ByVal idpaciente As String) As List(Of [ClasePaciente])
        Dim SQLElimina As String = "update Paciente set estatus=0 where idpaciente=" & idpaciente & ""
        Dim result As List(Of [ClasePaciente]) = New List(Of [ClasePaciente])


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePaciente
                'Elemento.idpaciente = idpaciente
                'Elemento.mensaje = "Datos eliminados correctamente"
                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePaciente
                'Elemento.idpaciente = idpaciente
                'Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function PacienteDatos() As List(Of ClasePaciente)
        Dim SQLConsulta As String = "select idpaciente,nombre from Paciente where estatus=1 order by nombre"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePaciente]) = New List(Of ClasePaciente)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClasePaciente

                    'Elemento.idpaciente = reader("idpaciente")
                    Elemento.nombre = reader("nombre")
                    'Elemento.apellido = reader("apellido")
                    'Elemento.
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePaciente
                'Elemento.idpaciente = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function MedicoDatos() As List(Of ClaseDoctor)
        Dim SQLConsulta As String = "select idempleado,nombre from empleado where idpuesto=7 order by nombre desc"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseDoctor]) = New List(Of ClaseDoctor)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseDoctor

                    Elemento.iddoctor = reader("idempleado")
                    Elemento.nombre = reader("nombre")
                    'Elemento.apellido = reader("apellido")
                    'Elemento.
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseDoctor
                Elemento.iddoctor = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClasePaciente
        Public idpaciente As String
        Public apellido As String
        Public nombre As String
        Public estado As String
        Public direccion As String
        Public telefono As String
        Public edad As String
        Public fechanacimiento As String
        Public ocupacion As String
        Public nacionalidad As String
        Public datosextras As String
        Public correo As String
        Public nhijos As Integer
        Public operaciones As String
        Public recomendado As String
        Public fechacreacion As String
        Public estatus As Integer
        Public genero As String
        Public foto As String
        Public idempresa As Integer
        Public mensaje As String
        Public alergias As String
    End Class

    Public Class ClaseDoctor
        Public iddoctor As Integer
        Public nombre As String
    End Class

End Class