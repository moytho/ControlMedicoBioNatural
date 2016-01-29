Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSMedicina
    Inherits System.Web.Services.WebService

    '<WebMethod()> _
    'Public Function MedicinaGuardar(ByVal nombre As String, ByVal idaplicacion As String, ByVal descripcion As String) As List(Of [ClaseMedicina])
    '    'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
    '    Dim SQLInserta As String = "insert into medicina " +
    '        "(nombre,idaplicacion,descripcion,estatus) " +
    '        "values " +
    '        "('" & nombre & "','" & idaplicacion & "','" & descripcion & "',1)"

    '    'MsgBox(SQLInserta)
    '    'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
    '    Dim result As List(Of [ClaseMedicina]) = New List(Of ClaseMedicina)()

    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        connection.Open()
    '        Dim comando As New SqlCommand
    '        Dim transaccion As SqlTransaction
    '        transaccion = connection.BeginTransaction
    '        comando.Connection = connection
    '        comando.Transaction = transaccion
    '        Try
    '            Dim Elemento As New ClaseMedicina
    '            'EJECUTO EL QUERY DE INSERCION
    '            comando.CommandText = SQLInserta
    '            comando.ExecuteNonQuery()

    '            'DETERMINO CUAL FUE EL CORRELATIVO QUE SE LE ASIGNO
    '            comando.CommandText = "SELECT @@IDENTITY"
    '            Dim Codigo As Integer = comando.ExecuteScalar

    '            Elemento.idmedicina = Codigo
    '            Elemento.nombre = nombre
    '            Elemento.mensaje = "Datos guardados correctamente"

    '            result.Add(Elemento)

    '            transaccion.Commit()

    '            Return result
    '        Catch ex As SqlException
    '            transaccion.Rollback()
    '            Dim Elemento As New ClaseMedicina
    '            Elemento.idmedicina = 0
    '            Elemento.nombre = nombre
    '            Elemento.mensaje = "Sucedio un error: " + ex.Message
    '            result.Add(Elemento)
    '            Return result

    '        End Try
    '    End Using
    'End Function

    '<WebMethod()> _
    'Public Function MedicinaEliminar(ByVal idmedicina As String) As List(Of [ClaseMedicina])
    '    Dim SQLElimina As String = "update medicina set estatus=0 where idmedicina=" & idmedicina & ""

    '    ' MsgBox(SQLElimina)
    '    Dim result As List(Of [ClaseMedicina]) = New List(Of ClaseMedicina)()

    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim Comando As New SqlCommand(SQLElimina, connection)
    '            Comando.ExecuteNonQuery()
    '            Dim Elemento As New ClaseMedicina
    '            Elemento.idmedicina = idmedicina
    '            Elemento.mensaje = "Dato eliminado correctamente"

    '            result.Add(Elemento)

    '        Catch ex As Exception
    '            Dim Elemento As New ClaseMedicina
    '            Elemento.idmedicina = idmedicina
    '            Elemento.mensaje = "ERROR: " + ex.Message
    '            result.Add(Elemento)
    '        End Try

    '    End Using
    '    Return result
    'End Function

    '<WebMethod()> _
    'Public Function MedicinaActualizar(ByVal nombre As String, ByVal idaplicacion As String, ByVal descripcion As String, ByVal idmedicina As String) As List(Of [ClaseMedicina])
    '    Dim SQLActualiza As String = "update medicina set nombre='" & nombre & "',idaplicacion='" & idaplicacion & "'," +
    '        "descripcion='" & descripcion & "' where idmedicina=" & idmedicina & ""
    '    Dim result As List(Of [ClaseMedicina]) = New List(Of ClaseMedicina)()
    '    Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
    '        Try
    '            connection.Open()
    '            Dim Comando As New SqlCommand(SQLActualiza, connection)
    '            Comando.ExecuteNonQuery()

    '            Dim Elemento As New ClaseMedicina

    '            Elemento.idmedicina = idmedicina
    '            Elemento.nombre = nombre
    '            Elemento.mensaje = "Datos actualizados correctamente"

    '            result.Add(Elemento)

    '            Return result
    '        Catch ex As Exception
    '            Dim Elemento As New ClaseMedicina
    '            Elemento.idmedicina = idmedicina
    '            Elemento.nombre = nombre
    '            Elemento.mensaje = "ERROR: " + ex.Message

    '            Return result
    '        End Try
    '    End Using
    'End Function


    <WebMethod()> _
    Public Function MedicinaDatos() As List(Of ClaseMedicina)
        Dim SQLConsulta As String = "select CodM,Descripcion from Medicina where estatus=1 order by Descripcion"
        'MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseMedicina]) = New List(Of ClaseMedicina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseMedicina

                    Elemento.idmedicina = reader("CodM")
                    Elemento.Descripcion = reader("Descripcion")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseMedicina
                Elemento.idmedicina = ""
                Elemento.Descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function MedicinaBuscar(ByVal idmedicina As String) As List(Of [ClaseMedicina])
        Dim SQLConsulta As String = "select CodM,Descripcion,Aplicacion,proveedor,Precio,Exis " +
                                    "from Medicina where CodM='" & idmedicina & "'"

        ' MsgBox(SQLConsulta)
        Dim result As List(Of [ClaseMedicina]) = New List(Of ClaseMedicina)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseMedicina

                    Elemento.idmedicina = reader("CodM")
                    'Elemento.nombre = reader("nombre")
                    Elemento.Aplicacion = IIf(reader("Aplicacion") IsNot DBNull.Value, reader("Aplicacion"), "")
                    Elemento.Descripcion = IIf(reader("Descripcion") IsNot DBNull.Value, reader("Descripcion"), "")
                    Elemento.proveedor = IIf(reader("proveedor") IsNot DBNull.Value, reader("proveedor"), "")
                    Elemento.Precio = IIf(reader("Precio") IsNot DBNull.Value, reader("Precio"), "")
                    Elemento.Exis = IIf(reader("Exis") IsNot DBNull.Value, reader("Exis"), "")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseMedicina
                Elemento.idmedicina = 0
                Elemento.Descripcion = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function





    Public Class ClaseMedicina
        Public idmedicina As String
        Public Descripcion As String
        Public Aplicacion As String
        Public proveedor As String
        Public Precio As String
        Public Exis As Integer
        Public mensaje As String
    End Class


End Class