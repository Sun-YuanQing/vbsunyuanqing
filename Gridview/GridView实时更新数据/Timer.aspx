<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timer.aspx.cs" Inherits="Gridview_GridView实时更新数据_Timer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>实时更新</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
<asp:UpdatePanel ID="UpdatePanel1" runat="server">  
    <ContentTemplate>  
        <asp:Timer ID="SystemTime" runat="server" Interval="500" OnTick="SystemTime_Tick"></asp:Timer>   
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </ContentTemplate>  
</asp:UpdatePanel>  
    </div>
    </form>
</body>
</html>
