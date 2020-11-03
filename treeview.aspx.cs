using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class treeview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TreeNode _tnode = new TreeNode();
        _tnode = Treeview1.SelectedNode;
        if (_tnode.Value.ToString() != null)
        {
            Response.Write(_tnode.Text + "<br/>");
            Response.Write(_tnode.Value + "<br/>");
            Response.Write(_tnode.ValuePath + "<br/>");
        }
        else
        {
            Response.Write(_tnode.Value.ToString());
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TreeNode _tnode = new TreeNode();
        _tnode.Text = "E";
        _tnode.Value = "V_E";
        //  Treeview1.Nodes.Add(_tnode );

        TreeNode _pnode = new TreeNode();
        _pnode = Treeview1.FindNode("CV/C1V/C2V/C3V");
        _pnode.ChildNodes.Add(_tnode);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (TreeNode _tnode in Treeview1.CheckedNodes)
            Response.Write(_tnode.ValuePath + "<br/>");
        {

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        String _vbsunyuanqing = "E:vbsunyuanqing";
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
        //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件
        DirectoryInfo Dir = new DirectoryInfo(dirPath);
        try
        {
            foreach (DirectoryInfo d in Dir.GetDirectories())//查找子目录
            {
                FindFile(d.ToString() + "&nbsp;");
                ListBox1.Items.Add(d.ToString() + "-"); //listBox1中填加目录名
            }

            //用下面代码限制文件的类型：
            //foreach(FileInfo f in Dir.GetFiles("*.---")) //查找文件
            //“*.---”指要访问的文件的类型的扩展名

            foreach (FileInfo f in Dir.GetFiles("*.aspx")) //查找文件
            {
                ListBox1.Items.Add(f.ToString()); //listBox1中填加文件名
            }
        }
        catch (Exception )
        {

        }
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}