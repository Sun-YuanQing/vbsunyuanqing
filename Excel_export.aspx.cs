using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Excel_export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_ck_Click(object sender, EventArgs e)
    {
        String str_select = " id, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20 ";
        String str_where = " (F14 is null or F14 like '0')";
       // if (this.txb_tim1 .Text .Trim() != "")
        //{
        //'    str_select += "  convert(varchar(12), times,20) <='" & itme_2.Text.Trim() & "'";
        //'}
        if (this.txb_fno  .Text .Trim() != "")
        {
            str_where += "   and F13 like  '+%  txb_fno.Text.Trim()  %+'";
        }
        if (this.txb_bm .Text.Trim () !="")
        {
            str_where += "  and F2 like  '%"+ txb_bm  .Text .Trim() +"%'";
        }
        DataSet ds = new DataSet();
       
       ds = select_excel1.excel1(str_select, str_where);
       this.GridView1.DataSource = ds;
       GridView1.DataBind();
    }
    protected void btn_dc_Click(object sender, EventArgs e)
    {

        String str_select = " id, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20 ";
        String str_where = "   1=1 ";
        // if (this.txb_tim1 .Text .Trim() != "")
        //{
        //'    str_select += "  convert(varchar(12), times,20) <='" & itme_2.Text.Trim() & "'";
        //'}
        if (this.txb_fno.Text.Trim() != "")
        {
            str_where += "   and F13 like  '+%  txb_fno.Text.Trim()  %+'";
        }
        if (this.txb_bm.Text.Trim() != "")
        {
            str_where += "  and F2 like  '%" + txb_bm.Text.Trim() + "%'";
        }
        DataSet ds = new DataSet();

        ds = select_excel1.excel1(str_select, str_where);
        this.GridView1.DataSource = ds;
        GridView1.DataBind();

        CreateExcxel(ds, "1", "欧克.xls");

    }          
      protected void CreateExcxel(DataSet ds, String typeid, String Filename)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); //GB2312 BIG5
        resp.ContentType = "application/ms-excel";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" +  HttpUtility.UrlEncode(Filename));
        string strLine = "";
      
   
        DataTable dt = ds.Tables[0];
        DataRow myRow = dt.Rows[0];
        if (typeid == "1")
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strLine = strLine + dt.Columns[i].ColumnName.ToString() + Convert.ToChar(9);
            }
            resp.Write(strLine);
            strLine = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
              
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    strLine = strLine + dt.Rows[i][j].ToString() + Convert.ToChar(9);
                    if (strLine.Contains("\r"))
                    {
                        strLine = strLine.Replace("\r","");// 字符串替换方法Replace，把r 替换为t
                    }
                }

                resp.Write("\r" + strLine);
                strLine = "";
            }
        } 
        resp.End();
    }
        //public static bool haha(DataSet ds)
        //{try
        //    { string FileName =  "d:\\abc.xls";
        //        DataTable dt = ds.Tables[0];
        //        FileStream objFileStream;
        //        StreamWriter objStreamWriter;
        //        string strLine = "";
        //        objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
        //        objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
        
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            strLine = strLine + dt.Columns[i].ColumnName.ToString() +Convert.ToChar(9);
        //        }
        //        objStreamWriter.WriteLine(strLine);
        //        strLine = "";
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            strLine = strLine + (i + 1) + Convert.ToChar(9);
        //            for (int j = 1; j < dt.Columns.Count; j++)
        //            {
        //                strLine = strLine + dt.Rows[i][j].ToString() +Convert.ToChar(9);
        //            }
        //            objStreamWriter.WriteLine(strLine);
        //            strLine = "";
        //        }
        //        objStreamWriter.Close();
        //        objFileStream.Close();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }

        //}
}