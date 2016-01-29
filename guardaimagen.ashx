<%@ WebHandler Language="VB" Class="guardaimagen" %>

Imports System
Imports System.Web
Imports System.IO

Public Class guardaimagen : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hello World")
        MsgBox ("hola")
        If (context.Request.Files.Count > 0) Then
            If (context.Request.Files.Count > 0) Then
                Dim path1 As String = context.Server.MapPath("~/images/")
                If Not Directory.Exists(path1) Then
                    Directory.CreateDirectory(path1)
                    Dim file = context.Request.Files(0)
                    Dim filename As String = Path.Combine(path1, file.FileName)
                    file.SaveAs(filename)
                    context.Response.ContentType = "text/plain"
                    Dim serializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    'Dim resultado = New {name= file.Filename}
                    Dim result = New With { _
                    Key .name = file.FileName _
                    }
                    context.Response.Write(serializer.Serialize(result))
                End If
            End If
        End If
      

    
    
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class