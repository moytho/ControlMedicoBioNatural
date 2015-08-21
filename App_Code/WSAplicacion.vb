Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSAplicacion
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function AplicacionGuardar(ByVal descripcion As String) As List(Of [ClaseAplicacion])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into aplicacion " +
            "(descripcion,estatus) " +
            "values " +
            "('" & descripcion & "',1)"

        ' MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseAplicacion]) = New List(Of ClaseAplicacion)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseAplicacion
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idaplicacion = Codigo
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = 0
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function AplicacionEliminar(ByVal idaplicacion As String) As List(Of [ClaseAplicacion])
        Dim SQLElimina As String = "update aplicacion set estatus=0 where idaplicacion=" & idaplicacion & ""

        ' MsgBox(SQLElimina)
        Dim result As List(Of [ClaseAplicacion]) = New List(Of ClaseAplicacion)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = idaplicacion
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = idaplicacion
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function


    <WebMethod()> _
    Public Function AplicacionActualizar(ByVal descripcion As String, ByVal idaplicacion As String) As List(Of [ClaseAplicacion])
        Dim SQLActualiza As String = "update aplicacion set descripcion='" & descripcion & "'" +
            " where idaplicacion=" & idaplicacion & ""
        Dim result As List(Of [ClaseAplicacion]) = New List(Of ClaseAplicacion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseAplicacion

                Elemento.idaplicacion = idaplicacion
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = idaplicacion
                Elemento.descripcion = descripcion
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function


    <WebMethod()> _
    Public Function AplicacionBuscar(ByVal idaplicacion As String) As List(Of [ClaseAplicacion])
        Dim SQLConsulta As String = "select idaplicacion,descripcion " +
                                    "from aplicacion where idaplicacion=" & idaplicacion & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseAplicacion]) = New List(Of ClaseAplicacion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseAplicacion

                    Elemento.idaplicacion = reader("idaplicacion")
                    Elemento.descripcion = reader("descripcion")
                    
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = 0
                Elemento.descripcion = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function




    <WebMethod()> _
    Public Function AplicacionDatos() As List(Of ClaseAplicacion)
        Dim SQLConsulta As String = "select idaplicacion,descripcion from aplicacion where estatus=1"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseAplicacion]) = New List(Of ClaseAplicacion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseAplicacion

                    Elemento.idaplicacion = reader("idaplicacion")
                    Elemento.descripcion = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseAplicacion
                Elemento.idaplicacion = 0
                Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function





    Public Class ClaseAplicacion
        Public idaplicacion As Integer
        Public descripcion As String
        Public mensaje As String
    End Class


End Class