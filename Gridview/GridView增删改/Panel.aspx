<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Panel.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
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
                            Height="221px" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="用户ID" />
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
                        <asp:Button ID="Bt_Add" runat="server" Text="添加记录" OnClick="Bt_Add_Click"  />
                       <asp:Button ID="Bt_Refresh" runat="server" Text="刷新记录" OnClick="Bt_Refresh_Click"/>
                        
                        <br />
                        <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
                            Height="67px" Width="270px" Style="z-index: 101; left: 56px; position: absolute;
                            top: 90px">
                            <table>
                                <tr>
                                    <td colspan="2" style="background-color: Gray; text-align: center;">
                                        用户信息</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        用户ID:</td>
                                    <td style="width: 128px">
                                        <asp:TextBox ID="T_CID" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        用户姓名:</td>
                                    <td style="width: 128px">
                                        <asp:TextBox ID="T_Name" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        性别:</td>
                                    <td style="width: 128px">
                                        <asp:TextBox ID="T_Sex" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        家庭住址:</td>
                                    <td style="width: 128px">
                                        <asp:TextBox ID="T_Address" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        邮政编码:</td>
                                    <td style="width: 128px">
                                        <asp:TextBox ID="T_Post" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 115px">
                                        <asp:Button ID="Bt_Save" runat="server" Text="保存" OnClick="Bt_Save_Click" />
                                        <asp:Button ID="Bt_Cacel" runat="server" Text="取消" OnClick="Bt_Cacel_Click" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
