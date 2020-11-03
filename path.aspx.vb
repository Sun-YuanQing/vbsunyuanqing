Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO
Partial Class path
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                Dim path As String = Request.QueryString("path")
                Dim vpath As String = ConfigurationSettings.AppSettings("path")
                vpath += path
                ' Shell("cmd /e " & vpath, vbHide)
                ' Dim f As New IO.FileInfo(vpath)
                'Console.WriteLine(f.Name)
                'Console.WriteLine(f.FullName)
                ' Console.WriteLine(f.Directory)
                ' Console.ReadLine()
                'Shell(vpath)
                '--------------------------------------------------
                '  Response.Redirect("" + vpath)
                'PC端
                ' Shell("explorer " + vpath, 1)
                '-------------------------------------------------------
                '  Me.hylnk.NavigateUrl = vpath
                '---------------------------------------------------
                ‘下载
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(vpath)
                Response.Clear()
                Response.Charset = "GB2312"
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(file.Name))
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/ms-excel"
                Response.WriteFile(file.FullName)
                Response.End()
                '-----------------------------------------------------------------------------------
            End If



        Catch ex As Exception

        End Try
    End Sub
End Class
