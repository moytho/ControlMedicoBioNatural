Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSPartedelcuerpo
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function muestraSubpartes(ByVal idpartedelcuerpo As String) As List(Of [ClaseSubPartedelcuerpo])
        Dim SQLConsulta As String = "select idsubpartedelcuerpo,nombre from subpartedelcuerpo where idpartedelcuerpo=" & idpartedelcuerpo & ""

        Dim result As List(Of [ClaseSubPartedelcuerpo]) = New List(Of ClaseSubPartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseSubPartedelcuerpo

                    Elemento.idpartedelcuerpo = reader("idsubpartedelcuerpo")
                    Elemento.nombre = reader("nombre")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSubPartedelcuerpo
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function SubPartedelcuerpoEliminar(ByVal idsubpartedelcuerpo As String) As List(Of [ClasePartedelcuerpo])
        Dim SQLElimina As String = "update subpartedelcuerpo set estatus=0 where idsubpartedelcuerpo=" & idsubpartedelcuerpo & ""
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        'MsgBox(SQLElimina)
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePartedelcuerpo

                Elemento.idpartedelcuerpo = idsubpartedelcuerpo
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = idsubpartedelcuerpo
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)

            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function SubPartedelcuerpoActualizar(ByVal nombre As String, ByVal idpartedelcuerpo As String, ByVal idsubpartedelcuerpo As String) As List(Of [ClasePartedelcuerpo])
        Dim SQLActualiza As String = "update subpartedelcuerpo set nombre='" & nombre & "',idpartedelcuerpo=" & idpartedelcuerpo & "  where idsubpartedelcuerpo=" & idsubpartedelcuerpo & ""

        'MsgBox(SQLActualiza)
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePartedelcuerpo

                Elemento.idpartedelcuerpo = idpartedelcuerpo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function SubPartedelcuerpoGuardar(ByVal idpartedelcuerpo As String, ByVal nombre As String) As List(Of [ClasePartedelcuerpo])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into subpartedelcuerpo " +
            "(idpartedelcuerpo,nombre,estatus) " +
            "values " +
            "(" & idpartedelcuerpo & ",'" & nombre & "',1)"

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClasePartedelcuerpo
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpartedelcuerpo = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function SubPartedelcuerpoBuscar(ByVal idsubpartedelcuerpo As String) As List(Of [ClaseSubPartedelcuerpo])
        Dim SQLConsulta As String = "select idpartedelcuerpo,nombre from subpartedelcuerpo where idsubpartedelcuerpo=" & idsubpartedelcuerpo & ""

        Dim result As List(Of [ClaseSubPartedelcuerpo]) = New List(Of ClaseSubPartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseSubPartedelcuerpo

                    Elemento.idpartedelcuerpo = reader("idpartedelcuerpo")
                    Elemento.nombre = reader("nombre")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSubPartedelcuerpo
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function SubPartedelcuerpoDatos() As List(Of ClaseSubPartedelcuerpo)
        Dim SQLConsulta As String = "select idparte,nombre from parte where estatus=1 order by nombre"
        Dim result As List(Of [ClaseSubPartedelcuerpo]) = New List(Of ClaseSubPartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseSubPartedelcuerpo

                    Elemento.idpartedelcuerpo = reader("idparte")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseSubPartedelcuerpo
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function PartedelcuerpoGuardar(ByVal nombre As String, ByVal masculino As Integer, ByVal femenino As Integer, ByVal partesuperior As Integer) As List(Of [ClasePartedelcuerpo])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into parte " +
            "(nombre,masculino,femenino,idpartesuperior,estatus) " +
            "values " +
            "('" & nombre & "'," & masculino & "," & femenino & "," & partesuperior & ",1)"

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClasePartedelcuerpo
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpartedelcuerpo = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PartedelcuerpoDatos() As List(Of ClasePartedelcuerpo)
        Dim SQLConsulta As String = "select idparte,nombre from parte where estatus=1 order by nombre"
        'MsgBox(SQLConsulta)
        'AND NOT idpartesuperior IS NULL 
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClasePartedelcuerpo

                    Elemento.idpartedelcuerpo = reader("idparte")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function PartedelcuerpoBuscar(ByVal idpartedelcuerpo As String) As List(Of [ClasePartedelcuerpo])
        Dim SQLConsulta As String = "select idparte,nombre,masculino,femenino,idpartesuperior from parte where idparte=" & idpartedelcuerpo & ""

        ' MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePartedelcuerpo

                    Elemento.idpartedelcuerpo = reader("idparte")
                    Elemento.nombre = reader("nombre")
                    Elemento.masculino = reader("masculino")
                    Elemento.femenino = reader("femenino")
                    Elemento.idpartesuperior = IIf(reader("idpartesuperior") Is DBNull.Value, 0, reader("idpartesuperior"))
                    
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function PartedelcuerpoActualizar(ByVal idparte As String, ByVal nombre As String, ByVal idpartesuperior As String, ByVal masculino As String, ByVal femenino As String) As List(Of [ClasePartedelcuerpo])
        
        Dim SQLActualiza As String = "update parte set nombre='" & nombre & "'," +
            "idpartesuperior=" & IIf(idpartesuperior = "", "null", idpartesuperior) & "," +
            "masculino=" & masculino & ",femenino=" & femenino & " where idparte=" & idparte & ""

        'MsgBox(SQLActualiza)
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePartedelcuerpo

                Elemento.idpartedelcuerpo = idparte
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = idparte
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PartedelcuerpoEliminar(ByVal idpartedelcuerpo As String) As List(Of [ClasePartedelcuerpo])
        Dim SQLElimina As String = "update parte set estatus=0 where idparte=" & idpartedelcuerpo & ""
        Dim result As List(Of [ClasePartedelcuerpo]) = New List(Of ClasePartedelcuerpo)()
        'MsgBox(SQLElimina)
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePartedelcuerpo

                Elemento.idpartedelcuerpo = idpartedelcuerpo
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePartedelcuerpo
                Elemento.idpartedelcuerpo = idpartedelcuerpo
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)

            End Try
        End Using
        Return result
    End Function

    Public Class ClasePartedelcuerpo
        Public idpartedelcuerpo As Integer
        Public nombre As String
        Public mensaje As String
        Public masculino As Integer
        Public femenino As Integer
        Public idpartesuperior As Integer
    End Class

    Public Class ClaseSubPartedelcuerpo
        Public idpartedelcuerpo As Integer
        Public idsubpartedelcuerpo As String
        Public nombre As String
        Public mensaje As String
    End Class

End Class