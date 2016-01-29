Imports System.IO
Partial Class privado_subirfoto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Using reader As New BinaryReader(Context.Request.InputStream)

            Dim image As Byte() = reader.ReadBytes(Context.Request.ContentLength)
            Dim now As String = Date.Now.ToString().Replace("/", "_").Replace(":", "-").Replace(" ", "_")
            'Session.Add("nom_img", now)
            Session("foto") = now + ".jpg"
            Dim camino As String
            camino = Path.Combine(Server.MapPath("~/privado/fotos/"), Session("foto").ToString)
            'Session("usuario") = camino
            '//Utilizamos un FileStream para crear un nuevo archivo temporal  
            Dim fs = New FileStream(camino, FileMode.CreateNew, FileAccess.Write)

            '//Un BinaryWriter para escribir la imagen descodificada  
            Dim bw = New BinaryWriter(fs)
            bw.Write(image)
            fs.Close()
        End Using
    End Sub
End Class
