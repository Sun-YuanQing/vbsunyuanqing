<%@ Page Language="C#" AutoEventWireup="true" CodeFile="domUI_Trreview.aspx.cs" Inherits="C_ListBox_Trreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     
        <div style ="float :left ;width :200px;height :500px;">
            <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>
        </div>
        <div style ="float :left ">
            <asp:Button ID="Button2"  runat="server" Text="以列表查看"   OnClick="Button2_Click  "/><br />
            <asp:Button ID="Button3"  runat="server" Text="上传数据"   OnClick="Button3_Click  "/>
        </div>
        <div style ="float :left">
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
  
        
    </form>
</body>
</html>
