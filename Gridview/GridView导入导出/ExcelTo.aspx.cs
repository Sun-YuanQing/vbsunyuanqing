using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.OleDb;

public partial class _Default : System.Web.UI.Page 
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    private DataSet CreateDataSource()
    {
        string strCon;
        strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../excel.xls") + "; Extended Properties=Excel 8.0;";
        OleDbConnection olecon = new OleDbConnection(strCon);
        OleDbDataAdapter myda = new OleDbDataAdapter("SELECT * FROM [abc$]", strCon);
        DataSet myds = new DataSet();
        myda.Fill(myds);
        return myds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = CreateDataSource();
        GridView1.DataBind();
       

    }
}
