<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ExcelTo.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>                    
        <asp:Button ID="Button1" runat="server" Text="Excel导入到GridView" OnClick="Button1_Click" />
       <asp:GridView ID="GridView1" runat="server"  Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
        Height="221px" Width="100%" >
       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
       <EditRowStyle BackColor="#999999" />
       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <PagerSettings Visible="False" />
                            <FooterStyle Font-Bold="True" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" />
        </asp:GridView>       
        </div>
    </form>
</body>
</html>
