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
/// select_excel1 的摘要说明
/// </summary>
public class select_excel1
{
    public static DataSet excel1(String str_select,String str_where)
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    SqlCommand comm  =new SqlCommand ("sp_select_Excel1",DBHelper .Conn );
    comm.CommandType = CommandType.StoredProcedure;
    comm.Parameters.Add("@str_select", SqlDbType.NVarChar, 300).Value = str_select;
    comm.Parameters.Add("str_where", SqlDbType.NVarChar, 300).Value = str_where;
    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand  = comm;

    DataSet ds = new DataSet();
    da.Fill(ds );
        return ds;
               
    }
}