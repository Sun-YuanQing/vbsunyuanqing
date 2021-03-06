﻿Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO


Partial Class bppmchart
    Inherits System.Web.UI.Page
    Public showth As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim sql As String = ""
                sql = "select F1 name,sum(F2) sarea,sum(F3) ngarea,sum(F4) sng from tiaoxingtu GROUP BY f1"
                Dim ds As New DataSet
                Dim sda As New SqlDataAdapter(sql, DBHelper.Conn)
                DBHelper.Conn.Open()
                sda.Fill(ds)
                Dim cds_detail As DataTable = ds.Tables(0)
                DBHelper.Conn.Close()
                If cds_detail.Rows.Count > 0 Then
                    Dim l_strYmonth As String
                    Dim l_dall, l_dsg, l_dqhd As String
                    Dim l_pall, l_psg, l_pqhd As String

                    'Chart1.Titles.Clear()     '移除所有对象
                    Chart1.Titles(0).Text = " 图形分析-"   '图形分析
                    Chart1.ChartAreas("default").AxisY2.Enabled = Dundas.Charting.WebControl.AxisEnabled.True
                    '柱形表上面的标题
                    Dim srs1 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs1.Color = Drawing.Color.SaddleBrown

                    srs1.Type = Dundas.Charting.WebControl.SeriesChartType.Column

                    Dim srs2 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs2.Color = Drawing.Color.LawnGreen
                    'srs2.LabelBackColor = Drawing.Color.Green
                    srs2.Type = Dundas.Charting.WebControl.SeriesChartType.Column

                    Dim srs3 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs3.Color = Drawing.Color.LightBlue
                    'srs2.LabelBackColor = Drawing.Color.Green
                    srs3.Type = Dundas.Charting.WebControl.SeriesChartType.Column


                    Dim srs4 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs4.Color = Drawing.Color.Red
                    'srs2.LabelBackColor = Drawing.Color.Green
                    srs4.MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.Circle
                    srs4.Type = Dundas.Charting.WebControl.SeriesChartType.Line
                    srs4.YAxisType = Dundas.Charting.WebControl.AxisType.Secondary


                    Dim srs5 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs5.Color = Drawing.Color.Yellow
                    'srs2.LabelBackColor = Drawing.Color.Green
                    srs5.MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.Circle
                    srs5.Type = Dundas.Charting.WebControl.SeriesChartType.Line
                    srs5.YAxisType = Dundas.Charting.WebControl.AxisType.Secondary


                    Dim srs6 As Dundas.Charting.WebControl.Series = New Dundas.Charting.WebControl.Series()
                    srs6.Color = Drawing.Color.RosyBrown
                    'srs2.LabelBackColor = Drawing.Color.Green
                    srs6.MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.Circle
                    srs6.Type = Dundas.Charting.WebControl.SeriesChartType.Line
                    srs6.YAxisType = Dundas.Charting.WebControl.AxisType.Secondary
                    '*****************显示列表************
                    Dim td1, td2, td3, td4, td5 As String
                    Dim tr1, tr2, tr3, tr4, tr5 As String
                    showth = "<table id =""table3"" style=""font-size:9pt;color:#004382"" cellspacing=""0"" bordercolordark=""white"" cellpadding=""0"" width=""800px"" bordercolorlight=""#cecece"" border=""1"">"
                    td1 = "<td width='120px' > &nbsp;</td>"
                    td2 = "<td width='120px' >出货量（TTL）（万） </td>"
                    td3 = "<td width='120px' > 出货不良面积（英寸）</td>"
                    td4 = "<td width='120px' > 面积不良率（百万）</td>"
                    td5 = "<td width='120px' > TTL DPPM</td>"
                    srs1.Name = "出货总面积"
                    srs2.Name = "出货量不良面积"
                    srs3.Name = "面积不良率"
                    srs4.Name = "出货总面积（曲线）"
                    srs5.Name = "出货量不良面积（曲线）"
                    srs6.Name = "面积不良率（曲线）"
                    Chart1.Series.Add(srs1)
                    Chart1.Series.Add(srs2)
                    Chart1.Series.Add(srs3)
                    Chart1.Series.Add(srs4)
                    Chart1.Series.Add(srs5)
                    Chart1.Series.Add(srs6)

                    For i As Integer = 0 To cds_detail.Rows.Count - 1 Step 1
                        l_strYmonth = (cds_detail.Rows(i)("name")).ToString()
                        l_dall = Double.Parse((cds_detail.Rows(i)("sarea")).ToString())
                        l_dsg = Double.Parse(cds_detail.Rows(i)("ngarea").ToString())
                        l_dqhd = Double.Parse(cds_detail.Rows(i)("sng").ToString())
                        l_pall = FormatNumber(Double.Parse(cds_detail.Rows(i)("sarea").ToString()), 0)
                        l_psg = FormatNumber(Double.Parse(cds_detail.Rows(i)("ngarea").ToString()), 0)
                        l_pqhd = FormatNumber(Double.Parse(cds_detail.Rows(i)("sng").ToString()), 0)

                        td1 += "<td allgn='center'>" & l_strYmonth & "</td>"
                        td2 += "<td allgn='right'>" & FormatNumber(l_dall, 0) & "</td>"
                        td3 += "<td allgn='right'>" & FormatNumber(l_dsg, 0) & "</td>"
                        td4 += "<td allgn='right'>" & FormatNumber(l_dqhd, 0) & "</td>"
                        td5 += "<td allgn='right'>" & l_pall & "</td>"
                        '表格的行数
                        'Chart1.Series(0).Points.AddXY(l_strYmonth, l_dall)
                        '' Chart1.Series(0).Points(i).AddY = l_dall.ToString
                        'Chart1.Series(1).Points.AddY(l_dsg)

                        'Chart1.Series(2).Points.AddY(l_dqhd)

                        'Chart1.Series(3).Points.AddY(l_psg)
                        'Chart1.Series(3).Points(i).Label = l_psg.ToString()

                        'Chart1.Series(4).Points.AddY(l_pqhd)
                        'Chart1.Series(4).Points(i).Label = l_pqhd.ToString()

                        'Chart1.Series(5).Points.AddY(l_pall)
                        'Chart1.Series(5).Points(i).Label = l_pall.ToString()


                    Next

                    '表格的高度
                    tr1 = "<tr height='30'>" & td1 & "</tr>"
                    tr2 = "<tr height='30'>" & td2 & "</tr>"
                    tr3 = "<tr height='30'>" & td3 & "</tr>"
                    tr4 = "<tr height='30'>" & td4 & "</tr>"
                    tr5 = "<tr height='30'>" & td5 & "</tr>"

                    showth += tr1 & tr2 & tr3 & tr4 & tr5 & "</table>"

                    Chart1.ChartAreas("default").AxisY.MajorGrid.Enabled = False
                    Chart1.ChartAreas("default").AxisY2.MajorGrid.Enabled = False
                    Chart1.ChartAreas("default").AxisX.MajorGrid.Enabled = False
                    'Chart1.ChartAreas("default").AxisY.Minimum = 0
                    'Chart1.ChartAreas("default").AxisY.Maximum = 0
                    'Chart1.ChartAreas("default").AxisY.Interval = 2
                    Chart1.ChartAreas("default").AxisY2.Arrows = Dundas.Charting.WebControl.ArrowsType.Triangle
                    Chart1.ChartAreas("default").AxisX.Interval = 1
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)

            End Try
        End If

    End Sub
End Class
