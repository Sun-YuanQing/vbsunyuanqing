using System;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

public partial class _26_4线程操作之线程休眠_Thread : System.Web.UI.Page
{
    GridView GridView1 = new GridView();
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    public  void DBGridView()
    {
        GridView1.Columns.Clear(); //清除书所有列
        GridView1.AutoGenerateColumns = false; //是否为每一列自动创建绑定字段
        this.div_menu.Controls.Add(GridView1);

        String _str_select = "  k.*,l2.*  ";
        String _str_tabel = " (select * from t_gridview_menuL0 l0 " +
" left join t_gridview_menuL1 l1 on (l0.m0_id =l1.m1_m0id )) k   left join   t_gridview_menuL2 l2 on (l2 .m2_m1id =k .m1_id ) ";
        String _str_where = " 1=1 ";
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

        GridView1.DataSource = ds;
     //   GridView1.DataKeyNames = new string[] { GridView1.Rows[0].Cells[0].ToString() };//设定GridView1的kye

        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)   //绑定普通数据列
        {
            
            BoundField bfColumn = new BoundField();
            bfColumn.DataField = ds.Tables[0].Columns[i].ColumnName;
            bfColumn.HeaderText = ds.Tables[0].Columns[i].Caption;
            GridView1.Columns.Add(bfColumn);
        }
      
        GridView1.Columns[1].Visible = false;
        CommandField cfModify = new CommandField();  //绑定命令列
        cfModify.ButtonType = ButtonType.Button;
        cfModify.SelectText = "修改";
        cfModify.ShowSelectButton = true;
        GridView1.Columns.Add(cfModify);

        GridView1.DataBind();
    }

    public  void method()
    {
        string state;
        for (int i = 0; i < 500; i++)
        {
            state = Thread.CurrentThread.ThreadState.ToString();
            if (i % 5 == 0)
            {
                Label1.Text  += state + i;
               // Response.Write("<Script> alert('第" + i + "?')</Script>");
            }
            
          
        }
       
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {

        ThreadStart trs = new ThreadStart(method);
        Thread tr = new Thread(trs);
        tr.Start();
        while (tr .IsAlive )
        {
            tr.Suspend();//挂起
            Thread.Sleep(20);
            tr.Resume();//恢复
           
        }
    }
}