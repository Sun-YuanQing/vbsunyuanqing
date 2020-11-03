<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel_export.aspx.cs" Inherits="Excel_export" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <div style="float:left ; ">
            <asp:Label ID="Label1" runat="server" Text="开始日期："></asp:Label>
          <asp:TextBox ID="txb_tim1" runat ="server" class="sang_Calender"  ></asp:TextBox><br />
            <asp:Label ID="Label2" runat="server" Text="截至日期："></asp:Label>
          <asp:TextBox ID="txb_tim2" runat ="server"  ></asp:TextBox><br />
            <asp:Label ID="Label3" runat="server" Text="文件编号："></asp:Label>
          <asp:TextBox ID="txb_fno" runat ="server"  ></asp:TextBox>
            </div>
            <div style ="float :left ;">
            <asp:Label ID="Label4" runat="server" Text="部门："></asp:Label>
          <asp:TextBox ID="txb_bm" runat ="server"  ></asp:TextBox><br />
            <asp:Label ID="Label5" runat="server" Text="分部："></asp:Label>
          <asp:TextBox ID="txb_fb" runat ="server"  ></asp:TextBox><br />
            <asp:Label ID="Label6" runat="server" Text="分组："></asp:Label>
          <asp:TextBox ID="txb_fz" runat ="server"  ></asp:TextBox>
            </div>
            <div style ="float:left;">
            <asp:Label ID="Label7" runat="server" Text="姓名："></asp:Label>
          <asp:TextBox ID="txb_xm" runat ="server"  ></asp:TextBox><br />
            <asp:Label ID="Label8" runat="server" Text="职位："></asp:Label>
          <asp:TextBox ID="txb_zw" runat ="server"  ></asp:TextBox><br />
            <asp:Label ID="Label10" runat="server" Text="邮箱："></asp:Label>
          <asp:TextBox ID="txb_yx" runat ="server"  ></asp:TextBox>
           </div>
            <div style ="float :left ;">
            <asp:Label ID="Label9" runat="server" Text="分机号："></asp:Label>
          <asp:TextBox ID="txb_fjh" runat ="server"  ></asp:TextBox>
            <br />
              <asp:Button ID="btn_ck" runat="server" Text="查 看" OnClick="btn_ck_Click" />
                <asp:Button ID="btn_dc" runat="server" Text="导 出" OnClick="btn_dc_Click" />
            </div>
            </div>
        
            <script type="text/javascript" src="datetime.js"></script>
        <div style ="float:none; clear :both ;"></div>
    <div style ="float:none;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns ="False">
            <Columns>
                <asp:BoundField DataField="F2" HeaderText="部门" SortExpression="F2" />
                <asp:BoundField DataField="F3" HeaderText="分部" SortExpression="F3" />
                <asp:BoundField DataField="F4" HeaderText="分组" SortExpression="F4" />
                <asp:BoundField DataField="F5" HeaderText="姓名" SortExpression="F5" />
                <asp:BoundField DataField="F6" HeaderText="职位" SortExpression="F6" />
                <asp:BoundField DataField="F7" HeaderText="手机" SortExpression="F7" />
                <asp:BoundField DataField="F8" HeaderText="QQ" SortExpression="F8" />
                <asp:BoundField DataField="F9" HeaderText="分机号" SortExpression="F10" />
                <asp:BoundField DataField="F10" HeaderText="短号" SortExpression="F10" />
                <asp:BoundField DataField="F11" HeaderText="邮箱" SortExpression="F11" />
                 <asp:BoundField DataField="F13" HeaderText="文件编号" SortExpression="F13" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
