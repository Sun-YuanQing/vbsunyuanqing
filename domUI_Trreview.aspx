<%@ Page Language="C#" AutoEventWireup="true" CodeFile="domUI_Trreview.aspx.cs" Inherits="C_ListBox_Trreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<script type="text/javascript" src="crossDomain.js"></script>
  <script  type="text/javascript">
      (function () {
          // 内部跨域iframe的代理页面
          var toProxyUrl = "http://localhost:1088/domUI_Trreview.aspx"; // 本地域（外层iframe)的代理地址
          var localProxyUrl = "http://localhost:9527/#/DomUI/DomUImian";
          // 本地页面注册用toSub，跨域页面注册用toMain
          var crossType = "toMain";
          //创建跨域对象
          var c = new crossDimain(toProxyUrl, localProxyUrl, crossType);
          // 注册跨域回调方法
          RFMGR.on("a2b", function (data) {
              alert("从B页面传过来的数据：" + data);
          });
      })();


      function sendMessage(data) {
          //跨域发送消息
          RFMGR.send("b2a", data);
      }
  </script>
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
