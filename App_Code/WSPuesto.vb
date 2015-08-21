Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSPuesto
    Inherits System.Web.Services.WebService

    'NACIONALIDADES

    <WebMethod()> _
    Public Function NacionalidadGuardar(ByVal nombre As String) As List(Of [ClaseNacionalidad])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpuestoOS

        Dim SQLInserta As String = "insert into nacionalidad (descripcion,estatus) values ('" & nombre & "',1)"

        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseNacionalidad]) = New List(Of ClaseNacionalidad)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseNacionalidad
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpuesto = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function NacionalidadBuscar(ByVal idpuesto As String) As List(Of [ClaseNacionalidad])
        Dim SQLConsulta As String = "select idnacionalidad,descripcion from nacionalidad where idnacionalidad=" & idpuesto & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseNacionalidad]) = New List(Of ClaseNacionalidad)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseNacionalidad

                    Elemento.idpuesto = reader("idnacionalidad")
                    Elemento.nombre = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function NacionalidadActualizar(ByVal idpuesto As String, ByVal nombre As String) As List(Of [ClaseNacionalidad])
        Dim SQLActualiza As String = "update nacionalidad set descripcion= '" & nombre & "'  where idnacionalidad=" & idpuesto & ""

        'MsgBox(SQLActualiza)
        Dim result As List(Of [ClaseNacionalidad]) = New List(Of ClaseNacionalidad)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseNacionalidad

                Elemento.idpuesto = idpuesto
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message


                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function NacionalidadEliminar(ByVal idpuesto As String) As List(Of [ClaseNacionalidad])
        Dim SQLElimina As String = "update nacionalidad set estatus=0 where idnacionalidad=" & idpuesto & ""
        Dim result As List(Of [ClaseNacionalidad]) = New List(Of [ClaseNacionalidad])


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "Datos eliminados correctamente"
                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function NacionalidadDatos() As List(Of ClaseNacionalidad)
        Dim SQLConsulta As String = "select idnacionalidad,descripcion from nacionalidad where estatus=1 order by descripcion"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseNacionalidad]) = New List(Of ClaseNacionalidad)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseNacionalidad

                    Elemento.idpuesto = reader("idnacionalidad")
                    Elemento.nombre = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseNacionalidad
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClaseNacionalidad
        Public idpuesto As Integer
        Public nombre As String
        Public mensaje As String
    End Class

    'PROFESIONES

    <WebMethod()> _
    Public Function ProfesionDatosFiltrado(ByVal nombre As String) As List(Of ClaseProfesion)
        Dim SQLConsulta As String = "select distinct top 10 idprofesion,descripcion from profesion where descripcion like '" & nombre & "%'"
        Dim result As List(Of ClaseProfesion) = New List(Of ClaseProfesion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseProfesion

                    Elemento.idpuesto = reader("idprofesion")
                    Elemento.nombre = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function ProfesionGuardar(ByVal nombre As String) As List(Of [ClaseProfesion])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpuestoOS

        Dim SQLInserta As String = "insert into profesion (descripcion,estatus) values ('" & nombre & "',1)"

        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of ClaseProfesion) = New List(Of ClaseProfesion)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseProfesion
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpuesto = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ProfesionBuscar(ByVal idpuesto As String) As List(Of ClaseProfesion)
        Dim SQLConsulta As String = "select idprofesion,descripcion from profesion where idprofesion=" & idpuesto & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of ClaseProfesion) = New List(Of ClaseProfesion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseProfesion

                    Elemento.idpuesto = reader("idprofesion")
                    Elemento.nombre = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function ProfesionActualizar(ByVal idpuesto As String, ByVal nombre As String) As List(Of [ClaseProfesion])
        Dim SQLActualiza As String = "update profesion set descripcion= '" & nombre & "'  where idprofesion=" & idpuesto & ""

        'MsgBox(SQLActualiza)
        Dim result As List(Of [ClaseProfesion]) = New List(Of ClaseProfesion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseProfesion

                Elemento.idpuesto = idpuesto
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message


                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function ProfesionEliminar(ByVal idpuesto As String) As List(Of [ClaseProfesion])
        Dim SQLElimina As String = "update profesion set estatus=0 where idprofesion=" & idpuesto & ""
        Dim result As List(Of [ClaseProfesion]) = New List(Of [ClaseProfesion])


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "Datos eliminados correctamente"
                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function ProfesionDatos() As List(Of ClaseProfesion)
        Dim SQLConsulta As String = "select idprofesion,descripcion from profesion where estatus=1 order by descripcion"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseProfesion]) = New List(Of ClaseProfesion)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseProfesion

                    Elemento.idpuesto = reader("idprofesion")
                    Elemento.nombre = reader("descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseProfesion
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClaseProfesion
        Public idpuesto As Integer
        Public nombre As String
        Public mensaje As String
    End Class

    'PUESTOS

    <WebMethod()> _
    Public Function PuestoGuardar(ByVal nombre As String) As List(Of [ClasePuesto])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBidpuestoOS

        Dim SQLInserta As String = "insert into puesto (nombre,estatus) values ('" & nombre & "',1)"

        'MsgBox(SQLInserta)
        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClasePuesto]) = New List(Of ClasePuesto)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClasePuesto
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
                comando.CommandText = "SELECT @@IDENTITY"
                Dim Codigo As Integer = comando.ExecuteScalar

                Elemento.idpuesto = Codigo
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result

            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = 0
                Elemento.nombre = nombre
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PuestoBuscar(ByVal idpuesto As String) As List(Of [ClasePuesto])
        Dim SQLConsulta As String = "select idpuesto,nombre from puesto where idpuesto=" & idpuesto & ""

        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePuesto]) = New List(Of ClasePuesto)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClasePuesto

                    Elemento.idpuesto = reader("idpuesto")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function PuestoActualizar(ByVal idpuesto As String, ByVal nombre As String) As List(Of [ClasePuesto])
        Dim SQLActualiza As String = "update puesto set nombre= '" & nombre & "'  where idpuesto=" & idpuesto & ""

        'MsgBox(SQLActualiza)
        Dim result As List(Of [ClasePuesto]) = New List(Of ClasePuesto)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePuesto

                Elemento.idpuesto = idpuesto
                Elemento.nombre = nombre
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message


                Return result
            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function PuestoEliminar(ByVal idpuesto As String) As List(Of [ClasePuesto])
        Dim SQLElimina As String = "update puesto set estatus=0 where idpuesto=" & idpuesto & ""
        Dim result As List(Of [ClasePuesto]) = New List(Of [ClasePuesto])


        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "Datos eliminados correctamente"
                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = idpuesto
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try
        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function PuestoDatos() As List(Of ClasePuesto)
        Dim SQLConsulta As String = "select idpuesto,nombre from puesto where estatus=1 order by nombre"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClasePuesto]) = New List(Of ClasePuesto)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClasePuesto

                    Elemento.idpuesto = reader("idpuesto")
                    Elemento.nombre = reader("nombre")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClasePuesto
                Elemento.idpuesto = 0
                Elemento.nombre = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    Public Class ClasePuesto
        Public idpuesto As Integer
        Public nombre As String
        Public mensaje As String
    End Class


End Class