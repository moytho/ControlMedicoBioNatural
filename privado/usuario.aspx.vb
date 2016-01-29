
Partial Class privado_usuario
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("menu.aspx")
        If Session("idusuario") = "" Then
            FormsAuthentication.RedirectToLoginPage()
        Else
            usuario.Text = Session("idusuario")
        End If
    End Sub
End Class
