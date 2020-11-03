<%@ Page Language="C#" AutoEventWireup="true" CodeFile="js width.aspx.cs" Inherits="main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="div_max" style  ="height :100%;width :100%;" >
               <div id="div_top" style ="height:60px;  ">
                   
               </div>
               <div id="div_da" style="float:left;height :100%;width :100% ;">
                   <div style="width:150px;height :400px;float :left ">

                   </div >
                   <div  class="main_page" style="width:100%;height :100%;float :left;border :1px solid ; " >
                        
     <iframe  scrolling="no" id="main" name="main" frameborder="0" 
          src="pcbainput.aspx" style="width:100%;height:900px">

     </iframe>  

                   </div>
                   <script>
                       // 计算页面的实际高度，iframe自适应会用到  
                       function calcPageHeight(doc) {
                           var cHeight = Math.max(doc.body.clientHeight, doc.documentElement.clientHeight)
                           var sHeight = Math.max(doc.body.scrollHeight, doc.documentElement.scrollHeight)
                           var height = Math.max(cHeight, sHeight)
                           return height
                       }
                       //根据ID获取iframe对象  
                       var ifr = document.getElementById('main')
                       ifr.onload = function () {
                           //解决打开高度太高的页面后再打开高度较小页面滚动条不收缩  
                           ifr.style.height = '0px';
                           var iDoc = ifr.contentDocument || ifr.document
                           var height = calcPageHeight(iDoc)
                           if (height < 850) {
                               height = 850;
                           }
                           ifr.style.height = height + 'px'
                       }
</script>  
               </div>
    </div>
    </form>
</body>
</html>
