<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Image.aspx.cs" Inherits="_Default" %>

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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="3" AllowSorting="True"
                            AutoGenerateColumns="False" HeaderStyle-VerticalAlign="Middle" CellPadding="3"
                            Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            Height="221px" Width="100%">
                            <Columns>
                              <asp:BoundField DataField="ImageID" HeaderText="编号" />
                                <asp:BoundField DataField="ImageName" HeaderText="用户名称" />
                                <asp:TemplateField HeaderText="头像">                               
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" Height="80" Width="100"  runat="server" ImageUrl='<%#"~/images/"+ Eval("Image")  %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="上传图片" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"  Width="80px" Height="50px"
               Style="z-index: 101; left: 56px; position: absolute; top: 90px">
                <table>
                    <tr>
                        <td style="width: 150px" colspan="2">
                            <asp:FileUpload ID="FU_image" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px">
                            图片名称:</td>
                        <td>
                            <asp:TextBox ID="T_image" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 70px">
                        </td>
                        <td style="width: 115px">
                            <asp:Button ID="Bt_Save" runat="server" Text="保存" OnClick="Bt_Save_Click" />
                            <asp:Button ID="Bt_Cacel" runat="server" Text="取消" OnClick="Bt_Cacel_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
