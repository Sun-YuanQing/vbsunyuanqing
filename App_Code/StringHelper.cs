﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; 

/// <summary>
///StringHelper 的摘要说明
/// </summary>
public class StringHelper
{
	public StringHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    #region String length formatter

    /// <summary>   
    /// 对字符串进行裁剪   
    /// </summary>   
    public static string Trim(string stringTrim, int maxLength)
    {
        return Trim(stringTrim, maxLength, "...");
    }

    /// <summary>   
    /// 对字符串进行裁剪(区分单字节及双字节字符)   
    /// </summary>   
    /// <param name="rawString">需要裁剪的字符串</param>   
    /// <param name="maxLength">裁剪的长度，按双字节计数</param>   
    /// <param name="appendString">如果进行了裁剪需要附加的字符</param>   
    public static string Trim(string rawString, int maxLength, string appendString)
    {
        if (string.IsNullOrEmpty(rawString) || rawString.Length <= maxLength)
        {
            return rawString;
        }
        else
        {
            int rawStringLength = Encoding.UTF8.GetBytes(rawString).Length;
            if (rawStringLength <= maxLength * 2)
                return rawString;
        }

        int appendStringLength = Encoding.UTF8.GetBytes(appendString).Length;
        StringBuilder checkedStringBuilder = new StringBuilder();
        int appendedLenth = 0;
        for (int i = 0; i < rawString.Length; i++)
        {
            char _char = rawString[i];
            checkedStringBuilder.Append(_char);

            appendedLenth += Encoding.Default.GetBytes(new char[] { _char }).Length;

            if (appendedLenth >= maxLength * 2 - appendStringLength)
                break;
        }

        return checkedStringBuilder.ToString() + appendString;
    }


    #endregion

    #region 特殊字符

    /// <summary>   
    /// 检测是否有Sql危险字符   
    /// </summary>   
    /// <param name="str">要判断字符串</param>   
    /// <returns>判断结果</returns>   
    public static bool IsSafeSqlString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }

    /// <summary>   
    /// 删除SQL注入特殊字符   
    /// --- 20070622 加入对输入参数sql为Null的判断   
    /// </summary>   
    public static string StripSQLInjection(string sql)
    {
        if (!string.IsNullOrEmpty(sql))
        {
            //过滤 ' --   
            string pattern1 = @"(\%27)|(\')|(\-\-)";

            //防止执行 ' or   
            string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

            //防止执行sql server 内部存储过程或扩展存储过程   
            string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

            sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
            sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
        }
        return sql;
    }

    public static string SQLSafe(string Parameter)
    {
        Parameter = Parameter.ToLower();
        Parameter = Parameter.Replace("'", "");
        Parameter = Parameter.Replace(">", ">");
        Parameter = Parameter.Replace("<", "<");
        Parameter = Parameter.Replace("\n", "<br>");
        Parameter = Parameter.Replace("\0", "·");
        return Parameter;
    }

    /// <summary>   
    /// 清除xml中的不合法字符   
    /// </summary>   
    /// <remarks>   
    /// 无效字符：   
    /// 0x00 - 0x08   
    /// 0x0b - 0x0c   
    /// 0x0e - 0x1f   
    /// </remarks>   
    public static string CleanInvalidCharsForXML(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        else
        {
            StringBuilder checkedStringBuilder = new StringBuilder();
            Char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int charValue = Convert.ToInt32(chars[i]);

                if ((charValue >= 0x00 && charValue <= 0x08) || (charValue >= 0x0b && charValue <= 0x0c) || (charValue >= 0x0e && charValue <= 0x1f))
                    continue;
                else
                    checkedStringBuilder.Append(chars[i]);
            }

            return checkedStringBuilder.ToString();

            //string result = checkedStringBuilder.ToString();   
            //result = result.Replace("&#x0;", "");   
            //return Regex.Replace(result, @"[\u0000-\u0008\u000B\u000C\u000E-\u001A\uD800-\uDFFF]", delegate(Match m) { int code = (int)m.Value.ToCharArray()[0]; return (code > 9 ? "&#" + code.ToString() : "&#0" + code.ToString()) + ";"; });   
        }
    }


    /// <summary>   
    /// 改正sql语句中的转义字符   
    /// </summary>   
    public static string mashSQL(string str)
    {
        return (str == null) ? "" : str.Replace("\'", "'");
    }

    /// <summary>   
    /// 替换sql语句中的有问题符号   
    /// </summary>   
    public static string ChkSQL(string str)
    {
        return (str == null) ? "" : str.Replace("'", "''");
    }

    /// <summary>   
    ///  判断是否有非法字符   
    /// </summary>   
    /// <param name="strString"></param>   
    /// <returns>返回TRUE表示有非法字符，返回FALSE表示没有非法字符。</returns>   
    public static bool CheckBadStr(string strString)
    {
        bool outValue = false;
        if (strString != null && strString.Length > 0)
        {
            string[] bidStrlist = new string[9];
            bidStrlist[0] = "'";
            bidStrlist[1] = ";";
            bidStrlist[2] = ":";
            bidStrlist[3] = "%";
            bidStrlist[4] = "@";
            bidStrlist[5] = "&";
            bidStrlist[6] = "#";
            bidStrlist[7] = "\"";
            bidStrlist[8] = "net user";
            bidStrlist[9] = "exec";
            bidStrlist[10] = "net localgroup";
            bidStrlist[11] = "select";
            bidStrlist[12] = "asc";
            bidStrlist[13] = "char";
            bidStrlist[14] = "mid";
            bidStrlist[15] = "insert";
            bidStrlist[19] = "order";
            bidStrlist[20] = "exec";
            bidStrlist[21] = "delete";
            bidStrlist[22] = "drop";
            bidStrlist[23] = "truncate";
            bidStrlist[24] = "xp_cmdshell";
            bidStrlist[25] = "<";
            bidStrlist[26] = ">";
            string tempStr = strString.ToLower();
            for (int i = 0; i < bidStrlist.Length; i++)
            {
                if (tempStr.IndexOf(bidStrlist[i]) != -1)
                //if (tempStr == bidStrlist[i])   
                {
                    outValue = true;
                    break;
                }
            }
        }
        return outValue;
    }

    #endregion

    #region Tools
    /// <summary>   
    /// 去掉最后一个逗号   
    /// </summary>   
    /// <param name="String">要做处理的字符串</param>   
    /// <returns>去掉最后一个逗号的字符串</returns>   
    public static string DelLastComma(string String)
    {
        if (String.IndexOf(",") == -1)
        {
            return String;
        }
        return String.Substring(0, String.LastIndexOf(","));
    }

    /// <summary>   
    /// 删除最后一个字符   
    /// </summary>   
    /// <param name="str"></param>   
    /// <returns></returns>   
    public static string ClearLastChar(string str)
    {
        return (str == "") ? "" : str.Substring(0, str.Length - 1);
    }

    /// <summary>   
    /// html编码   
    /// </summary>   
    /// <param name="chr"></param>   
    /// <returns></returns>   
    public static string html_text(string chr)
    {
        if (chr == null)
            return "";
        chr = chr.Replace("'", "''");
        chr = chr.Replace("<", "<");
        chr = chr.Replace(">", ">");
        return (chr);
    }

    /// <summary>   
    /// html解码   
    /// </summary>   
    /// <param name="chr"></param>   
    /// <returns></returns>   
    public static string text_html(string chr)
    {
        if (chr == null)
            return "";
        chr = chr.Replace("<", "<");
        chr = chr.Replace(">", ">");
        return (chr);
    }

    public static bool JustifyStr(string strValue)
    {
        bool flag = false;
        char[] str = "^<>'=&*, ".ToCharArray(0, 8);
        for (int i = 0; i < 8; i++)
        {
            if (strValue.IndexOf(str[i]) != -1)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }

    public static string CheckOutputString(string key)
    {
        string OutputString = string.Empty;
        OutputString = key.Replace("<br>", "\n").Replace("<", "<").Replace(">", ">").Replace(" ", " ");
        return OutputString;

    }

    #endregion


    #region  字符串相关处理

    /// <summary>
    /// 将传入的Request.QueryString["XXX"]转化成字符串
    /// </summary>
    /// <param name="queryString">Request.QueryString["XXX"]</param>
    /// <returns>返回字符串</returns>
    public static string GetString(string queryString)
    {
        string tempString = "";

        if (!System.String.IsNullOrEmpty(queryString))
        {
            tempString = queryString.ToString();
        }

        return tempString;
    }

    /// <summary>
    /// 对输入框的特殊字串进行过滤，防止SQL注入
    /// </summary>
    /// <param name="strFromText">要被过滤的字符串</param>
    /// <returns>过滤后的字符串</returns>
    public static string SqlInsertEncode(string strFromText)
    {
        if (!System.String.IsNullOrEmpty(strFromText) && strFromText != "")
        {
            //strFromText = strFromText.Replace(";", "&#59;");
            strFromText = strFromText.Replace("!", "&#33;");
            strFromText = strFromText.Replace("@", "&#64;");
            strFromText = strFromText.Replace("$", "&#36;");
            //strFromText = strFromText.Replace("*", "&#42;");
            //strFromText = strFromText.Replace("(", "&#40;");
            //strFromText = strFromText.Replace(")", "&#41;");
            //strFromText = strFromText.Replace("-", "&#45;");
            //strFromText = strFromText.Replace("+", "&#43;");
            //strFromText = strFromText.Replace("=", "&#61;");
            //strFromText = strFromText.Replace("|", "|");
            //strFromText = strFromText.Replace("\\", "&#92;");
            //strFromText = strFromText.Replace("/", "&#47;");
            //strFromText = strFromText.Replace(":", "&#58;");
            //strFromText = strFromText.Replace("\"", "&#34;");
            strFromText = strFromText.Replace("'", "&#39;");
            strFromText = strFromText.Replace("<", "&#60;");
            strFromText = strFromText.Replace(">", "&#62;");
            //strFromText = strFromText.Replace(" ", "&#32;");
        }

        return strFromText;
    }

    /// <summary>
    /// 格式化从数据库中取出的字符串以显示到页面上
    /// </summary>
    /// <param name="strContent">从数据库中取出的字符串</param>
    /// <returns>可显示在页面上的字符串</returns>
    public static string HtmlShowEncode(string strContent)
    {
        if (!System.String.IsNullOrEmpty(strContent) && strContent != "")
        {
            strContent = strContent.Replace(" ", "&nbsp;");
            strContent = strContent.Replace("&#32;", "&nbsp;");
            strContent = strContent.Replace("\t", "&nbsp;&nbsp;");
            strContent = strContent.Replace("\r\n", "<br />");
        }

        return strContent;
    }

    /// <summary>
    /// 对查询字符串进行过滤
    /// </summary>
    /// <param name="strKeyWords">要被过滤的查询字符串</param>
    /// <returns>过滤后的字符串</returns>
    public static string SqlQueryEncode(string strKeyWords)
    {
        if (!System.String.IsNullOrEmpty(strKeyWords) && strKeyWords != "")
        {
            strKeyWords = strKeyWords.Replace("'", "");
            strKeyWords = strKeyWords.Replace("[", "[[]");
            strKeyWords = strKeyWords.Replace("_", "[_]");
            strKeyWords = strKeyWords.Replace("&", "[&]");
            strKeyWords = strKeyWords.Replace("#", "[#]");
            strKeyWords = strKeyWords.Replace("%", "[%]");
        }

        return strKeyWords;
    }

    /// <summary>
    /// 对SqlQueryEncode函数过滤后的字符串进行还原
    /// </summary>
    /// <param name="strKeyWords">经过SqlQueryEncode过滤后的字符串</param>
    /// <returns>还原后的字符串</returns>
    public static string SqlQueryDecode(string strKeyWords)
    {
        if (!System.String.IsNullOrEmpty(strKeyWords) && strKeyWords != "")
        {
            strKeyWords = strKeyWords.Replace("[[]", "[");
            strKeyWords = strKeyWords.Replace("[_]", "_");
            strKeyWords = strKeyWords.Replace("[&]", "&");
            strKeyWords = strKeyWords.Replace("[#]", "#");
            strKeyWords = strKeyWords.Replace("[%]", "%");
        }

        return strKeyWords;
    }

    #endregion
}
