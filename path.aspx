<%@ Page Language="VB" validateRequest="false" AutoEventWireup="false" CodeFile="path.aspx.vb" Inherits="path" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <script type="text/javascript">
        var img_now = 0;
        var img_cunt = 3;
        //每隔3秒滚动一次
        var t = setInterval("$('#hylnk').click()", 3000);
        </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
      <asp:HyperLink ID ="hylnk" runat="server" Text="上传格式下载" Target="_blank" >
               
           </asp:HyperLink>
        <asp:Image ID="Image1" runat="server"  ImageUrl="~/img/查看excel文档.png"/>
 </div>
    </form>
</body>
</html>
