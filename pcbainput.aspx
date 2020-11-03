<%@ Page Language="VB" validateRequest="false" AutoEventWireup="false" CodeFile="pcbainput.aspx.vb" Inherits="pcbainput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="js/jquery-1.8.3.js"></script>
    <script src="js/jqueryColor.js"></script>
            <title>前端导入</title>
       <style type="text/css">
           .auto-style1 {
               height: 128px;
           }
           .auto-style2 {
               height: 23px;
           }
       </style>
       </head>
    <body style=" height :100%;width :100%;">
       <form id="form1" runat="server" style=" height :100%;width :100%;">
           
           <div id="div_max"  style="float:left  ; height :100%;width :100%; " >
               
                               <div id="div_caidan">
           <asp:FileUpload ID="File1" runat="server" />
               <asp:ImageButton ID="imgbtnsearch"  Width="21px" height="21px"  imageurl="~/img/btndl_add.png" runat ="server"  style="margin:0px;" />
          <asp:Button ID="btn_server1" runat="server" Text="直接上传" style="height: 21px" />
                <asp:HyperLink ID ="hylnk" runat="server" Target="_blank" NavigateUrl="PC-201711191.xls">
               上传格式下载
           </asp:HyperLink><asp:Label  ID ="labe1_path" runat="server">----文件路径</asp:Label>
                  <asp:Button ID="btn_daochu" runat="server" Text="查看导入的数据VB" OnClick="btn_daochu_Click" />
                 
          </div>
           <div >
             <table id="Table1"     style ="font-size:9pt;color:#004382; "  cellspacing="0" bordercolordark="white"
            cellpadding="0" width="100%"  bordercolorlight="#cecece" border="1" runat ="server">
                       <tr>
                <td colspan="8" class="auto-style1">
                    <asp:DataGrid ID ="DataGrid1" runat="server"  Style="word-wrap:break-word" CssClass="" showHeader="false">
                        <AlternatingItemStyle backColor="#f2eded"></AlternatingItemStyle >
                        <ItemStyle HorizontalAlign="Center" Height="24px"></ItemStyle>
                        <HeaderStyle CssClass=""  Height="30px"></HeaderStyle>
                    </asp:DataGrid>
                </td>
       </tr>
        <tr>
            <td class="auto-style2"><asp:Button ID="btn_server" runat="server" Text="上 传" style="height: 21px" />
             </td>
        </tr>
    </table>
               </div>
           <div>
    <table id="Table2" style="font-size:9pt;color:#004382;" runat ="server" >
        <tr><td >
            <label>上传日期起：</label><asp:TextBox id="itme_1" runat ="server" class="sang_Calender" ></asp:TextBox>
</td>
            <td>  <label  >上传日期止：</label><asp:TextBox id="itme_2" runat ="server" class="sang_Calender" ></asp:TextBox></td>
           <td ><label  >上传人员：</label><asp:TextBox id="txtb_user" runat ="server" ></asp:TextBox></td>
            <td ><label >文件编号：</label><asp:TextBox  ID ="excel_id" runat ="server" /></td>
            <td><asp:Button id="btn_chaxun" runat="server" Text ="查 询" BorderColor="#9933FF" /></td>
        </tr>
       <tr ><td colspan ="6">
           <script type="text/javascript" src="datetime.js"></script>

        <asp:DataGrid  ID="DataGrid2" runat="server" Font-Size ="9pt" Width ="1000px" CssClass ="datagridfont"
             OnItemDataBound="DataGrid2_ItemDataBound"     PagerStyle-Mode ="NumericPages"
             PagerStyle-HorizontalAlign ="Right" 
             BorderWidth ="1px" CellPadding ="2" Font-Names ="Verdana" AlternatingItemStyle-BackColor ="#EEEEEE"
             AutoGenerateColumns ="false" >
<AlternatingItemStyle  BackColor ="#f2eded" />
           <ItemStyle  HorizontalAlign ="Center" />
            <HeaderStyle  CssClass ="datatitle" Height="30px" />
            <Columns >
          <asp:BoundColumn  DataField ="id" HeaderText ="序 号">
              <HeaderStyle  HorizontalAlign ="Center" CssClass ="MennBordern" />
           </asp:BoundColumn>
          <asp:HyperLinkColumn  Target ="_blank" DataNavigateUrlField ="paths"
                     DataNavigateUrlFormatString ="path.aspx?path={0}"
                     DataTextField ="fno" HeaderText ="查看文件">
           <HeaderStyle  HorizontalAlign ="Center" CssClass ="MennBordern"/>
           </asp:HyperLinkColumn>
             <asp:BoundColumn  DataField ="name" HeaderText ="文件名称">
              <HeaderStyle  HorizontalAlign ="Center" CssClass ="MennBordern" />
           </asp:BoundColumn> 
                <asp:BoundColumn  DataField ="times" HeaderText ="上传时间">
              <HeaderStyle  HorizontalAlign ="Center" CssClass ="MennBordern" />
           </asp:BoundColumn>
                <asp:BoundColumn  DataField ="usernames" HeaderText ="上传人员">
              <HeaderStyle  HorizontalAlign ="Center" CssClass ="MennBordern" />
           </asp:BoundColumn>
                <asp:HyperLinkColumn  Target ="_blank"
                     DataNavigateUrlField ="name"
                     DataNavigateUrlFormatString ="att_del.aspx?attid={0}"
                     DataTextField ="fno" HeaderText ="删除文件">
                </asp:HyperLinkColumn>
            </Columns>
            
        </asp:DataGrid>

       </td></tr>
       </table>
               </div>
                   </div>
         
    </form>
</body>
</html>
