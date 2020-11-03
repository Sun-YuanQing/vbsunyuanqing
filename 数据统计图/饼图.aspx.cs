using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 数据统计图_饼图 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> xData = new List<string>() { "A", "B", "C", "一月" };
       
        List<int> yData = new List<int>() { 10, 20, 30,299  };
     
        Bingtu_Chart.Series[0]["PieLabelStyle"] = "Outside";//将文字移出
        Bingtu_Chart.Series[0]["PieLineColor"] = "Black";  //绘制黑色的连线.
        Bingtu_Chart.Series[0].Points.DataBindXY(xData, yData);
     
    }
}