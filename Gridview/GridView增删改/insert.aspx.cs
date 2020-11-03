using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class insert : System.Web.UI.Page
{
    SqlConnection sqlcon;
    string strCon = "Data Source=(local);Database=wxd;Uid=sa;Pwd=sa";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
        
    }
    //绑定GridView
    public void bind()
    {
        string sqlstr = "select  * from Admin";
        sqlcon = new SqlConnection(strCon);
        SqlDataAdapter myda = new SqlDataAdapter(sqlstr,DBHelper.Conn);
        DataSet myds = new DataSet();
        DBHelper.OpenConnection();
        myda.Fill(myds, "Admin");
        GridView1.DataSource = myds;
        GridView1.DataBind();
        DBHelper.CloseConnection();
    }
    //添加记录
    protected void showAdd_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = true;
      
        bind();
     
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //var button = sender as Button;
        //GridViewRow gvr = (GridViewRow)button.Parent.Parent;
        //var Label = (Label)this.GridView1.Rows[gvr.RowIndex].FindControl("LabelVal");
        //int v = Convert.ToInt32(Label.Text);
        //Label.Text = (v + 1).ToString();
        
       // TextBox b2 = GridView1.FooterRow.FindControl("txt_ID") as TextBox;
       //// string post_code = ((TextBox)GridView1.FindControl("txt_ID")).Text.Trim();
       // String text = b2.Text;
       // text = ((TextBox)(GridView1.Rows[0].FindControl("txt_ID"))).Text.ToString().Trim();
       // Response.Write(text);
       // int v;
       // for (int i = 0; i < GridView1.Rows.Count; i++)
       // {
       //     TextBox tb = (TextBox)GridView1.Rows[i].FindControl("txt_ID");
       //      v = Convert.ToInt32(tb.Text);
       // }



        TextBox tb1 = (TextBox)GridView1.FooterRow.FindControl("txt_ID");
        int _txt_id = Convert.ToInt32(tb1.Text);
        TextBox tb2 = (TextBox)GridView1.FooterRow.FindControl("txt_Name");
        String _txt_Name = tb2.Text.ToString();
        DropDownList tb3 = (DropDownList)GridView1.FooterRow.FindControl("Ddl_Sex");
        String _Lb_Sex = tb3.Text.ToString();
        TextBox tb4 = (TextBox)GridView1.FooterRow.FindControl("txt_Address");
        String _txt_Address = tb4.Text.ToString();
        DBHelper.OpenConnection();
      
        SqlCommand comm =new  SqlCommand("sp_insert",DBHelper.Conn);
        comm.CommandType=CommandType.StoredProcedure;
        comm.Parameters.Add("@_comm_table", SqlDbType.NVarChar,300).Value = " Admin ";

        comm.Parameters.Add("@_comm_values", SqlDbType.NVarChar, 200).Value = " convert(int," + _txt_id + "),'" + _txt_Name + "',convert(nvarchar(max),'" + _Lb_Sex + "'),convert(nvarchar(max),'" + _txt_Address + "')," + 20 + "," + 30;

        int i  = comm.ExecuteNonQuery();
        DBHelper.CloseConnection();
        this.GridView1.ShowFooter = false;
        bind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = false;

        bind();
    }
}
