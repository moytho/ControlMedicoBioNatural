Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSSintoma
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function SintomaGuardar(ByVal descripcion As String, ByVal idpartedelcuerpo As String, ByVal idsubpartedelcuerpo As String, ByVal prioridad As String, ByVal femenino As Integer, ByVal masculino As Integer, ByVal observaciones As String) As List(Of [ClaseSintoma])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into sintoma " +
            "(descripcion,observaciones,idpartedelcuerpo,idsubpartedelcuerpo,prioridad,estatus,femenino,masculino) " +
            "values " +
            "('" & descripcion & "','" & observaciones & "'," & idpartedelcuerpo & ",NULL,'" & prioridad & "',1," & femenino & "," & masculino & ")"

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseSintoma
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idsintoma = Codigo
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = 0
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function SintomaEliminar(ByVal idsintoma As String) As List(Of [ClaseSintoma])
        Dim SQLElimina As String = "update sintoma set estatus=0 where idsintoma=" & idsintoma & ""

        'MsgBox(SQLElimina)
        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = idsintoma
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = idsintoma
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function SintomaActualizar(ByVal descripcion As String, ByVal idpartedelcuerpo As String, ByVal prioridad As String, ByVal idsintoma As String, ByVal observaciones As String) As List(Of [ClaseSintoma])
        Dim SQLActualiza As String = "update sintoma set descripcion='" & descripcion & "'," +
            "idpartedelcuerpo='" & idpartedelcuerpo & "'," +
            "prioridad='" & prioridad & "'," +
            "observaciones='" & observaciones & "'" +
            "where idsintoma=" & idsintoma & ""
        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseSintoma

                Elemento.idsintoma = idsintoma
                Elemento.descripcion = descripcion
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = idsintoma
                Elemento.descripcion = descripcion
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function SintomaDatos() As List(Of ClaseSintoma)
        Dim SQLConsulta As String = "select idsintoma,descripcion from sintoma where estatus=1"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseSintoma

                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.descripcion = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = 0
                Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function SintomaBuscar(ByVal idsintoma As String) As List(Of [ClaseSintoma])
        Dim SQLConsulta As String = "select idsintoma,descripcion,idpartedelcuerpo," +
            "idsubpartedelcuerpo,prioridad,ISNULL(observaciones,' ') observaciones " +
                                    "from sintoma where idsintoma=" & idsintoma & ""

        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseSintoma

                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.descripcion = reader("descripcion")
                    Elemento.idpartedelcuerpo = reader("idpartedelcuerpo")
                    'Elemento.idsubpartedelcuerpo = reader("idsubpartedelcuerpo")
                    Elemento.prioridad = reader("prioridad")
                    Elemento.observaciones = reader("observaciones")
                    
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSintoma
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function ConsultaSintoma(ByVal busqueda As String) As List(Of ClaseSintoma)

        Dim SQLConsulta As String = "select idsintoma,descripcion " +
            "from sintoma where descripcion like '" & busqueda & "%' and estatus=1 order by descripcion"

        Dim result As List(Of [ClaseSintoma]) = New List(Of ClaseSintoma)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseSintoma
                    Elemento.idsintoma = reader("idsintoma")
                    Elemento.descripcion = IIf(reader("descripcion") IsNot DBNull.Value, reader("descripcion"), "")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSintoma
                Elemento.idsintoma = SQLConsulta
                Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClaseSintoma
        Public idsintoma As Integer
        Public descripcion As String
        Public idpartedelcuerpo As Integer
        Public idsubpartedelcuerpo As Integer
        Public prioridad As String
        Public observaciones As String
        Public mensaje As String
    End Class

End Class