<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Page.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
      <script language="javascript" type="text/javascript">    
    // 判断多选是否与选中项（没有选中的返回false）
    function slcNo_click()
    {
        if (document.form1.checkboxname.length)
        {
            for (var i=0;i<document.form1.checkboxname.length;i++)
            {
                if(document.form1.checkboxname[i].checked)
                {
                    return true;
                }
            }
        }
        else
        {
            if(document.form1.checkboxname.checked)
            {
                return true;
            }
        }
        alert("请选择后再操作！");
        return false;
    }
    
    // 鼠标经过改变行的颜色
    if (!objbeforeItem)
    {
        var objbeforeItem=null;
        var objbeforeItembackgroundColor=null;
    }    
    function ItemOver(obj)
    {
        if(objbeforeItem)
        {
            objbeforeItem.style.backgroundColor = objbeforeItembackgroundColor;
        }
        objbeforeItembackgroundColor = obj.style.backgroundColor;
        objbeforeItem = obj;
        obj.style.backgroundColor = "#B9D1F3";     
    }
    //        
 
    // 多选的全选与取消
    function checkJs(boolvalue)
    {
        if(document.all.checkboxname.length>1)
        {
            for(var i=0;i<document.all.checkboxname.length;i++)
            {
                document.all.checkboxname[i].checked = boolvalue;            
            }
        }
        else
            document.all.checkboxname.checked = boolvalue;
    }
    //
    
    // 只有全部选中时“全选”选中
    function SingleCheckJs()
    {
        var flag1=false;
        var flag2=false;
        
        if (document.form1.checkboxname.length)
        {
            for (var i=0;i<document.form1.checkboxname.length;i++)
            {
                if(document.form1.checkboxname[i].checked)
                    flag1 = true;
                else
                    flag2 = true;
            }
        }
        else
        {
            if(document.form1.checkboxname.checked)
                flag1 = true;
            else
                flag2 = true;
        }
        
        if(flag1==true&&flag2==false)
            document.getElementById("chk").checked = true;
        else
            document.getElementById("chk").checked = false;
    }
    //
    </script>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="80%" style="font-size: 11px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="5" AllowSorting="True"
                            AutoGenerateColumns="False" HeaderStyle-VerticalAlign="Middle" CellPadding="3"
                            Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                             Width="100%" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="用户ID" Visible="false" />
                                <asp:TemplateField HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;全选"
                                    >
                                    <ItemTemplate>
                                        <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "CID")%>'
                                            onclick='SingleCheckJs();' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单选">
                                    <ItemTemplate>
                                        <input type="radio" id="RadioName" name="RadioName" value='<%# Eval("CID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编号">
                                    <ItemTemplate>
                                        <%# (Container.DataItemIndex+1).ToString()%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="用户姓名" />
                                <asp:BoundField DataField="Sex" HeaderText="性别" />
                                <asp:BoundField DataField="Address" HeaderText="家庭住址" />
                                <asp:BoundField DataField="Post" HeaderText="邮政编码" />
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <PagerSettings Visible="False" />
                            <FooterStyle Font-Bold="True" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="height: 25px">
                        <asp:LinkButton ID="btnFirst" CommandArgument="first" OnClick="PagerButtonClick" runat="server">首 页</asp:LinkButton>
                        <asp:LinkButton ID="btnPrev" CommandArgument="prev" OnClick="PagerButtonClick" runat="server">上一页</asp:LinkButton>
                        <asp:LinkButton ID="btnNext" CommandArgument="next" OnClick="PagerButtonClick" runat="server">下一页</asp:LinkButton>
                        <asp:LinkButton ID="btnLast" CommandArgument="last" OnClick="PagerButtonClick" runat="server">尾 页</asp:LinkButton>
                        <asp:Label ID="LblCurrentIndex" runat="server"></asp:Label>
                        <asp:Label ID="LblPageCount" runat="server"></asp:Label>
                        <asp:Label ID="LblRecordCount" runat="server"></asp:Label>
                      &nbsp;&nbsp;&nbsp;&nbsp; 跳转到: <asp:DropDownList ID="pageDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="pageDropDownList_SelectedIndexChanged"></asp:DropDownList>
                        
                       
                        </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
