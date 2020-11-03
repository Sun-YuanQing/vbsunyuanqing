using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Gridview_显示空表头_EmptyDataText : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    private void bind()
    {
        string _str_select = " * ";
        string _str_table = " t_menuL0 ";
        string _str_where = " 1=1 ";
        SqlCommand comm = new SqlCommand("sp_select", DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
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
            //int columnCount = GridView1.Rows[0].Cells.Count;
            //GridView1.Rows[0].Cells.Clear();
            //GridView1.Rows[0].Cells.Add(new TableCell());
            //GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
            //GridView1.Rows[0].Cells[0].Text = "无内容";
            GridView1.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else {
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = true;    //开始添加
        this.GridView1.EditIndex = -1;       //取消编辑
        bind();
    }
 
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        String _txt_Footer__m0_ttl = ((TextBox)GridView1.FooterRow.FindControl("txt_Footer__m0_ttl")).Text.ToString();
        String _txt_Footer__m0_Url = ((TextBox)GridView1.FooterRow.FindControl("txt_Footer__m0_Url")).Text.ToString();
        String _txt_Footer__m0_order = ((TextBox)GridView1.FooterRow.FindControl("txt_Footer__m0_order")).Text.ToString();
        String _txt_Footer__m0_enable = ((TextBox)GridView1.FooterRow.FindControl("txt_Footer__m0_enable")).Text.ToString();

        SqlCommand comm = new SqlCommand("sp_insert", DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_table", SqlDbType.NVarChar, 300).Value = " t_menuL0 ";
        comm.Parameters.Add("@_comm_values", SqlDbType.NVarChar, 200).Value = " convert(nvarchar(max),'" + _txt_Footer__m0_ttl + "'),convert(nvarchar(max),'" + _txt_Footer__m0_Url + "')," + 0 + ",convert(int," + _txt_Footer__m0_enable + ")";
        string i = " convert(nvarchar(max),'" + _txt_Footer__m0_ttl + "'),convert(nvarchar(max),'" + _txt_Footer__m0_Url + "')," + 0 + ",convert(int," + _txt_Footer__m0_enable + ")";
        int _int_insert = comm.ExecuteNonQuery();
        if (_int_insert > 0)
        {
            Response.Write("<script lang ='ja' >alert('添加数据成功！😂');</script>");
            this.GridView1.ShowFooter = false;    //开始添加
            this.GridView1.EditIndex = -1;       //取消编辑
            bind();
        }
        else
        {
            Response.Write("<script lang ='ja' >alert('添加数据失败！😭');</script>");
            this.GridView1.ShowFooter = false;    //开始添加
            this.GridView1.EditIndex = -1;       //取消编辑
            bind();
        }
    }
    protected void btn_insert_colse_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = false;    //开始添加
        this.GridView1.EditIndex = -1;       //取消编辑
        bind();
    }
}