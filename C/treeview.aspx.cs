using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class C_treeview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true )
        {
            String sql = "selECt * from Treeview ";
            SqlDataReader datar = DBHelper.GetReader(sql);

               TreeNode _tnode;
            while (datar.Read())
            {
                if (datar["er_id"].ToString() == "0")
                {
                    _tnode = new TreeNode();
                    _tnode.Text = datar["name"].ToString();
                    _tnode.Value = "yi_" + datar["yi_id"].ToString();
                    TreeView1.Nodes.Add(_tnode);
                }
                else
                { 
                    TreeNode _er_path = new TreeNode();
                     _er_path = TreeView1.FindNode("yi_" + datar["er_id"].ToString());
                     TreeNode    _tnode1 = new TreeNode();
                        _tnode1.Text = datar["name"].ToString();
                        _tnode1.Value = "er_" + datar["er_id"].ToString();
                       _er_path.ChildNodes.Add(_tnode1 );
                }
            }

        }
                DBHelper.CloseConnection();                   
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
}
