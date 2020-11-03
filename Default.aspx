<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!--
<iframe src ="top.aspx" style="height: 318px;"  >
      </iframe>-->
               <div>  <asp:Label ID="Label1" runat="server" Text="账 号"></asp:Label><asp:TextBox ID="txtbox_name"  runat="server" ></asp:TextBox></div>
        <div><asp:Label ID="Label2" runat="server" Text="密 码"></asp:Label><asp:TextBox ID="txtbox_pwd"  runat="server" ></asp:TextBox></div>
        <div><asp:Button  ID="btn_log" runat="server" text="登 陆"></asp:Button></div>
    </form>
    </body>
</html>
