﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
            <table cellpadding="0" cellspacing="0" border="0" width="80%" style="font-size: 11px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server"  Height="221px" Width="500px"
                            AutoGenerateColumns="False" CellPadding="3" Font-Size="9pt" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                             OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                            OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="用户ID" ReadOnly="true">  
                                <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="用户姓名" >
                                <ItemStyle Width="50px" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="Sex" HeaderText="性别" >
                                <ItemStyle Width="50px" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="家庭住址" >
                                <ItemStyle Width="140px" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="Post" HeaderText="邮政编码" >
                                <ItemStyle Width="50px" />
                                 </asp:BoundField>
                                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                                <ItemStyle Width="70px" />
                            </asp:CommandField>
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
            </table>
        </div>
    </form>
</body>
</html>
