
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO

Partial Class att_del
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id As String = Request.QueryString("attid")


        Dim comm As SqlCommand = New SqlCommand("sp_deleteExcelpath", DBHelper.Conn)
        comm.CommandType = CommandType.StoredProcedure
        comm.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = id
        Dim i As Integer
        i = DBHelper.GetNum(comm)
        If i > 0 Then
            Response.Write("<script >alert('删除成功！');window.close();</script>")
        End If

        Response.Write("<script >alert('刷新数据！');window.open('../pcbainput.aspx','_parent') ;</script>")
    End Sub
End Class
