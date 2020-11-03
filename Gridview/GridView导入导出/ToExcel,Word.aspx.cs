using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page 
{
    SqlConnection sqlcon;
    string strCon = "Data Source=(local);Database=wxd;Uid=sa;Pwd=sa";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    //绑定GridView
    public void bind()
    {
        string sqlstr = "select  * from Admin";
        sqlcon = new SqlConnection(strCon);
        SqlDataAdapter myda = new SqlDataAdapter(sqlstr,DBHelper.Conn);
        DataSet myds = new DataSet();
        DBHelper.OpenConnection();
        myda.Fill(myds, "Admin");
        GridView1.DataSource = myds;
        GridView1.DataBind();
    }
    //导出Excel
    protected void Button1_Click(object sender, EventArgs e)
    {
        Export("application/ms-excel", "学生成绩报表.xls");
    }
    //导出Excel函数
    private void Export(string FileType, string FileName)
    {
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        GridView1.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
    }
    //导出方法，必须有！！！！！！！！
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    //导出Word
    protected void Button2_Click(object sender, EventArgs e)
    {
        Export("application/ms-word", "员工信息.doc");
    }
}
