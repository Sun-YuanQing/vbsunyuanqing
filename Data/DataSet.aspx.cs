using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Data_DataSet : System.Web.UI.Page
{
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //当鼠标移到的时候设置该行颜色为""， 并保存原来的背景颜色
        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699CC'");
        //当鼠标移走时还原该行的背景色
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (true)//如果允许改变列宽
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("onmousemove", "SyDG_moveOnTd(this)");
                    e.Row.Cells[i].Attributes.Add("onmousedown", "SyDG_downOnTd(this)");
                    e.Row.Cells[i].Attributes.Add("onmouseup", "this.mouseDown=false");
                    e.Row.Cells[i].Attributes.Add("onmouseout", "this.mouseDown=false");
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack !=true   )
        {
            String _str_conn = "server=.;Database=MySchool;uid=sa;pwd=2222;";
            String _str_sql = "select yi_id ID,name 文件名,value 默认值,nooff 是否有效,er_id 上一级 from treeview  where er_id like '%3%'";
            SqlConnection conn = new SqlConnection(_str_conn);
            SqlDataAdapter da = new SqlDataAdapter(_str_sql, DBHelper.Conn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            //da.Fill(ds);
            //GridView1.DataSource = ds;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            DBHelper.CloseConnection();
        }
      
    }

    protected void gvdegreetype_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='GhostWhite'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
        }
    }
    protected void SqlDataAdapter_Click(object sender, EventArgs e)
    {
        String _str_conn = "server=.;Database=MySchool;uid=sa;pwd=2222;";
        String _str_sql = "select yi_id ID,name 文件名,value 默认值,nooff 是否有效,er_id 上一级 from treeview  where er_id like '%3%'";
        SqlConnection conn = new SqlConnection(_str_conn);
        SqlDataAdapter da = new SqlDataAdapter(_str_sql, DBHelper.Conn);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        da.Fill(dt);
        //da.Fill(ds);
        //GridView1.DataSource = ds;
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void SqlDataAdapter_comm_Click(object sender, EventArgs e)
    {

        String _str_select = " * ";
        String _str_tabel = " treeview ";
        String _str_where = "  yi_id like '%04%'";
        String _str_conn = "server=.;Database=myschool;uid=sa;pwd=2222;";
        SqlConnection conn = new SqlConnection(_str_conn);
        SqlCommand comm = new SqlCommand("sp_select", DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
        comm.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 300).Value = _str_tabel;
        comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = comm;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        sda.Fill(ds);
        sda.Fill(dt);
        GridView2.DataSource = ds;
        GridView2.DataBind();
    }
 


protected void SqlDataReader_Click(object sender, EventArgs e)
{
    String _str_conn = "server=.;Database=Myschool;uid=sa;pwd=2222;";
    String _str_sql = "select * from Treeview where yi_id like '%04%'";
    SqlConnection conn = new SqlConnection(_str_conn);
    SqlCommand comm = new SqlCommand(_str_sql, DBHelper.Conn);
    SqlDataReader reader = null;
    DBHelper.Conn.Open();
    reader = comm.ExecuteReader();
    GridView3.DataSource = reader;
    GridView3.DataKeyNames = new string[] {"yi_id"};
    GridView3.DataBind();
   DBHelper.Conn.Close();
}
protected void SqlDataReader_comm_Click(object sender, EventArgs e)
{
    String _str_conn = "server=.;Database=Myschool;uid=sa;pwd=2222;";
    String _str_select = " * ";
    String _str_tabel = " t_menuL1 ";
    String _str_where = " m1_id like '3' ";
    SqlConnection conn = new SqlConnection(_str_conn);
    SqlCommand comm = new SqlCommand("sp_select",DBHelper.Conn);
    comm.CommandType = CommandType.StoredProcedure;
    comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
    comm.Parameters.Add("@_comm_tabel",SqlDbType.NVarChar,300).Value = _str_tabel;
    comm.Parameters.Add("@_comm_where",SqlDbType.NVarChar,300).Value = _str_where;
    SqlDataReader sdr = null;
    DBHelper.OpenConnection();
    sdr = comm.ExecuteReader();
    GridView4.DataSource = sdr;

    GridView4.DataBind();
    DBHelper.CloseConnection();
}

protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
{
    String _yi_id = GridView3.DataKeys[e.NewSelectedIndex].Value.ToString();
    String _str_select = " * ";
    String _str_tabel = " treeview ";
    String _str_where = " yi_id like '"+ _yi_id +"'";
    SqlCommand comm = new SqlCommand("sp_select", DBHelper.Conn);
    comm.CommandType = CommandType.StoredProcedure;
    comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
    comm.Parameters.Add("@_comm_tabel",SqlDbType.NVarChar,300).Value = _str_tabel;
    comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
    SqlDataAdapter sda = new SqlDataAdapter();
    sda.SelectCommand = comm;
    DataSet ds = new DataSet();
    sda.Fill(ds);
    GridView5.DataSource = ds;
    GridView5.DataBind();
    
}
protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
{
    int i = GridView5.SelectedIndex;
    this.TextBox1.Text = GridView5.Rows[i].Cells[1].Text.ToString();
    GridViewRow row = GridView5.SelectedRow;
    this.TextBox2.Text = row.Cells[2].Text.ToString();
 
}

}