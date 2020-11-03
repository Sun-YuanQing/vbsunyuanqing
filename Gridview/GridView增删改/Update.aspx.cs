using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;

public partial class Update : System.Web.UI.Page
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
        SqlDataAdapter myda = new SqlDataAdapter(sqlstr, DBHelper.Conn);
        DataSet myds = new DataSet();
        DBHelper.OpenConnection();
        myda.Fill(myds);
        GridView1.DataSource = myds;
        GridView1.DataBind();
    }
    //编辑行
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;
        bind();
    }
    //更新行
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        sqlcon = new SqlConnection(strCon);
        string strsql = "update Admin set Name =@Name ,Sex =@Sex, Address =@Address,Post =@Post where CID = @CID ";
        SqlCommand sqlcmd = new SqlCommand(strsql, DBHelper.Conn);
        try
        {
            sqlcmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.Int, 4));
            sqlcmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
            sqlcmd.Parameters.Add(new SqlParameter("@Sex", SqlDbType.VarChar, 50));
            sqlcmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 50));
            sqlcmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.VarChar, 50));

            sqlcmd.Parameters["@CID"].Value = GridView1.Rows[e.RowIndex].Cells[0].Text;
            sqlcmd.Parameters["@Name"].Value = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            sqlcmd.Parameters["@Sex"].Value = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            sqlcmd.Parameters["@Address"].Value = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            sqlcmd.Parameters["@Post"].Value = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();


       DBHelper.OpenConnection();
            int i = sqlcmd.ExecuteNonQuery();
            this.GridView1.EditIndex = -1;
            if (i > 0)
            {
                Response.Write("<script lang ='ja' >alert('修改数据成功！');</script>");
            }

        }
        catch (SqlException ex)
        {

            throw ex;
        }
    DBHelper.CloseConnection();
        bind();
    }
    //取消编辑行
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GridView1.EditIndex = -1;
        bind();
    }
    //设定列宽
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
        {
            for (int i = 1; i < GridView1.Columns.Count - 1; i++)
            {

                TextBox txt = (TextBox)e.Row.Cells[i].Controls[0];
                txt.Width = Unit.Pixel(60);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            e.Row.Attributes["style"] = "Cursor:pointer";
        }

    }
}
