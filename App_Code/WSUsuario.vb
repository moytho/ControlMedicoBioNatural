Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSUsuario
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function UsuarioCP(ByVal idusuario As String, ByVal passwordactual As String, ByVal passwordnueva As String) As String

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim SQLInserta As String = ""
                'DETERMINO SI LA CONTRASEÑA DEL USUARIO COINCIDE
                SQLInserta = "select count(*) from usuario where idusuario='" & idusuario & "' and (CONVERT(VARCHAR(300)," +
                    "DECRYPTBYPASSPHRASE('DIOSESAMOR',password)))='" & passwordactual & "'"
                comando.CommandText = SQLInserta
                Dim contador As Integer = comando.ExecuteScalar
                If contador = 0 Then
                    Return "ERROR: La contraseña/usuario no coincide"
                Else
                    'EJECUTO EL QUERY DE ACTUALIZACION
                    SQLInserta = "update usuario SET " +
                    "password=ENCRYPTBYPASSPHRASE('DIOSESAMOR','" & passwordnueva & "') WHERE idusuario='" & idusuario & "'"
                    comando.CommandText = SQLInserta
                    comando.ExecuteNonQuery()
                    transaccion.Commit()
                    Return "Datos guardados correctamente"
                End If


            Catch ex As SqlException
                transaccion.Rollback()
                Return "ERROR: " + ex.Message
            End Try
        End Using
    End Function

    '<WebMethod()> _
    'Public Function UsuarioBuscar(ByVal id As String) As List(Of [ClaseUsuario])
    '    Dim SQLConsulta As String = "select id,idusuario,password,idperfil,idempleado " +
    '                                "from usuario where id=" & id & ""

    '    'MsgBox(SQLConsulta)
    '    Dim result As List(Of [ClaseUsuario]) = New List(Of ClaseUsuario)()
    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim cmd As New SqlCommand(SQLConsulta, connection)
    '            Dim reader As SqlDataReader = cmd.ExecuteReader()

    '            While (reader.Read())
    '                Dim Elemento As New ClaseUsuario

    '                Elemento.id = reader("id")
    '                Elemento.idusuario = reader("idusuario")
    '                Elemento.password = reader("password")
    '                Elemento.idperfil = reader("idperfil")
    '                Elemento.idempleado = reader("idempleado")
    '                result.Add(Elemento)
    '            End While

    '            reader.Close()
    '            cmd.Dispose()
    '        Catch ex As SqlException
    '            Dim Elemento As New ClaseUsuario
    '            Elemento.id = 0
    '            Elemento.idusuario = "Sin Datos " + ex.Message
    '            Elemento.mensaje = "ERROR: " + ex.Message
    '            result.Add(Elemento)
    '            Return result
    '        End Try
    '    End Using
    '    Return result

    'End Function

    '<WebMethod()> _
    'Public Function UsuarioGuardar(ByVal idusuario As String, ByVal password As String, ByVal idperfil As String, ByVal idempleado As String) As List(Of [ClaseUsuario])
    '    'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS

    '    Dim SQLInserta As String = "insert into usuario " +
    '        "(idusuario,password,fechacreacion,idperfil,idempleado,estatus) " +
    '        "values " +
    '        "('" & idusuario & "','" & password & "',getdate()," & idperfil & "," & idempleado & "," +
    '        "1)"
    '    MsgBox(SQLInserta)
    '    'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
    '    Dim result As List(Of [ClaseUsuario]) = New List(Of ClaseUsuario)()

    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        connection.Open()
    '        Dim comando As New SqlCommand
    '        Dim transaccion As SqlTransaction
    '        transaccion = connection.BeginTransaction
    '        comando.Connection = connection
    '        comando.Transaction = transaccion
    '        Try
    '            Dim Elemento As New ClaseUsuario
    '            'EJECUTO EL QUERY DE INSERCION
    '            comando.CommandText = SQLInserta
    '            comando.ExecuteNonQuery()

    '            'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
    '            comando.CommandText = "SELECT @@IDENTITY"
    '            Dim Codigo As Integer = comando.ExecuteScalar

    '            Elemento.id = Codigo
    '            Elemento.idusuario = idusuario
    '            Elemento.mensaje = "Datos guardados correctamente"

    '            result.Add(Elemento)

    '            transaccion.Commit()

    '            Return result

    '        Catch ex As SqlException
    '            transaccion.Rollback()
    '            Dim Elemento As New ClaseUsuario
    '            Elemento.id = 0
    '            Elemento.idusuario = idusuario
    '            Elemento.mensaje = "ERROR: " + ex.Message
    '            result.Add(Elemento)
    '            Return result

    '        End Try
    '    End Using
    'End Function

    '<WebMethod()> _
    'Public Function UsuarioBuscar(ByVal id As String) As List(Of [ClaseUsuario])
    '    Dim SQLConsulta As String = "select id,idusuario,password,idperfil,idempleado " +
    '                                "from usuario where id=" & id & ""

    '    'MsgBox(SQLConsulta)
    '    Dim result As List(Of [ClaseUsuario]) = New List(Of ClaseUsuario)()
    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim cmd As New SqlCommand(SQLConsulta, connection)
    '            Dim reader As SqlDataReader = cmd.ExecuteReader()

    '            While (reader.Read())
    '                Dim Elemento As New ClaseUsuario

    '                Elemento.id = reader("id")
    '                Elemento.idusuario = reader("idusuario")
    '                Elemento.password = reader("password")
    '                Elemento.idperfil = reader("idperfil")
    '                Elemento.idempleado = reader("idempleado")
    '                result.Add(Elemento)
    '            End While

    '            reader.Close()
    '            cmd.Dispose()
    '        Catch ex As SqlException
    '            Dim Elemento As New ClaseUsuario
    '            Elemento.id = 0
    '            Elemento.idusuario = "Sin Datos " + ex.Message
    '            Elemento.mensaje = "ERROR: " + ex.Message
    '            result.Add(Elemento)
    '            Return result
    '        End Try
    '    End Using
    '    Return result

    'End Function

    '<WebMethod()> _
    'Public Function UsuarioActualizar(ByVal id As String, ByVal idusuario As String, ByVal password As String, ByVal idperfil As String, ByVal idempleado As String) As List(Of [ClaseUsuario])
    '    Dim SQLActualiza As String = "update usuario set idusuario='" & idusuario & "',password='" & password & "', " +
    '        "idperfil='" & idperfil & "',idempleado='" & idempleado & "'  where id=" & id & ""

    '    MsgBox("HOLA SQLACTUALIZA")
    '    Dim result As List(Of [ClaseUsuario]) = New List(Of ClaseUsuario)()
    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim Comando As New SqlCommand(SQLActualiza, connection)
    '            Comando.ExecuteNonQuery()

    '            Dim Elemento As New ClaseUsuario

    '            Elemento.id = id
    '            Elemento.idusuario = idusuario
    '            Elemento.mensaje = "Datos actualizados correctamente"

    '            result.Add(Elemento)

    '            Return result
    '        Catch ex As Exception
    '            Dim Elemento As New ClaseUsuario
    '            Elemento.id = 0
    '            Elemento.idusuario = "Sin Datos " + ex.Message
    '            Elemento.mensaje = "ERROR: " + ex.Message


    '            Return result
    '        End Try
    '    End Using
    'End Function

    '<WebMethod()> _
    'Public Function UsuarioEliminar(ByVal id As String) As String
    '    Dim SQLElimina As String = "update usuario set estatus=0 where id=" & id & ""

    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim Comando As New SqlCommand(SQLElimina, connection)
    '            Comando.ExecuteNonQuery()

    '            Dim Elemento As New ClaseUsuario

    '            Return "Datos eliminado correctamente"

    '        Catch ex As Exception
    '            Return "ERROR: " + ex.Message

    '        End Try
    '    End Using
    'End Function

    <WebMethod()> _
    Public Function UsuarioDatos() As List(Of ClaseUsuario)
        Dim SQLConsulta As String = "select id,idusuario from usuario where estatus=1"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseUsuario]) = New List(Of ClaseUsuario)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseUsuario

                    Elemento.id = reader("id")
                    Elemento.idusuario = reader("idusuario")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseUsuario
                Elemento.id = 0
                Elemento.idusuario = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClaseUsuario
        Public id As Integer
        Public idusuario As String
        Public password As String
        Public fechacreacion As Date
        Public ultimoingreso As Date
        Public idperfil As String
        Public idempleado As String
        Public mensaje As String
    End Class


End Class