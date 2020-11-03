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
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            DataRowView mydrv = myds.Tables["Admin"].DefaultView[i];
            string score = Convert.ToString(mydrv["Price"]);
            if (Convert.ToDouble(score) < 40.00)//大家这里根据具体情况设置可能ToInt32等等
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.Red;
            }
       
        }
        sqlcon.Close();
    }
}
