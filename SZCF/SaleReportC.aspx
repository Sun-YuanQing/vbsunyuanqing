<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleReportC.aspx.cs" Inherits="Sales_SaleReportC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/jscript">
        var flag = true;

        function chkRadio(id) {

            id.checked = flag;

            flag = !flag;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" submitdisabledcontrols="True" v>
    <div>
        <asp:Label ID="Label3" runat="server" Text="地 区:"></asp:Label>
        <asp:DropDownList ID="ddl_address" runat="server" Height="16px" Width="97px" OnTextChanged="ddl_address_TextChanged1"
            AutoPostBack="True" AppendDataBoundItems="True">
            <asp:ListItem>SZCFERP</asp:ListItem>
            <asp:ListItem>GCERP</asp:ListItem>
        </asp:DropDownList>
          <asp:Label ID="Label1" runat="server" Text="币种:"></asp:Label>
              <asp:DropDownList ID="ddl_bz" runat="server" Height="18px" Width="120px" 
            AppendDataBoundItems="True"  Style="" AutoPostBack="True" 
            ontextchanged="ddl_bz_TextChanged">
            
            <asp:ListItem>RMB</asp:ListItem>
             <asp:ListItem>USD</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="销售客户:"></asp:Label>
        <asp:DropDownList ID="ddl_client" runat="server" Height="18px" Width="120px" 
            AppendDataBoundItems="True" Style="" AutoPostBack="True" 
            ontextchanged="ddl_client_SelectedIndexChanged">
            <asp:ListItem>请选择</asp:ListItem>
        </asp:DropDownList>
        
        
       
        <asp:RadioButton ID="rdbtn_ydxs"  ValidationGroup="B"  
            GroupName="B"  runat="server" 
            Text="月度销售报告" Checked="True" />
        <asp:RadioButton ID="rdbtn_jdzhbg"   ValidationGroup="B"  
            GroupName="B"  runat="server" 
            Text="季度综合报告" />
       
        <asp:CheckBox ID="Check_syntheses" runat="server" Text="数据视图" 
            />
            
    </div>

    <div    id="div_year1" style="width: 25px; height: 25px; background-color: #0ff; float: left;">
    </div>
    <div style="float: left;">
        <asp:TextBox ID="txt_year1" runat="server" Width="59px" Text="2017" Height="22px"></asp:TextBox>
        
        <asp:Label ID="lbl_year1_vs_year2" runat="server" Text="年对比年"></asp:Label>
    </div>
    <div  id="div_year2" style="width: 25px; height: 25px; background-color: #f00; float: left;">
    </div>
    <div style=" float: left;">
        <asp:TextBox ID="txt_year2" runat="server" Width="63px" Text="2018" Height="24px"></asp:TextBox>
  
    </div>
    
    
    
   <div   style =" clear:both" >
    
   
          <asp:DropDownList ID="ddl_month1" runat="server" Height="25px" Width="44px" AutoPostBack="True">
            <asp:ListItem Selected="True">1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lbl_month1_vs_month2" runat="server" Text="月至月"></asp:Label>
        <asp:DropDownList ID="ddl_month2" runat="server" Height="25px" Width="58px" >
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem Selected="True">12</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btn_query" runat="server" Text="对比结果" OnClick="btn_query_Click" />
        <asp:Button ID="btn_export" runat="server" Text="导出当前数据" OnClick="btn_exprot_Click" />
    
     </div>
    
    <div id="div_gv_vs" runat="server">
        <asp:GridView ID="gv_vs" runat="server">
        </asp:GridView>
    </div>
 
    <div   id="div_columnar" runat="server"   style="margin: 0px; padding: 0px; border: 1px solid  #f00">
        <asp:Chart ID="Chart_columnar" runat="server" Height="416px" Width="873px " Style="">
            <Series>
                <asp:Series Name="Series1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div   id="div_bingtu" runat="server">
        <div id="div_Bingtu1" runat="server" style="width: 50%; float: left;">
            <asp:Chart ID="Chart_Bingtu1" runat="server" Height="287px" Width="530px">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie" Legend="Legend1" IsValueShownAsLabel="True"
                        Label="#VALX:#VAL">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Enabled="False" Name="Legend1">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
        </div>
        <div id="div_Bingtu2"  runat="server" style="float: left; width: 49%;">
            <asp:Chart ID="Chart_Bingtu2" runat="server" Height="287px" Width="530px">
                <Series>
                    <asp:Series Name="Series2" ChartType="Pie" Legend="Legend2" IsValueShownAsLabel="True"
                        Label="#VALX:#VAL">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea2">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Enabled="False" Name="Legend2">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
        </div>
    </div>
    </form>
</body>
</html>
