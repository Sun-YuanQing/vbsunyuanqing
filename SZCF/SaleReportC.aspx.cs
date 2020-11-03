using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Generic;
using System.IO;

public partial class Sales_SaleReportC : System.Web.UI.Page
{

    HSql db = new HSql();                //sql操作类
    string sd;

    SqlConnection CONN;                  //受db .address选择          作为动态的链接对象
    DataSet ds = new DataSet();          //全局查询数据集 
    DataSet ds_sum = new DataSet();   //全局综合查询
    protected void Page_Load(object sender, EventArgs e)
    {



        Check_syntheses.Attributes.Add("onclick", "chkRadio(this)");   //取消选中  考虑，吗，
        if (db.address == "SZCFERP" && ddl_address.SelectedValue.ToString() == "SZCFERP")
        {


            if (ddl_client.Items.Count < 2)
            {
                db.strconn = "connstrSZCFERP";   //SZCFERP
                ddl_client.Items.Clear(); //全部删除
                HSql info = new HSql(db.strconn);
                info.Open(@"select cm_addr,cm_addr+'-'+cm_sort as cm_sort  from  cm_mstr");
                if (info.m_table.Rows.Count > 0)
                {
                    ddl_client.DataSource = info.m_table.DefaultView;
                    ddl_client.DataValueField = info.m_table.Columns["cm_addr"].ColumnName;
                    ddl_client.DataTextField = info.m_table.Columns["cm_sort"].ColumnName;
                }
                info.Close();
                //ListItem("显示的值", "后台隐藏值");
                ListItem al = new ListItem("--全部--", "全部");
                al.Selected = true;
                ddl_client.Items.Insert(0, al);
                this.ddl_client.DataBind();
            }
        }


    }
    public DataSet dbds()
    {
        db.address = ddl_address.SelectedValue.ToString();
        if (db.address == "SZCFERP")
        {
            CONN = db.m_conn;   //SZCFERP
        }
        else
        {
            CONN = db.ConnGCERP;     //GCERP
        }
        String sdh_cust = ddl_client.SelectedItem.Value.ToString(); //当前文本

        int YEAR1 = Convert.ToInt32(txt_year1.Text.Trim());
        int YEAR2 = Convert.ToInt32(txt_year2.Text.Trim());
        if (YEAR1 < 2000 || YEAR2 < 2000)
        {
            Response.Write("<script lang ='ja' >alert('填写年份,年份大于要2000');</script>");
        }
        string month1 = ddl_month1.SelectedValue.ToString();
        string month2 = ddl_month2.SelectedValue.ToString();

        string str_ddl_bz = ddl_bz.SelectedValue.ToString();

        db.OpenConnection(CONN);
        SqlCommand comm;
        if (rdbtn_jdzhbg.Checked == true)//判断 季度销售付款统计报告 是否选中
        {
            comm = new SqlCommand("sp_xsjD", CONN); //季度销售付款统计报告
        }
        else
        {
            comm = new SqlCommand("sp_xiao_shou02", CONN);//月销售统计报告
        }

        comm.CommandTimeout = 9000; //设置SQL执行的超时时间
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add("@sdh_cust", SqlDbType.NVarChar, 300).Value = sdh_cust;
        comm.Parameters.Add("@year1", SqlDbType.NVarChar, 300).Value = YEAR1;
        comm.Parameters.Add("@year2", SqlDbType.NVarChar, 300).Value = YEAR2;
        comm.Parameters.Add("@month1", SqlDbType.NVarChar, 300).Value = month1;
        comm.Parameters.Add("@month2", SqlDbType.NVarChar, 300).Value = month2;
        comm.Parameters.Add("@cm_curr", SqlDbType.NVarChar, 300).Value = str_ddl_bz;
        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = comm;

        sda.Fill(ds);
        db.CloseConnection(CONN);
        #region   如果是销售付款季度统计 清除不需要的列
        if (rdbtn_jdzhbg.Checked == true)
        {
            ds.Tables[0].Columns.Remove("客户代码销售");
            ds.Tables[0].Columns.Remove("客户简称销售");
            ds.Tables[0].Columns.Remove("客户代码付款");
            ds.Tables[0].Columns.Remove("客户简称付款");
        }
        #endregion


        foreach (DataRow dr in ds.Tables[0].Rows)
        {  ///遍历所有的行
            //foreach (DataColumn dc in dt.Columns)
            //{  //遍历所有的列
            //}

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                Type type = dc.DataType;
                string neam = type.Name;
                if (neam == "decimal" || neam == "Decimal")
                {
                    var value = dr[dc];
                    if (value == DBNull.Value)
                    {
                        value = 0;
                    }
                    decimal temp = Convert.ToDecimal(value);
                    dr[dc] = Math.Round(temp, 2);
                }
            }

        }
        return ds;
    }
    /// <summary>
    /// dbds全部汇总算法。  为什么不用SQL SUM（）:因为表关联子表时，具体的列不能以数字2018开头，SQL无法实现。 
    /// </summary>
    /// <returns></returns>
    public DataSet dbds_sum()
    {
        ds.Clear();
        dbds();

        DataTable dt_sum = new DataTable();
        DataRow dr_sum = dt_sum.NewRow();
        ds_sum.Tables.Add(dt_sum);
        DataColumn dc0 = new DataColumn("客户代码", Type.GetType("System.String"));
        dt_sum.Columns.Add(dc0);
        dr_sum[0] = "全 部";

        DataColumn dc1 = new DataColumn("客户简称", Type.GetType("System.String"));
        dt_sum.Columns.Add(dc1);
        dr_sum[1] = "合 计";
        DataColumn dc2 = new DataColumn("币种", Type.GetType("System.String"));



        Double sum;
        string he = "";

        string he1 = "";

        if (ds.Tables[0].Rows.Count > 0)    //
        {
            dt_sum.Columns.Add(dc2);
            dr_sum[2] = Convert.ToString(ds.Tables[0].Rows[0][2]);
            for (int i = 3; i < ds.Tables[0].Columns.Count; i++)  //只从有 数值 的 第四列 开始循环
            {

                DataColumn dc3 = new DataColumn(ds.Tables[0].Columns[i].ColumnName.ToString(), Type.GetType("System.Double"));
                dt_sum.Columns.Add(dc3);

                sum = 0.0;
                for (int J = 0; J < ds.Tables[0].Rows.Count; J++)
                {
                    if (J + 1 == ds.Tables[0].Rows.Count)   //下一行是最后一行跳出
                    {
                        break;
                    }

                    he = ds.Tables[0].Rows[J][0].ToString() + "---" + ds.Tables[0].Columns[i].ColumnName.ToString() + "-----" + ds.Tables[0].Rows[J][i].ToString();
                    he1 = ds.Tables[0].Rows[J + 1][0].ToString() + "---" + ds.Tables[0].Columns[i].ColumnName.ToString() + "-----" + ds.Tables[0].Rows[J + 1][i].ToString();
                    sum = sum + Convert.ToDouble(ds.Tables[0].Rows[J][i]) + Convert.ToDouble(ds.Tables[0].Rows[J + 1][i]);  //同一列加下一行
                    J += 1; //到下下一计算开始实际结束+2


                }
                dr_sum[i] = sum;  //赋值   问题：无法找到列 2。  原因：列没有被插入（理论少了“币种”）。 解决办法：在else一开始就加一列
            }
            dt_sum.Rows.Add(dr_sum);
            ds_sum.AcceptChanges();
        }
        else
        {
            Response.Write("<script lang ='ja' >alert('没有数据！!!!!');</script>");

        }


        return ds_sum;
    }
    private void binddl()
    {

        db.address = ddl_address.SelectedValue.ToString();
        if (db.address == "SZCFERP")
        {
            CONN = db.m_conn;   //SZCFERP
        }
        else
        {
            CONN = db.ConnGCERP;     //GCERP  表格，柱状图，饼图 所用数据库 SqlConnection 对象
        }
        if (db.address == "SZCFERP")
        {
            db.strconn = "connstrSZCFERP";   //SZCFERP
        }
        else
        {
            db.strconn = "connstrGCERP";     //GCERP  表格，柱状图，饼图 所用数据库 配置字符串 
        }

        // CONN = db.m_conn;   //SZCFERP
        // String str_select = " cm_addr,cm_sort ";

        //  String str_tabel = "  cm_mstr   ";

        //  String str_where = " 1=1 ";
        //  db.OpenConnection(CONN);
        //   SqlCommand comm = new SqlCommand("sp_select", CONN);
        //   comm.CommandType = CommandType.StoredProcedure;
        //   comm.Parameters.Add("@comm_select", SqlDbType.NVarChar, 300).Value = str_select;
        //   comm.Parameters.Add("@comm_tabel", SqlDbType.NVarChar, 300).Value = str_tabel;
        //   comm.Parameters.Add("@comm_where", SqlDbType.NVarChar, 300).Value = str_where;
        //   SqlDataAdapter sda = new SqlDataAdapter();
        //   sda.SelectCommand = comm;
        //   DataSet ds = new DataSet();
        ////    sda.Fill(ds);

        ddl_client.Items.Clear(); //全部删除
        string str_ddl_bz = ddl_bz.SelectedValue.ToString();

        HSql info = new HSql(db.strconn);
        info.Open(@"select cm_addr,cm_addr+'-'+cm_sort as cm_sort  from  cm_mstr where  cm_curr='" + str_ddl_bz + "'");

        if (info.m_table.Rows.Count > 0)
        {
            ddl_client.DataSource = info.m_table.DefaultView;
            ddl_client.DataValueField = info.m_table.Columns["cm_addr"].ColumnName;
            ddl_client.DataTextField = info.m_table.Columns["cm_sort"].ColumnName;
        }
        info.Close();

        //ListItem("显示的值", "后台隐藏值");
        ListItem al = new ListItem("--全部--", "全部");
        al.Selected = true;
        ddl_client.Items.Insert(0, al);


        //dropdownlist1.Items.Clear(); 全部删除
        //dropdownlist1.Items.Remove(要删除的选项); 
        //dropdownlist1.Items.RemoveAt(要删除的选项的索引);

        // this.DropDownList1.Items.Insert(0, "--请选择--");
        Check_syntheses.Visible = true;
        this.ddl_client.DataBind();
        //db.CloseConnection(CONN);
    }
    protected void btn_exprot_Click(object sender, EventArgs e)
    {
        dbds();      //全局数据集
        heji();      //插入合计数据

        string strddl_address = ddl_address.SelectedValue.ToString();  //区域
        if (strddl_address.ToString () =="GCERP")
        {
        strddl_address = "G_";
        }else{
            strddl_address = "S_";
        }


        string strddl_bz = ddl_bz.SelectedValue.ToString();           //币种       
        string strddl_client = ddl_client.SelectedItem.Value.ToString()+"_";  //客户
        string strxls_type = "";
       if (rdbtn_ydxs.Checked==true)
	{
        strxls_type = "月度销售对比_";
	}
       if (rdbtn_jdzhbg.Checked == true)
       {
           strxls_type = "综合对比报表_";
       }


       CreateExcxel(ds, "1", strxls_type +strddl_client+strddl_address+strddl_bz+".xls");

    }
    /// <summary>
    /// 导出Excxel
    /// </summary>
    /// <param name="ds">要导出的数据集（目前仅支持DataSet）</param>
    /// <param name="typeid">"1"为启用Excxel引擎</param>
    /// <param name="Filename">自定义Excxel名称以.xls结尾</param>
    protected void CreateExcxel(DataSet ds, String typeid, String Filename)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); //GB2312 BIG5
        resp.ContentType = "application/ms-excel";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Filename));
        string strLine = "";


        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        //  DataRow myRow = dt.Rows[0];
        if (typeid == "1")
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strLine = strLine + dt.Columns[i].ColumnName.ToString() + Convert.ToChar(9);
            }
            resp.Write(strLine);
            strLine = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strLine = strLine + dt.Rows[i][j].ToString() + Convert.ToChar(9);
                    if (strLine.Contains("\r"))
                    {
                        strLine = strLine.Replace("\r", "");// 字符串替换方法Replace，把r 替换为t
                    }
                }

                resp.Write("\r" + strLine);
                strLine = "";
            }
        }
        resp.End();
    }
    /// <summary>
    /// 导出Excxel固定地址
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="filenames"></param>
    /// <returns></returns>
    public static bool haha(DataSet ds, string filenames)
    {
        try
        {
            string FileName = filenames;// "d:\\abc.xls";
            DataTable dt = ds.Tables[0];
            FileStream objFileStream;
            StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);


            objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strLine = strLine + dt.Columns[i].ColumnName.ToString() + Convert.ToChar(9);
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strLine = strLine + (i + 1) + Convert.ToChar(9);
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    strLine = strLine + dt.Rows[i][j].ToString() + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            objStreamWriter.Close();
            objFileStream.Close();
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }

    }
    protected void ddl_address_TextChanged1(object sender, EventArgs e)
    {
        binddl();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        dbds();


        #region  gv_vs--全部--
        if (ds.Tables[0].Rows.Count > 0)
        {


            if (ddl_client.SelectedValue.ToString() != "全部")
            {
                div_gv_vs.Visible = true;
                gv_vs.Visible = true;
                div_columnar.Visible = true;

                Chart_columnar.Visible = true;
                div_bingtu.Visible = true;
                Check_syntheses.Checked = false;
                //选了单个客户
                #region RadioButton1 gv_vs++++++++++++++  #endregion

                gv_vs.DataSource = ds;
                gv_vs.DataBind();
                #endregion
                #region rdbtn_columnar 柱状图  #endregion

              
                    Chart_columnar.Series.Clear();
                    Series First = new Series("前一年");
                    Series Last = new Series("后一年");
                    Series Speed = new Series("速度");

                    First.ChartType = SeriesChartType.Column;
                    First.IsValueShownAsLabel = true;
                    First.Color = System.Drawing.Color.Cyan;


                    Last.ChartType = SeriesChartType.Column;
                    Last.IsValueShownAsLabel = true;
                    Last.Color = System.Drawing.Color.Red;

                    Speed.ChartType = SeriesChartType.Spline;
                    Speed.IsValueShownAsLabel = true;

                    Chart_columnar.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                    Chart_columnar.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    //Chart_columnar.ChartAreas[0].Area3DStyle.Enable3D = true;
                    Chart_columnar.ChartAreas[0].AxisX.IsMarginVisible = true;
                    String sdh_cust = ddl_client.SelectedValue.ToString();
                    Chart_columnar.ChartAreas[0].AxisX.Title = "客户代码" + sdh_cust;
                    Chart_columnar.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

                    Chart_columnar.ChartAreas[0].AxisY.Title = "";
                    Chart_columnar.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
                    Chart_columnar.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;

                    First.LegendText = "力气";
                    Last.LegendText = "血量";
                    //DataRow myRow = dt.Rows[0];
                    //resp.Write(strLine);
                    //strLine = "";
                    for (int J = 0; J < ds.Tables[0].Rows.Count; J++)
                    {

                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            //  strLine = strLine + ds.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                            if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2017") == true)
                            {
                                First.Points.AddXY(ds.Tables[0].Columns[i].ColumnName.ToString(), ds.Tables[0].Rows[0][i]);
                            }
                            if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2018") == true)
                            {
                                Last.Points.AddXY(ds.Tables[0].Columns[i].ColumnName.ToString(), ds.Tables[0].Rows[0][i]);
                            }
                        }
                    }
                    //Speed.Points.AddXY("A", "120");
                    //Speed.Points.AddXY("B", "133");
                    //Speed.Points.AddXY("C", "100");
                    //Speed.Points.AddXY("D", "98");
                    //Speed.Points.AddXY("E", "126");
                    //Speed.Points.AddXY("F", "89");

                    //把series添加到chart上
                    //  Chart_columnar.Series.Add(Speed);
                    Chart_columnar.Series.Add(First);
                    Chart_columnar.Series.Add(Last);
                 #endregion
                #region rdbtn_pie 饼 图  #endregion

                if (ds.Tables[0].Rows.Count > 0)
                {


                    List<string> xData = new List<string>() { };
                    List<Double> yData = new List<Double>() { };
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {
                        //  strLine = strLine + ds.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                        if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2017"))
                        {
                            xData.Add(ds.Tables[0].Columns[i].ColumnName.ToString());

                        }

                    }
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {

                        if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2017"))
                        {
                            yData.Add(Convert.ToDouble(ds.Tables[0].Rows[0][i]));
                        }

                    }
                    Chart_Bingtu1.Series[0]["PieLabelStyle"] = "Outside";//将文字移出
                    Chart_Bingtu1.Series[0]["PieLineColor"] = "Black";  //绘制黑色的连线.
                    Chart_Bingtu1.Series[0].Points.DataBindXY(xData, yData);
                    List<string> xData2 = new List<string>() { };
                    List<int> yData2 = new List<int>() { };
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {
                        //  strLine = strLine + ds.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                        if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2018"))
                        {
                            xData2.Add(ds.Tables[0].Columns[i].ColumnName.ToString());

                        }

                    }
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {

                        if (ds.Tables[0].Columns[i].ColumnName.ToString().Contains("2018"))
                        {
                            yData2.Add(Convert.ToInt32(ds.Tables[0].Rows[0][i]));
                        }

                    }
                    Chart_Bingtu2.Series[0]["PieLabelStyle"] = "Outside";//将文字移出
                    Chart_Bingtu2.Series[0]["PieLineColor"] = "Black";  //绘制黑色的连线.
                    Chart_Bingtu2.Series[0].Points.DataBindXY(xData2, yData2);
                }else{
                
                  Response.Write("<script lang ='ja' >alert('单个客户，饼图—没有数据！');</script>");
                }
                #endregion
                }
                else
                {

                Check_syntheses.Visible = true;


                div_gv_vs.Visible = true;
                gv_vs.Visible = true;
                div_columnar.Visible = false;

                Chart_columnar.Visible = false;
                div_bingtu.Visible = false;

                //综合数据集
                dbds_sum();
                //如果选了  --全部--  分两种情况
                if (Check_syntheses.Checked != true)
                {
                    //选了 --全部-- 【没】选合计
                    #region RadioButton1 gv_vs++++++++++++++  #endregion

                    gv_vs.Visible = true;
                    div_gv_vs.Visible = true;
                    div_columnar.Visible = false;
                    Chart_columnar.Visible = false;
                    div_bingtu.Visible = false;
               
                    DataRow dr = ds.Tables[0].NewRow();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        dr[i] = ds_sum.Tables[0].Rows[0][i];

                    }
                    ds.Tables[0].Rows.Add(dr);  //添加合计行
                    gv_vs.DataSource = ds;
                    gv_vs.DataBind();


                    #endregion
                }
                else
                {
                
                    //选了--全部--【和】选合计
                    div_gv_vs.Visible = true;
                    gv_vs.Visible = true;
                    div_columnar.Visible = true;

                    Chart_columnar.Visible = true;
                    div_bingtu.Visible = true;


                    #region RadioButton1 gv_vs++++++++++++++  #endregion
                    //ds.Merge(ds_sum);

                    //DataSet dss = new DataSet();
                    //dss.Merge(ds);
                    //dss.Merge(ds_sum);
                    //ds.Merge(ds_sum, false, MissingSchemaAction.AddWithKey);
                    //DataRow dr=  ds_sum.Tables[0].Rows[0];
                    //DataRow dr1 = new DataRow();
                    //dr1 = dr;
                    // ds.Tables[0].Rows.Add(dr1);
                    //  for (int ids = 0; ids < ds.Tables.Count; ids++)
                    //{
                    //    //DataTable dt = new DataTable();
                    //dt.TableName = "Table" + ids.ToString();
                    //dt.Columns.Add(new DataColumn("第一列"));
                    //dt.Columns.Add(new DataColumn("第二列"));



                    DataRow dr = ds.Tables[0].NewRow();
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        dr[i] = ds_sum.Tables[0].Rows[0][i];

                    }
                    ds.Tables[0].Rows.Add(dr);  //添加合计行
                    gv_vs.DataSource = ds;
                    gv_vs.DataBind();


                    #endregion
                    #region RadioButton1 柱状图


                    Chart_columnar.Series.Clear();
                    Series First = new Series("前一年");
                    Series Last = new Series("后一年");
                    Series Speed = new Series("速度");

                    First.ChartType = SeriesChartType.Column;
                    First.IsValueShownAsLabel = true;
                    First.Color = System.Drawing.Color.Cyan;


                    Last.ChartType = SeriesChartType.Column;
                    Last.IsValueShownAsLabel = true;
                    Last.Color = System.Drawing.Color.Red;

                    Speed.ChartType = SeriesChartType.Spline;
                    Speed.IsValueShownAsLabel = true;

                    Chart_columnar.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                    Chart_columnar.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    //Chart_columnar.ChartAreas[0].Area3DStyle.Enable3D = true;
                    Chart_columnar.ChartAreas[0].AxisX.IsMarginVisible = true;
                    String sdh_cust = ddl_client.SelectedValue.ToString();
                    Chart_columnar.ChartAreas[0].AxisX.Title = "客户代码" + sdh_cust;
                    Chart_columnar.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

                    Chart_columnar.ChartAreas[0].AxisY.Title = "";
                    Chart_columnar.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
                    Chart_columnar.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;

                    First.LegendText = "力气";
                    Last.LegendText = "血量";
                    //DataRow myRow = dt.Rows[0];
                    //resp.Write(strLine);
                    //strLine = "";

                    for (int J = 0; J < ds_sum.Tables[0].Rows.Count; J++)
                    {

                        for (int i = 0; i < ds_sum.Tables[0].Columns.Count; i++)
                        {
                            //  strLine = strLine + ds_sum.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                            if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2017") == true)
                            {
                                First.Points.AddXY(ds_sum.Tables[0].Columns[i].ColumnName.ToString(), ds_sum.Tables[0].Rows[0][i]);
                            }
                            if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2018") == true)
                            {
                                Last.Points.AddXY(ds_sum.Tables[0].Columns[i].ColumnName.ToString(), ds_sum.Tables[0].Rows[0][i]);
                            }
                        }
                    }
                    //把series添加到chart上
                    //  Chart_columnar.Series.Add(Speed);
                    Chart_columnar.Series.Add(First);
                    Chart_columnar.Series.Add(Last);
                    #endregion
                    #region RadioButton1 饼 图


                    List<string> xData = new List<string>() { };
                    List<Double> yData = new List<Double>() { };
                    for (int i = 2; i < ds_sum.Tables[0].Columns.Count; i++)
                    {
                        //  strLine = strLine + ds_sum.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                        if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2017"))
                        {
                            xData.Add(ds_sum.Tables[0].Columns[i].ColumnName.ToString());

                        }

                    }
                    for (int i = 2; i < ds_sum.Tables[0].Columns.Count; i++)
                    {

                        if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2017"))
                        {
                            yData.Add(Convert.ToDouble(ds_sum.Tables[0].Rows[0][i]));
                        }

                    }
                    Chart_Bingtu1.Series[0]["PieLabelStyle"] = "Outside";//将文字移出
                    Chart_Bingtu1.Series[0]["PieLineColor"] = "Black";  //绘制黑色的连线.
                    Chart_Bingtu1.Series[0].Points.DataBindXY(xData, yData);
                    List<string> xData2 = new List<string>() { };
                    List<int> yData2 = new List<int>() { };
                    for (int i = 2; i < ds_sum.Tables[0].Columns.Count; i++)
                    {
                        //  strLine = strLine + ds_sum.Tables[0].Columns[i].ColumnName.ToString() + Convert.ToChar(9);
                        if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2018"))
                        {
                            xData2.Add(ds_sum.Tables[0].Columns[i].ColumnName.ToString());

                        }

                    }
                    for (int i = 2; i < ds_sum.Tables[0].Columns.Count; i++)
                    {

                        if (ds_sum.Tables[0].Columns[i].ColumnName.ToString().Contains("2018"))
                        {
                            yData2.Add(Convert.ToInt32(ds_sum.Tables[0].Rows[0][i]));
                        }

                    }
                    Chart_Bingtu2.Series[0]["PieLabelStyle"] = "Outside";//将文字移出
                    Chart_Bingtu2.Series[0]["PieLineColor"] = "Black";  //绘制黑色的连线.
                    Chart_Bingtu2.Series[0].Points.DataBindXY(xData2, yData2);


                    #endregion
                }

            }
             
               
            
       
        #endregion
        }
        else
        {

            Response.Write("<script lang ='ja' >alert('没有数据！');</script>");
        }
    }

    protected void ddl_bz_TextChanged(object sender, EventArgs e)
    {

        binddl();

        if (ddl_client.SelectedValue.ToString() != "全部")
        {
            div_gv_vs.Visible = true;
            gv_vs.Visible = true;
            div_columnar.Visible = true;

            Chart_columnar.Visible = true;
            div_bingtu.Visible = true;
            Check_syntheses.Visible = false; ;
            Check_syntheses.Checked = false;
        }
        else
        {
            Check_syntheses.Visible = true;
            Check_syntheses.Checked = false;
        }

      
    }


    /// <summary>
    ///   在GridView中动态添加模板列
    /// </summary>
    /// <returns></returns>
    #region    在GridView中动态添加模板列  未使用
    ICollection CreateDataSource()
    {
        DataTable dt = new DataTable();//创建一个DataTable对象
        DataRow dr;
        //定义框架
        dt.Columns.Add(new DataColumn("id", typeof(Int32)));
        dt.Columns.Add(new DataColumn("text", typeof(string)));
        for (int i = 0; i < 6; i++)//循环遍历并添充DataTable内容
        {
            dr = dt.NewRow();
            dr[0] = i;
            dr[1] = "列表" + i.ToString();
            dt.Rows.Add(dr);
        }
        DataView dv = new DataView(dt);
        return dv;
    }
    public class GridViewTemplate : ITemplate//定义一个继承自ITemplate接口的类
    {
        private DataControlRowType dcrType;
        private string columnName;
        public GridViewTemplate(DataControlRowType type, string colname)
        {
            dcrType = type;
            columnName = colname;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (dcrType)
            {
                case DataControlRowType.Header:
                    Literal literal = new Literal();
                    literal.Text = columnName;
                    container.Controls.Add(literal);
                    break;
                case DataControlRowType.DataRow:
                    DropDownList ddl = new DropDownList();
                    ddl.ID = "dropdownlist";
                    ddl.AppendDataBoundItems = true;
                    ddl.Items.Add(new ListItem("-----请选择------", ""));
                    ddl.Items.Add(new ListItem("京广高速公路时刻表", "98"));
                    ddl.Items.Add(new ListItem("京昆高速公路时刻表", "368"));
                    ddl.Items.Add(new ListItem("京城高速公路时刻表", "698"));
                    container.Controls.Add(ddl);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion


    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue.ToString() == "全部")
        {
            Check_syntheses.Visible = true;
        }
        else
        {
            Check_syntheses.Visible = false;
        }

    }



    public void heji()
    {


        dbds();
        if (ds.Tables.Count >0)
        {
            dbds_sum();
       
        if (ds_sum.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                dr[i] = ds_sum.Tables[0].Rows[0][i];

            } ds.Tables[0].Rows.Add(dr);
        }

        }
        else
        {
            Response.Write("<script lang ='ja' >alert('ds_没有数据！');</script>");
        }
    }

}
