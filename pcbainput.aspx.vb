Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO

Partial Class pcbainput
    Inherits System.Web.UI.Page
    '默认加载方法 
    Public Sub yess()
        Try
            DBHelper.CloseConnection()
            Dim into As Integer = 0
            '存储过程没有selects
            Dim selects As String = " id, name, fno, userid, usernames, times, paths, delet, style, op, type, class "
            Dim wheres As String = " 1=1 and delet='0' "

            If itme_1.Text.Trim() <> "" Then
                wheres += "  and CONVERT (varchar(12),times,20) >='2017-01-01 00:00:00'"
            End If
            If itme_2.Text.Trim() <> "" Then
                wheres += " and convert(varchar(12), times,20) <='2017-12-31 23:59:59'"
            End If
            If txtb_user.Text.Trim() <> "" Then
                wheres += " and usernames lile '%" & txtb_user.Text.Trim() & "%'"
            End If
            Dim comm As New SqlCommand
            comm = New SqlCommand("sp_Excelpath_select", DBHelper.Conn)
            comm.CommandType = CommandType.StoredProcedure
            comm.Parameters.Add("@selects", SqlDbType.NVarChar, 200).Value = selects
            comm.Parameters.Add("@wheres", SqlDbType.NVarChar, 800).Value = wheres
            Dim dt As New DataTable
            dt = DBHelper.GetTable(comm)

            DataGrid2.DataSource = dt
            DataGrid2.DataBind()
        Catch ex As Exception

            Response.Write("<script lang ='ja' >alert('没有数据或服务器神游中^­­-^！');</script>")
        Finally
            DBHelper.CloseConnection()
        End Try
    End Sub
    '加载方法（条件可选）'2017-01-01 00:00:00'"
    Public Sub yes()
        Try
            DBHelper.CloseConnection()
            Dim into As Integer = 0
            '存储过程没有selects
            Dim selects As String = " id, name, fno, userid, usernames, times, paths, delet, style, fonts "
            Dim wheres As String = " 1=1 and delet='0'"

            If itme_1.Text.Trim() <> "" Then
                wheres += "  and CONVERT (varchar(12),times,20) >='" & itme_1.Text.Trim() & "'"
            End If
            If itme_2.Text.Trim() <> "" Then
                wheres += " and convert(varchar(12), times,20) <='" & itme_2.Text.Trim() & "'"
            End If
            If txtb_user.Text.Trim() <> "" Then
                wheres += " and usernames like '%" & txtb_user.Text.Trim() & "%'"
            End If
            If excel_id.Text.Trim() <> "" Then
                wheres += " and fno like '%" & excel_id.Text.Trim() & "%'"
            End If

            Dim comm As New SqlCommand
            comm = New SqlCommand("sp_Excelpath_select", DBHelper.Conn)
            comm.CommandType = CommandType.StoredProcedure
            comm.Parameters.Add("@selects", SqlDbType.NVarChar, 200).Value = selects
            comm.Parameters.Add("@wheres", SqlDbType.NVarChar, 800).Value = wheres
            Dim dt As New DataTable
            dt = DBHelper.GetTable(comm)
            DataGrid2.DataSource = dt
            DataGrid2.DataBind()
        Catch ex As Exception
            Response.Write("<script lang ='ja' >alert('没有数据或服务器神游中^­­-^！');</script>")
        End Try

    End Sub
    '获取文件中数据的方法DataGrid1
    Private Sub System_ShowemceIData(ByVal strfilename As String)
        DBHelper.CloseConnection()
        Dim strconexcel As String
        Dim oleconn As New OleDbConnection
        Dim olesda As New OleDbDataAdapter
        Dim olecmd As New OleDbCommand
        Dim ds As New DataSet
        Try
            'Microsoft.ACE.OLEDB.12.0
            ' Wicrosoft.jet.OleDb.4.0;    IIS要设置32位
            '驱动字符串对象d
            strconexcel = "Provider= Microsoft.jet.OleDb.4.0;" + "Data source=" + strfilename + ";Extended properties='Excel 8.0;IMEX=1;HDR=NO;';"
            '将 驱动字符串对象d 给执行对象
            oleconn.ConnectionString = strconexcel
            '打开执行对象
            oleconn.Open()
            '从执行对象数据源返回架构信息
            Dim schemaTable As DataTable = oleconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            '第二行开始？
            Dim tablename As String = schemaTable.Rows(0)(2).ToString().Trim()
            '第二行开始？
            Dim tkindno = "1-1"
            '查询Excel语句
            Dim excelsql As String
            excelsql = "select F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11  from [" + tablename + "]"
            'OleDbCommand接收结果集
            olecmd = New OleDbCommand(excelsql, oleconn)
            'OleDbDataAdapter接收=olecmd
            olesda.SelectCommand = olecmd
            'OleDbDataAdapter=ds
            olesda.Fill(ds, tkindno)
            '设置数据源
            DataGrid1.DataSource = ds.Tables(0)
            '绑定数据源
            DataGrid1.DataBind()
            '释放资源
            ds.Dispose()
        Catch ex As Exception
            DBHelper.CloseConnection()
        End Try
    End Sub
    '上传文件中的数据
    Protected Sub btn_server_Click(sender As Object, e As EventArgs) Handles btn_server.Click

        Dim strfilename As String = labe1_path.Text.Trim.ToString()
        If strfilename.Trim() <> "" Then
        Dim strconexcel As String
        'excel 数据源对象
        Dim oleconn As New OleDbConnection
        'excel 的OleDbDataAdapter数据连接
        Dim olesda As New OleDbDataAdapter
        'excel执行的对象
        Dim olecmd As New OleDbCommand
        'SQL 数据缓存DataSet对象
        Dim ds As New DataSet
        Dim oracmd As New SqlCommand
        '从labe1获取服务器文件名
            Try
                '打开数据
                DBHelper.OpenConnection()
                'Excel驱动链接对象字符串
                strconexcel = "Provider= Microsoft.jet.OleDb.4.0;" + "Data source=" + strfilename + ";Extended properties='Excel 8.0;IMEX=1;HDR=NO;';"
                '创建Excel驱动链接对象字符串
                oleconn.ConnectionString = strconexcel
                '打开Excel链接对象
                oleconn.Open()
                '获取Excel链接对象的架构信息赋值给 DataTable
                Dim schemaTable As DataTable = oleconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                '获取Excel链接对象的Sheet1&赋值给 tablename
                Dim tablename As String = schemaTable.Rows(0)(2).ToString().Trim()

                ' ========================================
                Dim strsql As String = ""
                '==================================
                Dim tkindno = "1-1"
                '查询 Sheet1& 对象数据的SQL
                Dim excelsql As String = "select F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11  from [" + tablename + "]"
                ' olecmd 获取执行 Sheet1&对象
                olecmd = New OleDbCommand(excelsql, oleconn)
                '将的执行 Sheet1&对象建立olesda仓库连接
                olesda.SelectCommand = olecmd
                '将Exceld的olesda 连接 给SQL数据缓存DataSet（ds）对象的1-1
                olesda.Fill(ds, "1-1")

                '查询数据库SQL的字符串-------------------------------------'F13 标识
                Dim sqlexcelemp As String = "select  F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F13 from Excel1 "
                '创建连接数据源连接
                Dim upsda As New SqlDataAdapter
                '连接执行的数据源对象
                upsda.SelectCommand = New SqlCommand(sqlexcelemp, DBHelper.Conn)
                '将连接执行的数据源对象生成表单
                Dim cd As SqlCommandBuilder = New SqlCommandBuilder(upsda)
                '创建缓存对象DataSet
                Dim upds = New DataSet
                '连接执行的数据源对象给缓存对象 upds
                upsda.Fill(upds)
                '创建一行DataTable数据
                Dim olerow As DataRow
                'for 循环次数为excel 给 ds 缓存的 1-1 表的条数 -1
                For j As Integer = 1 To ds.Tables("1-1").Rows.Count - 1
                    'olerow 获取excel 给 ds 缓存的 1-1 表的 一行数据
                    olerow = ds.Tables("1-1").Rows(j)
                    '如果excel 给 ds 缓存的 1-1 表的 一行数据的 第一个值不为空
                    If olerow("F1").ToString() <> "" Then
                        '创建与SQL Excel1表具有相同架构的一行数据
                        Dim dsrow As DataRow = upds.Tables(0).NewRow()
                        'excel 给 ds 缓存的 1-1 表的 F1 给 Excel1表具有相同架构的一行数据的 F1
                        dsrow("F1") = olerow("F1").ToString()
                        'excel 给 ds 缓存的 1-1 表的 F2 给 Excel1表具有相同架构的一行数据的 F2
                        'Excel1 行获取 （ olerow 获取（先让1-1 数据 的 F2等于空） 再 （ olerow 获取1-1 数据 的 F2））
                        dsrow("F2") = IIf(olerow("F2").ToString() = "", DBNull.Value, olerow("F2").ToString())
                        dsrow("F3") = IIf(olerow("F3").ToString() = "", DBNull.Value, olerow("F3").ToString())
                        dsrow("F4") = IIf(olerow("F4").ToString() = "", DBNull.Value, olerow("F4").ToString())
                        dsrow("F5") = IIf(olerow("F5").ToString() = "", DBNull.Value, olerow("F5").ToString())
                        dsrow("F6") = IIf(olerow("F6").ToString() = "", DBNull.Value, olerow("F6").ToString())
                        dsrow("F7") = IIf(olerow("F7").ToString() = "", DBNull.Value, olerow("F7").ToString())
                        dsrow("F8") = IIf(olerow("F8").ToString() = "", DBNull.Value, olerow("F8").ToString())
                        dsrow("F9") = IIf(olerow("F9").ToString() = "", DBNull.Value, olerow("F9").ToString())
                        dsrow("F10") = IIf(olerow("F10").ToString() = "", DBNull.Value, olerow("F10").ToString())
                        dsrow("F11") = IIf(olerow("F11").ToString() = "", DBNull.Value, olerow("F11").ToString())
                        'F13 标识
                        dsrow("F13") = excel.fno

                        'Excel1表具有相同架构的一行数据 加上（ （Excel1表具有相同架构的一行数据 ）获取 （excel 给 ds 缓存的 1-1 表的一行数据））
                        upds.Tables(0).Rows.Add(dsrow)
                    End If
                Next
                'Excel1'连接执行的数据源对象 更新为（Excel1 的 upds缓存得到的（excel 给 ds 缓存的 1-1 表数据））
                upsda.Update(upds.Tables(0))
                '释放 Excel1 连接执行的数据源对象
                upsda.Dispose()
                '****************************************
                oracmd.Connection = DBHelper.Conn
                oracmd.CommandType = CommandType.Text
                '执行数据事务
                oracmd.Transaction = DBHelper.Conn.BeginTransaction()
                '处理转正表
                strsql = ""

                strsql = "insert into Excel1 (F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14)" & _
                    " SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14 from Excel1"
                oracmd.CommandText = strsql
                oracmd.Connection = DBHelper.Conn
                Dim Excelcunot As Integer = oracmd.ExecuteNonQuery().ToString()
                Me.Table1.Visible = False

                '**************清除零食表的资料
                'strsql = "delete from Excel1 "
                'oracmd.CommandText = strsql
                'oracmd.Connection = DBHelper.Conn
                'oracmd.ExecuteNonQuery()
                'oracmd.Transaction.Commit(
                Response.Write("<script  >alert('导入成功！');window.navigate('pcbainput.aspx');</script>")
            Catch ex As Exception
            Finally
                oracmd.Connection.Close()

                If oleconn.State = ConnectionState.Open Then
                    oleconn.Dispose()
                End If
                If DBHelper.Conn.State = ConnectionState.Open Then
                    DBHelper.Conn.Dispose()
                End If
                DBHelper.CloseConnection()
                oleconn.Close()
                Me.labe1_path.Text = ""
            End Try
        End If
    End Sub
    Dim ra As New Random
    '编号生成规则
    Function getfno() As String
        Try
            Dim comm As New SqlCommand
            DBHelper.OpenConnection()

            comm = New SqlCommand("sp_Excelpath_newfno", DBHelper.Conn)
            comm.CommandType = CommandType.StoredProcedure
            Dim fno As String
            fno = comm.ExecuteScalar()
            Return fno
        Catch ex As Exception
        End Try
    End Function
    '上传文件中浏览文件
    Protected Sub imgbtnsearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnsearch.Click

        'Dim strfile As String = MapPath(Me.File1.FileName)
        Dim strfile As String = Me.File1.FileName
        'vb测试.xls
        If strfile.Trim() <> "" Then
            Me.Table1.Visible = True
            Dim userid As String = ""  'dyjfjfhgfhjj
            '设置文件上传目录
            Dim strFilePath As String = ""
            Dim strfilePathName As String = ""
            Dim strfilePhyName1 As String = ""
            excel.fno = getfno()

            '截取得到文件名
            Dim strfileSourceName As String = Mid(strfile, InStrRev(strfile, "\") + 1)
            Try
                If strfileSourceName <> "" Then
                    Dim isr As Integer = 0
                    '  生成上传到服务器的路径
                    strFilePath = "E:/upload/" & Now().ToString("yyyMMdd") & "/"
                    'strFilePath = Server.MapPath("./upload/") & Now().ToString("yyyMMdd") & "/"

                    Me.labe1_path.Text = strFilePath
                    If Directory.Exists(strFilePath) = False Then
                        Directory.CreateDirectory(strFilePath)
                    End If
                    ' If Right(strfile, 3).ToLower() = "xls" Then
                    '     Me.DisplayUploadStatus("mydiv", "正在上传Excel文档")
                    'End If
                    '虚拟文件名
                    strfilePathName = Now().ToString("yyyMMdd") & ra.Next(100).ToString.Trim() & Right(strfile, 4)
                    'strfilePathName	"2017082668.xls"	String
                    '动态虚拟文件路径
                    strfilePhyName1 = Now().ToString("yyyMMdd") & "\" & excel.fno & Right(strfile, 4)
                    'strfilePhyName1	"20170826/2017082668.xls"	String
                    '上传服务器的完整路径
                    Dim strfilePhyName2 As String
                    strfilePhyName2 = strFilePath & excel.fno & Right(strfile, 4)

                    Me.File1.PostedFile.SaveAs(strfilePhyName2)    '执行
                    Response.Write("<script> HideWait();</script>")

                    '重新赋值上传服务器的完整路径
                    Me.labe1_path.Text = strfilePhyName2

                    System_ShowemceIData(strfilePhyName2)      '获取文件中数据的方法（）

                    '获取时间
                    ' Dim time As New DateTime
                    ' time = Now().ToString("YYY-MM-DD HH:MI:SS")
                    ' time = Now().ToString("yyy-MM-dd HH:m:s")

                    '随机数？
                    ' Dim num As Integer = ra.Next(100).ToString.Trim()

                    ' comm.Parameters.Add("@selects", SqlDbType.NVarChar, 200).Value = selects
                    'comm.Parameters.Add("@wheres", SqlDbType.NVarChar, 800).Value = wheres
                    ' Dim dt As New DataTable
                    ' dt = DBHelper.GetTable(comm)
                    ' DataGrid2.DataSource = dt
                    ' DataGrid2.DataBind()
                    '列名
                    Dim into As String
                    into = " name, fno, userid, usernames,  paths, delet, style, op, type, class"
                    Dim selects As String
                    selects = " '" & strfileSourceName & "','" & excel.fno & "', '了','孙元清','" & strfilePhyName1 & "', '0', '了',   '了',  '了',   'IT 分中心' "
                    Dim comm As New SqlCommand
                    comm = New SqlCommand("sp_excelpath_insert", DBHelper.Conn)
                    comm.CommandType = CommandType.StoredProcedure
                    comm.Parameters.Add("@into", SqlDbType.NVarChar, 200).Value = into
                    comm.Parameters.Add("@select", SqlDbType.NVarChar, 200).Value = selects
                    Dim num As Integer = Convert.ToInt32(DBHelper.GetSca(comm))
                    If num > 0 Then
                        Response.Write("<script lang ='ja' >alert('上床文件成功^­­-^！');</script>")
                    End If
                    yes()
                End If
            Catch ex As Exception
                DBHelper.CloseConnection()
            End Try
        End If
    End Sub
    ' 加载中画面
    Private Sub DisplayUploadStatus(ByVal IdName As String, ByRef sTitie As String)
        Dim Restr As String = ""
        Restr = "<div id ='" + IdName + "' style ='font-size:14pt;z-index:101;position:absolute;top:30px;'>"
        Restr += "</div>"
        Restr += "<script>" + IdName + ".innerText='';</script>"
        Restr += "<script>"
        Restr += "var dots =0; var dotmax=10; function ShowWait()"
        Restr += "{ var output ; output ='" + sTitie + "';dots++;if (dots>=dotmax)dots=1;"
        Restr += "for( var x =0; x < dots ;x++){output += '.';}  " + IdName + ".innerText=output;}"
        Restr += "function StartShowWait()  {" + IdName + ".style.visibility = 'visible';"
        Restr += "window.setinterval('ShowWait()',1000);}"
        Restr += "function HideWait(){" + IdName + ".style.visibility ='hidden';" + IdName + ".style.display='none';"
        Restr += "window.clearInterval();}"
        Restr += "StartShowWait();</script>"
        Response.Write(Restr)
        Response.Flush()
        Thread.Sleep(4000)
    End Sub
    '查询 yes()
    Protected Sub btn_chaxun_Click(sender As Object, e As EventArgs) Handles btn_chaxun.Click
        yes()
    End Sub
    '点击DataGrid2
    Protected Sub DataGrid2_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.labe1_path.Visible = False
            Me.Table1.Visible = False
            Me.Table2.Visible = True
            yess() '默认加载方法
        End If

    End Sub
    Protected Sub btn_daochu_Click(sender As Object, e As EventArgs) Handles btn_daochu.Click
        Response.Write("<script >alert('刷新数据！');window.open('../Excel_daochu.aspx','_parent') ;</script>")
    End Sub
End Class
