
Partial Class privado_fotos_pagina
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("idusuario") = "" Then
            FormsAuthentication.RedirectToLoginPage()
        Else
            usuario.Text = Session("idusuario")
        End If
    End Sub
End Class
