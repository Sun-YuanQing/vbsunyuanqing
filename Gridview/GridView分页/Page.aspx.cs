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
    //绑定GridView
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    public void bind()
    {
        string connStr = "Data Source=(local);Database=wxd;Uid=sa;Pwd=sa";
        string SqlStr = "select  * from Admin";
        DataSet ds = new DataSet();
        try
        {
            SqlConnection conn = new SqlConnection(connStr);
            if (DBHelper.Conn.State.ToString() == "Closed")
                DBHelper.OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(SqlStr, DBHelper.Conn);
            da.Fill(ds, "Admin");
            if (DBHelper.Conn.State.ToString() == "Open") 
                DBHelper.CloseConnection();
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();

            btnFirst.Enabled = true;
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
           
            LblCurrentIndex.Text = "第 " + (GridView1.PageIndex + 1).ToString() + " 页";
            LblPageCount.Text = "共 " + GridView1.PageCount.ToString() + " 页";
            LblRecordCount.Text = "总共 " + ds.Tables[0].Rows.Count.ToString() + " 条";
            if (GridView1.PageIndex == 0)
            {
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
            }
            else if (GridView1.PageIndex == GridView1.PageCount-1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            pageDropDownList.Items.Clear();
            for (int i = 1; i < GridView1.PageCount+1; i++)
            {
                pageDropDownList.Items.Add(i.ToString());
            }
            pageDropDownList.SelectedValue = Convert.ToString(GridView1.PageIndex + 1);
            // 计算生成分页页码,分别为："首 页" "上一页" "下一页" "尾 页"
            btnFirst.CommandName = "1";
            btnPrev.CommandName = (GridView1.PageIndex == 0 ? "1" : GridView1.PageIndex.ToString());

            btnNext.CommandName = (GridView1.PageCount == 1 ? GridView1.PageCount.ToString() : (GridView1.PageIndex + 2).ToString());
            btnLast.CommandName = GridView1.PageCount.ToString();
          
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }

    }
    //分页
    protected void PagerButtonClick(object sender, EventArgs e)
    {
        GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
        bind();
    }
    //跳转页
    protected void pageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = Convert.ToInt32(pageDropDownList.SelectedValue) - 1;
        bind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes["onmouseover"] = "ItemOver(this)";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@;");
        }
    }
    
}
