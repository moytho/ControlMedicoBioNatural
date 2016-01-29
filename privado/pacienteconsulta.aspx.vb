
Partial Class privado_pacienteconsulta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("idusuario") = "sadhak"
        'Dim usuario As String = Session("idusuario")
        'idusuario.Text = usuario
        If Session("idusuario") = "" Then
            FormsAuthentication.RedirectToLoginPage()
        Else
            idusuario.Text = Session("idusuario")

        End If

    End Sub
End Class
