<%@ Page Language="C#" AutoEventWireup="true" CodeFile="饼图.aspx.cs" Inherits="数据统计图_饼图" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Chart ID="Bingtu_Chart" runat="server" Height="287px" Width="530px">
            <Series>
                <asp:Series Name="Series1" ChartType="Pie" Legend="Legend1" IsValueShownAsLabel="True" Label="#VALX:#VAL"></asp:Series>
            </Series>
         
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Enabled="False" Name="Legend1">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
