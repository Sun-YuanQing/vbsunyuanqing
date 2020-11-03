using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class webservier_webservicer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            bind();
        }

    }
    /// <summary>
    /// 查询
    /// </summary>
    #region 方法成员
    public  void bind()
    {
       
        GridView_Webserview.WebServiceSoapClient Dal = new GridView_Webserview.WebServiceSoapClient();
        int Recordcount = 0;
        int PageIndex = YYCMS.Request.GetQueryInt("p", 1);
        int PageSize = 10;
        string strWhere = "  1=1 ";

        DataSet ds = Dal.StudentsPager(out Recordcount, PageIndex, PageSize, strWhere);
        GridView1.DataSource = ds;

        GridView1.DataBind();

        Pager1.PageIndex = PageIndex;
        Pager1.PageSize = PageSize;
        Pager1.RecordCount = Recordcount;
    
    }

    /// <summary>
    /// 开始编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridView1.EditIndex = e.NewEditIndex;//开始编辑
        this.GridView1.ShowFooter = false;  //取消添加
        bind();
        int sex = Convert.ToInt32(((Label)GridView1.Rows[e.NewEditIndex].Cells[4].FindControl("lblSex")).Text);  //获取原来的值（隐藏的lblSex）
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_Sex_Edit")).SelectedIndex = sex;            //加载时绑定原来的值
        ((TextBox)GridView1.Rows[e.NewEditIndex].FindControl("txt_tel")).Enabled = false;             //设置权限
        int classid = Convert.ToInt32(((Label)GridView1.Rows[e.NewEditIndex].Cells[4].FindControl("lblclassid")).Text);
        ((DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlclassid")).SelectedIndex = classid;
    }
    /// <summary>
    /// 更新编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ok = string.Empty;
        GridView_Webserview.WebServiceSoapClient Dal = new GridView_Webserview.WebServiceSoapClient();
        int id = Convert.ToInt32(((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblid3")).Text);
        String name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0].FindControl("txt_name"))).Text.ToString().Trim();
        int Sex = Convert.ToInt32(((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddl_Sex_Edit")).Text);
        String tel = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0].FindControl("txt_tel"))).Text.ToString().Trim();
        int classid = Convert.ToInt32(((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlclassid")).Text);
        if (Sex != 2 && classid != 0)
        {
            ok = Dal.StudentsEdit(id, name, Sex, tel, classid);
            if (ok != "")
            {
                Response.Write("<script lang ='ja' >alert('修改数据成功！');</script>");
            }
            GridView1.EditIndex = -1;
            bind();
        }
        else {
            Response.Write("<script lang ='ja' >alert('修改数据失败！ 请注意选择项.');</script>");
        }
      
    }
    /// <summary>
    /// 取消编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bind();
    }
    #endregion 方法成员
    /// <summary>
    /// 行的样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            e.Row.Attributes["style"] = "Cursor:pointer";
        }
    }
    /// <summary>
    /// 绑定级别
    /// </summary>
    /// <param name="classid"></param>
    /// <returns></returns>
    public String classidname(String classid)
    {
        String name = String.Empty;
        switch (classid)
        {
            case "0": name = "请选择"; break;
            case "1": name = "连跳一段"; break;
            case "2": name = "连跳二段"; break;
            case "3": name = "连跳三段"; break;
            case "4": name = "连跳四段"; break;
        }

        return name;
    }
 
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
        //GridView1.EditIndex = 0;//第一行为编辑行
        //DataTable myda = (DataTable)this.ViewState["table"]; //实力创建的临时表
        //DataRow darow = myda.NewRow(); //给表添加新的行
        //darow[0] = ""; //给临时表添加记录值这里一行添加了六个值
        //darow[1] = xm;
        //darow[2] = xb;
        //darow[3] = csrq;
        //darow[4] = bjid;
        //darow[5] = jtdz;
        //darow[6] = lxdh;
        //myda.Rows.Add(darow); //添加这一行的记录插入到表
        //GridView1.DataSource = myda; //绑定数据源
        //GridView1.DataBind();
    }
    /// <summary>
    /// 开始添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {


        this.GridView1.ShowFooter = true;    //开始添加
        this.GridView1.EditIndex = -1;       //取消编辑
        bind();
    }
    /// <summary>
    /// 添加方法----------------------------------------------没有判断text为空----
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninsert_Click(object sender, EventArgs e)
    {
        String ok = String.Empty;
        GridView_Webserview.WebServiceSoapClient Dal = new GridView_Webserview.WebServiceSoapClient();
        String name = ((TextBox)GridView1.FooterRow.FindControl("txt_Name")).Text.ToString();
        int Sex = Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddl_Sex_Footer")).Text);
        String tel = ((TextBox)GridView1.FooterRow.FindControl("txt_tel")).Text.ToString();
        int classid = Convert.ToInt32(((DropDownList)GridView1.FooterRow.FindControl("ddlclassid")).Text);
     
       if (Sex != 2 && classid != 0)
       {
           ok  = Dal.StudentsAdd(name, Sex, tel, classid);
           if (ok != "")
           {
               Response.Write("<script lang ='ja' >alert('添加数据成功！');</script>");
           }
           this.GridView1.ShowFooter = false;
           bind();
       }
       else
       {
           Response.Write("<script lang ='ja' >alert('添加数据失败！ 请注意选择项.');</script>");
       }
    }
    /// <summary>
    /// 取消添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_insert_colse_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter =  false;
        bind();
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imabtn_Del_Click(object sender, ImageClickEventArgs e)
    {
        GridView_Webserview.WebServiceSoapClient Dal = new GridView_Webserview.WebServiceSoapClient();
        ImageButton ibtn = (ImageButton)sender;             //单击做出响应的控件
        GridViewRow gr = (GridViewRow)ibtn.Parent.Parent;   //控件中的单独行（ImageButton选中的行）
        int id = Convert.ToInt32(((Label)GridView1.Rows[gr.RowIndex].Cells[0].FindControl("lblid1")).Text);
        Dal.StudentsDelete(id);
        bind();
    }
}