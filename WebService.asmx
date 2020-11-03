<%@ WebService Language="C#" Class="WebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
//using vbsunyuanqing.App_Code; // vb
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections;

using Google.ProtocolBuffers;
using com.gexin.rp.sdk.dto;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using com.igetui.api.openservice.payload;
using System.Net;
/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    //+++++++++++++++++++++++++++++++++++++++++++++++
    //connstrAPP是用的正式数据库!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //++++++++++++++++++++↓↓↓↓↓↓↓+++++++++++++
    string connstr_GC_SZ = "connstrAPP";
    public WebService()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    //参数设置 
    //http的域名
    private static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";
   //定义常量, appId、appKey、masterSecret 采用本文档 "第二步 获取访问凭证 "中获得的应用配置 【个推】
    private static String APPID = "Tmnd3qnPXJ6c8hVyce6u4";
    private static String APPKEY = "kNLHV52krj5fmyRTcpaMX9";
    private static String MASTERSECRET = "lZeGPIE7mV6tRfgwha0HV9";
    //private static String CLIENTID = "80023f3021539037165092abca1256b2";
    //ef56781af4f5f86b83e3d2c03699f9df
    //private static String CLIENTID1 = "e605a0db5ce3cca9b76b012978064940";
    //private static String CLIENTID2 = "e605a0db5ce3cca9b76b012978064940";
    //private static String GroupName = "app推送";
    //private static String Badge = "50";
    //private static String TASKID = "OSA-0903_bWHwhpFPEC7i5nZwHmc6d";
    //private static String ALIAS = "请输入别名";
    //private static string PN = "13550347892";
    [WebMethod(Description = "指定设备（用户）推送消息.")]
    public void PushMessageToSingle(String CLIENTID, string begin, string end, string str_message)
    {
        IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

        //消息模版：TransmissionTemplate:透传模板

        TransmissionTemplate template = TransmissionTemplateDemo(begin, end, str_message);

        // 单推消息模型
        SingleMessage message = new SingleMessage();
        message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
        message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
        message.Data = template;
        //判断是否客户端是否wifi环境下推送，2为4G/3G/2G，1为在WIFI环境下，0为不限制环境
        //message.PushNetWorkType = 1;  

        com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
        target.appId = APPID;
        target.clientId = CLIENTID;
        //target.alias = ALIAS;
        try
        {
            String pushResult = push.pushMessageToSingle(message, target);

            Context.Response.Charset = "utf-8";
            Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Context.Response.Write(pushResult);
            Context.Response.End();
            //System.Console.WriteLine("----------------服务端返回结果：" + pushResult);
        }
        catch (RequestException e)
        {
            String requestId = e.RequestId;
            //发送失败后的重发
            String pushResult = push.pushMessageToSingle(message, target, requestId);
            Context.Response.Charset = "utf-8";
            Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Context.Response.Write(pushResult);
            Context.Response.End();
            //System.Console.WriteLine("----------------服务端返回结果：" + pushResult);
        }
    }
    public static TransmissionTemplate TransmissionTemplateDemo(string Begin, string End, string str_message)
    {
        TransmissionTemplate template = new TransmissionTemplate();
        template.AppId = APPID;
        template.AppKey = APPKEY;
        //应用启动类型，1：强制应用启动 2：等待应用启动
        template.TransmissionType = 1;
        //透传内容  
        template.TransmissionContent = str_message;
        //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟

        return template;
    }

    [WebMethod(Description = "查找有权限的用户(人).")]
    public void sp_up_user(string up_menu_id, string column)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@up_menu_id" , SqlDbType.NVarChar, 50,up_menu_id),
           hs.MakeInParam("@column" , SqlDbType.NVarChar, 50,column),
        };

        hs.RunProc("sp_up_user", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 未完生产单
    /// </summary>
    /// <param name="wo_nbr"></param>
    /// <param name="wo_line"></param>
    /// <param name="wo_part"></param>
    /// <param name="wo_due_date1"></param>
    /// <param name="wo_due_date2"></param>
    /// <param name="proc"></param>
    [WebMethod(Description = "未完生产单.")]
    public void sp_wo_mstr_untreated(string wo_nbr, string wo_line, string wo_part, string wo_due_date1, string wo_due_date2, string proc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@wo_nbr" , SqlDbType.NVarChar, 50,wo_nbr),
           hs.MakeInParam("@wo_line" , SqlDbType.NVarChar, 50,wo_line),
           hs.MakeInParam("@wo_part" , SqlDbType.NVarChar, 50,wo_part),
           hs.MakeInParam("@wo_due_date1" , SqlDbType.NVarChar, 50,wo_due_date1),
           hs.MakeInParam("@wo_due_date2" , SqlDbType.NVarChar, 50,wo_due_date2),
        };

        hs.RunProc(proc, paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    
    /// <summary>
    /// 查询sp_select  SQL
    /// </summary>
    /// <param name="sql"></param>
   [WebMethod(Description = "查询sp_select  SQL.")]
    public void sp_select(string sql)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
         tmpDS=  hs.DSet(sql);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("{");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
        }
        Json.Append("}");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 指定单别查询  出入库单据详情   sp_mov_mov_date  的进化版
    /// </summary>
    /// <param name="mov_mov"></param>
    /// <param name="mov_cat"></param>
    /// <param name="mov_type"></param>
    /// <param name="mov_doc_code"></param>
      [WebMethod(Description = "指定单别查询  出入库单据详情   sp_mov_mov_date  的进化版.")]
    public void sp_mov_doc_code_data(string mov_mov, string mov_cat, string mov_type, string mov_doc_code)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@mov_mov" , SqlDbType.NVarChar, 30 ,mov_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@mov_cat" , SqlDbType.NVarChar, 30 ,mov_cat),
                hs.MakeInParam("@mov_type" , SqlDbType.NVarChar, 30 ,mov_type),
                hs.MakeInParam("@mov_doc_code" , SqlDbType.NVarChar, 30 ,mov_doc_code),
            };
        hs.RunProc("sp_mov_doc_code_data", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\""+tmpDS.Tables[a].TableName.ToString() + "\":[");
                //  Json.Append("[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }

        // Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    
    /// <summary>
    /// 指定单别查询  出入库单据头表 sp_mov_mov_query 的进化版
    /// </summary>
    /// <param name="mov_mov"></param>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <param name="mov_cat">出入库原因代码C005送货通知单备货 m1011,m1012</param>
    /// <param name="mov_type">类型RCT入仓ISS出仓TR转仓</param>
    /// <param name="mov_crt_by"></param>
    /// <param name="part"></param>
    /// <param name="mov_doc_code">单别（固定值30 计划外入仓单31计划外出仓单32转仓单）</param>
    /// <param name="pst">过账</param>
    [WebMethod(Description = "指定单别查询  出入库单据头表 sp_mov_mov_query 的进化版.")]
    public void sp_mov_mov_type_query(string mov_mov, string date1, string date2, string mov_cat,string mov_type,string mov_crt_by,string part,string mov_doc_code, int pst)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@mov_mov" , SqlDbType.NVarChar, 30 ,mov_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@date1" , SqlDbType.NVarChar, 30 ,date1),
                hs.MakeInParam("@date2" , SqlDbType.NVarChar, 30 ,date2),
                hs.MakeInParam("@mov_cat" , SqlDbType.NVarChar, 30 ,mov_cat),
                hs.MakeInParam("@mov_type" , SqlDbType.NVarChar, 30 ,mov_type),
                hs.MakeInParam("@mov_crt_by" , SqlDbType.NVarChar, 30 ,mov_crt_by),
                hs.MakeInParam("@part" , SqlDbType.NVarChar, 50 ,part),
                hs.MakeInParam("@mov_doc_code" , SqlDbType.NVarChar, 30 ,mov_doc_code),
                hs.MakeInParam("@pst" , SqlDbType.Int,2,pst),
            };
        hs.RunProc("sp_mov_mov_type_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 执行添加 sql语句
    /// </summary>
    /// <param name="sql">strsql</param>
    [WebMethod(Description = "执行添加 sql语句.")]
     public void InsertTable(string sql)
     {
        //连接字符串
         string str = ConfigurationManager.ConnectionStrings[connstr_GC_SZ].ConnectionString;  
         //声明数据库连接
         SqlConnection conn = new SqlConnection(str);
         SqlConnection con = new SqlConnection(str);//连接数据库
         con.Open();
         SqlTransaction trans = con.BeginTransaction();//事物对象
         string insertok = "失败";
         try 
	     {
            SqlCommand com = new SqlCommand();//数据操作对象
            com.Connection = con;//指定连接
            com.Transaction = trans;//指定事物
            com.CommandText = sql;
            com.ExecuteNonQuery();//执行该行
            trans.Commit();//如果全部执行完毕.提交
            insertok = "添加成功";
	      }
	      catch (Exception e)
	      {
              insertok = e.ToString();
             trans.Rollback();
	      }
         Context.Response.Charset = "utf-8";
         Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
         Context.Response.Write(insertok);
         Context.Response.End();
     }  
    /// <summary>
    ///获取拆解的明细数量
    /// </summary>
    /// <param name="part"></param>
   [WebMethod(Description = "获取拆解的明细数量.")]
    public void sp_split_det_new(int numder, string ps_par,string ps_comp)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@numder" , SqlDbType.Int,999999,numder),  //拆解最大数
           hs.MakeInParam("@ps_par" , SqlDbType.NVarChar,2000,ps_par),
           hs.MakeInParam("@ps_comp" , SqlDbType.NVarChar,2000,ps_comp),
        };
        hs.RunProc("sp_split_det_new", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
        
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    
    /// <summary>
    /// --拆解物料级别  物料之间的关系
    /// </summary>
    /// <param name="part"></param>
    [WebMethod(Description = "拆解物料级别  物料之间的关系.")]
    public void sp_treeview(string part)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
        };
        hs.RunProc("sp_treeview", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        } Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 可 组装 子件物料 库存查询，库位(可用)不包含IQC,OVR,RTV,WIP,CH
    /// </summary>
    /// <param name="part">物料</param>
    /// <param name="loc">库位</param>
    [WebMethod(Description = "可 组装 子件物料 库存查询，库位(可用)不包含IQC,OVR,RTV,WIP,CH.")]
    public void sp_loc_psrt_query_2(string part, string loc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
           hs.MakeInParam("@loc" , SqlDbType.NVarChar, 50,loc),
        };
        hs.RunProc("sp_loc_psrt_query_2", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 关联bom_mstr树结构可 组装 物料查询，库位(可用)不包含IQC,OVR,RTV,WIP,CH
    /// </summary>
    /// <param name="part">物料</param>
    /// <param name="loc">库位</param>
    [WebMethod(Description = "关联bom_mstr树结构可 组装 物料查询，库位(可用)不包含IQC,OVR,RTV,WIP,CH.")]
    public void sp_loc_psrt_query_1(string part, string loc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
           hs.MakeInParam("@loc" , SqlDbType.NVarChar, 50,loc),
        };
        hs.RunProc("sp_loc_psrt_query_1", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询可拆解的物料，库位不包含IQC,OVR,RTV,WIP,CH,''
    /// </summary>
    /// <param name="part">物料</param>
    /// <param name="loc">库位</param>
     [WebMethod(Description = "查询可拆解的物料，库位不包含IQC,OVR,RTV,WIP,CH,''.")]
    public void sp_loc_psrt_query(string part, string loc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
           hs.MakeInParam("@loc" , SqlDbType.NVarChar, 50,loc),
        };
        hs.RunProc("sp_loc_psrt_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
   /// <summary>
    /// 登录修改登录版本和登录时间
   /// </summary>
   /// <param name="user_id"></param>
   /// <param name="app_version"></param>
   [WebMethod(Description = "登录修改登录版本和登录时间.")]
    public void sp_client_login_update(string user_id, string app_version, string app_clientid, string app_appid, string app_appkey,
        string system_os_name,string system_os_version,string system_device_model,string system_device_vendor,
       string system_device_uuid, string system_device_imsi)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 50,user_id),
           hs.MakeInParam("@app_version" , SqlDbType.NVarChar, 50,app_version),
           hs.MakeInParam("@app_clientid" , SqlDbType.NVarChar, 50,app_clientid),
           hs.MakeInParam("@app_appid" , SqlDbType.NVarChar, 50,app_appid),
           hs.MakeInParam("@app_appkey" , SqlDbType.NVarChar, 50,app_appkey),
            hs.MakeInParam("@system_os_name" , SqlDbType.NVarChar, 50,system_os_name),
           hs.MakeInParam("@system_os_version" , SqlDbType.NVarChar, 50,system_os_version),
           hs.MakeInParam("@system_device_model" , SqlDbType.NVarChar, 50,system_device_model),
           hs.MakeInParam("@system_device_vendor" , SqlDbType.NVarChar, 50,system_device_vendor),
           hs.MakeInParam("@system_device_uuid" , SqlDbType.NVarChar, 50,system_device_uuid),
           hs.MakeInParam("@system_device_imsi" , SqlDbType.NVarChar, 50,system_device_imsi),
        };
        hs.RunProc("sp_client_login_update", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 插入登录用户API
    /// </summary>
    /// <param name="user_id"></param>
     [WebMethod(Description = "插入登录用户API.")]
    public void sp_inesrt_login_client(string user_id,string user_name,string app_id,string app_token,
        string app_clientid,string app_appid,string app_appkey,string app_version,
        string system_os_name,string system_os_version,string system_device_model,string system_device_vendor,
        string system_device_uuid, string system_device_imsi)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 50,user_id),
           hs.MakeInParam("@user_name" , SqlDbType.NVarChar, 50,user_name),
           hs.MakeInParam("@app_id" , SqlDbType.NVarChar, 50,app_id),
           hs.MakeInParam("@app_token" , SqlDbType.NVarChar, 50,app_token),
           hs.MakeInParam("@app_clientid" , SqlDbType.NVarChar, 50,app_clientid),
           hs.MakeInParam("@app_appid" , SqlDbType.NVarChar, 50,app_appid),
           hs.MakeInParam("@app_appkey" , SqlDbType.NVarChar, 50,app_appkey),
           hs.MakeInParam("@app_version" , SqlDbType.NVarChar, 50,app_version),
           hs.MakeInParam("@system_os_name" , SqlDbType.NVarChar, 50,system_os_name),
           hs.MakeInParam("@system_os_version" , SqlDbType.NVarChar, 50,system_os_version),
           hs.MakeInParam("@system_device_model" , SqlDbType.NVarChar, 50,system_device_model),
           hs.MakeInParam("@system_device_vendor" , SqlDbType.NVarChar, 50,system_device_vendor),
           hs.MakeInParam("@system_device_uuid" , SqlDbType.NVarChar, 50,system_device_uuid),
           hs.MakeInParam("@system_device_imsi" , SqlDbType.NVarChar, 50,system_device_imsi),
        };
        hs.RunProc("sp_inesrt_login_client", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
           
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询用户登录API参数
    /// </summary>
    /// <param name="user_id"></param>
     [WebMethod(Description = "查询用户登录API参数.")]
    public void sp_login_client_query(string user_id,string app_clientid)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 50,user_id),
            hs.MakeInParam("@app_clientid" , SqlDbType.NVarChar, 50,app_clientid),
        };
        hs.RunProc("sp_login_client_query", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 未完采购
    /// </summary>
    /// <param name="po_vend"></param>
    /// <param name="po_nbr"></param>
    /// <param name="pod_pr"></param>
    /// <param name="part"></param>
    /// <param name="po_ord_date1"></param>
    /// <param name="po_ord_date2"></param>
    /// <param name="pod_due_date1"></param>
    /// <param name="pod_due_date2"></param>
    /// <param name="po_type"> </param>
    [WebMethod(Description = "未完采购.")]
    public void sp_po_mstr_untreated(string po_vend, string po_nbr, string pod_pr, string part, string po_ord_date1, string po_ord_date2, string pod_due_date1, string pod_due_date2, string po_type)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@po_vend" , SqlDbType.NVarChar, 50,po_vend),
           hs.MakeInParam("@po_nbr" , SqlDbType.NVarChar, 50,po_nbr),
           hs.MakeInParam("@pod_pr" , SqlDbType.NVarChar, 50,pod_pr),
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
           hs.MakeInParam("@po_ord_date1" , SqlDbType.NVarChar, 50,po_ord_date1),
           hs.MakeInParam("@po_ord_date2" , SqlDbType.NVarChar, 50,po_ord_date2),
           hs.MakeInParam("@pod_due_date1" , SqlDbType.NVarChar, 50,pod_due_date1),
           hs.MakeInParam("@pod_due_date2" , SqlDbType.NVarChar, 50,pod_due_date2),
           hs.MakeInParam("@po_type" , SqlDbType.NVarChar, 50,po_type),
        };

        hs.RunProc("sp_po_mstr_untreated", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    
    /// <summary>
    /// 未完订单
    /// </summary>
    /// <param name="cust"></param>
    /// <param name="so_nbr"></param>
    /// <param name="so_ord_date1"></param>
    /// <param name="so_ord_date2"></param>
    /// <param name="sod_due_date1"></param>
    /// <param name="sod_due_date2"></param>
    /// <param name="part"></param>
    /// <param name="proc">执行的存储过程</param>
   [WebMethod(Description = "未完订单.")]
    public void sp_so_mstr_query(string cust, string so_nbr, string so_ord_date1, string so_ord_date2, string sod_due_date1, string sod_due_date2, string part,string  proc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@cust" , SqlDbType.NVarChar, 50,cust),
           hs.MakeInParam("@so_nbr" , SqlDbType.NVarChar, 50,so_nbr),
           hs.MakeInParam("@so_ord_date1" , SqlDbType.NVarChar, 50,so_ord_date1),
           hs.MakeInParam("@so_ord_date2" , SqlDbType.NVarChar, 50,so_ord_date2),
           hs.MakeInParam("@sod_due_date1" , SqlDbType.NVarChar, 50,sod_due_date1),
           hs.MakeInParam("@sod_due_date2" , SqlDbType.NVarChar, 50,sod_due_date2),
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part),
        };

        hs.RunProc(proc, paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
           
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    
    [WebMethod]
    public void sp_ld_det_ch01_cust(string ld_loc, string ld_part)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@ld_loc" , SqlDbType.NVarChar, 50,ld_loc),
           hs.MakeInParam("@ld_part" , SqlDbType.NVarChar, 50,ld_part),
        };
        hs.RunProc("sp_ld_det_ch01_cust", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 送货单数据集
    /// </summary>
    /// <param name="time1">送货日期</param>
    /// <param name="time2">送货日期</param>
    /// <param name="cust">送货客户</param>
    /// <param name="dn_dn">送货单号</param>
    /// <param name="part">送货明细物料</param>
    [WebMethod(Description = "送货单数据集.")]
    public void sp_dn_dn_data(string time1, string time2, string cust, string dn_dn, string part)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@time1" , SqlDbType.NVarChar, 50,time1), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
           hs.MakeInParam("@time2" , SqlDbType.NVarChar, 50,time2),
           hs.MakeInParam("@cust" , SqlDbType.NVarChar, 50,cust),
           hs.MakeInParam("@dn_dn" , SqlDbType.NVarChar, 50,dn_dn),
            hs.MakeInParam("@part" , SqlDbType.NVarChar, 100,part),
           
        };
        hs.RunProc("sp_dn_dn_data", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
   /// <summary>
   /// 送货单明细
   /// </summary>
   /// <param name="dnd_dn"></param>
   [WebMethod(Description = "送货单明细.")]
    public void sp_dnd_det_query( string dnd_dn)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
         
           hs.MakeInParam("@dnd_dn" , SqlDbType.NVarChar, 50,dnd_dn),
         
        };
        hs.RunProc("sp_dnd_det_query", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
  /// <summary>
   /// 送货单头表
  /// </summary>
  /// <param name="time1">送货日期</param>
  /// <param name="time2">送货日期</param>
  /// <param name="cust">送货客户</param>
  /// <param name="dn_dn">送货单号</param>
  /// <param name="part">送货明细物料</param>
   [WebMethod(Description = "送货单头表.")]
    public void sp_dn_mstr_query(string time1, string time2, string cust, string dn_dn, string part,int pst)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@time1" , SqlDbType.NVarChar, 50,time1), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
           hs.MakeInParam("@time2" , SqlDbType.NVarChar, 50,time2),
           hs.MakeInParam("@cust" , SqlDbType.NVarChar, 50,cust),
           hs.MakeInParam("@dn_dn" , SqlDbType.NVarChar, 50,dn_dn),
            hs.MakeInParam("@part" , SqlDbType.NVarChar, 100,part),
            hs.MakeInParam("@pst" , SqlDbType.Int, 2,pst),
           
        };
        hs.RunProc("sp_dn_mstr_query", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 物料代码描述混合查询代码
    /// </summary>
    /// <param name="part"></param>

    [WebMethod(Description = "物料代码描述混合查询代码.")]
    public void sp_loc_part(string part)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            
        };
        hs.RunProc("sp_loc_part", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"", "“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_ptp1_loc(string ptp1_part)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@ptp1_part" , SqlDbType.NVarChar, 50,ptp1_part), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            
        };
        hs.RunProc("sp_ptp1_loc", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_wo_grn(string part)
    {

        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@part" , SqlDbType.NVarChar, 50,part), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
        };
        hs.RunProc("sp_wo_grn", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询物料库存信息
    /// </summary>
    /// <param name="ld_part">物料</param>
    [WebMethod(Description = "查询物料库存信息.")]
    public void sp_scan_query(string ld_part)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@ld_part" , SqlDbType.VarChar, 30 ,ld_part), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
              
            };

        hs.RunProc("sp_scan_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_up(string usr_user,string up_menu_id)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@usr_user" , SqlDbType.VarChar, 30 ,usr_user), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@up_menu_id" , SqlDbType.VarChar, 30 ,up_menu_id),
            };
        hs.RunProc("sp_up", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_edit_iqcd_iqc(string iqcd_iqc, string iqcd_part, int iqcd_line, float iqcd_qty,float iqcd_qty_acpt,float iqcd_qty_rts)
    {
        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@iqcd_iqc" , SqlDbType.NVarChar, 50,iqcd_iqc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@iqcd_part" , SqlDbType.NVarChar, 100,iqcd_part),
                hs.MakeInParam("@iqcd_line" , SqlDbType.Int,999999,iqcd_line),
                hs.MakeInParam("@iqcd_qty" , SqlDbType.Float,999999,iqcd_qty),
                hs.MakeInParam("@iqcd_qty_acpt" , SqlDbType.Float,999999,iqcd_qty_acpt),
                hs.MakeInParam("@iqcd_qty_rts" , SqlDbType.Float,999999,iqcd_qty_rts),
                
            };

        hs.RunProc("sp_edit_iqcd_iqc", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_edit_fgrd_fgr(string fgrd_fgr, string fgrd_part, int fgrd_line, int fgrd_qty)
    {
        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@fgrd_fgr" , SqlDbType.NVarChar, 50,fgrd_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@fgrd_part" , SqlDbType.NVarChar, 100,fgrd_part),
                hs.MakeInParam("@fgrd_line" , SqlDbType.Int,99999 ,fgrd_line),
                hs.MakeInParam("@fgrd_qty" , SqlDbType.Int,99999,fgrd_qty),
            };

        hs.RunProc("sp_edit_fgrd_fgr", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    /// <summary>
    /// 删除IQC检验入库单
    /// </summary>
    /// <param name="iqcd_iqc">单号</param>
    /// <param name="iqcd_line">行号</param>
     [WebMethod(Description = "删除IQC检验入库单.")]
    public void sp_iqcd_delete(string iqcd_iqc, int iqcd_line)
    {

        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@iqcd_iqc" , SqlDbType.NVarChar, 50,iqcd_iqc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@iqcd_line" , SqlDbType.Int,999 ,iqcd_line),
            };
        hs.RunProc("sp_iqcd_delete", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["msg"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    /// <summary>
    /// 删除入库单
    /// </summary>
    /// <param name="fgrd_fgr"></param>
    /// <param name="fgrd_line"></param>
 [WebMethod(Description = "删除入库单.")]
    public void sp_fgrd_delete(string fgrd_fgr, int fgrd_line)
    {

        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@fgrd_fgr" , SqlDbType.NVarChar, 50,fgrd_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@fgrd_line" , SqlDbType.Int,999 ,fgrd_line),
            };
        hs.RunProc("sp_fgrd_delete", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["msg"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
   /// <summary>
   /// 删除转仓单
   /// </summary>
   /// <param name="movd_mov"></param>
   /// <param name="movd_line"></param>
     [WebMethod(Description = "删除转仓单.")]
    public void sp_movd_delete(string movd_mov, int movd_line)
    {
        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@movd_mov" , SqlDbType.NVarChar, 50,movd_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@movd_line" , SqlDbType.Int,999 ,movd_line),
            };
        hs.RunProc("sp_movd_delete", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["msg"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_grn_grn_date(string time1, string time2, string usr_user, string grn_grn, string grnd_part)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@time1" , SqlDbType.NVarChar, 50,time1), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
           hs.MakeInParam("@time2" , SqlDbType.NVarChar, 50,time2),
           hs.MakeInParam("@usr_user" , SqlDbType.NVarChar, 50,usr_user),
           hs.MakeInParam("@grn_grn" , SqlDbType.NVarChar, 50,grn_grn),
            hs.MakeInParam("@grnd_part" , SqlDbType.NVarChar, 100,grnd_part),
           
        };
        hs.RunProc("sp_grn_grn_date", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_iqc_iqc_date(string iqc_iqc)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@iqc_iqc" , SqlDbType.NVarChar, 50,iqc_iqc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
        };
        hs.RunProc("sp_iqc_iqc_date", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }

    [WebMethod]
    public void sp_fgr_fgr_date(string fgr_fgr)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@fgr_fgr" , SqlDbType.NVarChar, 50,fgr_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
        };
        hs.RunProc("sp_fgr_fgr_date", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_mov_mov_date(string mov_mov)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
           hs.MakeInParam("@mov_mov" , SqlDbType.NVarChar, 50,mov_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            
        };
        hs.RunProc("sp_mov_mov_date", paramsArr, ref tmpDS);
        hs.Close();

        DataTable dt = new DataTable();
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            Json.Append("{");
            for (int a = 0; a < tmpDS.Tables.Count; a++)
            {
                dt = tmpDS.Tables[a];
              //    Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                Json.Append("\"" + tmpDS.Tables[a].TableName.ToString() + "\":[");
                //  Json.Append("[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]");
                if (a < tmpDS.Tables.Count - 1)
                {
                    Json.Append(",");
                }
            }
            Json.Append("}");
        }

        // Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }

    [WebMethod]
    public void sp_edit_movd_mov(string movd_mov, string movd_part, int movd_line, int movd_qty)
    {

        string msg = "[请联系统管理员] 未知错误！";
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@movd_mov" , SqlDbType.NVarChar, 50,movd_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
               
                hs.MakeInParam("@movd_part" , SqlDbType.NVarChar, 100,movd_part),
                hs.MakeInParam("@movd_line" , SqlDbType.Int,999 ,movd_line),
                hs.MakeInParam("@movd_qty" , SqlDbType.Int,99999999,movd_qty),
            };

        hs.RunProc("sp_edit_movd_mov", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    
    /// <summary>
    /// 格创4.0送货单过账
    /// </summary>
    /// <param name="dn_dn"></param>
    /// <param name="check_sum"></param>
    /// <param name="user_id"></param>
    /// <param name="menu_id"></param>
    /// <param name="sign"></param>
     [WebMethod(Description = "格创4.0送货单过账.")]
    public void sp_sodnmt01_pst(string dn_dn, int check_sum, string user_id, string menu_id, int sign)
    {
        string msg = "[请联系统管理员] 未知错误！";

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@dn_dn" , SqlDbType.VarChar, 30 ,dn_dn), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@check_sum" , SqlDbType.Int,2 ,check_sum),
                hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 100 ,user_id),
                hs.MakeInParam("@menu_id" , SqlDbType.NVarChar, 100 ,menu_id),
                hs.MakeInParam("@sign" , SqlDbType.Int,2,sign),
         };

        hs.RunProc("sp_sodnmt01_pst", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    /// <summary>
    /// 深圳送货单过账
    /// </summary>
    /// <param name="iqc_iqc"></param>
    /// <param name="check_sum"></param>
    /// <param name="user_id"></param>
    /// <param name="menu_id"></param>
    /// <param name="sign"></param>
    [WebMethod(Description = "深圳送货单过账.")]
    public void sp_sodn_pst(string dn_dn, int check_sum, string user_id, string menu_id, int sign)
    {
        string msg = "[请联系统管理员] 未知错误！";

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@dn_dn" , SqlDbType.VarChar, 30 ,dn_dn), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@check_sum" , SqlDbType.Int,2 ,check_sum),
                hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 100 ,user_id),
                hs.MakeInParam("@menu_id" , SqlDbType.NVarChar, 100 ,menu_id),
                hs.MakeInParam("@sign" , SqlDbType.Int,2,sign),
              
              
               
            };

        hs.RunProc("sp_sodn_pst", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    /// <summary>
    /// 格创4.0iqc过账
    /// </summary>
    /// <param name="iqc_iqc"></param>
    /// <param name="check_sum"></param>
    /// <param name="user_id"></param>
    /// <param name="menu_id"></param>
    /// <param name="sign"></param>
    [WebMethod(Description = "格创4.0iqc过账.")]
    public void sp_puiqmt01_pst(string iqc_iqc, int check_sum, string user_id, string menu_id, int sign)
    {
        string msg = "[请联系统管理员] 未知错误！";

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@iqc_iqc" , SqlDbType.VarChar, 30 ,iqc_iqc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@check_sum" , SqlDbType.Int,2 ,check_sum),
                hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 100 ,user_id),
                hs.MakeInParam("@menu_id" , SqlDbType.NVarChar, 100 ,menu_id),
                hs.MakeInParam("@sign" , SqlDbType.Int,2,sign),
              
              
               
            };

        hs.RunProc("sp_puiqmt01_pst", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    /// <summary>
    /// 入库单过账
    /// </summary>
    /// <param name="fgr_fgr"></param>
    /// <param name="check_sum"></param>
    /// <param name="user_id"></param>
    /// <param name="menu_id"></param>
    /// <param name="sign"></param>
   [WebMethod(Description = "入库单过账.")]
    public void sp_wofrmt01_pst(string fgr_fgr, int check_sum, string user_id, string menu_id, int sign)
    {
        string msg = "[请联系统管理员] 未知错误！";

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
        {
                hs.MakeInParam("@fgr_fgr" , SqlDbType.VarChar, 30 ,fgr_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@check_sum" , SqlDbType.Int,2 ,check_sum),
                hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 100 ,user_id),
                hs.MakeInParam("@menu_id" , SqlDbType.NVarChar, 100 ,menu_id),
                hs.MakeInParam("@sign" , SqlDbType.Int,2,sign),
              
              
               
            };

        hs.RunProc("sp_wofrmt01_pst", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }
    //选择入库单据 sp_fgrd_query
    [WebMethod(Description = "选择入库单据.")]
    public void sp_fgrd_query(string fgrd_fgr)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@fgrd_fgr" , SqlDbType.VarChar, 30 ,fgrd_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
     
            };

        hs.RunProc("sp_fgrd_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 选择单据 sp_mov_movd_query
    /// </summary>
    /// <param name="movd_mov"></param>

      [WebMethod(Description = "选择单据.")]
    public void sp_mov_movd_query(string movd_mov)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@movd_mov" , SqlDbType.VarChar, 30 ,movd_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            };
        hs.RunProc("sp_mov_movd_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询备货单
    /// </summary>
    /// <param name="mov_mov">备货单</param>
    /// <param name="time1">单据日期开始</param>
    /// <param name="time2">单据日期结束</param>
     [WebMethod(Description = "查询备货单.")]
    public void sp_iqc_mstr_query(string iqc_iqc, string time1, string time2, string cust, int  pst)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@iqc_iqc" , SqlDbType.VarChar, 30 ,iqc_iqc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@time1" , SqlDbType.VarChar, 30 ,time1),
                hs.MakeInParam("@time2" , SqlDbType.VarChar, 30 ,time2),
                hs.MakeInParam("@cust" , SqlDbType.VarChar, 30 ,cust),
                hs.MakeInParam("@pst" , SqlDbType.Int, 2 ,pst),
            };

        hs.RunProc("sp_iqc_mstr_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询入仓单
    /// </summary>
    /// <param name="fgr_fgr">单号</param>
    /// <param name="time1">单据日期开始</param>
    /// <param name="time2">单据日期结束</param>
   [WebMethod(Description = "查询入仓单.")]
    public void sp_fgr_fgr_query(string fgr_fgr, string time1, string time2, string wo_line, string fgrd_wo_nbr ,int pst)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@fgr_fgr" , SqlDbType.VarChar, 30 ,fgr_fgr), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@time1" , SqlDbType.VarChar, 30 ,time1),
                hs.MakeInParam("@time2" , SqlDbType.VarChar, 30 ,time2),
                hs.MakeInParam("@wo_line" , SqlDbType.VarChar, 30 ,wo_line),
                hs.MakeInParam("@fgrd_wo_nbr" , SqlDbType.VarChar, 30 ,fgrd_wo_nbr),
                hs.MakeInParam("@pst" , SqlDbType.Int, 2 ,pst),
            };

        hs.RunProc("sp_fgr_fgr_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    /// <summary>
    /// 查询备货单
    /// </summary>
    /// <param name="mov_mov">备货单</param>
    /// <param name="time1">单据日期开始</param>
    /// <param name="time2">单据日期结束</param>
     [WebMethod(Description = "查询备货单.")]
    public void sp_mov_mov_query(string mov_mov, string time1, string time2, string movd_dna_nbr, string cust, int pst)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@mov_mov" , SqlDbType.NVarChar, 30 ,mov_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@time1" , SqlDbType.NVarChar, 30 ,time1),
                hs.MakeInParam("@time2" , SqlDbType.NVarChar, 30 ,time2),
                hs.MakeInParam("@movd_dna_nbr" , SqlDbType.NVarChar, 30 ,movd_dna_nbr),
                hs.MakeInParam("@cust" , SqlDbType.NVarChar, 30 ,cust),
                hs.MakeInParam("@pst" , SqlDbType.Int,2,pst),
            };
        hs.RunProc("sp_mov_mov_query", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }
    [WebMethod]
    public void sp_mov_pst(string mov_mov, int check_sum, string user_id, string menu_id, string sign, string proc)
    {
        String msg = "[请联系统管理员] 未知错误！";

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@mov_mov" , SqlDbType.NVarChar, 30 ,mov_mov), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@check_sum" , SqlDbType.Int,2 ,check_sum),
                hs.MakeInParam("@user_id" , SqlDbType.NVarChar, 100 ,user_id),
                hs.MakeInParam("@menu_id" , SqlDbType.NVarChar, 100 ,menu_id),
                hs.MakeInParam("@sign" , SqlDbType.Int,2,sign),
                hs.MakeInParam("@proc" , SqlDbType.NVarChar,50,proc),
                
            };

        hs.RunProc("sp_mov_pst", paramsArr, ref tmpDS);
        hs.Close();
        if (tmpDS.Tables != null)
        {
            msg = tmpDS.Tables[0].Rows[0]["返回信息"].ToString();
        }
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(msg);
        Context.Response.End();
    }

    [WebMethod]
    public void json_tabel(string json, string dataname)
    {
      
        DataTable dataTable = new DataTable("VS_JSON");
        //实例化        
        DataTable result;
        try
        {
            string insertok = "未知";
 JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue;
            //取得最大数值            
            ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
            if (arrayList.Count > 0)
            {
                foreach (Dictionary<string, object> dictionary in arrayList)
                {
                    if (dictionary.Keys.Count<string>() == 0)
                    {
                        result = dataTable;
                        //   return result;
                    }
                    if (dataTable.Columns.Count == 0)
                    {

                        foreach (string current in dictionary.Keys)
                        {
                            //dataTable.Columns.Add(current, dictionary[current].GetType());
                            dataTable.Columns.Add(current);
                        }
                    }
                    DataRow dataRow = dataTable.NewRow();
                    foreach (string current in dictionary.Keys)
                    {
                        if (!dataTable.Columns.Contains(current))
                        {
                            dataTable.Columns.Add(current);
                        }
                        dataRow[current] = dictionary[current];
                    }
                    dataTable.Rows.Add(dataRow);
                    //循环添加行到DataTable中               
                }
            }
            InsertTable(dataTable, dataname, ref  insertok);

            Context.Response.Charset = "utf-8";
            Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Context.Response.Write(insertok);
            Context.Response.End();

        }
        catch (Exception)
        {
            throw;
        }
        //return result;

    }
    [WebMethod]
    public void sp_select_loc()
    {
        
        string str = ConfigurationManager.ConnectionStrings[connstr_GC_SZ].ConnectionString;
        //声明数据库连接
        SqlConnection conn = new SqlConnection(str);//连接数据库
        conn.Open();
        SqlTransaction trans = conn.BeginTransaction();//事物对象
        try
        {
            string sql = "select loc_site,loc_loc,loc_desc,loc_type from gcerp40_demo.dbo.loc_mstr where loc_site='1200' ";
            SqlCommand comm = new SqlCommand();//数据操作对象
            comm.Connection = conn;//指定连接
            comm.Transaction = trans;//指定事物
            comm.CommandText = sql;
          //  com.ExecuteNonQuery();//执行该行
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = comm;
            DataTable dt = new DataTable();
            sda.Fill(dt);   
            trans.Commit();//如果全部执行完毕.提交
           
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null )
            {
               if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                           Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            Context.Response.Charset = "utf-8";
            Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Context.Response.Write(Json);
            Context.Response.End();
        }
        catch
        {
            trans.Rollback();

        }
    }
    public void InsertTable(DataTable dt, string TabelName, ref string insertok)
    {
        
        string str = ConfigurationManager.ConnectionStrings["connstrGCERP"].ConnectionString;
        //声明数据库连接
        SqlConnection conn = new SqlConnection(str);
        SqlConnection con = new SqlConnection(str);//连接数据库
        con.Open();
        SqlTransaction trans = con.BeginTransaction();//事物对象
        try
        {
            SqlCommand com = new SqlCommand();//数据操作对象
            com.Connection = con;//指定连接
            com.Transaction = trans;//指定事物
            string sql = "";
            if (TabelName == "fgr_mstr")
            {
                sql = "insert into " + TabelName + "(fgr_fgr, fgr_doc_code, fgr_date, fgr_site, fgr_loc, fgr_type, fgr_rmks, fgr_crt_by,fgr_crt_date,fgr_pst) values";
                for (int ii = 0; ii < dt.Rows.Count; ii++)
                {//对datatable循环
                    if (ii == dt.Rows.Count - 1)
                    {
                        sql = sql + "('" + dt.Rows[ii]["fgr_fgr"].ToString() + "','" + dt.Rows[ii]["fgr_doc_code"].ToString() + "','" + dt.Rows[ii]["fgr_date"].ToString() + "','" + dt.Rows[ii]["fgr_site"].ToString() + "','" + dt.Rows[ii]["fgr_loc"].ToString() + "','" + dt.Rows[ii]["fgr_type"].ToString() + "','" + dt.Rows[ii]["fgr_rmks"].ToString() + "','" + dt.Rows[ii]["fgr_crt_by"].ToString() + "','" + dt.Rows[ii]["fgr_crt_date"].ToString() + "'," + dt.Rows[ii]["fgr_pst"].ToString() + ")";//某一行的数据 
                    }
                    else
                    {
                        sql = sql + "('" + dt.Rows[ii]["fgr_fgr"].ToString() + "','" + dt.Rows[ii]["fgr_doc_code"].ToString() + "','" + dt.Rows[ii]["fgr_date"].ToString() + "','" + dt.Rows[ii]["fgr_site"].ToString() + "','" + dt.Rows[ii]["fgr_loc"].ToString() + "','" + dt.Rows[ii]["fgr_type"].ToString() + "','" + dt.Rows[ii]["fgr_rmks"].ToString() + "','" + dt.Rows[ii]["fgr_crt_by"].ToString() + "','" + dt.Rows[ii]["fgr_crt_date"].ToString() + "'," + dt.Rows[ii]["fgr_pst"].ToString() + "),";//某一行的数据 
                    }
                }
            }
            if (TabelName == "fgrd_det")
            {
                sql = "insert  " + TabelName + "(fgrd_fgr, fgrd_line,   fgrd_wo_nbr, fgrd_wo_lot, fgrd_qty,  fgrd_part, fgrd_site, fgrd_loc, fgrd_rmks) ";
                for (int i = 0; i < dt.Rows.Count; i++)
                {//对datatable循环


                    if (i == dt.Rows.Count - 1)
                    {
                        sql = sql + "select '" + dt.Rows[i]["fgrd_fgr"].ToString() + "'," + dt.Rows[i]["fgrd_line"].ToString() + ",'" + dt.Rows[i]["fgrd_wo_nbr"].ToString() + "','" + dt.Rows[i]["fgrd_wo_lot"].ToString() + "'," + dt.Rows[i]["fgrd_qty"].ToString() + ",'" + dt.Rows[i]["fgrd_part"].ToString() + "','" + dt.Rows[i]["fgrd_site"].ToString() + "','" + dt.Rows[i]["fgrd_loc"].ToString() + "','" + dt.Rows[i]["fgrd_rmks"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + "select '" + dt.Rows[i]["fgrd_fgr"].ToString() + "'," + dt.Rows[i]["fgrd_line"].ToString() + ",'" + dt.Rows[i]["fgrd_wo_nbr"].ToString() + "','" + dt.Rows[i]["fgrd_wo_lot"].ToString() + "'," + dt.Rows[i]["fgrd_qty"].ToString() + ",'" + dt.Rows[i]["fgrd_part"].ToString() + "','" + dt.Rows[i]["fgrd_site"].ToString() + "','" + dt.Rows[i]["fgrd_loc"].ToString() + "','" + dt.Rows[i]["fgrd_rmks"].ToString() + "' union ";
                    }
                }
            }
            if (TabelName == "mov_mstr")
            {
                sql = "insert  " + TabelName + "(mov_mov, mov_doc_code,   mov_date, mov_cat, mov_type,  mov_site_fr, mov_loc_fr, mov_site_to, mov_loc_to,mov_rmks,mov_crt_by,mov_crt_date,mov_pst) ";
                for (int i = 0; i < dt.Rows.Count; i++)
                {//对datatable循环
                    if (i == dt.Rows.Count - 1)
                    {
                        sql = sql + "select '" + dt.Rows[i]["mov_mov"].ToString() + "'," + dt.Rows[i]["mov_doc_code"].ToString() + ",'" + dt.Rows[i]["mov_date"].ToString() + "','" + dt.Rows[i]["mov_cat"].ToString() + "','" + dt.Rows[i]["mov_type"].ToString() + "','" + dt.Rows[i]["mov_site_fr"].ToString() + "','" + dt.Rows[i]["mov_loc_fr"].ToString() + "','" + dt.Rows[i]["mov_site_to"].ToString() + "','" + dt.Rows[i]["mov_loc_to"].ToString() + "','" + dt.Rows[i]["mov_rmks"].ToString() + "','" + dt.Rows[i]["mov_crt_by"].ToString() + "','" + dt.Rows[i]["mov_crt_date"].ToString() + "'," + dt.Rows[i]["mov_pst"].ToString() + "";
                    }
                    else
                    {
                        sql = sql + "select '" + dt.Rows[i]["mov_mov"].ToString() + "'," + dt.Rows[i]["mov_doc_code"].ToString() + ",'" + dt.Rows[i]["mov_date"].ToString() + "','" + dt.Rows[i]["mov_cat"].ToString() + "','" + dt.Rows[i]["mov_type"].ToString() + "','" + dt.Rows[i]["mov_site_fr"].ToString() + "','" + dt.Rows[i]["mov_loc_fr"].ToString() + "','" + dt.Rows[i]["mov_site_to"].ToString() + "','" + dt.Rows[i]["mov_loc_to"].ToString() + "','" + dt.Rows[i]["mov_rmks"].ToString() + "','" + dt.Rows[i]["mov_crt_by"].ToString() + "','" + dt.Rows[i]["mov_crt_date"].ToString() + "'," + dt.Rows[i]["mov_pst"].ToString() + " union ";
                    }
                }
            }
            if (TabelName == "movd_det")
            {
                sql = "insert  " + TabelName + "(movd_mov, movd_site_fr,   movd_loc_fr, movd_site_to, movd_loc_to,  movd_qty, movd_line, movd_part, movd_price,movd_rmks,movd_dna_nbr,movd_dna_line,movd_dna_cust) ";
                for (int i = 0; i < dt.Rows.Count; i++)
                {//对datatable循环
                    if (i == dt.Rows.Count - 1)
                    {
                        sql = sql + "select '" + dt.Rows[i]["movd_mov"].ToString() + "'," + dt.Rows[i]["movd_site_fr"].ToString() + ",'" + dt.Rows[i]["movd_loc_fr"].ToString() + "'," + dt.Rows[i]["movd_site_to"].ToString() + ",'" + dt.Rows[i]["movd_loc_to"].ToString() + "'," + dt.Rows[i]["movd_qty"].ToString() + "," + dt.Rows[i]["movd_line"].ToString() + ",'" + dt.Rows[i]["movd_part"].ToString() + "'," + dt.Rows[i]["movd_price"].ToString() + ",'" + dt.Rows[i]["movd_rmks"].ToString() + "','" + dt.Rows[i]["movd_dna_nbr"].ToString() + "','" + dt.Rows[i]["movd_dna_line"].ToString() + "','" + dt.Rows[i]["movd_dna_cust"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + "select '" + dt.Rows[i]["movd_mov"].ToString() + "'," + dt.Rows[i]["movd_site_fr"].ToString() + ",'" + dt.Rows[i]["movd_loc_fr"].ToString() + "'," + dt.Rows[i]["movd_site_to"].ToString() + ",'" + dt.Rows[i]["movd_loc_to"].ToString() + "'," + dt.Rows[i]["movd_qty"].ToString() + "," + dt.Rows[i]["movd_line"].ToString() + ",'" + dt.Rows[i]["movd_part"].ToString() + "'," + dt.Rows[i]["movd_price"].ToString() + ",'" + dt.Rows[i]["movd_rmks"].ToString() + "','" + dt.Rows[i]["movd_dna_nbr"].ToString() + "','" + dt.Rows[i]["movd_dna_line"].ToString() + "','" + dt.Rows[i]["movd_dna_cust"].ToString() + "' union ";
                    }
                }
            }
            if (TabelName == "iqc_mstr")
            {
                sql = "insert  " + TabelName + "(iqc_iqc, iqc_doc_code, iqc_site, iqc_date, iqc_grn,  iqc_loc_raw, iqc_loc_rts, iqc_crt_by, iqc_crt_date,iqc_pst,iqc_rmks) ";
                for (int i = 0; i < dt.Rows.Count; i++)
                {//对datatable循环
                    if (i == dt.Rows.Count - 1)
                    {
                        sql = sql + "select '" + dt.Rows[i]["iqc_iqc"].ToString() + "'," + dt.Rows[i]["iqc_doc_code"].ToString() + ",'" + dt.Rows[i]["iqc_site"].ToString() + "','" + dt.Rows[i]["iqc_date"].ToString() + "','" + dt.Rows[i]["iqc_grn"].ToString() + "','" + dt.Rows[i]["iqc_loc_raw"].ToString() + "','" + dt.Rows[i]["iqc_loc_rts"].ToString() + "','" + dt.Rows[i]["iqc_crt_by"].ToString() + "','" + dt.Rows[i]["iqc_crt_date"].ToString() + "','" + dt.Rows[i]["iqc_pst"].ToString() + "','" + dt.Rows[i]["iqc_rmks"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + "select '" + dt.Rows[i]["iqc_iqc"].ToString() + "'," + dt.Rows[i]["iqc_doc_code"].ToString() + ",'" + dt.Rows[i]["iqc_site"].ToString() + "','" + dt.Rows[i]["iqc_date"].ToString() + "','" + dt.Rows[i]["iqc_grn"].ToString() + "','" + dt.Rows[i]["iqc_loc_raw"].ToString() + "','" + dt.Rows[i]["iqc_loc_rts"].ToString() + "','" + dt.Rows[i]["iqc_crt_by"].ToString() + "','" + dt.Rows[i]["iqc_crt_date"].ToString() + "','" + dt.Rows[i]["iqc_pst"].ToString() + ",'" + dt.Rows[i]["iqc_rmks"].ToString() + "' union ";
                    }
                }
            }
            if (TabelName == "iqcd_det")
            {
                sql = "insert  " + TabelName + "(iqcd_iqc, iqcd_line,   iqcd_grnd_grn, iqcd_grnd_line, iqcd_site,  iqcd_loc_iqc, iqcd_loc_raw, iqcd_loc_rts, iqcd_part,iqcd_um,iqcd_qty_pending,iqcd_qty_acpt,iqcd_rmks,iqcd_qty_rts) ";
                for (int i = 0; i < dt.Rows.Count; i++)
                {//对datatable循环
                    if (i == dt.Rows.Count - 1)
                    {
                        sql = sql + "select '" + dt.Rows[i]["iqcd_iqc"].ToString() + "'," + dt.Rows[i]["iqcd_line"].ToString() + ",'" + dt.Rows[i]["iqcd_grnd_grn"].ToString() + "'," + dt.Rows[i]["iqcd_grnd_line"].ToString() + ",'" + dt.Rows[i]["iqcd_site"].ToString() + "','" + dt.Rows[i]["iqcd_loc_iqc"].ToString() + "','" + dt.Rows[i]["iqcd_loc_raw"].ToString() + "','" + dt.Rows[i]["iqcd_loc_rts"].ToString() + "','" + dt.Rows[i]["iqcd_part"].ToString() + "','" + dt.Rows[i]["iqcd_um"].ToString() + "'," + dt.Rows[i]["iqcd_qty_pending"] + "," + dt.Rows[i]["iqcd_qty_acpt"] + ",'" + dt.Rows[i]["iqcd_rmks"] + "'," + dt.Rows[i]["iqcd_qty_rts"] + "";
                    }
                    else
                    {
                        sql = sql + "select '" + dt.Rows[i]["iqcd_iqc"].ToString() + "'," + dt.Rows[i]["iqcd_line"].ToString() + ",'" + dt.Rows[i]["iqcd_grnd_grn"].ToString() + "'," + dt.Rows[i]["iqcd_grnd_line"].ToString() + ",'" + dt.Rows[i]["iqcd_site"].ToString() + "','" + dt.Rows[i]["iqcd_loc_iqc"].ToString() + "','" + dt.Rows[i]["iqcd_loc_raw"].ToString() + "','" + dt.Rows[i]["iqcd_loc_rts"].ToString() + "','" + dt.Rows[i]["iqcd_part"].ToString() + "','" + dt.Rows[i]["iqcd_um"].ToString() + "'," + dt.Rows[i]["iqcd_qty_pending"] + "," + dt.Rows[i]["iqcd_qty_acpt"] + ",'" + dt.Rows[i]["iqcd_rmks"] + "'," + dt.Rows[i]["iqcd_qty_rts"] + " union ";
                    }
                }
            }
            com.CommandText = sql;
            com.ExecuteNonQuery();//执行该行
            trans.Commit();//如果全部执行完毕.提交
            insertok = "成功";
        }
        catch
        {
            trans.Rollback();

        }
        //try
        //{
        //    conn.Open();
        //    //声明SqlBulkCopy ,using释放非托管资源
        //    using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
        //    {
        //        //一次批量的插入的数据量
        //        sqlBC.BatchSize = 1000;
        //        //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
        //        sqlBC.BulkCopyTimeout = 60;
        //        //設定 NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件。 
        //        sqlBC.NotifyAfter = 10000;
        //        // sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);
        //        //设置要批量写入的表
        //        sqlBC.DestinationTableName = TabelName;
        //        //自定义的datatable和数据库的字段进行对应
        //        sqlBC.ColumnMappings.Add("fgr_fgr", "fgr_fgr");
        //        sqlBC.ColumnMappings.Add("fgr_doc_code", "fgr_doc_code");
        //        sqlBC.ColumnMappings.Add("fgr_date", "fgr_date");
        //        sqlBC.ColumnMappings.Add("fgr_site", "fgr_site");
        //        sqlBC.ColumnMappings.Add("fgr_loc", "fgr_loc");
        //        sqlBC.ColumnMappings.Add("fgr_type", "fgr_type");
        //        sqlBC.ColumnMappings.Add("fgr_rmks", "fgr_rmks");
        //        sqlBC.ColumnMappings.Add("fgr_crt_by", "fgr_crt_by");
        //        sqlBC.ColumnMappings.Add("fgr_crt_date", "fgr_crt_date");



        //        for (int i = 0; i < dtColum.Count; i++)
        //        {
        //            sqlBC.ColumnMappings.Add(dtColum[i].ColumnName.ToString(), dtColum[i].ColumnName.ToString());
        //        }
        //        //批量写入
        //        sqlBC.WriteToServer(dt);
        //        sqlBC.Close(); 
        //    }
        //    conn.Dispose();
        // }
        //catch (Exception)
        //{

        //    throw;
        //}

    }
    //响应时事件
    void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
    {
        // Response.Write("<br/> OK! ");
    }
    //---------------------------------
      [WebMethod(Description = "登录.")]
    public void sp_login(string user, string passwor)
    {
        string pwd = Common.EncryptPWD(passwor);           //密码

        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@usr_user" , SqlDbType.NVarChar, 30 ,user), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@usr_password" , SqlDbType.NVarChar, 100 ,pwd),
            };

        hs.RunProc("sp_login", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }

    //-------------------

     [WebMethod(Description = "未完成的生产单.")]
    public void sp_WoXiang(string wo_part, string wo_nbr, string wo_line, string wo_due_date1, string wo_due_date2, string wo_rel_date1, string wo_rel_date2)
    {
        StringBuilder Json = new StringBuilder();
        HSql hs = new HSql(connstr_GC_SZ);
        //DataSet ds = new DataSet();
        //ds = hs.DSet(@"select top 3* from dnad_det");
        //DataTable dt = ds.Tables[0];

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            { 
                hs.MakeInParam("@wo_part" , SqlDbType.NVarChar,50,wo_part), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
                hs.MakeInParam("@wo_nbr" , SqlDbType.NVarChar,200,wo_nbr),
                hs.MakeInParam("@wo_line" , SqlDbType.NVarChar,100,wo_line),
                hs.MakeInParam("@wo_due_date1" , SqlDbType.NVarChar,50,wo_due_date1),
                hs.MakeInParam("@wo_due_date2" , SqlDbType.NVarChar,50,wo_due_date2),
                hs.MakeInParam("@wo_rel_date1" , SqlDbType.NVarChar,50,wo_rel_date1),
                hs.MakeInParam("@wo_rel_date2" , SqlDbType.NVarChar,50,wo_rel_date2),
            };
        hs.RunProc("sp_WoXiang", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }




    [WebMethod(Description = "生成单号.")]
    public void sp_auto_nbr(string eff_date, string code_code)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);
        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            {  
                hs.MakeInParam("@eff_date" , SqlDbType.DateTime,50,eff_date),
                hs.MakeInParam("@code_code" , SqlDbType.NVarChar,50,code_code),// (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            };

        hs.RunProc("sp_auto_nbr", paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "GB2312";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }

  [WebMethod(Description = "查询通知单.")]
    public void sp_tong_zhi_dan(string site, string time1, string time2, string dna_cust, string dna_nbr, string pt_desc)
    {
        StringBuilder Json = new StringBuilder();

        HSql hs = new HSql(connstr_GC_SZ);

        //DataSet ds = new DataSet();
        //ds = hs.DSet(@"select top 3* from dnad_det");
        //DataTable dt = ds.Tables[0];

        DataSet tmpDS = null;
        SqlParameter[] paramsArr =   
            {  
                hs.MakeInParam("@time1" , SqlDbType.NVarChar, 30 ,time1),
                hs.MakeInParam("@time2" , SqlDbType.NVarChar, 30,time2),
                hs.MakeInParam("@dna_cust" , SqlDbType.NVarChar, 300, dna_cust),
                hs.MakeInParam("@dna_nbr" , SqlDbType.NVarChar, 255,dna_nbr),
                hs.MakeInParam("@pt_desc" , SqlDbType.NVarChar, 15 ,pt_desc), // (DateTime.Now)日期格式可以直接用字串，SQL Server可以自动把字符串认成日期型
            };

        hs.RunProc(site, paramsArr, ref tmpDS);
        hs.Close();
        DataTable dt = new DataTable();
        Json.Append("[");
        if (tmpDS != null && tmpDS.Tables.Count > 0)
        {
            dt = tmpDS.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString().Replace("\"","“") + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
        }
        Json.Append("]");
        Context.Response.Charset = "utf-8";
        Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Context.Response.Write(Json);
        Context.Response.End();
    }



    public int totalRows;

    [WebMethod]
    public int GetRows()
    {
        return totalRows;
    }


    public DataSet web_ds(string web_conn)
    {

        HSql hs = new HSql(connstr_GC_SZ);


        DataSet ds = new DataSet();
        ds = hs.DSet(@"select top 12* from dnad_det");
        return ds;
    }


    //[WebMethod]
    //public List<User_Info> getd()
    //{
    //    HSql hs = new HSql();

    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = hs.m_conn;
    //    cmd.CommandText = "GetUserInfo";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    hs.m_conn.Open();


    //    SqlDataAdapter sda = new SqlDataAdapter();
    //    sda.SelectCommand = cmd;
    //    DataSet ds = new DataSet();
    //    sda.Fill(ds);
    //    hs.m_conn.Close();

    //    DataTable dt = ds.Tables[0];
    //    List<User_Info> listData = new List<User_Info> { };

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        User_Info model = new User_Info();
    //        model.Id = Convert.ToInt32(dt.Rows[i]["id"]);
    //        model.Name = Convert.ToString(dt.Rows[i]["name"]);
    //        model.Pwd = Convert.ToString(dt.Rows[i]["pwd"]);
    //        listData.Add(model);
    //    }
    //    return listData;
    //}

    //[WebMethod]
    //public List<User_Info> getD(int request_page_num, int page_size)
    //{
    //    HSql hs = new HSql();

    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = hs.m_conn;
    //    cmd.CommandText = "GetUserInfo";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    hs.m_conn.Open();
    //    SqlParameter[] parms = new SqlParameter[] { 
    //            new SqlParameter("@request_page_num",SqlDbType.Int),
    //            new SqlParameter("@page_size",SqlDbType.Int)
    //        };
    //    parms[0].Value = request_page_num;
    //    parms[1].Value = page_size;
    //    cmd.Parameters.AddRange(parms);

    //    SqlDataAdapter sda = new SqlDataAdapter();
    //    sda.SelectCommand = cmd;
    //    DataSet ds = new DataSet();
    //    sda.Fill(ds);
    //    hs.m_conn.Close();

    //    totalRows = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
    //    DataTable dt = ds.Tables[1];

    //    List<User_Info> listData = new List<User_Info> { };

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        User_Info model = new User_Info();
    //        model.Id = Convert.ToInt32(dt.Rows[i]["id"]);
    //        model.Name = Convert.ToString(dt.Rows[i]["name"]);
    //        model.Pwd = Convert.ToString(dt.Rows[i]["pwd"]);
    //        listData.Add(model);
    //    }
    //    return listData;
    //}
    /////------------------

    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    //[WebMethod]
    //public string StudentsAdd(String _str_name, int _int_sex, String _str_tel, int _int_classid)
    //{
    //    YYCMS.Students Dal = new YYCMS.Students();

    //    Dal.Name = _str_name;
    //    Dal.Sex = _int_sex;
    //    Dal.Tel = _str_tel;
    //    Dal.ClassID = _int_classid;
    //    int i = Dal.Add();
    //    string select = "";
    //    if (i > 0)
    //    {
    //        select = "创建成功！";
    //    }
    //    return select;
    //}
    //[WebMethod]
    //public String StudentsEdit(int _int_id, String _str_name, int _int_sex, String _str_tel, int _int_classid)
    //{

    //    YYCMS.Students Dal = new YYCMS.Students();
    //    Dal.ID = _int_id;
    //    Dal.Name = _str_name;
    //    Dal.Sex = _int_sex;
    //    Dal.Tel = _str_tel;
    //    Dal.ClassID = _int_classid;
    //    int i = Dal.Update();
    //    string up = "";
    //    if (i > 0)
    //    {
    //        up = "修改成功！";
    //    }
    //    return up;
    //}
    //[WebMethod]
    //public DataSet StudentsPager(int PageIndex, int PageSize, string strWhere, out int Recordcount)
    //{
    //    YYCMS.Students Dal = new YYCMS.Students();
    //    DataSet Admin_Pager = Dal.Pager(PageIndex, PageSize, strWhere, out  Recordcount);
    //    return Admin_Pager;
    //}
    //[WebMethod]
    //public String StudentsDelete(int id)
    //{
    //    YYCMS.Students Dal = new YYCMS.Students();
    //    Dal.ID = id;
    //    int i = Dal.delete();
    //    string ok = "";
    //    if (i > 0)
    //    {
    //        ok = "删除成功！";
    //    }
    //    return ok;
    //}
}
