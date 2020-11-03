<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webservicer.aspx.cs" Inherits="webservier_webservicer" %>

<%@ Register Src="~/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False" CellPadding="5" Font-Size="9pt" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCreated="GridView1_RowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="用户ID"   >
                        <ItemTemplate >
                            <asp:Label ID="lblid1" runat="server" Text='<%# Eval("id")%>' Enabled="true" ></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                             <asp:Label ID="lblid2" runat="server" Text='<%# Eval("id")%>' ReadOnly="true" ></asp:Label>
                        </FooterTemplate>
                         <EditItemTemplate>
                             <asp:Label ID="lblid3" runat="server" Text='<%# Eval("id")%>' ReadOnly="true" ></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="姓名">
                            <ItemStyle Width="90px" />
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                        </ItemTemplate>
                            <FooterTemplate>
                            <asp:TextBox ID="txt_name" runat="server" Width="90px"  ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_name" runat="server" Text='<%# Eval("name")%>' Width="90px" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="性别">
                         <ItemStyle Width="90px" />
                        <ItemTemplate>
                            <asp:Label ID="lblSex" runat="server" Text='<%# (Eval("Sex").ToString()== "1")?"男" : "女" %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:DropDownList ID="ddl_Sex_Footer" runat="server" Width="90px">
                                <asp:ListItem Value="2">请选择</asp:ListItem>
                                <asp:ListItem Value="1">男</asp:ListItem>
                                <asp:ListItem Value="0">女</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblSex" runat="server" Text='<%# Eval("Sex") %>' Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddl_Sex_Edit" runat="server" Width="90px" >
                                <asp:ListItem Value="0">女</asp:ListItem>
                                <asp:ListItem Value="1">男</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="电话">
                         <ItemStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lbltel" runat="server" Text='<%# Eval("tel")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_tel" runat="server" Width="100px" ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_tel" runat="server"  Text='<%# Eval("tel")%>' Width="100px" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="级别">
                        <ItemStyle Width="100px" />
                        <ItemTemplate>
                             
                            <asp:Label ID="lblclassid" runat="server" Text='<%# classidname(Eval("classid").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblclassid" runat="server" Text='<%# Eval("classid") %>' Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlclassid" runat="server" Width="100px"   >
                             <asp:ListItem Value="0">请选择</asp:ListItem> 
                                <asp:ListItem Value="1">连跳一段</asp:ListItem>
                                 <asp:ListItem Value="2">连跳二段</asp:ListItem>
                                 <asp:ListItem Value="3">连跳三段</asp:ListItem>
                                <asp:ListItem Value="4">连跳四段</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlclassid" runat="server" Width="100px" >
                                <asp:ListItem Value="0">请选择</asp:ListItem> 
                                <asp:ListItem Value="1">连跳一段</asp:ListItem>
                                 <asp:ListItem Value="2">连跳二段</asp:ListItem>
                                 <asp:ListItem Value="3">连跳三段</asp:ListItem>
                                <asp:ListItem Value="4">连跳四段</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                        <ItemStyle Width="70px" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="添加">
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="添加" CommandName="E" OnClick="btnAdd_Click"/>
                        </ItemTemplate>
                          <FooterTemplate>
                         <asp:Button ID="btninsert" runat="server" Text="提交" CommandName="E" OnClick="btninsert_Click" />
                               <asp:Button ID="btn_insert_colse" runat="server" Text="取消" CommandName="E" OnClick="btn_insert_colse_Click"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                           <asp:ImageButton ID="imabtn_Del" runat="server" ImageUrl="../img/u=163316761,2398350077&fm=27&gp=0.jpg" OnClick="imabtn_Del_Click"  Width="25px"/>
                        </ItemTemplate>
                   
                    </asp:TemplateField>
                  
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <PagerSettings Visible="False" />
                <FooterStyle  Font-Bold="true"/>
                <HeaderStyle Font-Bold="False" Font-Italic="False" />
            </asp:GridView>
            <uc1:Pager runat="server" ID="Pager1"  />
        </div>
    </form>
</body>
</html>
