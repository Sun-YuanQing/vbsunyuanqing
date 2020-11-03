using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
namespace YYCMS
{
    /// <summary>  
    ///Students类 
    /// </summary>  
    public class Students
    {
        public Students()
        { }
        #region Model
        public int ID { set; get; }
        public string Name { set; get; }
        public int? Sex { set; get; }
        public string Tel { set; get; }
        public int? ClassID { set; get; }
        #endregion Model
        #region 方法成员
        /// <summary> 
        /// 增加一条数据 
        /// </summary> 
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Students(");
            strSql.Append("Name,Sex,Tel,ClassID");
            strSql.Append(") values (");
            strSql.Append("@Name,@Sex,@Tel,@ClassID");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
      new SqlParameter("@Name",SqlDbType.NVarChar,10), 
      new SqlParameter("@Sex",SqlDbType.Int), 
      new SqlParameter("@Tel",SqlDbType.NVarChar,20), 
      new SqlParameter("@ClassID",SqlDbType.Int) 
     };
            parameters[0].Value = Name;
            parameters[1].Value = Sex;
            parameters[2].Value = Tel;
            parameters[3].Value = ClassID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary> 
        /// 修改一条数据
        /// </summary> 
        public int Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Students set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("ClassID=@ClassID");
            strSql.Append(" where ID=@Id");
            SqlParameter[] parameters = { 
      new SqlParameter("@ID",SqlDbType.Int), 
      new SqlParameter("@Name",SqlDbType.NVarChar,10), 
      new SqlParameter("@Sex",SqlDbType.Int), 
      new SqlParameter("@Tel",SqlDbType.NVarChar,20), 
      new SqlParameter("@ClassID",SqlDbType.Int) 
      };
            parameters[0].Value = ID;
            parameters[1].Value = Name;
            parameters[2].Value = Sex;
            parameters[3].Value = Tel;
            parameters[4].Value = ClassID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary> 
        /// 删除一条数据 
        /// </summary> 
        public int delete()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Students ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = { 
new SqlParameter("@ID", SqlDbType.Int,4) 
};
            parameters[0].Value = ID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary> 
        /// 获取数据列表 
        /// </summary> 
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from Students ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary> 
        /// 分页查询 
        /// </summary> 
        /// <param name="PageIndex">当前第几页</param> 
        /// <param name="PageSize">每页条数</param> 
        /// <param name="strWhere">条件</param> 
        /// <param name="Recordcount">记录总条数</param> 
        /// <returns></returns> 
        public DataSet Pager(int PageIndex, int PageSize, string strWhere, out int Recordcount)
        {
            string strSql = string.Empty;
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = " 1=1 ";
            }
            strSql = string.Format("select top {0} * from Students where id not in (select top {1} id from Students where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere);
            DataSet ds = DbHelperSQL.Query(strSql);
            string strSql2 = string.Format("select id from Students where {0}", strWhere);
            DataSet dsCount = DbHelperSQL.Query(strSql2);
            try
            {
                Recordcount = dsCount.Tables[0].Rows.Count;
            }
            catch
            {
                Recordcount = 0;
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary> 
        /// 批量删除数据 
        /// </summary> 
        public int BatchDelete(string _idstr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from Students ");
            strSql.Append(string.Format(" where ID in ({0}) ", _idstr));
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion 方法成员
    }
}

