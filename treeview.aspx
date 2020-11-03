 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="treeview.aspx.cs" Inherits="treeview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type ="text/css" >
        .lv1 {
        font-size :18px;color :red ;
        }
        .lv2 {
        
        font-size :19px;color :green ;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style ="width :200px;float :left ;">
    
<asp:treeview ID="Treeview1" runat="server" ShowCheckBoxes="All" ImageSet="Arrows" >
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <LevelStyles>
        <asp:TreeNodeStyle CssClass="lv1" Font-Underline="False" />
        <asp:TreeNodeStyle CssClass="lv2" Font-Underline="False" />
    </LevelStyles>
    <Nodes>
        <asp:TreeNode Text="A" Value="AV">
            <asp:TreeNode Text="A1" Value="A1V"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="B" Value="BV">
            <asp:TreeNode Text="B1" Value="B1V">
                <asp:TreeNode Text="B2" Value="B2V"></asp:TreeNode>
            </asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="C" Value="CV">
            <asp:TreeNode Text="C1" Value="C1V">
                <asp:TreeNode Text="C2" Value="C2V">
                    <asp:TreeNode Text="C3" Value="C3V"></asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="D" Value="DV"></asp:TreeNode>
    </Nodes>
        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
    <ParentNodeStyle Font-Bold="False" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:treeview>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="显示路径" />
        <asp:Button ID="Button2" runat="server" Text="在C3增加E" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="打勾的路径" OnClick="Button3_Click" />

    </div>

        <div style="float:left; border :1px solid #808080 ;width :500px;height :500px;">
                    <asp:Button ID="Button4" runat="server" Text="Button" OnClick="Button4_Click" />
            <asp:ListBox ID="ListBox1" runat="server" style=" Width :500px; height :600px;" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">

            </asp:ListBox>

        </div>
    </form>
</body>
</html>
