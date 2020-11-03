using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Gridview_GridView实时更新数据_Timer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            //bind();
        }

    }
    private void bind()
    {
        string _str_select = " * ";
        string _str_table = " Admin ";
        string _str_where = " 1=1 ";
        SqlCommand comm = new SqlCommand("sp_select", DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
        comm.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 300).Value = _str_table;
        comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
        SqlDataAdapter sda = new SqlDataAdapter();
        DBHelper.OpenConnection();
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
        else
        {
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
        }
    }
    protected void SystemTime_Tick(object sender, EventArgs e)
    {
        bind();
    }
}