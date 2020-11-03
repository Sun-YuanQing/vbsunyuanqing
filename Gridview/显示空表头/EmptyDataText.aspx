<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmptyDataText.aspx.cs" Inherits="Gridview_显示空表头_EmptyDataText" %>

<%@ Register Src="~/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="6"
                AutoGenerateColumns="False" CellPadding="5" Font-Size="9pt" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
             >
              
                <Columns>
                    <asp:TemplateField HeaderText="一级IDm0_id"   >
                        <ItemTemplate >
                            <asp:Label ID="lbl__m0_id" runat="server" Text='<%# Eval("m0_id")%>'  Enabled="true" ></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                             <asp:Label ID="lbl_Footer__m0_id" runat="server" Text='<%# Eval("m0_id")%>'   Enabled="true" ></asp:Label>
                        </FooterTemplate>
                         <EditItemTemplate>
                             <asp:Label ID="lbl_EditItem__m0_id" runat="server" Text='<%# Eval("m0_id")%>'  Enabled="true" ></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="模块名称m0_ttl">
                            <ItemStyle Width="90px" />
                        <ItemTemplate>
                            <asp:Label ID="lbl_m0_ttl" runat="server" Text='<%# Eval("m0_ttl")%>'></asp:Label>
                        </ItemTemplate>
                            <FooterTemplate>
                            <asp:TextBox ID="txt_Footer__m0_ttl" runat="server" Width="90px"  ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_EditItem__m0_ttl" runat="server" Text='<%# Eval("m0_ttl")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="识别路径m0_Url">
                         <ItemStyle Width="90px" />
                        <ItemTemplate>
                            <asp:Label ID="lbl__m0_Url" runat="server" Text='<%# Eval("m0_Url") %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:TextBox ID="txt_Footer__m0_Url" runat="server" Width="90px"  ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_EditItem__m0_Url" runat="server" Text='<%# Eval("m0_Url")%>' Width="90px" ></asp:TextBox>
                        </EditItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预留字段m0_order">
                         <ItemStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lbl__m0_order" runat="server" Text='<%# Eval("m0_order")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_Footer__m0_order" runat="server" Width="100px" ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_EditItem__m0_order" runat="server"  Text='<%# Eval("m0_order")%>' Width="100px" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="预留字段m0_enable">
                         <ItemStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lbl__m0_enable" runat="server" Text='<%# Eval("m0_enable")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_Footer__m0_enable" runat="server" Width="100px" ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_EditItem__m0_enable" runat="server"  Text='<%# Eval("m0_enable")%>' Width="100px" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
 
                    <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                        <ItemStyle Width="70px" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="添加">
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="添加"  OnClick="btnAdd_Click"/>
                        </ItemTemplate>
                          <FooterTemplate>
                         <asp:Button ID="btn_insert" runat="server" Text="提交"  OnClick="btn_insert_Click" />
                               <asp:Button ID="btn_insert_colse" runat="server" Text="取消" OnClick="btn_insert_colse_Click"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                           <asp:ImageButton ID="imabtn_Del" runat="server" ImageUrl="../../img/u=163316761,2398350077&fm=27&gp=0.jpg"  Width="25px"/>
                        </ItemTemplate>
                   
                    </asp:TemplateField>
                  
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
              
                <FooterStyle  Font-Bold="true"/>
                <HeaderStyle Font-Bold="False" Font-Italic="False" />
            </asp:GridView>

    </div>
    </form>
</body>
</html>
