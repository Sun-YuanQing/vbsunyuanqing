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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        { 
            //判断是否表头
            case  DataControlRowType.Header:
              //第一行表头
                 TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();

                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "2");
                tcHeader[0].Attributes.Add("bgcolor", "Azure");
                tcHeader[0].Text = "用户ID";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("colspan", "4");
                tcHeader[1].Attributes.Add("bgcolor", "Azure");
                tcHeader[1].Text = "基  本  信  息</th></tr><tr>";

                //第二行表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("bgcolor", "Azure");
                tcHeader[2].Text = "用户姓名";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("bgcolor", "Azure");
                tcHeader[3].Text = "性别";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("bgcolor", "Azure");
                tcHeader[4].Text = "家庭住址";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("bgcolor", "Azure");
                tcHeader[5].Text = "邮政编码";
               
            break;
        }
    }
}
