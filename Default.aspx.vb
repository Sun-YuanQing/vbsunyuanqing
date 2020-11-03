Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btn_log_Click(sender As Object, e As EventArgs) Handles btn_log.Click
        Dim user As String
        Dim pwd As String
        Dim sql As String
        Dim ds As New DataSet
        user = txtbox_name.Text.Trim
        pwd = txtbox_pwd.Text.Trim
        sql = "select StudentNo, LoginPwd, StudentName from Student where StudentNo like '" & user & "' or StudentName like '" & user & "' and LoginPwd like '" & pwd & "'"
        DBHelper.OpenConnection()


        Dim da As New SqlDataAdapter(sql, DBHelper.Conn)
        da.Fill(ds)
        DBHelper.CloseConnection()
        Dim cookei As HttpCookie
        If (ds.Tables(0).Rows.Count) > 0 Then
            cookei = New HttpCookie("LTSCookie")
            cookei.Expires = DateAndTime.Now.AddDays(1)
            cookei("user") = HttpUtility.UrlEncode(ds.Tables(0).Rows(0).Item("StudentName"))
            cookei("userid") = ds.Tables(0).Rows(0).Item("StudentNo")

            Response.Write("pcbainput.aspx" & "?pass=" & Request.Cookies("LTSCookie")("pass") & "&name=" & Server.UrlEncode(HttpUtility.UrlDecode(Request.Cookies("LTSCookie")("StudentName"))))
        End If


        ' Response .Write ("<script lang ='ja' >alret('""')











    End Sub

    Private Function HttpCookie(p1 As String) As Object
        Throw New NotImplementedException
    End Function

    Private Function cookie() As Object
        Throw New NotImplementedException
    End Function

End Class
