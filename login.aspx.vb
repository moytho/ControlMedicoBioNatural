Imports System.Data.SqlClient
Imports System.Data
Partial Class login
    Inherits System.Web.UI.Page
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim str As String
            Dim Adaptador As New SqlDataAdapter
            Dim TablaUsuario As New DataTable
            Using conexion As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("strconexion").ConnectionString)
                conexion.Open()
                Dim comando As SqlCommand
                comando = conexion.CreateCommand
                comando.CommandType = Data.CommandType.Text
                comando.Parameters.Add("@idusuario", SqlDbType.VarChar, 15).Value = txtusuario.Value 'pass_usr=AES_ENCRYPT('nuevo','DIOSESAMOR');
                comando.Parameters.Add("@password", SqlDbType.VarChar, 15).Value = txtpassword.Value
                str = "select idusuario from usuario where idusuario = @idusuario and " +
                    "(CONVERT(VARCHAR(300)," +
                    "DECRYPTBYPASSPHRASE('DIOSESAMOR',password)))=@password and estatus=1"
                comando.CommandText = str
                Adaptador.SelectCommand = comando
                Adaptador.Fill(TablaUsuario)
                If TablaUsuario.Rows.Count > 0 Then

                    Session("idusuario") = TablaUsuario.Rows(0).Item("idusuario")
                    'Session("idagencia") = TablaUsuario.Rows(0).Item("id_agc")
                    'Session("idempresa") = TablaUsuario.Rows(0).Item("id_empsa")
                    'Session("idusuariotipo") = TablaUsuario.Rows(0).Item("id_tipoU")
                    resultado.InnerHtml = ""
                    comando.Dispose()

                    FormsAuthentication.RedirectFromLoginPage(txtusuario.Value, False)

                Else
                    resultado.InnerHtml = "Datos incorrectos"
                End If
            End Using
        Catch ex As SqlException
            'LblEstado.Text = "Sucedio un error: " + ex.Message
        Finally
            'conexion.Close()
            'conexion = Nothing
        End Try
    End Sub
End Class
