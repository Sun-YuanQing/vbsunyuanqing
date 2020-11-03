using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
public partial class C_Start : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        makenav();
    }
    protected void makenav()
    {

        String _str_conn = "server=.;Database=Myschool;uid=sa;pwd=2222;";
        String _str_sql = "select k.*,l2.* from(select * from t_gridview_menuL0 l0 " +
 " left join t_gridview_menuL1 l1 on (l0.m0_id =l1.m1_m0id )) k " +
" left join   t_gridview_menuL2 l2 on (l2 .m2_m1id =k .m1_id )";
        SqlConnection conn = new SqlConnection(_str_conn);
        SqlCommand comm = new SqlCommand(_str_sql, DBHelper.Conn);
        SqlDataReader dt = null;
        DBHelper.Conn.Close();
        DBHelper.OpenConnection();
        dt = comm.ExecuteReader();

        Panel pal_menuset, pal_submeun, pal_san_menu;
        HyperLink a_m0, a_m1, a_m2;
     
        while (dt.Read())
        {
            string s_m0 = dt["m0_id"].ToString();

            pal_menuset = (Panel)div_menu.FindControl("div_menuset_" + s_m0);
            if (pal_menuset == null)
            {
                pal_menuset = new Panel(); pal_menuset.CssClass = "menuset";
                pal_menuset.ID = "div_menuset_" + s_m0;
                div_menu.Controls.Add(pal_menuset);
            }
            pal_menuset = (Panel)div_menu.FindControl("div_menuset_" + s_m0);
            // pal_menuset.BackImageUrl = "../图片素材/home/submenu_bg.gif";
            a_m0 = (HyperLink)FindControl("a_m0_" + s_m0);
            if (a_m0 == null)
            {
                if (dt["m0_url"].ToString().IndexOf(".aspx") != -1)
                {
                    a_m0 = new HyperLink(); a_m0.CssClass = "m0";
                    a_m0.ID = "a_m0_" + s_m0;
                    a_m0.Text = dt["m0_ttl"].ToString();
                    a_m0.NavigateUrl = dt["m0_url"].ToString();
                    pal_menuset.Controls.Add(a_m0);
                }
                else
                {
                    a_m0 = new HyperLink(); a_m0.CssClass = "m0";
                    a_m0.ID = "a_m0_" + s_m0;
                    a_m0.Text = dt["m0_ttl"].ToString();
                    // a_m0.NavigateUrl = dt["m0_url"].ToString();
                    pal_menuset.Controls.Add(a_m0);
                }
            }
            a_m0.Target = "myFrameName";
            string s_m1 = dt["m1_id"].ToString();
            if (!(dt["m1_ttl"] is DBNull))
            {
                pal_submeun = (Panel)FindControl("div_submenu_" + s_m0);//pal_menuset = (Panel)div_menu.FindControl("div_menuset_" + s_m0);
                if (pal_submeun == null)
                {
                    pal_submeun = new Panel(); pal_submeun.CssClass = "submenu";
                    pal_submeun.ID = "div_submenu_" + s_m0;
                    pal_menuset.Controls.Add(pal_submeun);              //div_menu.Controls.Add(pal_menuset);   
                }
                pal_submeun = (Panel)FindControl("div_submenu_" + s_m0);//pal_menuset = (Panel)div_menu.FindControl("div_menuset_" + s_m0);
                a_m1 = (HyperLink)FindControl("a_m1_" + s_m1);           //a_m0 = (HyperLink)FindControl("a_m0_" + s_m0);
                 if (a_m1 == null)
                 {
                     a_m1 = new HyperLink(); a_m1.CssClass = "m1";
                     a_m1.ID = "a_m1_" + s_m1;                          // a_m1.ID = "a_m0_" + s_m0;
                     a_m1.Text = dt["m1_ttl"].ToString();
                     a_m1.NavigateUrl = dt["m1_url"].ToString();

                     pal_submeun.Controls.Add(a_m1);                  //pal_menuset.Controls.Add(a_m0);
                     a_m1.Target = "myFrameName";
                 }
                 string s_m2 = dt["m2_id"].ToString();
                 if (!(dt["m2_ttl"] is DBNull))
                 {
                     pal_san_menu = (Panel)FindControl("div_san_menu_" + s_m1);//("div_san_menu_" + s_m1)以上级id编制三级框架
                     if (pal_san_menu == null)//如果三级框架存在
                     {
                         pal_san_menu = new Panel(); pal_san_menu.CssClass = "san_menu";
                         pal_san_menu.ID = "div_san_menu_" + s_m1;
                         pal_submeun.Controls.Add(pal_san_menu);
                     }
                     pal_san_menu = (Panel)FindControl("div_san_menu_" + s_m1);
                     a_m2 = (HyperLink)FindControl("a_m2_" + s_m2);
                     if (a_m2 == null)
                     {
                         a_m2 = new HyperLink(); a_m2.CssClass = "m2";
                         a_m2.ID = "a_m2_" + s_m2;
                         a_m2.Text = dt["m2_ttl"].ToString();
                         a_m2.NavigateUrl = dt["m2_url"].ToString();

                         pal_san_menu.Controls.Add(a_m2);
                         a_m2.Target = "myFrameName";
                     }

                 }
             }
          
        }
        Panel p = new Panel();
        p.Style.Add("clear", "both");
        div_menu.Controls.Add(p);
        
    }

}