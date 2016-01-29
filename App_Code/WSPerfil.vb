Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSPerfil
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function PerfilGuardar(ByVal nombre As String) As List(Of [ClasePerfil])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS

        Dim SQLInserta As String = "insert into perfil " +
            "(nombre,estatus) " +
            "values ('" & nombre & "',1)"
        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePerfil]) = New List(Of ClasePerfil)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClasePerfil
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idperfil = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PerfilBuscar(ByVal idperfil As String) As List(Of [ClasePerfil])
        Dim SQLConsulta As String = "select idperfil,nombre from perfil where idperfil=" & idperfil & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePerfil]) = New List(Of ClasePerfil)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePerfil

                    Elemento.idperfil = reader("idperfil")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function


    <WebMethod()> _
    Public Function PerfilActualizar(ByVal nombre As String, ByVal idperfil As String) As List(Of [ClasePerfil])
        Dim SQLActualiza As String = "update perfil set nombre='" & nombre & "' where idperfil=" & idperfil & ""
        Dim result As List(Of [ClasePerfil]) = New List(Of ClasePerfil)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePerfil

                Elemento.idperfil = idperfil
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = idperfil
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PerfilDatos() As List(Of ClasePerfil)
        Dim SQLConsulta As String = "select idperfil,nombre from perfil where estatus=1 order by nombre"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePerfil]) = New List(Of ClasePerfil)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClasePerfil

                    Elemento.idperfil = reader("idperfil")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function
    <WebMethod()> _
    Public Function PerfilEliminar(ByVal idperfil As String) As List(Of [ClasePerfil])
        Dim SQLElimina As String = "update perfil set estatus=0 where idperfil=" & idperfil & ""

        'MsgBox(SQLElimina)
        Dim result As List(Of [ClasePerfil]) = New List(Of ClasePerfil)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = idperfil
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePerfil
                Elemento.idperfil = idperfil
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function


    Public Class ClasePerfil
        Public idperfil As Integer
        Public nombre As String
        Public estatus As String
        Public mensaje As String
    End Class


End Class