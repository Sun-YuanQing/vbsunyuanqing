<%@ Page Language="C#" AutoEventWireup="true" CodeFile="insert.aspx.cs" Inherits="insert" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" width="300px" style="font-size: 11px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server"  PageSize="5" AllowSorting="True"
                            AutoGenerateColumns="False" HeaderStyle-VerticalAlign="Middle" CellPadding="4"
                            Height="221px" Font-Size="9pt" ForeColor="#333333" GridLines="None" >
                            <Columns>
                                <asp:TemplateField HeaderText="用户ID">
                                <ControlStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Lb_ID" runat="Server" Text='<%# Bind("CID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_ID" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="姓名">
                                <ControlStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Lb_Name" runat="Server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="性别">
                                    <ItemTemplate>
                                        <asp:Label ID="Lb_Sex" runat="Server" Text='<%# Bind("Sex") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="Ddl_Sex" runat="server">
                                            <asp:ListItem Value="男">男</asp:ListItem>
                                            <asp:ListItem Value="女">女</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="家庭住址">
                                 <ControlStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Lb_Address" runat="Server" Text='<%# Bind("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Address" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnAdd" runat="server" Text="添 加"  OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="取 消"  OnClick="btnCancel_Click"/>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />
                            <PagerSettings Visible="False" />
                            <FooterStyle Font-Bold="True" BackColor="#507CD1" ForeColor="White" />
                            <HeaderStyle Font-Bold="True" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr><td style="height: 24px">
                    <asp:Button ID="showAdd" runat="server" Text="添加记录" Width="200px" OnClick="showAdd_Click" /></td></tr>
            </table>
        </div>
    </form>
</body>
</html>
