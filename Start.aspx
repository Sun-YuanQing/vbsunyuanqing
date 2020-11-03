<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="C_Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>无标题文档</title>
    <link href="css/Start.css" rel="stylesheet" />
    <script src="js/Start.js"></script>
    <style type ="text/css" >
         body{font-family:"宋体", Arial,Verdana, sans-serif, Helvetica;font-size:12px;margin:0;  padding :0px; background:#f4f5eb;color:#000;}
        .header{width:100%;height:53px;background:url(C/images/header_bei.gif);}
        .header01{float:left;height:53px;width:45px;margin-left:15px;background:url(images/header01.gif) no-repeat;}
        .header02{float:left;height:28px;width:auto;margin-left:10px;padding-top:25px;font-size:20px;font-family:"楷体";}
        .header03{float:right;height:53px;width:67px;margin-right:15px;background:url(c/images/logo.gif);}
        #LeftBox {background-image:url(图片素材/按钮样式/page-bg.jpg); float:left;width:15%;height:100%;
                  padding:0px 4px 0px 4px; margin:8px 0px 8px 0px; height:100%;
        }
        a { text-decoration :none ;
        }
        a:hover  {color :#fb08d8; 
        }
        .menuset {
        }
         .submenu {display: none ;
        }
        .m0 {padding:7px;display: block;border: 1px solid #97a8f7;  margin:5px 2px 5px 2px;
            background-image: url(图片素材/按钮样式/btn-20.png);   background-size:cover;  border-radius: 4px;
        }
        .m1 {padding:4px;display: block;  border-bottom:1px solid #555252;color:#e8dada;font-family:KaiTi;
        } 
        .m1:first-of-type {margin-top:-4px;
        }      
        .m1:last-of-type {margin-bottom:-4px;border-bottom:none;
        }
        .san_menu {display: none ;
        }
        .m2 {padding:4px;display: block;  border-bottom:1px solid #555252;color:#e8dada;font-family:KaiTi;
        } 
        .m2:first-of-type {margin-top:-4px;
        }      
        .m2:last-of-type {margin-bottom:-4px;border-bottom:none;
        }

        #Mobile {float:left ;width :1%;height:100%;
        }
        .center {background :no-repeat;position :fixed; margin-top:15%;
        }
        .right{width:auto;height:90%;margin-left:20px!important;margin-left:10px;border:1px solid #b4b4b4;background-position:center;}
        .rrcc{float :left ;border :1px solid #000;width :82%;height :100%;margin-top:8px;}
    </style>
</head>
    
<body>
      <form id="form1" runat="server">
<div class="header">
	<div class="header03"></div>
	<div class="header01"></div>
	<div class="header02">信息管理系统</div>
</div><div  style ="height :95%">
<div  id="LeftBox" >
   <div id="div_menu" runat ="server" ></div>
</div>
   <div  id="Mobile" style ="float:left ;  width :1%;height  :100%;">
   <img id="center" class="center"alt="图片" src="images/center0.gif" onclick="show_menuC()"   />
   </div>
<div id="RightBox" class="rrcc" >
     <iframe id="Iframe1" name="myFrameName" runat="server"  src="ListBox_Trreview.aspx"  style="width :100%; height:1100px; "></iframe>
</div>
   </div>
</form> 
</body>
     <script src="jquery.js"></script>
        <script type="text/javascript" >
            $(document).ready(function () {
                $(".m0").click(function () {
                    var id = $(this).attr("id");  //当前的id----attr("id")为id属性
                    var id0 = document.getElementById(id);//转换
                    var id1 = $(id0).next().attr("id");
                    var id2 = document.getElementById(id1);
                    var display = id2.currentStyle ? id2.currentStyle : document.defaultView.getComputedStyle(id2, null);//兼容
                    var display1 = (display.display);
                    if (display1 == "none") {
                        $("+.submenu", this).fadeIn(300);
                    } else {
                        $("+.submenu", this).fadeOut(300);
                    }
                });
                $(".m1").click(function () {
                    var id = $(this).attr("id");  //当前的id----attr("id")为id属性
                    var id0 = document.getElementById(id);//转换
                    var id1 = $(id0).next().attr("id");
                    var id2 = document.getElementById(id1);
                    var display = id2.currentStyle ? id2.currentStyle : document.defaultView.getComputedStyle(id2, null);//兼容
                    var display1 = (display.display);
                    if (display1 == "none") {
                        $("+.san_menu", this).fadeIn(300);
                    } else {
                        $("+.san_menu", this).fadeOut(300);
                    }
                });
            })
        </script>
</html>

