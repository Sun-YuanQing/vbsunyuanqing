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
            //ViewState["SortOrder"] = "CID";
            //ViewState["OrderDire"] = "ASC";

            bind();
        }
    }
    //绑定GridView
    public void bind()
    {
        string sortExpression = this.GridView1.Attributes["SortExpression"];
        string sortDirection = this.GridView1.Attributes["SortDirection"];

        string sqlstr = "select  * from Admin";
        sqlcon = new SqlConnection(strCon);
        SqlDataAdapter myda = new SqlDataAdapter(sqlstr,DBHelper.Conn);
        DBHelper.OpenConnection();
        DataSet myds = new DataSet();
        
        myda.Fill(myds, "Admin");


        DataView dv = myds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;

        GridView1.DataSource = myds;
        GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
         if (ViewState["SortOrder"].ToString() == sPage)
         {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                 ViewState["OrderDire"] = "Desc";
         }
         else
         {
             ViewState["SortOrder"] = e.SortExpression;
         }
         bind();

    }
}
