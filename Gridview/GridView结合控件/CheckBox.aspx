<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CheckBox.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�ޱ���ҳ</title>
    
    <script language="javascript" type="text/javascript">   
   // ��ѡ��ȫѡ��ȡ��
    function checkJs(boolvalue)
    {
        if(document.all.checkboxname.length>1)
        {
            for(var i=0;i<document.all.checkboxname.length;i++)
            {
                document.all.checkboxname[i].checked = boolvalue;            
            }
        }
        else
            document.all.checkboxname.checked = boolvalue;
    }  
    // ֻ��ȫ��ѡ��ʱ��ȫѡ��ѡ��
    function SingleCheckJs()
    {
        var flag1=false;
        var flag2=false;
        
        if (document.form1.checkboxname.length)
        {
            for (var i=0;i<document.form1.checkboxname.length;i++)
            {
                if(document.form1.checkboxname[i].checked)
                    flag1 = true;
                else
                    flag2 = true;
            }
        }
        else
        {
            if(document.form1.checkboxname.checked)
                flag1 = true;
            else
                flag2 = true;
        }
        
        if(flag1==true&&flag2==false)
            document.getElementById("chk").checked = true;
        else
            document.getElementById("chk").checked = false;
    }
    </script>   
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table cellpadding="0" cellspacing="0" border="0" width="80%" style="font-size: 11px">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="5" AllowSorting="True"
                            AutoGenerateColumns="False" HeaderStyle-VerticalAlign="Middle" CellPadding="3"
                            Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            Height="221px" Width="100%" >
                            <Columns>
                              <asp:TemplateField HeaderText="&lt;input type='checkbox' id='chk' name='chk' onclick='checkJs(this.checked);'  /&gt;ȫѡ"
                                    >
                                    <ItemTemplate>
                                        <input type="checkbox" id="checkboxname" name="checkboxname" value='<%# DataBinder.Eval(Container.DataItem, "CID")%>'
                                            onclick='SingleCheckJs();' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CID" HeaderText="�û�ID"/>                             
                                <asp:BoundField DataField="Name" HeaderText="�û�����" />
                                <asp:BoundField DataField="Sex" HeaderText="�Ա�" />
                                <asp:BoundField DataField="Address" HeaderText="��ͥסַ" />
                                <asp:BoundField DataField="Post" HeaderText="��������" />
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <PagerSettings Visible="False" />
                            <FooterStyle Font-Bold="True" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
