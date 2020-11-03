using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class C_事务_SqlTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
        
    }
    public void bind() {
        string _str_select = " * ";
        string _str_table = " t_gridview_menuL0 ";
        string _str_where = " 1=1 ";
        SqlCommand comm = new SqlCommand("sp_select",DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_select",SqlDbType.NVarChar,300).Value = _str_select;
        comm.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 300).Value = _str_table;
        comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = comm;
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView1.Rows[0].Cells[0].Text = "无内容";
            GridView1.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //主键没用到
            GridView1.DataKeyNames = new String[] { "m0_id" };
        }

       
    }
    //编辑
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;//开始编辑
        this.GridView1.ShowFooter = false;  //取消添加
        bind();

        int _int_m0_id = Convert.ToInt32(((Label)GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("lbl_EditItem__m0_id")).Text);
        int _int_m0_order = Convert.ToInt32(((TextBox)GridView1.Rows[e.NewEditIndex].Cells[3].FindControl("txt_EditItem__m0_order")).Text);
        
        String _str_select = " m0_order ";
        String _str_table = " t_gridview_menuL0 ";
        String _str_where = " m0_id='" + _int_m0_id + "'";
        DBHelper.OpenConnection();
        SqlCommand comm1 = new SqlCommand("sp_select",DBHelper.Conn);
        comm1.CommandType = CommandType.StoredProcedure;
        comm1.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
        comm1.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 300).Value = _str_table;
        comm1.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
       int m0_order=Convert.ToInt32( comm1.ExecuteScalar());
       if (m0_order > 0)
       {
           Response.Write("<script lang ='ja' >alert('数据正在由他人修改！，请稍等☺');</script>");
       }
       else
       {
         //没有人修改时，修改数据为“锁定”
           _str_select = " set m0_order=" + 1 + " ";
           SqlCommand comm = new SqlCommand("sp_upDate", DBHelper.Conn);
           comm.CommandType = CommandType.StoredProcedure;
           comm.Parameters.Add("@comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
           comm.Parameters.Add("@comm_table", SqlDbType.NVarChar, 300).Value = _str_table;
           comm.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
           int ok = comm.ExecuteNonQuery();
         
       }
       DBHelper.CloseConnection();
    }
    /// 更新编辑
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String _str_select = null;
        String _str_table = null;
        String _str_where = null;
        int ok;

        //创建了一个事务
        SqlTransaction strans = null;
        try
        {
            int m0_id = Convert.ToInt32(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lbl_EditItem__m0_id")).Text);
            String m0_ttl = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0].FindControl("txt_EditItem__m0_ttl"))).Text.ToString().Trim();
            String m0_Url = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0].FindControl("txt_EditItem__m0_Url"))).Text.ToString().Trim();
            int m0_order = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].Cells[3].FindControl("txt_EditItem__m0_order")).Text);
            int m0_enable = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].Cells[4].FindControl("txt_EditItem__m0_enable")).Text);
                   //判断数据锁定
            if (m0_id >= 0 && m0_ttl != string.Empty && m0_Url != string.Empty && m0_order <= 0 && m0_enable >= 0)
            {
                _str_select = "  set m0_ttl='" + m0_ttl + "', m0_Url='" + m0_Url + "', m0_order=" + 0 + ",  m0_enable=" + m0_enable;
                _str_table = "  t_gridview_menuL0  ";
                _str_where = "  m0_id ='" + m0_id + "'";

                DBHelper.OpenConnection();
                //创建了一个事务
                strans = DBHelper.Conn.BeginTransaction();

                SqlCommand comm = new SqlCommand("sp_update", DBHelper.Conn);

                comm.Transaction = strans;

                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
                comm.Parameters.Add("@comm_table", SqlDbType.NVarChar, 300).Value = _str_table;
                comm.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = _str_where;

                ok = comm.ExecuteNonQuery();
                if (ok > 0)
                {
                    strans.Commit();
                    Response.Write("<script lang ='ja' >alert('修改数据成功！');</script>");

                }
                else
                {
                    Response.Write("<script lang ='ja' >alert('修改数据失败！ 请注意选择项.');</script>");
                    strans.Rollback();
                }
                DBHelper.CloseConnection();
            }
            else {
                Response.Write("<script lang ='ja' >alert('修改数据失败！ 或数据被锁定！');</script>");
                GridView1.EditIndex = -1;
            }
            GridView1.EditIndex = -1;
            bind();

        }
        catch (Exception eX)
        {

            Console.WriteLine(eX.Message);
            strans.Rollback();
        }

    }
    /// 取消编辑
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
    }
    //添加
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = true;    //开始添加
        this.GridView1.EditIndex = -1;       //取消编辑
        bind();
    }
    //开始添加
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        Response.Write("<script lang ='ja' >alert('功能制作未完！待续☺');</script>");
    }
    //取消添加
    protected void btn_insert_colse_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = false;    //开始添加
        this.GridView1.EditIndex =-1;       //取消编辑
        bind();
    }
   
    // 删除
    protected void imabtn_Del_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ibtn = (ImageButton)sender;             //单击做出响应的控件
        GridViewRow gr = (GridViewRow)ibtn.Parent.Parent;   //控件中的单独行（ImageButton选中的行）
        int _int__m0_id = Convert.ToInt32(((Label)GridView1.Rows[gr.RowIndex].Cells[0].FindControl("lbl__m0_id")).Text);
        
        String _str_select2 = "  ";
        String _str_table2 = " t_gridview_menuL2  ";
        String _str_where2 = "  m2_m0id='" + _int__m0_id + "'";
        DBHelper.OpenConnection();
        SqlCommand comm2 = new SqlCommand("sp_Delete", DBHelper.Conn);
        comm2.CommandType = CommandType.StoredProcedure;
        comm2.Parameters.Add("@comm_select ", SqlDbType.NVarChar, 300).Value = _str_select2;
        comm2.Parameters.Add("@comm_table", SqlDbType.NVarChar, 300).Value = _str_table2;
        comm2.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = _str_where2;
       int _int_ok2= comm2.ExecuteNonQuery();

        String _str_select1 = "  ";
        String _str_table1 = " t_gridview_menuL1  ";
        String _str_where1 = "  m1_m0id='" + _int__m0_id + "'";
        SqlCommand comm1 = new SqlCommand("sp_Delete", DBHelper.Conn);
        comm1.CommandType = CommandType.StoredProcedure;
        comm1.Parameters.Add("@comm_select ", SqlDbType.NVarChar, 300).Value = _str_select1;
        comm1.Parameters.Add("@comm_table", SqlDbType.NVarChar, 300).Value = _str_table1;
        comm1.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = _str_where1;
       int _int_ok1= comm1.ExecuteNonQuery();

        String _str_select = "  ";
        String _str_table = " t_gridview_menuL0  ";
        String _str_where = "  m0_id='" + _int__m0_id + "'";
        SqlCommand comm = new SqlCommand("sp_Delete",DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@comm_select ", SqlDbType.NVarChar, 300).Value = _str_select;
        comm.Parameters.Add("@comm_table", SqlDbType.NVarChar, 300).Value = _str_table;
        comm.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
        int _int_ok = comm.ExecuteNonQuery();

        int smun = _int_ok + _int_ok1 + _int_ok2;
        if (smun > 0)
        {
          //  strans.Commit();
            Response.Write("<script lang ='ja' >alert('删除数据成功！☺');</script>");

        }
        else
        {
            Response.Write("<script lang ='ja' >alert('删除数据失败！😭');</script>");
          //  strans.Rollback();
        }
        DBHelper.CloseConnection(); 
        bind();
    }
    //行的样式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            e.Row.Attributes["style"] = "Cursor:pointer";
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //分页
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}