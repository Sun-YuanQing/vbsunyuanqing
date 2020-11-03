using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//download by http://www.codefans.net
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    SqlConnection sqlcon;
    string strCon = "Data Source=(local);Database=MySchool;Uid=sa;Pwd=2222";
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
        DBHelper.CloseConnection();
        //DataTable table = myds.Tables["student"];
        //DataRow row = table.NewRow();
        //row["stu_id"] = "1001";
        //row["stu_name"] = "小明";
        //table.Rows.Add(row);
        
    }
  
}
