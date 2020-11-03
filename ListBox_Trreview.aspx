<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListBox_Trreview.aspx.cs" Inherits="C_ListBox_Trreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="_da" runat ="server" style ="width :800px;height:900px; margin-left :10%;float :left ; margin-top :10%;border:1PX solid #0094ff ;position :absolute ; z-index :4;">
            <div id="da1" runat ="server" style ="float:left ;position :absolute; z-index :5;border:1PX solid #0094ff ;height:900px;background-color  :blue ; "></div>
            <div id="da2" runat ="server" style ="float:left ;"></div>
        </div>
        <div style ="float :left ;width :200px;height :500px;">
            <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>
        </div>
        <div style ="float :left ">
            <asp:Button ID="Button1" runat="server" Text="查询aspx" OnClick="Button1_Click" /><br />
            <asp:Button ID="Button2"  runat="server" Text="以列表查看"   OnClick="Button2_Click  "/><br />
            <asp:Button ID="Button3"  runat="server" Text="上传数据"   OnClick="Button3_Click  "/>
        </div>
        <div style ="float :left">
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    <div style ="float :left ">
    <asp:ListBox ID="ListBox1" runat="server" style="width :400px;height :500px;" ></asp:ListBox>
    </div>
        
    </form>
</body>
</html>
