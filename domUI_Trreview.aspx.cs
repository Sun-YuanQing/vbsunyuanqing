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
    String _vbsunyuanqing = "E:/HMTL5_Demo/myGit-wrok/vbsunyuanqing/";
    protected void OnPreRenderComplete(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {   //C#开始遍历指定的文件夹
            //  C#遍历指定文件夹中的所有文件 
            DirectoryInfo TheFolder = new DirectoryInfo(_vbsunyuanqing);
            //开始遍历文件E:vbsunyuanqing/
            int m0_id = 0;
            int m1_id = 0;
            int m2_id = 0;
            // 【文件夹】下_A
            _new_ds_dt_dc_5.ds.Clear();
            ds.Clear();
            foreach (DirectoryInfo _A in TheFolder.GetDirectories())
            { // this.ListBox1.Items.Add(NextFolder.Name);
               if (_A.Name == ".git" || _A.Name == ".vs") {
                    continue;
               }

               if (_A.Name != null)
               {
                   m0_id++;
                   _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL0(m0_id, _A.Name, _A.Name, 0, 0));
               }
               TreeNode _a = new TreeNode();         // 创建文件夹节点
               _a.Text = (_A.Name);
               _a.Value = (_A.Name);
               this.TreeView1.Nodes.Add(_a);         //将文件夹节点添加到TreeView1

                // 【文件夹】下_A_B  
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

                   
                    // 【文件夹】下_A_B_C
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

                    // 【文件夹】下_A_B_C 不过滤GetFiles("*.aspx"))
                    foreach (FileInfo _CX in _A_B_C.GetFiles())  
                   {
                       if (_CX.Name != null)
                       {
                           m2_id++;
                           String m2_URL = _A.Name + "/" + _B.Name + "/" + _CX.Name;
                           _new_ds_dt_dc_5.ds.Merge(_new_ds_dt_dc_5.t_gridview_menuL2(m2_id, m0_id, m1_id, _CX.Name, m2_URL, 0, 0));
                       }
                        //创建E:vbsunyuanqing/  下_A_B_C的【文件】节点
                        TreeNode _cx = new TreeNode();
                       _cx.Text = (_CX.Name);
                       _cx.Value = (_CX.Name);

                       TreeNode _abcx = new TreeNode();
                       _abcx = this.TreeView1.FindNode(_a.Value + "/" + _b.Value);  
                       _abcx.ChildNodes.Add(_cx);

                   }
                
                  
               }
                // 【文件夹】下_A_B 不过滤GetFiles("*.aspx"))
                foreach (FileInfo _bX in _A_B.GetFiles())  
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
            }
             // 【文件夹】下_A  不过滤GetFiles("*.aspx"))
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
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
       DataSet ds = new DataSet();
       //ds.Merge(_new_ds_dt_dc_5 .ds.Tables ["t_gridview_menuL0"] );
       ds.Merge(_new_ds_dt_dc_5.ds.Tables["t_gridview_menuL1"]);
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
    protected void selecteTree(object sender, EventArgs e)
    {
        try
        {
            Response.Write("selecteTree");
        }
        catch (Exception)
        {
        }
    }
}


  

   

