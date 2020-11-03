<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Thread.aspx.cs" Inherits="_26_4线程操作之线程休眠_Thread" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divshow" runat="server">
                <div id="div_menu" runat="server"></div>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
