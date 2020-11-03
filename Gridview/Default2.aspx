<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
    
    <style type="text/css">
/* Reset style */
* { margin:0; padding:0; word-break:break-all; }
    body { background:#FFF; color:#333; font:12px/1.6em Helvetica, Arial, sans-serif; }
    h1, h2, h3, h4, h5, h6 { font-size:1em; }
    a { color:#1F376D; text-decoration:none; }
    a:hover { color:#BD0A01; text-decoration:underline; }
    ul, li { list-style:none; }
    fieldset, img { border:none; }
    /* Board style */
    .board { width:409px; margin:50px; }
    .board_caption { height:27px; background:url(/jscss/demoimg/200903/tabsbg.gif) no-repeat 0 0; }
    .board_caption h3 { float:left; width:73px; height:22px; height:20px; padding:3px 0 4px; margin-right:1px; background:url(/jscss/demoimg/200903/tabsbg.gif) no-repeat; text-align:center; line-height:20px; font-weight:normal; cursor:pointer; }
    .board_caption h3 a { display:block; width:100%; }
    .board_caption .normal { background-position:-100px -50px; }
    .board_caption .current { background-position:0 -50px; font-weight:bold; }
    .board_content { padding:0 0 5px; border-right:1px solid #D4D4D4; border-bottom:1px solid #D4D4D4; border-left:1px solid #D4D4D4;}
    .board_content .normal { display:none; }
    .board_content .current { display:block; }
    .board_content ul { width:98%; overflow:hidden; padding:5px 0 0 5px; }
    .board_content ul li { display:inline; float:left; width:180px; height:24px; overflow:hidden; margin-left:6px; padding-left:8px; background:#FFF url(/jscss/demoimg/200903/tabsbg.gif) no-repeat 0 -88px; line-height:24px; }
    .board_content ul li a { font-size:14px; }
</style>
 <script type="text/javascript" language="javascript">
     function secBoard(elementID,listName,n) {
             var elem = document.getElementById(elementID);
             var elemlist = elem.getElementsByTagName("h3");
             for (var i=0; i<elemlist.length; i++) {
                 elemlist[i].className = "normal";
                 var m = i+1;
                 document.getElementById(listName+"_"+m).className = "normal";
                                                    }
                 elemlist[n-1].className = "current";
                 document.getElementById(listName+"_"+n).className = "current";
                                            }
</script>  
</head>
<body>
    <form id="form1" runat="server">
   <div class="board">
<div class="board_caption" id="Div1">
<h3 class="current"><a href="http://codefans.net" target="_blank" onmousemove="secBoard('hotinfo_caption','infolist',1);">Դ��</a></h3>
<h3 class="normal"><a href="http://codefans.net" target="_blank" onmousemove="secBoard('hotinfo_caption','infolist',2);">�Ƽ�</a></h3>
<h3 class="normal"><a href="http://codefans.net" target="_blank" onmousemove="secBoard('hotinfo_caption','infolist',3);">���</a></h3>
</div>
<div class="board_content">
<div class="current" id="Div2">
<ul>
<li><a href="/soft/3628.shtml" target="_blank">VBԶ��������ӡ��ļ����䡢��Ϣ����ʵ������</a></li><li><a href="/soft/3627.shtml" target="_blank">һ�ǿ��VB���繤����Դ����</a></li><li><a href="/soft/3626.shtml" target="_blank">��Դָ����ϰ�������VB��</a></li><li><a href="/soft/1115.shtml" target="_blank">Magento��Դ���� v1.2</a></li>
</ul>
</div>
<div class="normal" id="Div3">
<ul>
<li><a href="/soft/3621.shtml" target="_blank">ASP FSO�����ļ������ϴ����༭ϵͳ</a></li><li><a href="/soft/3614.shtml" target="_blank">VB����������ʾ�ؼ�</a></li><li><a href="/soft/3603.shtml" target="_blank">һ��Java�������ϷԴ��</a></li><li><a href="/soft/3617.shtml" target="_blank">���ش�Access���ݿ����߹����� v9.0</a></li>
</ul>
</div>
<div class="normal" id="Div4">
<ul>
<li><a href="/soft/3611.shtml" target="_blank">VB6д��MSSQL���ݿ������</a></li><li><a href="/soft/3609.shtml" target="_blank">osFileManager PHP��վ�ļ�����ϵͳ v2.2</a></li><li><a href="/soft/1035.shtml" target="_blank">56770 Eshop �ֱ����Ͽ���ϵͳ v8.4</a></li><li><a href="/soft/3601.shtml" target="_blank">JAVAʵ��CLDC��MIDP�ײ��̵Ĵ���</a></li>
</ul>
</div>
</div>
</div><!--Demo1 end-->
    </form>
</body>
</html>
