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
            this.Panel2.Visible = false;
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
    //增加记录
    protected void Bt_Add_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = true;
    }
    //取消
    protected void Bt_Cacel_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = false;
    }
    //保存
    protected void Bt_Save_Click(object sender, EventArgs e)
    {
      sqlcon = new SqlConnection(strCon);
      string Sql = "insert into Admin (CID,Name,Sex,Address,Post) values("+Convert.ToInt32(this.T_CID.Text)+",'"+this.T_Name.Text+"','"+this.T_Sex.Text+"','"+this.T_Address.Text+"','"+this.T_Post.Text+"')";
      SqlCommand sqlcom = new SqlCommand(Sql,DBHelper.Conn);
      DBHelper.OpenConnection();
      int count=sqlcom.ExecuteNonQuery();
 DBHelper.CloseConnection();
      this.Panel2.Visible = false;
    }
    //刷新记录
    protected void Bt_Refresh_Click(object sender, EventArgs e)
    {
        bind();
    }
}
