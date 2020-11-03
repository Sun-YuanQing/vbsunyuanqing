using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//download by http://www.codefans.net
using System.Data.SqlClient;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    SqlConnection sqlcon;
    string strCon = "Data Source=(local);Database=wxd;Uid=sa;Pwd=sa";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Panel2.Visible = false;
            bind();
        }
    }
    //绑定GridView
    public void bind()
    {
        string sqlstr = "select  [ImageName],[ImageID] ,IMAGE from [Image]";
        sqlcon = new SqlConnection(strCon);
        SqlDataAdapter myda = new SqlDataAdapter(sqlstr,DBHelper.Conn);
        DataSet myds = new DataSet();
        DBHelper.OpenConnection();
        myda.Fill(myds, "Admin");
        GridView1.DataSource = myds;
        GridView1.DataBind();
    }
    protected void Bt_Cacel_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = false;
    }
    //保存图片
    protected void Bt_Save_Click(object sender, EventArgs e)
    {        
        Stream imgStream=FU_image.PostedFile.InputStream;
        int imgLen=FU_image.PostedFile.ContentLength;
        string imgName=this.T_image.Text;
        byte[] imgBinaryData=new byte[imgLen];
        int n=imgStream.Read(imgBinaryData,0,imgLen);
    
     SqlConnection connection = new SqlConnection("Data Source=(local);Database=wxd;Uid=sa;Pwd=sa");
     SqlCommand command = new SqlCommand("insert into Image (ImageName,Image) values ( @img_name, @img_data)",DBHelper.Conn);
 
     SqlParameter param0 = new SqlParameter("@img_name", SqlDbType.VarChar, 50);
     param0.Value = imgName;
     command.Parameters.Add(param0);
 
     SqlParameter param1 = new SqlParameter("@img_data", SqlDbType.Image);
     param1.Value = imgBinaryData;
     command.Parameters.Add(param1);

     DBHelper.OpenConnection();
     int numRowsAffected = command.ExecuteNonQuery();
      DBHelper.CloseConnection();
     this.Panel2.Visible = false;
     this.T_image.Text = "";
     bind();
    }
    //显示保存
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Panel2.Visible = true;
    }
}
