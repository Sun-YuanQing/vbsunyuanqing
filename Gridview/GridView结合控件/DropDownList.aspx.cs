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
        DropDownList ddl;  
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            DataRowView mydrv = myds.Tables["Admin"].DefaultView[i];
            if (Convert.ToString(mydrv["Sex"]).Trim() == "男")
            {
                ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                ddl.SelectedIndex = 0;
            }
            if (Convert.ToString(mydrv["Sex"]).Trim() == "女")
            {
                ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                ddl.SelectedIndex = 1;
            }
        }
        sqlcon.Close();   
    }
  
  
}
