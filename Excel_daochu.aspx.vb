Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Excel_daochu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub CreateExcel(ByVal ds As DataSet, ByVal typeid As String, ByVal FileName As String)

        Dim resp As HttpResponse
        resp = Page.Response
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")  'GB2312 BIG5
        resp.ContentType = "application/ms-excel"
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + Web.HttpUtility.UrlEncode(FileName))
        Dim colHeaders As String = ""
        Dim Is_itme As String = ""
        Dim i As Integer = 0
        Dim dt As DataTable = ds.Tables(0)
        Dim myRow() As DataRow = dt.Select("")
        If typeid = "1" Then
            For i = 0 To dt.Columns.Count - 2
                colHeaders += dt.Columns(i).Caption.ToString.Trim() + Chr(9)
            Next
            colHeaders += dt.Columns(dt.Columns.Count - 1).Caption.ToString.Trim() + Chr(10)
            resp.Write(colHeaders)
            For Each row As DataRow In myRow
                For i = 0 To dt.Columns.Count - 2
                    Is_itme += row(i).ToString().Trim() + Chr(9)
                Next
                Is_itme += row(dt.Columns.Count - 1).ToString().Trim() + Chr(10)
                resp.Write(Is_itme)
                Is_itme = ""
            Next
        Else
            resp.Write(ds.GetXml())
        End If
        resp.End()
    End Sub


    Protected Sub btn_dc_Click(sender As Object, e As EventArgs) Handles btn_dc.Click, btn_fanhui.Click
        Dim str_select As String = " id, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20 "
        Dim str_where As String = " (F14 is null or F14 like '0')"
        ' if (this.txb_tim1 .Text .Trim() != "")
        '{
        '    str_select += "  convert(varchar(12), times,20) <='" & itme_2.Text.Trim() & "'";
        '}
        If txb_bm.Text.Trim() <> "" Then
            str_where += "  and F2 like  '%" + txb_bm.Text.Trim() + "%'"
        End If
        If txb_fb.Text.Trim.ToString() <> "" Then
            str_where += " and F3 like '%" + txb_fb.Text.Trim.ToString() + "%'"
        End If
        If txb_fz.Text.Trim.ToString() <> "" Then
            str_where += " and F4 like '%" + txb_fz.Text.Trim.ToString() + "%'"
        End If
        If txb_xm.Text.Trim() <> "" Then
            str_where += "   and F5 like  '%" + txb_xm.Text.Trim() + "%'"
        End If
        If txb_zw.Text.Trim() <> "" Then
            str_where += " and F6 like  '%" + txb_zw.Text.Trim() + "%'"
        End If
        If txb_fjh.Text.Trim.ToString() <> "" Then
            str_where += " and F9 like '%" + txb_fjh.Text.Trim.ToString() + "%'"
        End If
        If txb_yx.Text.Trim.ToString() <> "" Then
            str_where += " and F11 like '%" + txb_yx.Text.Trim.ToString() + "%'"
        End If
        If txb_fno.Text.Trim() <> "" Then
            str_where += "   and F13 like  '%" + txb_fno.Text.Trim() + "%'"
        End If
        Dim ds As DataSet
        ds = select_excel1.excel1(str_select, str_where)
        '  Me.GridView1.DataSource = ds
        ' GridView1.DataBind()
        CreateExcel(ds, 1, "Data.xls")
    End Sub
    Protected Sub btn_ck_Click(sender As Object, e As EventArgs) Handles btn_ck.Click
        Dim str_select As String = " id, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20 "
        Dim str_where As String = " (F14 is null or F14 like '0')"
        ' if (this.txb_tim1 .Text .Trim() != "")
        '{
        '    str_select += "  convert(varchar(12), times,20) <='" & itme_2.Text.Trim() & "'";
        '}
        If txb_bm.Text.Trim() <> "" Then
            str_where += "  and F2 like  '%" + txb_bm.Text.Trim() + "%'"
        End If
        If txb_yx.Text.Trim.ToString() <> "" Then
            str_where += " and F3 like '%" + txb_fb.Text.Trim.ToString() + "%'"
        End If
        If txb_fz.Text.Trim.ToString() <> "" Then
            str_where += " and F4 like '%" + txb_fz.Text.Trim.ToString() + "%'"
        End If
        If txb_xm.Text.Trim() <> "" Then
            str_where += "   and F5 like  '%" + txb_xm.Text.Trim() + "%'"
        End If
        If txb_zw.Text.Trim() <> "" Then
            str_where += " and F6 like  '%" + txb_zw.Text.Trim() + "%'"
        End If
        If txb_fjh.Text.Trim.ToString() <> "" Then
            str_where += " and F9 like '%" + txb_fjh.Text.Trim.ToString() + "%'"
        End If
        If txb_fb.Text.Trim.ToString() <> "" Then
            str_where += " and F11 like '%" + txb_yx.Text.Trim.ToString() + "%'"
        End If
        If txb_fno.Text.Trim() <> "" Then
            str_where += "   and F13 like  '%" + txb_fno.Text.Trim() + "%'"
        End If
        Dim ds As DataSet
        ds = select_excel1.excel1(str_select, str_where)
        GridView1.DataSource = ds
        GridView1.DataBind()
    End Sub
End Class
