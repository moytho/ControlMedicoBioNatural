Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSEmpleado
    Inherits System.Web.Services.WebService
    'EMPLEADOS
    <WebMethod()> _
    Public Function EmpleadoGuardar(ByVal nombre As String, ByVal idpuesto As String) As List(Of [ClaseEmpleado])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into empleado " +
            "(nombre,idpuesto,fechacreacion,estatus) " +
            "values " +
            "('" & nombre & "','" & idpuesto & "',getdate(),1)"
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseEmpleado]) = New List(Of ClaseEmpleado)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseEmpleado
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idempleado = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseEmpleado
                Elemento.idempleado = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function EmpleadoActualizar(ByVal nombre As String, ByVal idpuesto As String, ByVal idempleado As String) As List(Of [ClaseEmpleado])
        Dim SQLActualiza As String = "update empleado set nombre='" & nombre & "',idpuesto='" & idpuesto & "' where idempleado=" & idempleado & ""
        Dim result As List(Of [ClaseEmpleado]) = New List(Of ClaseEmpleado)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseEmpleado

                Elemento.idempleado = idempleado
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseEmpleado
                Elemento.idempleado = idempleado
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function EmpleadoDatos() As List(Of ClaseEmpleado)
        Dim SQLConsulta As String = "select idempleado,nombre from empleado where estatus=1"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseEmpleado]) = New List(Of ClaseEmpleado)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseEmpleado

                    Elemento.idempleado = reader("idempleado")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseEmpleado
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function EmpleadoBuscar(ByVal idempleado As String) As List(Of [ClaseEmpleado])

        Dim SQLConsulta As String = "select idempleado,nombre,idpuesto " +
                                    "from empleado where idempleado=" & idempleado & ""
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseEmpleado]) = New List(Of ClaseEmpleado)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseEmpleado

                    Elemento.idempleado = reader("idempleado")
                    Elemento.nombre = reader("nombre")
                    Elemento.idpuesto = reader("idpuesto")

                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseEmpleado
                Elemento.idempleado = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function EmpleadoEliminar(ByVal idempleado As String) As String
        Dim SQLElimina As String = "update empleado set estatus=0 where idempleado=" & idempleado & ""

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseEmpleado

                Return "Datos eliminado correctamente"

            Catch ex As Exception
                Return "ERROR: " + ex.Message

            End Try
        End Using
    End Function

    Public Class ClaseEmpleado
        Public idempleado As Integer
        Public nombre As String
        Public idpuesto As Integer
        Public fechacreacion As String
        Public mensaje As String
    End Class

End Class