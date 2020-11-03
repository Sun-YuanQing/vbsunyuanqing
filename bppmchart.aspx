<%@ Page Language="VB" AutoEventWireup="false" CodeFile="bppmchart.aspx.vb" Inherits="bppmchart" %>
<%@ Register Assembly="DundasWebChart" Namespace ="Dundas.Charting.WebControl" TagPrefix ="DCWC" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
         <table id="Table1" style="Font-Size:9pt; color:#004382;" runat="server" bordercolor ="White"   width="800px" bordercolorlight="#cecece" >
          <tr><td>
               <asp:Button ID="Button1" runat="server" Text="Button" /></td>
              </tr>
             
              </table> 
    <div>
        <table id="Table" style="Font-Size:9pt; color:#004382;" runat="server" bordercolor ="White"   
            width="800px" bordercolorlight="#cecece" >
           <tr>
               <td>
                   <DCWC:Chart ID="Chart1" runat="server" width="800px" Height="500px"
                      BackColor ="PaleTurquoise" BackGradientEndColor ="White"
                       BackGradientType ="DiagonalLeft"  BorderLineColor ="Black"
                        Palette ="SeaGreen" ImageUrl ="a_#SEQ(1,3)">
                       <Series >
                           <DCWC:Series  Name ="Default" BorderColor ="64,64,64" XValueType ="Double"  YValueType ="Double" 
                                ChartType="pie" ShadowOffset ="1">
                           </DCWC:Series>
                       </Series>
                       <ChartAreas >
                           <DCWC:ChartArea  name="Default" BackColor ="Transparent" BorderColor ="" >
                           </DCWC:ChartArea>
                       </ChartAreas>
                       <Legends >
                           <DCWC:Legend  Alignment ="Center"  BackColor ="Transparent" BorderColor ="Black" 
                                Docking ="Top"  LegendStyle ="Row" Name="Default">
                           </DCWC:Legend>
                       </Legends>
                       <Titles >
                           <DCWC:Title   Font ="Microsoft Sans Serif,12pt,style=Bold" Name ="Titlel"
                               Text ="图形分析">
                             </DCWC:Title>
                          
                       </Titles>
                       <BorderSkin FrameBackColor ="MediumTurquoise" FrameBackGradientEndColor ="Teal" 
                            PageColor ="Control" SkinStyle ="Emboss"  />
                       <UI >
                           <Toolbar >
                               <BorderSkin  PageColor ="Transparent" SkinStyle ="Emboss" />
                           </Toolbar>
                       </UI>
                     </DCWC:Chart>
               </td>
           </tr>
            <tr><td><%=showth%>
                <asp:Chart ID="Chart2" runat="server">
                    <series>
                        <asp:Series Name="Series1" XValueMember="l_addll">
                        </asp:Series>
                    </series>
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </chartareas>
                    <Titles>
                        <asp:Title Name="Title1" Text="德国韩国的风格和">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
                </td></tr>
        </table>
    
    </div>
   
            
        
        </div> 
                      
                
    </form>
</body>
</html>
