using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///Common 的摘要说明
/// </summary>
public class Common
{
    #region "页面加载中效果"
    /// <summary>
    /// 页面加载中效果
    /// </summary>
    public static void initJavascript()
    {
        HttpContext.Current.Response.Write(" <script language=JavaScript type=text/javascript>");
        HttpContext.Current.Response.Write("var t_id = setInterval(animate,20);");
        HttpContext.Current.Response.Write("var pos=0;var dir=2;var len=0;");
        HttpContext.Current.Response.Write("function animate(){");
        HttpContext.Current.Response.Write("var elem = document.getElementById('progress');");
        HttpContext.Current.Response.Write("if(elem != null) {");
        HttpContext.Current.Response.Write("if (pos==0) len += dir;");
        HttpContext.Current.Response.Write("if (len>32 || pos>199) pos += dir;");
        HttpContext.Current.Response.Write("if (pos>199) len -= dir;");
        HttpContext.Current.Response.Write(" if (pos>199 && len==0) pos=0;");
        HttpContext.Current.Response.Write("elem.style.left = pos;");
        HttpContext.Current.Response.Write("elem.style.width = len;");
        HttpContext.Current.Response.Write("}}");
        HttpContext.Current.Response.Write("function remove_loading() {");
        HttpContext.Current.Response.Write(" this.clearInterval(t_id);");
        HttpContext.Current.Response.Write("var targelem = document.getElementById('loader_container');");
        HttpContext.Current.Response.Write("targelem.style.display='none';");
        HttpContext.Current.Response.Write("targelem.style.visibility='hidden';");
        HttpContext.Current.Response.Write("}");
        HttpContext.Current.Response.Write("</script>");
        HttpContext.Current.Response.Write("<style>");
        HttpContext.Current.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
        HttpContext.Current.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:12px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:250px; border:0px solid #5a667b; text-align:left; z-index:2;}");
        HttpContext.Current.Response.Write("#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
        HttpContext.Current.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:233px; font-size:1px;}");
        HttpContext.Current.Response.Write("</style>");
        HttpContext.Current.Response.Write("<div id=loader_container>");
        HttpContext.Current.Response.Write("<div id=loader>");
        HttpContext.Current.Response.Write("<table align='center' border='1' width='87%' cellspacing='0' cellpadding='4' style='border-collapse: collapse' bgcolor='#FFFFEC' height='87'>");
        HttpContext.Current.Response.Write("<tr><td bgcolor='#99CCCC' style='font-size: 12px;' height='24' align='center'>");
        HttpContext.Current.Response.Write("SZCFERP 友情提示</td></tr>");
        HttpContext.Current.Response.Write("<tr><td style='font-size: 12px; line-height: 200%' align='center'>正在载入数据中，请耐心等待...");
        HttpContext.Current.Response.Write("</td></tr></table>");
        HttpContext.Current.Response.Write("");
        HttpContext.Current.Response.Write("");
        HttpContext.Current.Response.Write("");
        HttpContext.Current.Response.Write("");

        HttpContext.Current.Response.Write("<div id=loader_bg><div id=progress> </div></div>");
        HttpContext.Current.Response.Write("</div></div>");
        HttpContext.Current.Response.Flush();
    }

    /// <summary>
    /// 页面加载中效果2 [显示略有不同]]
    /// </summary>
    public static void initJavascript_2()   
    {
        HttpContext.Current.Response.Write(" <script language=JavaScript type=text/javascript>");
        HttpContext.Current.Response.Write("var t_id = setInterval(animate,20);");
        HttpContext.Current.Response.Write("var pos=0;var dir=2;var len=0;");
        HttpContext.Current.Response.Write("function animate(){");
        HttpContext.Current.Response.Write("var elem = document.getElementById('progress');");
        HttpContext.Current.Response.Write("if(elem != null) {");
        HttpContext.Current.Response.Write("if (pos==0) len += dir;");
        HttpContext.Current.Response.Write("if (len>32 || pos>79) pos += dir;");
        HttpContext.Current.Response.Write("if (pos>79) len -= dir;");
        HttpContext.Current.Response.Write(" if (pos>79 && len==0) pos=0;");
        HttpContext.Current.Response.Write("elem.style.left = pos;");
        HttpContext.Current.Response.Write("elem.style.width = len;");
        HttpContext.Current.Response.Write("}}");
        HttpContext.Current.Response.Write("function remove_loading() {");
        HttpContext.Current.Response.Write(" this.clearInterval(t_id);");
        HttpContext.Current.Response.Write("var targelem = document.getElementById('loader_container');");
        HttpContext.Current.Response.Write("targelem.style.display='none';");
        HttpContext.Current.Response.Write("targelem.style.visibility='hidden';");
        HttpContext.Current.Response.Write("}");
        HttpContext.Current.Response.Write("</script>");
        HttpContext.Current.Response.Write("<style>");
        HttpContext.Current.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
        HttpContext.Current.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:12px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:130px; border:1px solid #5a667b; text-align:left; z-index:2;}");
        HttpContext.Current.Response.Write("#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
        HttpContext.Current.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:113px; font-size:1px;}");
        HttpContext.Current.Response.Write("</style>");
        HttpContext.Current.Response.Write("<div id=loader_container>");
        HttpContext.Current.Response.Write("<div id=loader>");
        HttpContext.Current.Response.Write("<div align=center>正在载入数据中，<br>请耐心等待 ...</div>");
        HttpContext.Current.Response.Write("<div id=loader_bg><div id=progress> </div></div>");
        HttpContext.Current.Response.Write("</div></div>");
        HttpContext.Current.Response.Flush();
    }
    #endregion

    ////// 数据库SQL SERVER链接测试
    ////public static string TestSQLLink(string ConnectionStr)
    ////{
    ////    string retVal = "";
    ////    SqlConnection conn;

    ////    try
    ////    {
    ////       conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionStr].ConnectionString);
    ////       conn.Open();
    ////       retVal = conn.State.ToString();
    ////       conn.Close();
    ////    }
    ////    catch
    ////    {
    ////        retVal = "false";
    ////    }
    ////    return retVal;
    ////}


    #region ---------- 用户密码 ---------------------------------------------------------
    /*------------------------------------------------------------
      *函数： EncryptPWD(string inString)
      *功能： 
      *    将参数 inString 编码加密成 GCERP 密码格式，具体算法由 GCERP系统数据库usr_mstr表分析所得；
      *    密码长度为 0-8位；
      *------------------------------------------------------------*/
    public static string EncryptPWD(string inString)
    {
        string[] outchar = new string[8];
        int i, pos, ch;

        //密码原字符
        string char_sc = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";  //注意由于C#程序语言要求，字符串中双引号“"”和“\”前要加‘\’符，原字符为【 !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~】
        //编码后首位字符
        string char_pw = "^]`_ZY\\[fehgbadcnmpojilkvuxwrqtsUTWVQPSR]\\_^YX[Zedgfa`cbmlonihkjutwvqpsr}|(~yx{z.-0/*),+6587214"; //对应的变换后首字符【^]`_ZY\[fehgbadcnmpojilkvuxwrqtsUTWVQPSR]\_^YX[Zedgfa`cbmlonihkjutwvqpsr}|(~yx{z.-0/*),+6587214】
        string char_pw_null = "makkhNL0";  // 空密码对应的编码字符

        int len_in = inString.Length;
        string outString = "";

        try
        {
            if (len_in == 0)
            {
                outString = char_pw_null; // 空密码对应的编码字符
            }
            else
            {
                for (i = 0; i < len_in; i++)
                {
                    pos = char_sc.IndexOf(inString.Substring(i, 1));
                    ch = Asc(char_pw.Substring(pos, 1)) - i;
                    if (ch < 40) { ch = ch + 87; }          //当转换后的字符ASCII码小于40时，要变换为从126开始(即Asc(39)变为Asc(126),类推)
                    outchar[i] = Chr(ch);

                    ///调试用：Response.Write("i=" + i.ToString() +" -- "+ch.ToString()+ "<br>");
                }

                //不足8位字符是通过换算补全
                if (len_in < 8)
                {
                    for (i = len_in; i < 8; i++)
                    {
                        ch = Asc(char_pw_null.Substring(i - len_in, 1)) - len_in;
                        outchar[i] = Chr(ch).ToString();

                        ///调试用：Response.Write("i=" + i.ToString() +" -- "+ch.ToString()+ "<br>");
                    }
                }
                //将字符组成编码字符串
                for (i = 0; i < 8; i++)
                {
                    outString = outString + outchar[i];
                }
            }
            return (outString);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }



    /// <summary>
    /// 用户密码MD5加密函数
    /// </summary>
    /// <param name="password">密码</param>
    /// <returns>加密值</returns>
    public static string MD5Encrypt(string password)
    {
        return MD5(password, 16);
    }


    /// <summary>
    /// MD5加密用户密码
    /// </summary>
    /// <param name="password">密码</param>
    /// <param name="codeLength">多少位</param>
    /// <returns>加密密码</returns>
    public static string MD5(string password, int codeLength)
    {
        if (!string.IsNullOrEmpty(password))
        {
            // 16位MD5加密（取32位加密的9~25字符）  
            if (codeLength == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToUpper().Substring(8, 16);
            }

            // 32位加密
            if (codeLength == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToUpper();
            }
        }
        return string.Empty;
    }


    #endregion

    #region ----------- 字符处理及格式转换 ------------------------------------------------

    //字符转ASCII码 
    public static int Asc(string character)
    {
        if (character.Length == 1)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
            return (intAsciiCode);
        }
        else
        {
            throw new Exception("Character is not valid. ");
        }

    }

    //ASCII码转字符 
    public static string Chr(int asciiCode)
    {
        if (asciiCode >= 0 && asciiCode <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)asciiCode };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        else
        {
            throw new Exception("ASCII Code is not valid. ");
        }
    }

    //去除字符串前后空格
    public static string STrim(string sInString)
    {
        if (sInString == null || sInString.Length == 0)
            return "";

        sInString = sInString.Trim();
        return sInString;
    }

    //去除小数点后面多余的零
    public static string RemoveTailZero(string num)
    {
        //string arraylist = Convert.ToString(num);
        string arraylist = num.ToString();

        int temp = 0;
        string strRtn = "";
        for (int i = arraylist.Length - 1; i >= 0; i--)
        {
            if (arraylist[i].ToString() != "0")
            {
                temp = i;
                break;
            }
        }
        for (int i = 0; i <= temp; i++)
        {
            strRtn += arraylist[i].ToString();
        }

        //末尾字符为小数点的话则去除
        if (strRtn.Substring(strRtn.Length-1) == ".")
            strRtn = strRtn.Substring(0, strRtn.Length - 1); 

        return strRtn;
    }

    /// <summary>
    ///在字符串sInString后面增加指定次数的字符串sChar
    /// </summary>
    /// <param name="num">重复填充的次数</param>
    /// <param name="sInString">原始字符串</param>
    /// <param name="sChar">要重复填充在尾部的字符串</param>
    public static string PadBlank(int num, string sInString, string sChar)
    {
        if (num <= 0) return "";

        if (sInString == null || sInString.Length == 0)
            sInString = "";

        if (sChar == null || sChar.Length == 0)
            return sInString;

        for (int i = 0; i < num; i++)
        {
            sInString += sChar;
        }
        return sInString;
    }

    //---------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 对输入框的特殊字串进行过滤，防止SQL注入
    /// </summary>
    /// <param name="strFromText">要被过滤的字符串</param>
    /// <returns>过滤后的字符串</returns>
    public static string SqlInsertEncode(string strFromText)
    {
        if (!System.String.IsNullOrEmpty(strFromText) && strFromText != "")
        {
            strFromText = strFromText.Replace(";", "&#59;");
            strFromText = strFromText.Replace("!", "&#33;");
            strFromText = strFromText.Replace("@", "&#64;");
            strFromText = strFromText.Replace("$", "&#36;");
            strFromText = strFromText.Replace("*", "&#42;");
            strFromText = strFromText.Replace("(", "&#40;");
            strFromText = strFromText.Replace(")", "&#41;");
            strFromText = strFromText.Replace("-", "&#45;");
            strFromText = strFromText.Replace("+", "&#43;");
            strFromText = strFromText.Replace("=", "&#61;");
            strFromText = strFromText.Replace("|", "&#124;");
            strFromText = strFromText.Replace("\\", "&#92;");
            strFromText = strFromText.Replace("/", "&#47;");
            strFromText = strFromText.Replace(":", "&#58;");
            strFromText = strFromText.Replace("\"", "&#34;");
            strFromText = strFromText.Replace("'", "&#39;");
            strFromText = strFromText.Replace("<", "&#60;");
            strFromText = strFromText.Replace(" ", "&#32;");
            strFromText = strFromText.Replace(">", "&#62;");
            strFromText = strFromText.Replace(" ", "&#32;");
        }
        return strFromText;
    }

    //格式化SQL字符串：将其中的双引号改为单引号，并去除前后空格，转为大写
    //public static string FormatParameter(string formatStr)
    //{
    //    string rStr = formatStr;
    //    if ((formatStr != null) && (formatStr != string.Empty))
    //    {
    //        rStr = rStr.Replace("\"", "'");
    //    }
    //    return rStr.Trim().ToUpper();
    //}

    /// <summary>
    ///	格式化字符串：将其中的双引号改为中文双引号，“\”改为“\\”，去除回车符，并去除前后空格，并转为大写
    /// </summary>
    /// <param name="formatStr">需格式化字符串</param>
    public static string FormatParameter(string formatStr)
    {
        string rStr = "";

        if ((formatStr != null) && (formatStr != string.Empty))
        {
            rStr = formatStr.Trim().ToUpper();
            rStr = rStr.Replace("\"", "＂");
            rStr = rStr.Replace("\\", "\\\\");
            rStr = StringHelper.SqlInsertEncode(rStr);  // 过滤特殊字符
        }

        return rStr;
    }

    /// <summary>
    ///	格式化字符串：将其中的双引号改为中文双引号，“\”改为“\\”，去除回车符，并去除前后空格，保持原形不转大写
    /// </summary>
    /// <param name="formatStr">需格式化字符串</param>
    public static string FormatParameter2(string formatStr)
    {
        string rStr = "";

        if ((formatStr != null) && (formatStr != string.Empty))
        {
            rStr = formatStr.Trim();
            rStr = rStr.Replace("\n", "");
            rStr = rStr.Replace("\r", "");
            rStr = rStr.Replace("\"", "＂");
            rStr = rStr.Replace("\\", "\\\\");
            rStr = StringHelper.SqlInsertEncode(rStr);  // 过滤特殊字符
        }

        return rStr;
    }

    /// <summary>
    /// 将数字字符串转为数字
    /// </summary>
    /// <param name="formatStr">需数字型字符串</param>
    public static double ConvertToDouble(string formatStr)
    {
        double rVal = 0.0;
        string rStr = "";

        if ((formatStr != null) && (formatStr != string.Empty))
        {
            rStr = formatStr.Trim();
            rStr = rStr.Replace("\n", "");
            rStr = rStr.Replace("\r", "");
            rStr = rStr.Replace("\"", "＂");
            rStr = rStr.Replace("\\", "\\\\");
            rStr = StringHelper.SqlInsertEncode(rStr);  // 过滤特殊字符

            rVal = Double.Parse(rStr);
        }

        return rVal;
    }

    #endregion

    #region -------------- 日期及时间格式转换 -------------------------------------------

    /// <summary>
    ///	格式化时间值为短日期（"yyyy-mm-dd"）
    /// </summary>
    /// <param name="dTime">时间值</param>
    public static string FormatDate(DateTime dTime)
    {
        return dTime.ToString("yyyy-MM-dd");
       //或 return string.Concat(new object[] { dTime.Year, "-", dTime.Month, "-", dTime.Day });
    }

    /// <summary>
    ///	格式化时间字符串为短日期（"yyyy-mm-dd"）
    /// </summary>
    /// <param name="sTime">时间字符串</param>
    public static string FormatDate(String sTime)
    {
        string retV = "";

        try
        {
            if (!string.IsNullOrEmpty(sTime))
            {
                DateTime dTime = DateTime.Parse(sTime);
                retV = dTime.ToString("yyyy-MM-dd");
                //或 return string.Concat(new object[] { dTime.Year, "-", dTime.Month, "-", dTime.Day });
            }
        }
        catch
        {
            retV = "";
        }

        return retV;
    }

    /// <summary>
    ///	格式化时间值为长时间（"yyyy-MM-dd HH:mm:ss"）
    /// </summary>
    /// <param name="dTime">时间值</param>
    public static string FormatTime(DateTime dTime)
    {
        return dTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    ///	格式化时间字符串为长时间（"yyyy-MM-dd HH:mm:ss"）
    /// </summary>
    /// <param name="sTime">时间字符串</param>
    public static string FormatTime(String sTime)
    {
        string retV = "";

        try
        {
            if (!string.IsNullOrEmpty(sTime))
            {
                DateTime dTime = DateTime.Parse(sTime);
                retV = dTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        catch
        {
            retV = "";
        }

        return retV;
    }

    #endregion

    #region -------------- 数字格式转换 -------------------------------------------

    /// <summary>
    ///	格式化数字格式字符串，保留2位小数；非数字则返回 0.00
    /// </summary>
    /// <param name="formatStr">需格式化字符串</param>
    public static string FormatNumber(string formatStr)
    {
        string rStr = FormatParameter(formatStr);

        try
        {
            //或 double result = double.Parse(rStr);  //可以转为Double数据的话则说明是数字
            double result = Convert.ToDouble(rStr);

            rStr = result.ToString("n2");
            return rStr;
        }
        catch
        {
            return "0.00";
        }
    }

    /// <summary>
    ///	格式化数字格式字符串，保留指定位小数；非数字则返回 0
    /// </summary>
    /// <param name="formatStr">需格式化字符串</param>
    /// <param name="Digitr">保留的小数位数</param>
    public static string FormatNumber(string formatStr, int iDigit)
    {
        int dt = iDigit;
        string rStr = FormatParameter(formatStr);

        if (dt < 0) dt = 0;

        try
        {
            //或 double result = double.Parse(rStr);  //可以转为Double数据的话则说明是数字
            double result = Convert.ToDouble(rStr);

            if (result != 0)
            {
                rStr = result.ToString("n" + dt);
            }
            return rStr;
        }
        catch
        {
            return "0";
        }
    }


    /// <summary>
    ///	格式化数字为字符串，保留指定位小数；数值0的话则返回 0
    /// </summary>
    /// <param name="formatValue">需格式化的浮点数值</param>
    /// <param name="Digitr">保留的小数位数</param>
    public static string FormatNumber(float formatValue, int iDigit)
    {
        int dt = iDigit;
        string rStr = "0";

        if (dt < 0) dt = 0;

        try
        {
            if (formatValue != 0)
            {
                rStr = formatValue.ToString("n" + dt);
            }
            return rStr;
        }
        catch
        {
            return "0";
        }
    }


    //格式化Bool值为1或0
    public static int FormatBool(bool formatBool)
    {
        return (formatBool == true ? 1 : 0);
    }

    #endregion

    //生成随机临时流水号
    public static string GenRandomBarbs()
    {
        string dn = "ls";
        Random r = new Random();
        dn += DateTime.Now.Second.ToString() + r.Next(1000000).ToString();
        return dn;
    }

    //若字串为空，则返回最大字符串
    public static string GetMaxString(string formatStr)
    {
        string retStr = Common.FormatParameter(formatStr);

        if (retStr == "")
        {
            retStr = ""; // 字符串中为 8个 ASCII 为 255 的字符，作为最大字符串
        }

        return retStr;
    }


    /// <summary>
    /// 获取筛选条件语句
    /// </summary>
    /// <param name="FieldName">筛选字段名</param>
    /// <param name="Formula">条件值</param>
    /// <returns>生成后的筛选条件语句</returns>
    public static string GetOptCondition(string FieldName, string Formula)
    {
        string rtnString = "";

        if (Formula != "")
        {
             rtnString = rtnString + string.Format(" (" + FieldName +"= '{0}') ", Formula);
        }

        return rtnString;
    }

    /// <summary>
    /// 获取筛选条件语句
    /// </summary>
    /// <param name="FieldName">筛选字段名</param>
    /// <param name="Operator">比较运算符:=,<,>,<=,>=,!=,LIKE 等</param>
    /// <param name="Formula">条件值</param>
    /// <returns>生成后的筛选条件语句</returns>
    public static string GetOptCondition(string FieldName, string Operator, string Formula)
    {
        string rtnString = "";

        if ((Operator != "") || (Formula != ""))
        {
            if (Formula != "")
            {
                switch (Operator.Trim().ToUpper())
                {
                    case "=":
                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                    case "!=":
                        rtnString = rtnString + string.Format(" (" + FieldName + " " + Operator + " '{0}') ", Formula);
                        break;
                    case "LIKE":
                        rtnString = rtnString + string.Format(" (" + FieldName + " LIKE '{0}%') ", Formula);
                        break;
                }
            }
        }

        return rtnString;
    }

    /// <summary>
    /// 获取区间筛选条件语句（仅有起始或终止区间值，则返回大于或小于的条件语句）
    /// </summary>
    /// <param name="FieldName">筛选字段名</param>
    /// <param name="Str1">起始区间值</param>
    /// <param name="Str2">终止区间值</param>
    /// <returns>生成后的区间筛选条件语句</returns>
    public static string GetBetweenCondition(string FieldName, string Str1, string Str2)
    {
        string rtnString = "";

        if ((Str1 != "") || (Str2 != ""))
        {
            if ((Str1 != "") && (Str2 != ""))
            {
                rtnString = rtnString + string.Format(" (" + FieldName + " BETWEEN '{0}' AND '{1}') ", Str1, Str2);
            }
            else if (Str1 != "")
            {
                rtnString = rtnString + string.Format(FieldName + " >= '{0}'", Str1);
            }
            else
            {
                rtnString = rtnString + string.Format(FieldName + " <= '{0}'", Str2);
            }

        }

        return rtnString;
    }

    /// <summary>
    /// 根据筛选条件生成总筛选条件语句
    /// </summary>
    /// <param name="sTotalCondition">总的筛选条件语句</param>
    /// <param name="sSonCondition">子筛选条件语句</param>
    /// <returns>生成后的总筛选条件语句</returns>
    public static string MakeSQLCondition(string sTotalCondition, string sSonCondition)
    {
        string rtnSQLString = "";

        if (sSonCondition != "")
        {
            if (sTotalCondition != "")
                rtnSQLString = sTotalCondition + " AND " + sSonCondition;
            else
                rtnSQLString = sTotalCondition + sSonCondition;
        }
        else
        {
            rtnSQLString = sTotalCondition;     // 无子条件直接返回原总的筛选条件
        }

        return rtnSQLString;
    }

    #region --------------- 小写金额转大写 ------------------------------------------------------------
    /// <summary>金额转大写
    /// </summary>
    /// <param name="LowerMoney">小写金额</param>
    /// <returns></returns>
    public static string MoneyToChinese(string LowerMoney)
    {
        string functionReturnValue = null;
        bool IsNegative = false; // 是否是负数
        if (LowerMoney.Trim().Substring(0, 1) == "-")
        {
            // 是负数则先转为正数
            LowerMoney = LowerMoney.Trim().Remove(0, 1);
            IsNegative = true;
        }
        string strLower = null;
        string strUpart = null;
        string strUpper = null;
        int iTemp = 0;
        // 保留两位小数 123.489→123.49　　123.4→123.4
        LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
        if (LowerMoney.IndexOf(".") > 0)
        {
            if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
            {
                LowerMoney = LowerMoney + "0";
            }
        }
        else
        {
            LowerMoney = LowerMoney + ".00";
        }
        strLower = LowerMoney;
        iTemp = 1;
        strUpper = "";
        while (iTemp <= strLower.Length)
        {
            switch (strLower.Substring(strLower.Length - iTemp, 1))
            {
                case ".":
                    strUpart = "元";
                    break;
                case "0":
                    strUpart = "零";
                    break;
                case "1":
                    strUpart = "壹";
                    break;
                case "2":
                    strUpart = "贰";
                    break;
                case "3":
                    strUpart = "叁";
                    break;
                case "4":
                    strUpart = "肆";
                    break;
                case "5":
                    strUpart = "伍";
                    break;
                case "6":
                    strUpart = "陆";
                    break;
                case "7":
                    strUpart = "柒";
                    break;
                case "8":
                    strUpart = "捌";
                    break;
                case "9":
                    strUpart = "玖";
                    break;
            }

            switch (iTemp)
            {
                case 1:
                    strUpart = strUpart + "分";
                    break;
                case 2:
                    strUpart = strUpart + "角";
                    break;
                case 3:
                    strUpart = strUpart + "";
                    break;
                case 4:
                    strUpart = strUpart + "";
                    break;
                case 5:
                    strUpart = strUpart + "拾";
                    break;
                case 6:
                    strUpart = strUpart + "佰";
                    break;
                case 7:
                    strUpart = strUpart + "仟";
                    break;
                case 8:
                    strUpart = strUpart + "万";
                    break;
                case 9:
                    strUpart = strUpart + "拾";
                    break;
                case 10:
                    strUpart = strUpart + "佰";
                    break;
                case 11:
                    strUpart = strUpart + "仟";
                    break;
                case 12:
                    strUpart = strUpart + "亿";
                    break;
                case 13:
                    strUpart = strUpart + "拾";
                    break;
                case 14:
                    strUpart = strUpart + "佰";
                    break;
                case 15:
                    strUpart = strUpart + "仟";
                    break;
                case 16:
                    strUpart = strUpart + "万";
                    break;
                default:
                    strUpart = strUpart + "";
                    break;
            }

            strUpper = strUpart + strUpper;
            iTemp = iTemp + 1;
        }

        strUpper = strUpper.Replace("零拾", "零");
        strUpper = strUpper.Replace("零佰", "零");
        strUpper = strUpper.Replace("零仟", "零");
        strUpper = strUpper.Replace("零零零", "零");
        strUpper = strUpper.Replace("零零", "零");
        strUpper = strUpper.Replace("零角零分", "整");
        strUpper = strUpper.Replace("零分", "整");
        strUpper = strUpper.Replace("零角", "零");
        strUpper = strUpper.Replace("零亿零万零元", "亿元");
        strUpper = strUpper.Replace("亿零万零元", "亿元");
        strUpper = strUpper.Replace("零亿零万", "亿");
        strUpper = strUpper.Replace("零万零元", "万元");
        strUpper = strUpper.Replace("零亿", "亿");
        strUpper = strUpper.Replace("零万", "万");
        strUpper = strUpper.Replace("零元", "元");
        strUpper = strUpper.Replace("零零", "零");

        // 对壹元以下的金额的处理
        if (strUpper.Substring(0, 1) == "元")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "零")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "角")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "分")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "整")
        {
            strUpper = "零元整";
        }
        functionReturnValue = strUpper;

        if (IsNegative == true)
        {
            return "负" + functionReturnValue;
        }
        else
        {
            return functionReturnValue;
        }
    }
    #endregion

    /// <summary>
    /// DataTable直接输出Excel
    /// </summary>
    /// <param name="dtData">DataTable数据表对象</param>
    /// <param name="FileName">XLS文件名</param>
    public static void DataTable2Excel(System.Data.DataTable dtData, string FileName)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        // 当前对话
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;

        if (dtData != null)
        {
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            curContext.Response.Charset = "";
            // 导出excel文件
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid
            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            // 返回客户端
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }

    #region SqlServer数据类型、C# SqlDbType对应关系及转换 -----【暂时未用】
    // SqlDbType转换为C#数据类型
    public static Type SqlType2CsharpType(SqlDbType sqlType)
    {
        switch (sqlType)
        {
            case SqlDbType.BigInt:
                return typeof(Int64);
            case SqlDbType.Binary:
                return typeof(Object);
            case SqlDbType.Bit:
                return typeof(Boolean);
            case SqlDbType.Char:
                return typeof(String);
            case SqlDbType.DateTime:
                return typeof(DateTime);
            case SqlDbType.Decimal:
                return typeof(Decimal);
            case SqlDbType.Float:
                return typeof(Double);
            case SqlDbType.Image:
                return typeof(Object);
            case SqlDbType.Int:
                return typeof(Int32);
            case SqlDbType.Money:
                return typeof(Decimal);
            case SqlDbType.NChar:
                return typeof(String);
            case SqlDbType.NText:
                return typeof(String);
            case SqlDbType.NVarChar:
                return typeof(String);
            case SqlDbType.Real:
                return typeof(Single);
            case SqlDbType.SmallDateTime:
                return typeof(DateTime);
            case SqlDbType.SmallInt:
                return typeof(Int16);
            case SqlDbType.SmallMoney:
                return typeof(Decimal);
            case SqlDbType.Text:
                return typeof(String);
            case SqlDbType.Timestamp:
                return typeof(Object);
            case SqlDbType.TinyInt:
                return typeof(Byte);
            case SqlDbType.Udt://自定义的数据类型
                return typeof(Object);
            case SqlDbType.UniqueIdentifier:
                return typeof(Object);
            case SqlDbType.VarBinary:
                return typeof(Object);
            case SqlDbType.VarChar:
                return typeof(String);
            case SqlDbType.Variant:
                return typeof(Object);
            case SqlDbType.Xml:
                return typeof(Object);
            default:
                return null;
        }
    }

    // sql server数据类型（如：varchar） 转换为SqlDbType类型
    public static SqlDbType SqlTypeString2SqlType(string sqlTypeString)
    {
        SqlDbType dbType = SqlDbType.Variant;  //默认为Object

        switch (sqlTypeString)
        {
            case "int":
                dbType = SqlDbType.Int;
                break;
            case "varchar":
                dbType = SqlDbType.VarChar;
                break;
            case "bit":
                dbType = SqlDbType.Bit;
                break;
            case "datetime":
                dbType = SqlDbType.DateTime;
                break;
            case "decimal":
                dbType = SqlDbType.Decimal;
                break;
            case "float":
                dbType = SqlDbType.Float;
                break;
            case "image":
                dbType = SqlDbType.Image;
                break;
            case "money":
                dbType = SqlDbType.Money;
                break;
            case "ntext":
                dbType = SqlDbType.NText;
                break;
            case "nvarchar":
                dbType = SqlDbType.NVarChar;
                break;
            case "smalldatetime":
                dbType = SqlDbType.SmallDateTime;
                break;
            case "smallint":
                dbType = SqlDbType.SmallInt;
                break;
            case "text":
                dbType = SqlDbType.Text;
                break;
            case "bigint":
                dbType = SqlDbType.BigInt;
                break;
            case "binary":
                dbType = SqlDbType.Binary;
                break;
            case "char":
                dbType = SqlDbType.Char;
                break;
            case "nchar":
                dbType = SqlDbType.NChar;
                break;
            case "numeric":
                dbType = SqlDbType.Decimal;
                break;
            case "real":
                dbType = SqlDbType.Real;
                break;
            case "smallmoney":
                dbType = SqlDbType.SmallMoney;
                break;
            case "sql_variant":
                dbType = SqlDbType.Variant;
                break;
            case "timestamp":
                dbType = SqlDbType.Timestamp;
                break;
            case "tinyint":
                dbType = SqlDbType.TinyInt;
                break;
            case "uniqueidentifier":
                dbType = SqlDbType.UniqueIdentifier;
                break;
            case "varbinary":
                dbType = SqlDbType.VarBinary;
                break;
            case "xml":
                dbType = SqlDbType.Xml;
                break;
        }
        return dbType;
    }

    //// SqlDbType类型转换为sql server数据类型字串（如：varchar） 
    public static string SqlType2SqlTypeString(SqlDbType sqlType)
    {
        switch (sqlType)
        {
            case SqlDbType.BigInt:
                return "bigint";
            case SqlDbType.Binary:
                return "binary";
            case SqlDbType.Bit:
                return "bit";
            case SqlDbType.Char:
                return "char";
            case SqlDbType.DateTime:
                return "datetime";
            case SqlDbType.Decimal:
                return "decimal";
            case SqlDbType.Float:
                return "float";
            case SqlDbType.Image:
                return "image";
            case SqlDbType.Int:
                return "int";
            case SqlDbType.Money:
                return "money";
            case SqlDbType.NChar:
                return "nchar";
            case SqlDbType.NText:
                return "ntext";
            case SqlDbType.NVarChar:
                return "nvarchar";
            case SqlDbType.Real:
                return "real";
            case SqlDbType.SmallDateTime:
                return "smalldatetime";
            case SqlDbType.SmallInt:
                return "smallint";
            case SqlDbType.SmallMoney:
                return "smallmoney";
            case SqlDbType.Text:
                return "text";
            case SqlDbType.Timestamp:
                return "timestamp";
            case SqlDbType.TinyInt:
                return "tinyint";
            case SqlDbType.Udt://自定义的数据类型
                return "udt";
            case SqlDbType.UniqueIdentifier:
                return "uniqueidentifier";
            case SqlDbType.VarBinary:
                return "varbinary";
            case SqlDbType.VarChar:
                return "varchar";
            case SqlDbType.Variant:
                return "sql_variant";
            case SqlDbType.Xml:
                return "xml";
            default:
                return "";
        }
    }


    // sql server中的数据类型，转换为C#中的类型类型
    public static Type SqlTypeString2CsharpType(string sqlTypeString)
    {
        SqlDbType dbTpe = SqlTypeString2SqlType(sqlTypeString);

        return SqlType2CsharpType(dbTpe);
    }

    // 将sql server中的数据类型，转化为C#中的类型的字符串
    public static string SqlTypeString2CsharpTypeString(string sqlTypeString)
    {
        Type type = SqlTypeString2CsharpType(sqlTypeString);

        return type.Name;
    }
    #endregion



    //数据库操作错误信息显示
    public static void ShowDBErrMsg(int flag, string msg)
    {
        string ErrMsg = "";
        HSql hs = new HSql();

        hs.Open("SELECT Top 1 msg_desc FROM [msg_mstr] WHERE msg_id="+flag.ToString());

        if (hs.NextRow())
        {
            if (msg != "") ErrMsg = "【" + msg + "】 ";

            ErrMsg = ErrMsg + hs["msg_desc"].ToString().Trim(); 
        }
        else
        {
            ErrMsg = "发生未知类型错误：" + flag.ToString();
        }

        if (ErrMsg != "")
        {
            Jscript.AlertAndGoBack(" 错误提示： " + ErrMsg);
        }
    }


    //数据库操作错误信息显示【未用】
    public static void ShowDBErrMsg_OLD(int flag, string msg)
    {
        string ErrMsg = "";

        switch (flag)
        {
            case 203201:
                ErrMsg = "更改前的物料编码在物料表中有定义！";
                break;
            case 203202:
                ErrMsg = "更改后的物料编码在物料表中无定义！";
                break;
            case 203203:
                ErrMsg = "更改【" + msg + "】时出现错误，请与系统管理员联系！";
                break;
            case 203205:
                ErrMsg =  msg + "表中存在未定义物料编码！";
                break;

            case 330010:
                ErrMsg = "【" + msg + "】不存在此客户指定货币的应收期初数据！";
                break;
            case 330011:
                ErrMsg = "【" + msg + "】该客户已存在早于此期间的应收期初数据，不允许再做应收期初审核！";
                break;
            case 330012:
                ErrMsg = "【" + msg + "】不存在此客户的该现行期间的应收期初数据！";
                break;

            case 409210:
                ErrMsg = "【" + msg + "】当前用户没有此库位的权限！";
                break;

            case 501201:
                ErrMsg = "参数传递错误！";
                break;
            case 501202:
                ErrMsg = "退货单不存在！";
                break;
            case 501204:
                ErrMsg = "该单据已经过帐！";
                break;
            case 501205:
                ErrMsg = "该单据未过帐！";
                break;

            case 620101:
                ErrMsg = "月结日历未定义！";
                break;
            //case 620104:
            //    ErrMsg = "【" + msg + "】 该日期所属的日期期间已帐务冻结！";
            //    break;

            case 704500:
                ErrMsg = "【" + msg + "】库存数量不足，库存不允许为负数！";
                break;
            case 704501:
                ErrMsg = "【" + msg + "】备品数量超出允许的比例！";
                break;				
            case 704502:
                ErrMsg = "【" + msg + "】送货数量超出允许的比例！";
                break;
            case 704503:
                ErrMsg = "【" + msg + "】送货单不存在！";
                break;
            case 704504:
                ErrMsg = "该单据已经过帐！";
                break;
            case 704505:
                ErrMsg = "该单据未过帐！";
                break;
            case 704506:
                ErrMsg = "【" + msg + "】客户订单 已经取消，请检查！";
                break;
            case 704507:
                ErrMsg = "【" + msg + "】客户订单 已经关闭，请检查！";
                break;
            case 704508:
                ErrMsg = "【" + msg + "】客户订单中的客户与送货单中的客户不一致！";
                break;
            case 704509:
                ErrMsg = "【" + msg + "】客户订单中的地点与送货单中的地点不一致！";
                break;

            case 704510:
                ErrMsg = "退货单不存在！";
                break;
            case 704511:
                ErrMsg = "【" + msg + "】客户订单中的客户与退货单中的客户不一致！";
                break;
            case 704512:
                ErrMsg = "【" + msg + "】客户订单中的地点与退货单中的地点不一致！";
                break;
            case 704513:
                ErrMsg = "【" + msg + "】客户订单明细不存在，请检查！";
                break;
            case 704514:
                ErrMsg = "【" + msg + "】退货数量大于送货数量，请检查！";
                break;

            case 01040201:
                ErrMsg = "将要复制的物料编码已存在 ！";
                break;
            case 01040202:
                ErrMsg = "需要复制的物料编码不存在！";
                break;


            case 04010201:
                ErrMsg = "更改前的客户编码在客户表中未定义 ！";
                break;
            case 04010202:
                ErrMsg = "更改后的客户编码在客户表中已经定义！";
                break;

            //case 790102:
            //    ErrMsg = "输入参数不合法！";
            //    break;
            case 790103:
                ErrMsg = "【" + msg + "】 物料编号未定义！";
                break;
            case 790104:
                ErrMsg = "【" + msg + "】 地点未定义！";
                break;
            case 790105:
                ErrMsg = "【" + msg + "】 地点/库位未定义！";
                break;
            case 790107:
                ErrMsg = "出入库类型错误！";
                break;
            case 790108:
                ErrMsg = "【" + msg + "】 库存不允许为负数！";
                break;
            //case 790109:
            //    ErrMsg = "【" + msg + "】 超出系统有效日期范围！";
            //    break;
            //case 790110:
            //    ErrMsg = "【" + msg + "】 月结日历定义错误，该日期不属于任何期间！";
            //    break;
            case 790112:
                ErrMsg = "【" + msg + "】 兑换率未设置！";
                break;

            case 80001:
                ErrMsg = "此期间已经关帐！";
                break;
            case 89903:
                ErrMsg = "月结日历未定义！";
                break;


            // ERP 3.0
            case 90030601:
                ErrMsg = "【" + msg + "】 单据编号生成规则设置错误！";
                break;
            case 90030603:
                ErrMsg = "单据编码规则表名设置错误！";
                break;
            case 90030604:
                ErrMsg = "单据编码规则字段名设置错误！";
                break;



            default:
                ErrMsg = "发生未知类型错误！";
                break;

        }
        if (ErrMsg != "") 
        {
            Jscript.AlertAndGoBack(ErrMsg);
        }
    }

}
