using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Reflection;



/// <summary>
///HSQL Class 的摘要说明
/// </summary>
public class HSql
{
    public SqlConnection m_conn;
    public SqlConnection ConnGCERP;
    public SqlDataAdapter m_da;
    public SqlDataReader m_dr;
    public DataSet m_ds;
    public DataTable m_table;

    private string connectionString;
    private string ConnstrGCERP;
    private readonly string RETURNVALUE = "RETURNVALUE";

    public string address = "SZCFERP";          //接收下拉列表选择区域   判断区域 改变配置strconn链接字符  和   CONN链接对象
    public string strconn = "";                 //受address选择          改变配置链接字符
    /*

    // 数据连接池
    private SqlConnection con;

    public ZSql();
    public ZSql(IDbConnection conn);
    public ZSql(string connstr);

    public int rowcount { get; }
    public int rowindex { get; set; }
    public DataRowCollection Rows { get; }

    public object this[string sname] { get; set; }

    public void Close();
    public void config(string sqlstr, DataTable dt);
    public void Delete();
    public DataSet DSet(string sqlstr);
    public bool Find(string sname, object val);
    public object GetScalar(string sqlstr);
    public SqlDataReader GetSqlDataReader(string sqlstr);
    public DataRow NewRow();
    public bool NextRow();
    public int Open(string sqlstr);
    public int Open(string sqlstr, DataTable dt);
    public IDataReader Open_dr(string sqlstr);
    public void RemoveAt(int i);
    public int Update();
    public int Update(string idname);
    */

    public DataRowCollection Rows 
    {
        get
        {
            return m_table.Rows;
        }
    }

    public object this[string sname]
    {
        get
        {
            return m_dr[sname];
        }
    }

    #region 构造函数,SqlConnection对象初始化
    public HSql()
    {
        //数据库连接字符串(web.config来配置)
        //<add key="ConnectionString" value="server=127.0.0.1;database=DATABASE;uid=sa;pwd=" />  
        //  connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

        ConnstrGCERP = ConfigurationManager.ConnectionStrings["connstrGCERP"].ConnectionString;
        connectionString = ConfigurationManager.ConnectionStrings["connstrSZCFERP"].ConnectionString;
        Initialization();
    }

    public HSql(string ConnectionStr)
    {
        connectionString = ConfigurationManager.ConnectionStrings[ConnectionStr].ConnectionString;
        Initialization();
    }

    /* 
    public HSql(string ConnectionKey)
    {
        connectionString = ConfigurationSettings.AppSettings[ConnectionKey];
        Initialization();
    }

    public HSql(string connstr)
    {
        connectionString = connstr;
        Initialization();
    }
    */

    /// <summary>
    /// 初始化函数
    /// </summary>
    protected void Initialization()
    {
        try
        {
            m_conn = new SqlConnection(connectionString);
            ConnGCERP = new SqlConnection(ConnstrGCERP);
            /*   if (m_conn.State == ConnectionState.Closed)
                   m_conn.Open();  */
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message.ToString());
        }
    }
    #endregion
    /// <summary>
    /// 打开数据库
    /// </summary>
    public void OpenConnection(SqlConnection Conn)
    {


      
        if (Conn.State == ConnectionState.Closed)
        {
            Conn.Open();
        }
        if (Conn.State == ConnectionState.Broken)
        {
            Conn.Close();
            Conn.Open();
        }

    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseConnection(SqlConnection Conn)
    {

      
        if (Conn.State == ConnectionState.Open)
        {
            Conn.Close();
        }
        if (Conn.State == ConnectionState.Broken)
        {
            Conn.Close();
        }

    }

    protected void Open()
    {
        Initialization();

        // 打开数据库连接
        try
        {
            if (m_conn.State == ConnectionState.Closed)
                   m_conn.Open(); 
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message.ToString());
        }
    }

    public int Open(string sqlstr)
    {
        m_table = FillDataTable(sqlstr);     //填充 DataTable
        return GetDataReader(sqlstr);        //获取 DataReader
    }

    // 数据库SQL SERVER链接测试
    public bool TestSQLLink()
    {
        bool retVal = false;

        try
        {
            m_conn.Open();
            //retVal = m_conn.State.ToString();
            retVal = true;
            m_conn.Close();
        }
        catch
        {
            retVal = false;
        }
        return retVal;
    }

    //根据SQL语句填充 DataTable
    private DataTable FillDataTable(string sqlstr)
    {
        using (SqlDataAdapter sda = new SqlDataAdapter(sqlstr, m_conn))
        {
            try
            {
                sda.SelectCommand.CommandTimeout = 600; //设置查询SQL的超时时间
                //sda.UpdateCommand.CommandTimeout = 600; //设置修改SQL的超时时间
                //sda.InsertCommand.CommandTimeout = 600; //设置插入SQL的超时时间
                //sda.DeleteCommand.CommandTimeout = 600; //设置删除SQL的超时时间

                DataTable dt = new DataTable();

                dt.BeginLoadData();
                sda.Fill(dt);
                dt.EndLoadData();

                m_da = sda; // Add By HYL -20120227

                return dt;
            }
            catch (System.Data.SqlClient.SqlException Ex)
            {
                m_conn.Close();
                throw new Exception(Ex.Message);
            }
        }
    }

    //根据SQL语句获取 DataReader
    private int GetDataReader(string sqlstr)
    {
        if (m_dr != null) m_dr.Close();

        using (SqlCommand cmd = new SqlCommand(sqlstr, m_conn))
        {
            try
            {
                cmd.CommandTimeout = 600; //设置SQL执行的超时时间

                m_conn.Open();
                int rows = cmd.ExecuteNonQuery();
                m_dr = cmd.ExecuteReader();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException Ex)
            {
                m_conn.Close();
                throw new Exception(Ex.Message);
            }
        }
    }

    /* 关闭数据库链接 */
    public void Close()
    {
        if (m_dr != null) m_dr.Close();
        if (m_conn != null) m_conn.Close();
    }

    //释放资源
    public void Dispose()
    {
        // 确认连接是否已经关闭
        if (m_conn != null)
        {
            m_conn.Dispose();
            m_conn = null; 
        }
    } 

    public bool NextRow()
    {
       if (!m_dr.Read())
        {
            m_dr.Close();
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 判断指定数据表中是否存在指定字段为指定关键字的数据
    /// </summary>
    /// <param name="TbName">SQL表名</param>
    /// <param name="FieldName">SQL字段名</param>
    /// <param name="KeyValue">指定的关键值</param>
    /// <returns></returns>
    public bool IsExists(string TbName, string FieldName, string KeyValue)
    {
        bool ret = false;
        string sql = "select count(*) from " + TbName.Trim() + " where " + FieldName.Trim() + "='" + KeyValue.Trim() + "'";

        if (Convert.ToInt32(GetScalar(sql)) == 0)
        {
            ret = false;
        }
        else
        {
            ret = true;
        }

        return ret;
    }

    /// <summary>
    /// 执行SQL语句并返回DataSet
    /// </summary>
    /// <param name="sqlstr">SQL语句</param>
    /// <returns></returns>
    public DataSet DSet(String sqlstr)
    {
        using (SqlDataAdapter sda = new SqlDataAdapter(sqlstr, m_conn))
        {
            try
            {
                sda.SelectCommand.CommandTimeout = 600; //设置查询SQL的超时时间
                //sda.InsertCommand.CommandTimeout = 600; //设置插入SQL的超时时间
                //sda.UpdateCommand.CommandTimeout = 600; //设置修改SQL的超时时间
                //sda.DeleteCommand.CommandTimeout = 600; //设置删除SQL的超时时间

                DataSet ds = new DataSet();
                m_conn.Open();
                sda.Fill(ds);
                m_conn.Close();
                return ds;
            }
            catch (System.Data.SqlClient.SqlException Ex)
            {
                m_conn.Close();
                throw new Exception(Ex.Message);
            }
        }
    }

    //生成一个对象并返回该结果集第一行第一列
    public object GetScalar(string sqlstr)
    {
        using (SqlCommand cmd = new SqlCommand(sqlstr, m_conn))
        {
            try
            {
                cmd.CommandTimeout = 600; //设置SQL执行的超时时间

                m_conn.Open();
                object obj = cmd.ExecuteScalar();
                //CommadnBehavior.CloseConnection是将于DataReader的数据库链接关联起来 
                //当关闭DataReader对象时候也自动关闭链接
                return obj;
            }
            catch (System.Data.SqlClient.SqlException Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                m_conn.Close();
            }
        }
    }

    /// <summary>
    /// 执行简单SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public int ExecuteSql(string sqlstr)
    {
        using (SqlCommand cmd = new SqlCommand(sqlstr, m_conn))
        {
            try
            {
                cmd.CommandTimeout = 600; //设置SQL执行的超时时间

                m_conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                m_conn.Close();
            }
        }
    }


    //将 DataReader 填充到 DataTable 的专用函数
    private DataTable GetTable(IDataReader reader)
    {
        DataTable tb = new DataTable();
        DataColumn col;
        DataRow row;
        int i;

        //动态添加列
        for (i = 0; i < reader.FieldCount; i++)
        {
            col = new DataColumn();
            col.ColumnName = reader.GetName(i);
            col.DataType = reader.GetFieldType(i);
            tb.Columns.Add(col);
        }

        //添加数据
        while (reader.Read())
        {
            row = tb.NewRow();
            for (i = 0; i < reader.FieldCount; i++)
            {
                row[i] = reader[i];
            }
            tb.Rows.Add(row);
        }
        return tb;
    }

    /*
     * 
    //将 DataReader 填充到 DataTable   的专用函数
    private DataTable GetTable(SqlDataReader _reader)
    {
        DataTable dataTable1 = _reader.GetSchemaTable();
        DataTable dataTable2 = new DataTable();
        string[] arrayList = new string[dataTable1.Rows.Count];
     
        //动态添加列
        for (int i = 0; i < dataTable1.Rows.Count; i++)
        {
            DataColumn dataColumn = new DataColumn();
            if (!dataTable2.Columns.Contains(dataTable1.Rows[i]["ColumnName"].ToString()))
            {
                dataColumn.ColumnName = dataTable1.Rows[i]["ColumnName"].ToString();
                dataColumn.Unique = Convert.ToBoolean(dataTable1.Rows[i]["IsUnique"]);
                dataColumn.AllowDBNull = Convert.ToBoolean(dataTable1.Rows[i]["AllowDBNull"]);
                dataColumn.ReadOnly = Convert.ToBoolean(dataTable1.Rows[i]["IsReadOnly"]);
                dataColumn.DataType = (Type)dataTable1.Rows[i]["DataType"];
                arrayList[i] = dataColumn.ColumnName;
                dataTable2.Columns.Add(dataColumn);
            }
        }
     
        //添加数据
            dataTable2.BeginLoadData();
        while (_reader.Read())
        {
            DataRow dataRow = dataTable2.NewRow();
            for (int j = 0; j < arrayList.Length; j++)
            {
                dataRow[arrayList[j]] = _reader[arrayList[j]];
            }
            dataTable2.Rows.Add(dataRow);
        }
        _reader.Close();
        dataTable2.EndLoadData();

        return dataTable2;
    } 

  
    //DataReader 填充到 DataTable 的函数 
    private DataTable FillDataTable(IDataReader oDataReader) 
    { 
         DataTable oDataTable; 
         DataTable oSchemaTable; 
         DataRow oDataRow; 

         oDataTable = new DataTable(); 
         oSchemaTable = new DataTable(); 
         oSchemaTable = oDataReader.GetSchemaTable(); 

         for (int iLoop = 0; iLoop <= oSchemaTable.Rows.Count - 1; iLoop++)
         { 
           oDataTable.Columns.Add(oSchemaTable.Rows[iLoop]["ColumnName"], oSchemaTable.Rows[iLoop]["DataType"]); 
         }
    
         while (oDataReader.Read)
         { 
           oDataRow = oDataTable.NewRow; 
           for (int iLoop = 0; iLoop <= oSchemaTable.Rows.Count - 1; iLoop++)
           { 
             oDataRow(iLoop) = oDataReader(oSchemaTable.Rows[iLoop]["ColumnName"]); 
           } 
           oDataTable.Rows.Add(oDataRow); 
         } 
         oDataReader.Close(); 
         oSchemaTable.Rows.Clear(); 
         return oDataTable; 
    }
        */


    #region 操作存储过程
    /// <summary>
    /// 运行存储过程(已重载)：执行无参数和返回int型的存储过程
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <returns>存储过程的返回值</returns>
    public int RunProc(string procName)
    {
        SqlCommand cmd = CreateCommand(procName, null);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        cmd.ExecuteNonQuery();
        this.Close();
        return (int)cmd.Parameters["ReturnValue"].Value;
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行传入参数和返回int型的存储过程
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <returns>影响的行数</returns>
    public int RunProc(string procName, SqlParameter[] prams)
    {
        SqlCommand cmd = CreateCommand(procName, prams);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        int ret=cmd.ExecuteNonQuery();
        this.Close();
        return ret;
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行无参数的存储过程，并返回DataReader
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="dataReader">结果集</param>
    public void RunProc(string procName, out SqlDataReader dataReader)
    {
        SqlCommand cmd = CreateCommand(procName, null);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行传入参数的存储过程，并返回DataReader
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <param name="dataReader">结果集</param>
    public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
    {
        SqlCommand cmd = CreateCommand(procName, prams);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行无参数的存储过程，并返回DataSet
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="dataSet">结果集</param>
    public void RunProc(string procName, ref DataSet dataSet)
    { 
        if (dataSet == null)
        {
            dataSet = new DataSet();
        }
        
        //创建SqlDataAdapter
        SqlDataAdapter da = CreateProcDataAdapter(procName, null);
        try
        {
            da.SelectCommand.CommandTimeout = 600; //设置查询SQL的超时时间

            //读取数据
            da.Fill(dataSet);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().IndexOf("无法打开与 SQL Server 的连接") != -1)
            {
                HttpContext.Current.Response.Write("<script>alert('错误提示：/n/n目前SZCFERP系统无法连接到远程数据库，请与管理员联系！');location.href=document.referrer</script>");
            }
            else
            {
                throw new Exception(Ex.Message);
            }
        }
        finally
        {
            //关闭数据库的连接
            Close();
        }
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行传入参数的存储过程，并返回DataSet
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <param name="dataSet">结果集</param>
    public void RunProc(string procName, SqlParameter[] prams, ref DataSet dataSet)
    {
        if (dataSet == null)
        {
            dataSet = new DataSet();
        }

        //创建SqlDataAdapter
        SqlDataAdapter da = CreateProcDataAdapter(procName, prams);
        try
        {
            da.SelectCommand.CommandTimeout = 600; //设置查询SQL的超时时间

            //读取数据
            da.Fill(dataSet);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().IndexOf("无法打开与 SQL Server 的连接") != -1)
            {
                HttpContext.Current.Response.Write("<script>alert('错误提示：/n/n目前SZCFERP系统无法连接到远程数据库，请与管理员联系！');location.href=document.referrer</script>");
            }
            else
            {
                throw new Exception(Ex.Message);
            }
        }
        finally
        {
            //关闭数据库的连接
            Close();
        }
    }

    /// <summary>
    /// 运行存储过程(已重载)：执行传入参数和表名的存储过程，并返回DataSet
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <param name="tableName">表名</param>
    /// <param name="dataSet">结果集</param>
    public void RunProc(string procName, SqlParameter[] prams, string tableName, ref DataSet dataSet) 
    { 
        if (dataSet == null) 
        {
            dataSet = new DataSet();
        }
        //创建SqlDataAdapter
        SqlDataAdapter da = CreateProcDataAdapter(procName, prams); 
        try
        {
            da.SelectCommand.CommandTimeout = 600; //设置查询SQL的超时时间

            //读取数据
            da.Fill(dataSet,tableName);
        }
        catch (Exception Ex)
        {
            if (Ex.Message.ToString().IndexOf("无法打开与 SQL Server 的连接") != -1)
            {
                HttpContext.Current.Response.Write("<script>alert('错误提示：/n/n目前SZCFERP系统无法连接到远程数据库，请与管理员联系！');location.href=document.referrer</script>");
            }
            else
            {
                throw new Exception(Ex.Message);
            }
        }
        finally
        {
            //关闭数据库的连接 
            Close();
        }
    } 

    /// <summary>
    /// 运行存储过程(已重载)：执行传入参数的存储过程，并返回DataSet
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <param name="dataSet">结果集</param>
    /// <param name="datatable">表名</param>
    public void RunProc(string procName, SqlParameter[] prams, out DataSet dataSet, string datatable)
    {
        // 确定连接是打开的
        Initialization();
        m_conn.Open();

        SqlCommand cmd = CreateCommand(procName, prams);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, datatable);
        Close();

        dataSet = ds;

    }

    /// <summary>
    /// 创建Command对象用于访问存储过程
    /// </summary>
    /// <param name="procName">存储过程的名字</param>
    /// <param name="prams">存储过程的输入参数列表</param>
    /// <returns>Command对象</returns>
    private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
    {
        // 确定连接是打开的
        Initialization();
        m_conn.Open();
        //command = new SqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) );
        SqlCommand cmd = new SqlCommand(procName, m_conn);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        cmd.CommandType = CommandType.StoredProcedure;
        // 添加存储过程的输入参数列表
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
                cmd.Parameters.Add(parameter);
        }
        // 返回Command对象
        return cmd;
    }

    /// <summary>
    /// 创建输入参数
    /// </summary>
    /// <param name="ParamName">参数名</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Value">参数值</param>
    /// <returns>新参数对象</returns>
    public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
    }

    /// <summary>
    /// 创建输出参数
    /// </summary>
    /// <param name="ParamName">参数名</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <returns>新参数对象</returns>
    public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
    }

    /// <summary>
    /// 创建存储过程参数
    /// </summary>
    /// <param name="ParamName">参数名</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Direction">参数的方向(输入/输出)</param>
    /// <param name="Value">参数值</param>
    /// <returns>新参数对象</returns>
    public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
    {
        SqlParameter param;

        if (Size > 0)
        {
            param = new SqlParameter(ParamName, DbType, Size);
        }
        else
        {   
            //当参数大小为0时，不使用该参数大小值 
            param = new SqlParameter(ParamName, DbType);
        }
        param.Direction = Direction;
        if (!(Direction == ParameterDirection.Output && Value == null))
        {
            param.Value = Value;
        }
        return param;
    }

    //创建一个SqlDataAdapter对象，用此来执行存储过程

    private SqlDataAdapter CreateProcDataAdapter(string procName, SqlParameter[] prams)
    {
        //打开数据库连接
        Open();
        
        //设置SqlDataAdapter对象
        SqlDataAdapter da = new SqlDataAdapter(procName, m_conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = 600; //设置连接时限为600秒,防止超时

        //添加把存储过程的参数
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
            {
                da.SelectCommand.Parameters.Add(parameter);
            }
        } 
        //添加返回参数ReturnValue
        da.SelectCommand.Parameters.Add(
            new SqlParameter(RETURNVALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue,false, 0, 0, string.Empty, DataRowVersion.Default, null));
        
        //返回创建的SqlDataAdapter对象
        return da;
    } 
    #endregion


    #region 调用SQL自定义函数

    /// <summary>
    /// 调用SQL自定义函数(已重载)：调用无参数和返回int型的自定义函数
    /// </summary>
    /// <param name="funcName">自定义函数的名字</param>
    /// <returns>自定义函数的返回值</returns>
    public int RunFunc(string funcName)
    {
        SqlCommand cmd = CreateCommand(funcName, null);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        cmd.Parameters.Add("@returnInt", SqlDbType.NVarChar);
        cmd.Parameters["@returnInt"].Direction = ParameterDirection.ReturnValue;  //返回参数 
        cmd.ExecuteNonQuery();
        this.Close();

        return (int)cmd.Parameters["@returnInt"].Value;
    }

    /// <summary>
    /// 调用SQL自定义函数(已重载)：调用传入参数和返回int型的自定义函数
    /// </summary>
    /// <param name="funcName">自定义函数的名字</param>
    /// <param name="prams">自定义函数的输入参数列表</param>
    /// <returns>自定义函数的返回值</returns>
    public int RunFunc(string funcName, SqlParameter[] prams)
    {
        SqlCommand cmd = CreateCommand(funcName, prams);
        cmd.CommandTimeout = 600;   //设置连接时限为600秒,防止超时
        cmd.Parameters.Add("@returnInt", SqlDbType.NVarChar);
        cmd.Parameters["@returnInt"].Direction = ParameterDirection.ReturnValue;  //返回参数 
        int ret = cmd.ExecuteNonQuery();
        this.Close();

        return (int)cmd.Parameters["@returnInt"].Value;
    }

    #endregion
}
