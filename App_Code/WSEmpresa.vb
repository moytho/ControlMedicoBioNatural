Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSEmpresa
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function EmpresaGuardar(ByVal nombre As String, ByVal telefono As String, ByVal fax As String, ByVal direccion As String, ByVal correo As String, ByVal eslogan As String) As List(Of [ClaseEmpresa])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into empresa " +
            "(nombre,telefono,fax,direccion,correo,eslogan,estatus) " +
            "values " +
            "('" & nombre & "','" & telefono & "','" & fax & "'," +
            "'" & direccion & "','" & correo & "','" & eslogan & "',1)"
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseEmpresa]) = New List(Of ClaseEmpresa)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseEmpresa
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idempresa = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)
                transaccion.Commit()
                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function EmpresaBuscar(ByVal idempresa As String) As List(Of [ClaseEmpresa])
        Dim SQLConsulta As String = "select idempresa,nombre,telefono,fax,direccion,correo,eslogan " +
                                    "from empresa where idempresa=" & idempresa & ""
        Dim result As List(Of [ClaseEmpresa]) = New List(Of ClaseEmpresa)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseEmpresa

                    Elemento.idempresa = reader("idempresa")
                    Elemento.nombre = reader("nombre")
                    Elemento.telefono = reader("telefono")
                    Elemento.fax = reader("fax")
                    Elemento.direccion = reader("direccion")
                    Elemento.correo = reader("correo")
                    Elemento.eslogan = reader("eslogan")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function EmpresaActualizar(ByVal nombre As String, ByVal telefono As String, ByVal fax As String, ByVal direccion As String, ByVal correo As String, ByVal eslogan As String, ByVal idempresa As String) As List(Of [ClaseEmpresa])
        Dim SQLActualiza As String = "update empresa set nombre='" & nombre & "',telefono='" & telefono & "'," +
            "fax='" & fax & "',direccion='" & direccion & "',correo='" & correo & "',eslogan='" & eslogan & "' where idempresa=" & idempresa & ""
        Dim result As List(Of [ClaseEmpresa]) = New List(Of ClaseEmpresa)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseEmpresa
                
                Elemento.idempresa = idempresa
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = idempresa
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function EmpresaEliminar(ByVal idempresa As String) As List(Of [ClaseEmpresa])
        Dim SQLElimina As String = "update empresa set estatus=0 where idempresa=" & idempresa & ""
        Dim result As List(Of [ClaseEmpresa]) = New List(Of ClaseEmpresa)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = idempresa
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = idempresa
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function EmpresaDatos() As List(Of ClaseEmpresa)
        Dim SQLConsulta As String = "select idempresa,nombre from empresa where estatus=1"
        Dim result As List(Of [ClaseEmpresa]) = New List(Of ClaseEmpresa)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseEmpresa

                    Elemento.idempresa = reader("idempresa")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseEmpresa
                Elemento.idempresa = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClaseEmpresa
        Public idempresa As Integer
        Public nombre As String
        Public telefono As String
        Public fax As String
        Public direccion As String
        Public correo As String
        Public eslogan As String
        Public mensaje As String
    End Class


End Class