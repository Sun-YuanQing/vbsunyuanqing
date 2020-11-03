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
        GridView1.DataKeyNames = new string[] { "CID"};
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int CID = (int)GridView1.DataKeys[e.RowIndex].Value;
        string Sql = "delete Admin where CID=" + CID + "";
        sqlcon = new SqlConnection(strCon);
        SqlCommand sqlcom = new SqlCommand( Sql,DBHelper.Conn);
        try
        {
            DBHelper.OpenConnection();
            sqlcom.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw ex; 
        }
    DBHelper.CloseConnection();
        bind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[1].Text + "\"吗?')");


            }
        }
    }
}
