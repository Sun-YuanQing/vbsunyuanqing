<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Asc.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
                            Height="221px" Width="100%" OnSorting="GridView1_Sorting" BackImageUrl="~/Img/060528205025893.jpg" >
                            <Columns>
                             
                                <asp:BoundField DataField="CID" HeaderText="用户ID" SortExpression="CID"/>                             
                                <asp:BoundField DataField="Name" HeaderText="用户姓名" SortExpression="Name"/>
                                <asp:BoundField DataField="Sex" HeaderText="性别"  SortExpression="Sex"/>
                                <asp:BoundField DataField="Address" HeaderText="家庭住址" SortExpression="Address" />
                                <asp:BoundField DataField="Post" HeaderText="邮政编码"  SortExpression="Post"/>
                            </Columns>
                           
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
