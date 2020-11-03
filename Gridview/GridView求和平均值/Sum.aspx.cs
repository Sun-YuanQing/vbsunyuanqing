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
    }
    private   int sum = 0;//取指定列的数据和，你要根据具体情况对待可能你要处理的是double
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowIndex >= 0)
        {
            sum  =sum+ Convert.ToInt32(e.Row.Cells[5].Text);      
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
             e.Row.Cells[0].ColumnSpan = e.Row.Cells.Count;

             e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

             e.Row.Cells[0].Text = "总价格为："+ sum.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;平均价格为：" + ((int)(sum / GridView1.Rows.Count)).ToString();

             for (int i = e.Row.Cells.Count; i > 1; i--)
             {
                 e.Row.Cells.RemoveAt(i - 1);
             }
        }
    }
  
}
