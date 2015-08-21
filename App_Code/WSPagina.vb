Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Public Class WSPagina
    Inherits System.Web.Services.WebService

 
    <WebMethod()> _
    Public Function PaginaGuardar(ByVal nombre As String) As List(Of ClasePagina)
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into pagina " +
            "(nombre,estatus) " +
            "values " +
            "('" & nombre & "',1)"

        ' MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of ClasePagina) = New List(Of ClasePagina)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClasePagina
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpagina = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                'CONFIRMO LOS DATOS'
                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePagina
                Elemento.idpagina = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PaginaEliminar(ByVal idpagina As String) As List(Of ClasePagina)
        Dim SQLElimina As String = "update pagina set estatus=0 where idpagina=" & idpagina & ""

        ' MsgBox(SQLElimina)
        Dim result As List(Of ClasePagina) = New List(Of ClasePagina)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClasePagina
                Elemento.idpagina = idpagina
                Elemento.mensaje = "Pagina eliminada correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePagina
                Elemento.idpagina = idpagina
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function PaginaActualizar(ByVal nombre As String, ByVal idpagina As String) As List(Of ClasePagina)
        Dim SQLActualiza As String = "update pagina set nombre='" & nombre & "' " +
            " where idpagina=" & idpagina & ""
        Dim result As List(Of ClasePagina) = New List(Of ClasePagina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePagina

                Elemento.idpagina = idpagina
                Elemento.nombre = nombre
                Elemento.mensaje = "Pagina actualizada correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClasePagina
                Elemento.idpagina = idpagina
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PaginaBuscar(ByVal idpagina As String) As List(Of ClasePagina)
        Dim SQLConsulta As String = "select idpagina,nombre " +
                                    "from pagina where idpagina=" & idpagina & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of ClasePagina) = New List(Of ClasePagina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePagina

                    Elemento.idpagina = reader("idpagina")
                    Elemento.nombre = reader("nombre")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePagina
                Elemento.idpagina = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function PaginaDatos() As List(Of ClasePagina)
        Dim SQLConsulta As String = "select idpagina,nombre from pagina  where estatus=1"
        'MsgBox(SQLConsulta)
        Dim result As List(Of ClasePagina) = New List(Of ClasePagina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClasePagina

                    Elemento.idpagina = reader("idpagina")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePagina
                Elemento.idpagina = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function





    Public Class ClasePagina
        Public idpagina As Integer
        Public nombre As String
        Public mensaje As String

    End Class

End Class