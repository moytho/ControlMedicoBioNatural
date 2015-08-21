Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.Services

Partial Class privado_paciente
    Inherits System.Web.UI.Page

    <WebMethod(EnableSession:=True)>
    Public Shared Function actualizarfoto(ByVal idpaciente As Integer) As String
        Dim foto As String = HttpContext.Current.Session("foto").ToString
        Try
            Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)

                Dim SQLActualiza As String = "update Pacientes set " +
                "foto='" & foto & "' where CodE=" & idpaciente & ""

                connection.Open()
                Dim Comando As New SqlCommand(SQLActualiza, connection)
                Comando.ExecuteNonQuery()


            End Using
            Return "Datos guardados correctamente"
        Catch ex As Exception
            Return "ERROR:" + ex.Message
        End Try
    End Function

    'Protected Sub fupload_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fupload.UploadedComplete
    '    Try
    '        Dim TamanoImagen As Integer = e.FileSize

    '        If TamanoImagen > 1000 Then

    '            LE ASIGNAMOS EL NOMBRE A LA FOTO CON EL CODIGO DEL PACIENTE'
    '            Dim NuevoNombreArchivo As String = hidpaciente.Value + ".jpg"
    '            Dim Ruta As String = ""
    '            Dim RutaCompleta As String = ""

    '            Ruta = Server.MapPath("~/privado/fotos/")
    '            RutaCompleta = Ruta + NuevoNombreArchivo
    '            Using connection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)


    '                Dim SQLActualiza As String = "update Pacientes set " +
    '                "foto='" & NuevoNombreArchivo & "' where CodE=" & hidpaciente.Value & ""


    '                connection.Open()
    '                Dim Comando As New SqlCommand(SQLActualiza, connection)
    '                Comando.ExecuteNonQuery()


    '            End Using

    '            Dim strFoto As Boolean = (System.IO.File.Exists(RutaCompleta))
    '            If strFoto = True Then
    '                Si la Foto con el Nombre Del Empleado existe entonces sobreescribimos
    '                File.Delete(RutaCompleta)
    '                Guardamos la foto
    '                fupload.SaveAs(RutaCompleta)
    '            Else
    '                Guardamos la foto
    '                fupload.SaveAs(RutaCompleta)
    '            End If
    '        Else

    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)

    '    Finally

    '    End Try

    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Session("foto") = ""
        End If
        If Session("idusuario") = "" Then
            FormsAuthentication.RedirectToLoginPage()
        Else
            usuario.Text = Session("idusuario")
            usuario.Text.ToUpper()

        End If
    End Sub




End Class
