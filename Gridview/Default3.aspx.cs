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
//download by http://www.codefans.net
public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string aa = "北京@上海@重庆|辽宁@广州@河北";
        string[] bb = aa.Split('|','@');
        for (int I = 0; I < bb.Length; I++)
        {
            Response.Write(bb[I]);
            Response.Write("</br>");
        }
    }
}
