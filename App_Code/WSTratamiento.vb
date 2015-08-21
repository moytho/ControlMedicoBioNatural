Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Data
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSTratamiento
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function cargaTiposTratamientos() As List(Of TipoTratamiento)
        Dim SQLConsulta As String = "select idtipotratamiento,descripcion from tipotratamiento order by descripcion"
        Dim result As List(Of [TipoTratamiento]) = New List(Of TipoTratamiento)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New TipoTratamiento
                    Elemento.idtipotratamiento = reader("idtipotratamiento")
                    Elemento.descripcion = IIf(reader("descripcion") IsNot DBNull.Value, reader("descripcion"), "")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New TipoTratamiento
                'Elemento.idtipotratamiento = 0
                'Elemento.descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                'Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function TratamientoDatosSP(ByVal letra As String) As List(Of ST)
        Dim result As List(Of [ST]) = New List(Of ST)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                Dim SqlCmd As SqlCommand = New SqlCommand
                SqlCmd.CommandText = "sp_retorna_tratamientos"

                connection.Open()

                SqlCmd.Connection = connection
                SqlCmd.CommandType = CommandType.StoredProcedure
                SqlCmd.Parameters.Add(New SqlParameter("@valor", SqlDbType.VarChar, 3))
                SqlCmd.Parameters("@valor").Value = letra


                Dim lector As SqlDataReader = SqlCmd.ExecuteReader
                Dim CodigoTratamiento As String = ""
                While (lector.Read())
                    Dim Elemento As New ST

                    Elemento.idtratamiento = lector("CodT")
                    result.Add(Elemento)
                End While

                lector.Close()

                SqlCmd.Dispose()

            Catch ex As SqlException
                Dim Elemento As New ST
                Elemento.idtratamiento = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function TratamientoDatos() As List(Of ClaseTratamiento)
        Dim SQLConsulta As String = "select CodT,Descripcion from Tratamiento where estatus=1 order by Descripcion"
        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While (reader.Read())
                    Dim Elemento As New ClaseTratamiento

                    Elemento.idtratamiento = reader("CodT")
                    Elemento.Descripcion = IIf(reader("Descripcion") IsNot DBNull.Value, reader("Descripcion"), "")
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = 0
                Elemento.Descripcion = "Sin Datos " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function TratamientoBuscar(ByVal idtratamiento As String) As List(Of [ClaseTratamiento])
        Dim SQLConsulta As String = "select CodT," +
                                    "replace(Descripcion,'" & Chr(34) & "','')  Descripcion," +
                                    "cast(replace(cast(Otros as nvarchar(max)),'" & Chr("34") & "','') as ntext) Otros," +
                                    "idtipotratamiento,existencia,tratamientopadre,cantidadarebajar " +
                                    "from Tratamiento where CodT='" & idtratamiento & "'"

        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim cmd As New SqlCommand(SQLConsulta, connection)
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Dim Elemento As New ClaseTratamiento
                    Elemento.idtratamiento = reader("CodT")
                    Elemento.Descripcion = IIf(reader("Descripcion") IsNot DBNull.Value, reader("Descripcion"), "")
                    Elemento.Otros = IIf(reader("Otros") IsNot DBNull.Value, reader("Otros"), "")
                    Elemento.idtipotratamiento = reader("idtipotratamiento")
                    Elemento.existencia = IIf(reader("existencia") IsNot DBNull.Value, reader("existencia"), 0)
                    Elemento.tratamientopadre = IIf(reader("tratamientopadre") IsNot DBNull.Value, reader("tratamientopadre"), "")
                    Elemento.existenciaarebajar = IIf(reader("cantidadarebajar") IsNot DBNull.Value, reader("cantidadarebajar"), 0)
                    result.Add(Elemento)
                End While

                reader.Close()
                cmd.Dispose()
            Catch ex As SqlException
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = 0
                Elemento.Descripcion = "Sin Datos " + ex.Message
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
                Return result
            End Try
        End Using
        Return result

    End Function

    <WebMethod()> _
    Public Function TratamientoGuardar(ByVal idtratamiento As String, ByVal Descripcion As String,
                                       ByVal Otros As String, ByVal idtipotratamiento As Integer,
                                       ByVal existencia As Integer, ByVal tratamientopadre As String,
                                          ByVal existenciaarebajar As Integer) As List(Of [ClaseTratamiento])
        'FORMAMOS EL QUERY DE INSERCION CON LOS DATOS RECIBIDOS
        Dim SQLInserta As String = "insert into Tratamiento " +
            "(CodT,Descripcion,Otros,estatus,idtipotratamiento,existencia,tratamientopadre,cantidadarebajar) " +
            "values " +
            "('" & idtratamiento & "','" & Descripcion & "','" & Otros & "',1," & idtipotratamiento & "," & existencia & ",'" & tratamientopadre & "'," & existenciaarebajar & ")"

        'DECLARAMOS EL ARREGLO QUE CONTENDRA LAS LISTA DE VALORES QUE QUEREMOS RETORNAR
        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            connection.Open()
            Dim comando As New SqlCommand
            Dim transaccion As SqlTransaction
            transaccion = connection.BeginTransaction
            comando.Connection = connection
            comando.Transaction = transaccion
            Try
                Dim Elemento As New ClaseTratamiento
                'EJECUTO EL QUERY DE INSERCION
                comando.CommandText = SQLInserta
                comando.ExecuteNonQuery()

                Elemento.idtratamiento = idtratamiento
                Elemento.Descripcion = Descripcion
                Elemento.Otros = Otros
                Elemento.mensaje = "Datos guardados correctamente"

                result.Add(Elemento)

                transaccion.Commit()

                Return result
            Catch ex As SqlException
                transaccion.Rollback()
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = 0
                Elemento.Descripcion = Descripcion
                Elemento.mensaje = "Sucedio un error: " + ex.Message
                result.Add(Elemento)
                Return result

            End Try
        End Using
    End Function

    <WebMethod()> _
    Public Function TratamientoEliminar(ByVal idtratamiento As String) As List(Of [ClaseTratamiento])
        Dim SQLElimina As String = "update Tratamiento set estatus=0 where CodT='" & idtratamiento & "'"

        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()

        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLElimina, connection)
                Comando.ExecuteNonQuery()
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = idtratamiento
                Elemento.mensaje = "Dato eliminado correctamente"

                result.Add(Elemento)

            Catch ex As Exception
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = idtratamiento
                Elemento.mensaje = "ERROR: " + ex.Message
                result.Add(Elemento)
            End Try

        End Using
        Return result
    End Function

    <WebMethod()> _
    Public Function TratamientoActualizar(ByVal Otros As String, ByVal Descripcion As String,
                                          ByVal idtratamiento As String, ByVal idtipotratamiento As Integer,
                                          ByVal existencia As Integer, ByVal tratamientopadre As String,
                                          ByVal existenciaarebajar As Integer) As List(Of [ClaseTratamiento])



        Dim SQLActualiza As String = "update Tratamiento set Otros=REPLACE('" & Otros & "','" & Chr(34) & "','')," +
                                     "Descripcion=REPLACE('" & Descripcion & "','" & Chr(34) & "','')," +
                                     "idtipotratamiento=" & idtipotratamiento & ",existencia=" & existencia & "," +
                                     "tratamientopadre='" & tratamientopadre & "',cantidadarebajar=" & existenciaarebajar & " " +
                                     "where CodT='" & idtratamiento & "'"
        Dim result As List(Of [ClaseTratamiento]) = New List(Of ClaseTratamiento)()
        Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
            Try
                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()

                Dim Elemento As New ClaseTratamiento

                Elemento.idtratamiento = idtratamiento
                Elemento.Descripcion = Descripcion
                Elemento.Otros = Otros
                Elemento.mensaje = "Datos actualizados correctamente"

                result.Add(Elemento)

                Return result
            Catch ex As Exception
                Dim Elemento As New ClaseTratamiento
                Elemento.idtratamiento = idtratamiento
                Elemento.Descripcion = Descripcion
                Elemento.mensaje = "ERROR: " + ex.Message

                Return result
            End Try
        End Using
    End Function

    Public Class ClaseTratamiento
        Public idtratamiento As String
        Public nombre As String
        Public Descripcion As String
        Public Otros As String
        Public mensaje As String
        Public idtipotratamiento As Integer
        Public existencia As Integer
        Public tratamientopadre As String
        Public existenciaarebajar As Integer
    End Class

    Public Class ST
        Public idtratamiento As String
    End Class

    Public Class TipoTratamiento
        Public idtipotratamiento As Integer
        Public descripcion As String
    End Class
End Class