﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataSet.aspx.cs" Inherits="Data_DataSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../jquery.js"></script>
 
    <script type="text/javascript">
        function SyDG_moveOnTd(td) {
            if (event.offsetX > td.offsetWidth - 10)
                td.style.cursor = 'w-resize';
            else
                td.style.cursor = 'default';
            if (td.mouseDown != null && td.mouseDown == true) {
                if (td.oldWidth + (event.x - td.oldX) > 0)
                    td.width = td.oldWidth + (event.x - td.oldX);
                td.style.width = td.width;
                td.style.cursor = 'w-resize';

                table = td;
                while (table.tagName != 'TABLE') table = table.parentElement;
                table.width = td.tableWidth + (td.offsetWidth - td.oldWidth); table.style.width = table.width;
            }
        }
        function SyDG_downOnTd(td) {
            if (event.offsetX > td.offsetWidth - 10) {
                td.mouseDown = true;
                td.oldX = event.x;
                td.oldWidth = td.offsetWidth;
                table = td; while (table.tagName != 'TABLE') table = table.parentElement;
                td.tableWidth = table.offsetWidth;
            }
        }
        $(document).ready(function () {
            $("#Button1").mouseenter(function () {
                $("#_div_1_1_content").fadeIn(100);
            });
            $("#_div_1_1_mouseleave").mouseleave(function () {
                $("#_div_1_1_content").fadeOut(100);
            });
            $("#Button2").mouseenter(function () {
                $("#_div_1_2_content_0").fadeIn(100);
                $("#_div_1_2_content_1").fadeIn(100);
           
                $("#_div_1_2_content_2").fadeIn(100);
                document.getElementById('#_div_1_2_mouseleave').style.width = '100%';
            });
            $("#_div_1_2_mouseleave").mouseleave(function () {
                $("#_div_1_2_content_0").fadeOut(100);
                $("#_div_1_2_content_1").fadeOut(100);
                $("#_div_1_2_content_2").fadeOut(100);
            });
            $("#Button3").mouseenter(function () {
                $("#_div_2_1_content").fadeIn(100);
            });
            $("#_div_2_1_mouseleave").mouseleave(function () {
                $("#_div_2_1_content").fadeOut(100);
            });
            $("#Button4").mouseenter(function () {
                $("#_div_2_2_content").fadeIn(200);
            });
            $("#_div_2_2_mouseleave").mouseleave(function () {
                $("#_div_2_2_content").fadeOut(200);
            });
            var temp = 0;
 function show_menuC(){
                if (temp == 1) {
                    document.getElementById('#_div_1_2_content_1').style.width = '50%';
                    document.getElementById('#_div_1_2_content_2').style.display = 'block';
                    temp = 1;
                }
            }
        });
    </script>
    <style type ="text/css" >
      
    </style>
</head>
    
<body>
    <form id="form1" runat="server">
    <div id="_div_0" runat="server" style ="width:100%;border :1px solid #ff6a00 ; ">
        <asp:Label ID="Label1" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    </div>    
    <div id="_div_1" style ="width:100%;border :1px solid #ff6a00 ; ">
        <div id ="_div_1_1" style ="width:50%;border :1px solid #ff6a00 ;float:left;">
        <div id ="_div_1_1_mouseleave" >
                          <asp:Button  ID="Button1" runat="server" Text="SqlDataAdapter(sql,conn)_ds_dt" OnClick="SqlDataAdapter_Click"     />
                  <div id="_div_1_1_content" style="display :none ;background-color :#00ff90" class="more111" >
                    protected void SqlDataAdapter_Click(object sender, EventArgs e)<br />
    {<br />
        String _str_conn = "server=.;Database=MySchool;uid=sa;pwd=2222;";<br />
        String _str_sql = "select * from treeview  where er_id like '%3%'";<br />
        SqlConnection conn = new SqlConnection(_str_conn);<br />
        SqlDataAdapter da = new SqlDataAdapter(_str_sql, conn);<br />
        DataSet ds = new DataSet();<br />
        DataTable dt = new DataTable();<br />
        da.Fill(dt);<br />
        //da.Fill(ds);<br />
        //GridView1.DataSource = ds;<br />
        GridView1.DataSource = dt;<br />
        GridView1.DataBind();<br />
    }<br />
                </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" ViewStateMode="Enabled" SelectedRowStyle-BackColor="#9900CC" OnRowDataBound="gvdegreetype_RowDataBound" OnRowCreated="GridView1_RowCreated">      
        </asp:GridView>
        </div>
         <div runat ="server"  id ="div_1_2" style ="width :49%;border:1px solid #ffd800 ; float:left;">

           <div  id="_div_1_2_mouseleave" style=" width :100%;float :left ;">
             
              <div><asp:Button ID  ="Button2" runat="server" Text="SqlDataAdapter.SelectCommand=comm;"
                     OnClick="SqlDataAdapter_comm_Click"  /></div> 

      <div id="_div_1_2_content_0" style ="height :200px ; display :none ;position :absolute ;margin-left :-400px;">
      <div id="_div_1_2_content_1" style ="width :100%;float :left ;display :none ;background-color :#00ff90;" >
       protected void SqlDataAdapter_comm_Click(object sender, EventArgs e)<br />
    {<br />
    String _str_select = " * ";<br />
    String _str_tabel = " treeview ";<br />
    String _str_where = "  yi_id like '%04%'";<br />
    String _str_conn = "server=.;Database=myschool;uid=sa;pwd=2222;";<br />
    SqlConnection conn = new SqlConnection(_str_conn );<br />
    SqlCommand comm = new SqlCommand("sp_select",conn);<br />
    comm.CommandType = CommandType.StoredProcedure;<br />
    comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 200).Value = _str_select;<br />
    comm.Parameters.Add("@_comm_tabel", SqlDbType.NVarChar, 200).Value = _str_tabel;<br />
    comm.Parameters.Add("@_comm_where", SqlDbType.NVarChar, 200).Value = _str_where;<br />
    SqlDataAdapter sda=new SqlDataAdapter ();<br />
    sda.SelectCommand = comm;<br />
    DataSet ds = new DataSet();<br />
    DataTable dt = new DataTable();<br />
    sda.Fill(ds);<br />
    sda.Fill(dt);<br />
    GridView2.DataSource = ds;<br />
    GridView2.DataBind();<br />
    }<br />
              </div><div id="_div_1_2_content_2" style ="width :100%;float:left ;
                display :none ;position :absolute ;margin-left :350px;margin-top :215px; background-color :#ffd800;">
                 use myschool<br />
go<br />
create proc sp_select<br />
@_comm_select nvarchar(max),<br />
@_comm_tabel nvarchar(300),<br />
@_comm_where nvarchar(max)<br />
as<br />
declare @_comm_int int<br />
exec ('select' + @_comm_select + 'from'+ @_comm_tabel+' where '+ @_comm_where )<br />
              </div>
                </div> 
             </div>
            <div >
             <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView1_RowDataBound">
             </asp:GridView>
                </div>
        </div>
      </div> 

        

    <div id ="_div_2" style="width :100%; border :1px solid #ff6a00 ;width:100%;" >
            <div id="_div_2_1" style="width:50%;float:left;">
            <div  id ="_div_2_1_mouseleave">
                <asp:Button id="Button3" runat ="server"  Text ="SqlDataReader_comm(sql,conn)_SqlDataReader"
                    OnClick="SqlDataReader_Click"/>
            <div id="_div_2_1_content" style="display:none;">
                    
protected void SqlDataReader_Click(object sender, EventArgs e)<br />
{<br />
    String _str_conn = "server=.;Database=Myschool;uid=sa;pwd=2222;";<br />
    String _str_sql = "select * from treeview where yi_id like '%04%'";<br />
    SqlConnection conn = new SqlConnection(_str_conn);<br />
    SqlCommand comm = new SqlCommand(_str_sql ,conn);<br />
    SqlDataReader reader = null;<br />
    conn.Open();<br />
    reader = comm.ExecuteReader();<br />
        GridView2.DataSource = reader;<br />
        GridView2.DataBind();<br />
       conn.Close();<br />
}<br />
                </div>
            </div>
            <div>
                <asp:GridView runat="server" id="GridView3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="3" OnSelectedIndexChanging="GridView3_SelectedIndexChanging"
                 >
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="Blue" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
              
        </div>
              <div id ="_div_2_2" runat="server" style="float:left;width:49%;">
                  <div id="_div_2_2_mouseleave"  runat="server">
                      <asp:Button  ID="Button4" runat="server" Text="SalDataReader_comm(sql,comm)_ExecuteScalar()" OnClick="SqlDataReader_comm_Click" />
                      <div id ="_div_2_2_content" runat="server" style="display:none;">
                              String _str_conn = "server=.;Database=Myschool;uid=sa;pwd=2222;";<br/>
    String _str_select = " * ";<br/>
    String _str_tabel = " t_menuL1 ";<br/>
    String _str_where = " m1_id like '3' ";<br/>
    SqlConnection conn = new SqlConnection(_str_conn);<br />
    SqlCommand comm = new SqlCommand("sp_select",DBHelper.Conn);<br />
    comm.CommandType = CommandType.StoredProcedure;<br />
    comm.Parameters.Add("@_comm_select", SqlDbType.NVarChar, 300).Value = _str_select;<br />
    comm.Parameters.Add("@_comm_tabel",SqlDbType.NVarChar,300).Value = _str_tabel;<br />
    comm.Parameters.Add("@_comm_where",SqlDbType.NVarChar,300).Value = _str_where;<br />
    SqlDataReader sdr = null;<br />
    DBHelper.OpenConnection();<br />
    sdr = comm.ExecuteReader();<br />
    GridView4.DataSource = sdr;<br />
    GridView4.DataBind();<br />
    DBHelper.CloseConnection();<br />
                      </div>
                  </div>
                  <div>
                       <asp:GridView runat="server" id="GridView4">
                </asp:GridView>
                  </div>
                </div>
     </div>

    <div id ="_div_3" style="width :100%; border :1px solid #ff6a00 ;width:100%;" >
       <div id="_div_3_1" style="width:50%;float:left;border:1px solid #ffd800;">
           <asp:GridView ID="GridView5" runat="server" OnSelectedIndexChanged="GridView5_SelectedIndexChanged" >
               
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
           </asp:GridView>
       </div>
       <div id="_div_3_2" style="width:49%;border:1px solid #b6ff00;float:left;">
            <asp:Label ID="Label6" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label8" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        <asp:Label ID="Label9" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label10" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
        <asp:Label ID="Label11" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label12" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
        <asp:Label ID="Label13" runat="server" Text="第一"></asp:Label><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox><br />
       </div>
    </div>
   </form>
</body>
</html>
