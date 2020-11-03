using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class 数据统计图_柱状图02 : System.Web.UI.Page
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

        Chart1.Series.Clear();
        Series Strength = new Series("力量");
        Series PH = new Series("血量");
        Series Speed= new Series("速度");

        Strength.ChartType = SeriesChartType.Column;
        Strength.IsValueShownAsLabel = true;
        Strength.Color = System.Drawing.Color.Cyan;


        PH.ChartType = SeriesChartType.Column;
        PH.IsValueShownAsLabel = true;
        PH.Color = System.Drawing.Color.Red;

        Speed.ChartType = SeriesChartType.Spline;
        Speed.IsValueShownAsLabel = true;

        Chart1.ChartAreas[0].AxisX.MajorGrid.Interval =0.5;
        Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled =true;
        //Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        Chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
        Chart1.ChartAreas[0].AxisX.Title = "我的英雄【悟空】";
        Chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

        Chart1.ChartAreas[0].AxisY.Title = "我是你孙爷爷";
        Chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
        Chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
        


        Strength.LegendText = "力气";
        Strength.Points.AddXY("A", ds.Tables[0].Rows[0]["A1"]);
        Strength.Points.AddXY("B", "88");
        Strength.Points.AddXY("C", "60");
        Strength.Points.AddXY("D", "93");
        Strength.Points.AddXY("E", "79");
        Strength.Points.AddXY("F1", "85");
        Strength.Points.AddXY("F2", "85");
        Strength.Points.AddXY("F3", "85");
        Strength.Points.AddXY("F4", "85");
        Strength.Points.AddXY("F5", "85");
        Strength.Points.AddXY("F6", "85");
        Strength.Points.AddXY("F", "85");

        PH.LegendText = "血量";
        PH.Points.AddXY("儿", ds.Tables[0].Rows[0]["B1"]);
        PH.Points.AddXY("B", "58");
        PH.Points.AddXY("C", "60");
        PH.Points.AddXY("D", "63");
        PH.Points.AddXY("E", "39");
        PH.Points.AddXY("F", "65");
        PH.Points.AddXY("B", "58");
        PH.Points.AddXY("C", "60");
        PH.Points.AddXY("D", "63");
        PH.Points.AddXY("E", "39");
        PH.Points.AddXY("F2", "65");
        PH.Points.AddXY("F", "65");

        Speed.Points.AddXY("A", "120");
        Speed.Points.AddXY("B", "133");
        Speed.Points.AddXY("C", "100");
        Speed.Points.AddXY("D", "98");
        Speed.Points.AddXY("E", "126");
        Speed.Points.AddXY("F", "89");

        //把series添加到chart上
        Chart1.Series.Add(Speed); 
        Chart1.Series.Add(Strength);
        Chart1.Series.Add(PH);
    }
}