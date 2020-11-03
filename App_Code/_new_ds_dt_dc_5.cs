using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

/// <summary>
/// _new_ds_dt_dc_5 的摘要说明
/// </summary>
public class _new_ds_dt_dc_5
{

    public static DataSet ds = new DataSet();

    public static String tc1 = "";
    public static String tc2 = "";

    public static DataSet t_gridview_menuL0(int m0_id, String m0_ttl, String m0_url, int m0_order, int m0_enadle)
        {
            //DataSet ds = new DataSet();
            //ds.Merge(_new_ds_dt_dc_5.ds.Tables["t_menuL0"]);
            ////  ds.Merge(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL1"]);
            //GridView1.DataSource = ds;
            //GridView1.DataBind();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("t_gridview_menuL0");
            ds.Tables.Add(dt);
            DataColumn dc1 = new DataColumn("m0_id", Type.GetType("System.Int32"));
            DataColumn dc2 = new DataColumn("m0_ttl", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("m0_url", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("m0_order", Type.GetType("System.Int32"));
            DataColumn dc5 = new DataColumn("m0_enadle", Type.GetType("System.Int32"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);

            DataRow dr = dt.NewRow();
            dr[0] = m0_id;
            dr["m0_ttl"] = m0_ttl.ToString();
            dr["m0_url"] = m0_url.ToString();
            dr["m0_order"] = m0_order;
            dr["m0_enadle"] = m0_enadle;
            dt.Rows.Add(dr);
            ds.AcceptChanges();
            return ds;
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataSet t_gridview_menuL1(int m1_id, int m1_m0id, String m1_ttl, String m1_url, int m1_order, int m1_enadle)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("t_gridview_menuL1");
        ds.Tables.Add(dt);
        DataColumn dc1 = new DataColumn("m1_id", Type.GetType("System.Int32"));
        DataColumn dc6 = new DataColumn("m1_m0id", Type.GetType("System.Int32"));//保持数据库的架构，不能将6放置最后
        DataColumn dc2 = new DataColumn("m1_ttl", Type.GetType("System.String"));
        DataColumn dc3 = new DataColumn("m1_url", Type.GetType("System.String"));
        DataColumn dc4 = new DataColumn("m1_order", Type.GetType("System.Int32"));
        DataColumn dc5 = new DataColumn("m1_enadle", Type.GetType("System.Int32"));
      
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc6);   //保持数据库的架构，不能将6放置最后
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);
        dt.Columns.Add(dc4);
        dt.Columns.Add(dc5);
        
        DataRow dr = dt.NewRow();
        dr[0] = m1_id;
        dr["m1_m0id"] = m1_m0id;
        dr["m1_ttl"] = m1_ttl.ToString();
        dr["m1_url"] = m1_url.ToString();
        dr["m1_order"] = m1_order;
        dr["m1_enadle"] = m1_enadle;
        dt.Rows.Add(dr);
        ds.AcceptChanges();
        return ds;


        //
        // TODO: 在此处添加构造函数逻辑
        //

    }
    public static DataSet t_gridview_menuL2(int m2_id, int m2_m0id, int m2_m1id, String m2_ttl, String m2_url, int m2_order, int m2_enadle)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("t_gridview_menuL2");
        ds.Tables.Add(dt);
        DataColumn dc1 = new DataColumn("m2_id", Type.GetType("System.Int32"));
        DataColumn dc2 = new DataColumn("m2_m1id", Type.GetType("System.Int32"));
        DataColumn dc3 = new DataColumn("m2_m0id", Type.GetType("System.Int32"));
       
        DataColumn dc4 = new DataColumn("m2_ttl", Type.GetType("System.String"));
        DataColumn dc5 = new DataColumn("m2_url", Type.GetType("System.String"));
        DataColumn dc6 = new DataColumn("m2_order", Type.GetType("System.Int32"));
        DataColumn dc7 = new DataColumn("m2_enadle", Type.GetType("System.Int32"));
      
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);
        dt.Columns.Add(dc4);
        dt.Columns.Add(dc5);
        dt.Columns.Add(dc6);
        dt.Columns.Add(dc7);
        DataRow dr = dt.NewRow();
        dr[0] = m2_id;
        dr["m2_m0id"] = m2_m0id;
        dr["m2_m1id"] = m2_m1id;
        dr["m2_ttl"] = m2_ttl.ToString();
        dr["m2_url"] = m2_url.ToString();
        dr["m2_order"] = m2_order;
        dr["m2_enadle"] = m2_enadle;
        dt.Rows.Add(dr);
        ds.AcceptChanges();
        return ds;


        //
        // TODO: 在此处添加构造函数逻辑
        //

    }

}