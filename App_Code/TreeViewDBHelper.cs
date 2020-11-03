using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// TreeViewDBHelper 的摘要说明
/// </summary>
public class TreeViewDBHelper
{
	public TreeViewDBHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //创建连接字符串
    private static readonly string connString = "server=.;database=treeview;MultipleActiveResultSets=true;uid=sa;pwd=2222";
    //创建连接对象
    private static SqlConnection _conn;
    public static SqlConnection Conn
    {
        get
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(connString);
            }
            return TreeViewDBHelper._conn;
        }
    }
    /// <summary>
    /// 打开数据库
    /// </summary>
    public static void OpenConnection()
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
    public static void CloseConnection()
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
    /// <summary>
    /// 获取首行首列的值（执行查询单个值）
    /// </summary>
    public static object GetSca(string sql)
    {
        SqlCommand comm = new SqlCommand(sql, Conn);
        object obj = null;
        try
        {
            OpenConnection();
            obj = comm.ExecuteScalar();
        }
        catch (Exception)
        {

            ;
        }
        finally
        {
            CloseConnection();
        }

        return obj;

    }
    /// <summary>
    /// 获取受影响的行数值（执行增删改）
    /// </summary>
    public static int GetNum(string sql)
    {
        SqlCommand comm = new SqlCommand(sql, Conn);
        int num = 0;
        try
        {
            OpenConnection();
            num = comm.ExecuteNonQuery();
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            CloseConnection();
        }

        return num;

    }
    /// <summary>
    /// 获取查询一张表的数据
    /// </summary>
    public static SqlDataReader GetReader(string sql)
    {
        SqlCommand comm = new SqlCommand(sql, Conn);
        SqlDataReader reader = null;
        try
        {
            OpenConnection();
            reader = comm.ExecuteReader();
        }
        catch (Exception)
        {

            throw;
        }

        return reader;

    }

    //查询获取一张表的数据（使用断开式）
    public static DataTable GetTable(string sql)
    {
        //创建执行命令（作用跟SqlCommand一样，只是SqlCommand是用于连接式，SqlDataAdapter断开式）
        SqlDataAdapter sda = new SqlDataAdapter(sql, Conn);
        //创建一张数据表
        DataTable dt = new DataTable();
        try
        {
            //将数据填充到数据表中
            sda.Fill(dt);
        }
        catch (Exception)
        {

            throw;
        }
        return dt;
    }














    //存储过程调用方法
    public static object GetSca(SqlCommand comm)
    {

        object obj = null;
        try
        {
            OpenConnection();
            obj = comm.ExecuteScalar();
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            CloseConnection();
        }

        return obj;

    }
    /// <summary>
    /// 获取受影响的行数值（执行增删改）
    /// </summary>
    public static int GetNum(SqlCommand comm)
    {

        int num = 0;
        try
        {
            OpenConnection();
            num = comm.ExecuteNonQuery();
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            CloseConnection();
        }

        return num;

    }
    /// <summary>
    /// 获取查询一张表的数据
    /// </summary>
    public static SqlDataReader GetReader(SqlCommand comm)
    {

        SqlDataReader reader = null;
        try
        {
            OpenConnection();
            reader = comm.ExecuteReader();
        }
        catch (Exception)
        {

            throw;
        }

        return reader;

    }

    //查询获取一张表的数据（使用断开式）
    public static DataTable GetTable(SqlCommand comm)
    {
        //创建执行命令（作用跟SqlCommand一样，只是SqlCommand是用于连接式，SqlDataAdapter断开式）

        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = comm;
        DataTable dt = new DataTable();
        try
        {
            //将数据填充到数据表中
            sda.Fill(dt);
        }
        catch (Exception)
        {

            throw;
        }
        return dt;
    }
}