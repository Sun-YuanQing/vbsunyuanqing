using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class C_ListBox_Trreview : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {   //C#开始遍历指定的文件夹
            String _vbsunyuanqing = "C:/vbsunyuanqing/";
            //  C#遍历指定文件夹中的所有文件 
            DirectoryInfo TheFolder = new DirectoryInfo(_vbsunyuanqing);
            //开始遍历文件E:vbsunyuanqing/
            int m0_id = 0;
            int m1_id = 0;
           int m2_id = 0;
            //开始遍历文件夹E:vbsunyuanqing/--------------------------------------------------------------------------
           foreach (DirectoryInfo _A in TheFolder.GetDirectories())
           { // this.ListBox1.Items.Add(NextFolder.Name);
               if (_A.Name != null)
               {
                   m0_id++;
                   _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL0(m0_id, _A.Name, _A.Name, 0, 0));
               }
               TreeNode _a = new TreeNode();          // 创建文件夹节点
               _a.Text = (_A.Name);
               _a.Value = (_A.Name);
               this.TreeView1.Nodes.Add(_a);         //将文件夹节点添加到TreeView1
              
               // C#第二次遍历E:vbsunyuanqing/  下【a文件夹】的【文件】
               DirectoryInfo _A_B = new DirectoryInfo(_vbsunyuanqing + _A.Name);     //开始遍历E:vbsunyuanqing/  下【a文件夹】的【b文件夹】
               foreach (DirectoryInfo _B in _A_B.GetDirectories())
               {
                   if (_B.Name != null)
                   {
                       m1_id++;
                       String m1_URL = _A.Name + "/" + _B.Name;
                       _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL1(m1_id, m0_id, _B.Name, m1_URL, 0, 0));
                   }
                   TreeNode _b = new TreeNode();
                   _b.Text = (_B.Name);
                   _b.Value = (_B.Name);
                   TreeNode _AB = new TreeNode();
                   _AB = this.TreeView1.FindNode(_a.Value);     //获取E:vbsunyuanqing/  下【a文件夹】的节点
                   _AB.ChildNodes.Add(_b);          //将E:vbsunyuanqing/  下【a文件夹】的【b文件夹】节点添加到E:vbsunyuanqing/  下【a文件夹】的节点中
               
                   //----------------------san------------
                   DirectoryInfo _A_B_C = new DirectoryInfo(_vbsunyuanqing + _A.Name + "/" + _B.Name);
                   foreach (DirectoryInfo _C in _A_B_C.GetDirectories())
                   {
                       if (_C.Name != null)
                       {
                           m2_id++;
                           String m2_URL = _A.Name + "/" + _B.Name + "/" + _C.Name;
                           _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL2(m2_id, m0_id,m1_id ,_C.Name, m2_URL, 0, 0));
                       }
                       TreeNode _c = new TreeNode();
                       _c.Text = (_C.Name);
                       _c.Value = (_C.Name);
                       TreeNode _abc = new TreeNode();
                       _abc = this.TreeView1.FindNode(_a.Value + "/" + _b.Value);     //获取E:vbsunyuanqing/  下【a文件夹】的节点
                       _abc.ChildNodes.Add(_c);          //将E:vbsunyuanqing/  下【a文件夹】的【b文件夹】节点添加到E:vbsunyuanqing/  下【a文件夹】的节点中
                      
               
                   }
                  
                   foreach (FileInfo _CX in _A_B_C.GetFiles("*.aspx"))   //遍历E:vbsunyuanqing/  下【a文件夹】的【文件】
                   {
                       if (_CX.Name != null)
                       {
                           m2_id++;
                           String m2_URL = _A.Name + "/" + _B.Name + "/" + _CX.Name;
                           _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL2(m2_id, m0_id, m1_id, _CX.Name, m2_URL, 0, 0));
                       }
                       //创建E:vbsunyuanqing/  下【文件夹】的【文件】节点
                       TreeNode _cx = new TreeNode();
                       _cx.Text = (_CX.Name);
                       _cx.Value = (_CX.Name);

                       TreeNode _abcx = new TreeNode();
                       _abcx = this.TreeView1.FindNode(_a.Value + "/" + _b.Value);  
                       _abcx.ChildNodes.Add(_cx);

                   }
                   //-------------------------------san------------

                  
               }
               foreach (FileInfo _bX in _A_B.GetFiles("*.aspx"))   //遍历E:vbsunyuanqing/  下【a文件夹】的【文件】
               {
                   if (_bX.Name != null)
                   {
                       m1_id++;
                       String m1_URL = _A.Name + "/" + _bX.Name;
                       _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL1(m1_id, m0_id, _bX.Name, m1_URL, 0, 0));
                   }
                   //创建E:vbsunyuanqing/  下【文件夹】的【文件】节点
                   TreeNode _bx = new TreeNode();
                   _bx.Text = (_bX.Name);
                   _bx.Value = (_bX.Name);

                   TreeNode _pnode1 = new TreeNode();
                   _pnode1 = this.TreeView1.FindNode(_a.Value);  //获取E:vbsunyuanqing/  下【a文件夹】的节点
                   _pnode1.ChildNodes.Add(_bx);                 //将E:vbsunyuanqing/  下【a文件夹】的【文件】节点添加到E:vbsunyuanqing/  下【文件夹】的节点中
               }//_bX
           }//A
               foreach (FileInfo _X in TheFolder.GetFiles("*.aspx"))
               {
                   if (_X.Name != null)
                   {
                       m0_id++;
                       _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL0(m0_id, _X.Name, _X.Name, 0, 0));
                   }
                   TreeNode _x = new TreeNode();          // 创建文件节点
                   _x.Text = (_X.Name);
                   _x.Value = (_X.Name);
                   this.TreeView1.Nodes.Add(_x);             //将文件节点添加到TreeView1

               
               }//_AX
            //-----------------------------------------------------------------------------------------------------------
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        //C#开始遍历指定的文件夹
        String _vbsunyuanqing = "C:vbsunyuanqing";
        ListBox1.Items.Clear();
        //  C#遍历指定文件夹中的所有文件 
        DirectoryInfo TheFolder = new DirectoryInfo(_vbsunyuanqing);
        //遍历文件夹
        //foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
        //{
        //    this.ListBox1  .Items.Add(NextFolder.Name);
        //}
        //遍历文件
        //foreach(FileInfo NextFile in TheFolder.GetFiles())
        // this.ListBox1 .Items.Add(NextFile.Name);
        FindFile(_vbsunyuanqing);
        //-----------遍历文件夹-------------------------------------------
        //DirectoryInfo dir = new DirectoryInfo(@"c:\");
        //foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
        //{//如果用GetDirectories("ab*"),那么全部以ab开头的目录会被显示
        //    Response.Write(dChild.Name + "<BR>");//打印目录名
        //    Response.Write(dChild.FullName + "<BR>");//打印路径和目录名
        //}
        //-----------------------------------------------------------     
        //  //  2、遍历一个目录下的全部文件，要用到System.IO.DirectoryInfo 类的GetFiles方法：
        //DirectoryInfo dir = new DirectoryInfo(@"c:\");
        //   foreach  (DirectoryInfo dChild in dir.GetFiles("*")) 
        //{//如果用GetFiles("*.txt"),那么全部txt文件会被显示
        //    Response.Write(dChild.Name + "<BR>");//打印文件名
        //    Response.Write(dChild.FullName + "<BR>");//打印路径和文件名
        //}

    }

    //采用递归的方式遍历，文件夹和子文件中的所有文件。
    public void FindFile(string dirPath) //参数dirPath为指定的目录
    {
        //在指定目录及子目录下查找文件,在ListBox1中列出子目录及文件
        DirectoryInfo Dir = new DirectoryInfo(dirPath);
        try
        {
            foreach (DirectoryInfo d in Dir.GetDirectories())//查找子目录
            {
               // FindFile(d.ToString() + "&nbsp;" + "00000");
                ListBox1.Items.Add(d.ToString() );
               // ListBox1.Items.Add(d.ToString() + "-"); //ListBox1中填加目录名
            }

            //用下面代码限制文件的类型：
            //foreach(FileInfo f in Dir.GetFiles("*.---")) //查找文件
            //“*.---”指要访问的文件的类型的扩展名

            foreach (FileInfo f in Dir.GetFiles("*.aspx")) //查找文件
            {
                ListBox1.Items.Add(f.ToString()); //ListBox1中填加文件名
            }
        }
        catch (Exception )
        {
             
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Merge(_new_ds_dt_dc_5 .ds.Tables ["t_gridview_menuL0"] );
      //  ds.Merge(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL1"]);
        GridView1.DataSource = ds;
    
        GridView1.DataBind();
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            DBHelper.OpenConnection();
            SqlBulkCopy sbc3 = new SqlBulkCopy(DBHelper.Conn);
            sbc3.DestinationTableName = "t_gridview_menuL2";
            sbc3.WriteToServer(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL2"]);

            DBHelper.OpenConnection();
            SqlBulkCopy sbc2 = new SqlBulkCopy(DBHelper.Conn);
            sbc2.DestinationTableName = "t_gridview_menuL1";
            sbc2.WriteToServer(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL1"]);


            SqlBulkCopy sbc1 = new SqlBulkCopy(DBHelper.Conn);
            sbc1.DestinationTableName = "t_gridview_menuL0";
            sbc1.WriteToServer(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL0"]);
            DBHelper.CloseConnection();
        }
        catch (Exception)
        {
        }
        _new_ds_dt_dc_5.ds.Clear();
    }

}


  

   

