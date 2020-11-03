using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;


public partial class 数据统计图_柱状图 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        String _str_select = " * ";
        String _str_tabel = " Chart ";
        String _str_where = " 1=1 ";
        SqlCommand comm = new SqlCommand("sp_select", DBHelper.Conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;
        comm.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 300).Value = _str_tabel;
        comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 300).Value = _str_where;
        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = comm;
        DataSet ds = new DataSet();
        sda.Fill(ds);

        //清除默认的series
        Chart1.Series.Clear();
        //new 一个叫做【Strength】的系列
        Series Strength = new Series("力量");
        //设置chart的类型，这里为柱状图
        Strength.ChartType = SeriesChartType.Column;
        //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
        Strength.Points.AddXY("A", "90");
        Strength.Points.AddXY("B", "88");
        Strength.Points.AddXY("C", "60");
        Strength.Points.AddXY("D", "93");
        Strength.Points.AddXY("E", "79");
        Strength.Points.AddXY("F", "8");
        //把series添加到chart上
        Chart1.Series.Add(Strength); 
     
    }
}