using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;

    /// <summary>
    /// һЩ���õ�Js����
    /// <summary>
    public class Jscript
    {

        /// ����JavaScriptС����
        /// </summary>
        /// <param name="js">������Ϣ</param>
        public static void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);

            #endregion
        }

        /// <summary>
        /// ����JavaScriptС����,��������һ��
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public static void AlertAndGoBack(string message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');history.go(-1);</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// ����JS����
        /// </summary>
        /// <param name="ScriptCode">�ű�����</param>
        public static void RunJs(string ScriptCode)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    " + ScriptCode + ";</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ������Ϣ����ת���µ�URL
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        /// <param name="toURL">���ӵ�ַ</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }

        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// �رյ�ǰ���ڲ��ҷ���ֵ
        /// </summary>
        public static void CloseWindowReturnValues(string value)
        {
            #region

             System.Text.StringBuilder Str = new System.Text.StringBuilder();
             Str.Append("<Script language='JavaScript'type=\"text/javascript\">");
             Str.Append("var str='"+value+"';");
              Str.Append("top.returnValue=str;");
              Str.Append("top.close();</Script>");

            HttpContext.Current.Response.Write(Str.ToString());
            HttpContext.Current.Response.End();
            #endregion
        }


        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ��ָ����С���´���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="width">��</param>
        /// <param name="heigth">��</param>
        /// <param name="top">ͷλ��</param>
        /// <param name="left">��λ��</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url">���ӵ�ַ</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ��ָ����Сλ�õ�ģʽ�Ի���
        /// </summary>
        /// <param name="webFormUrl">���ӵ�ַ</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <param name="top">������λ��</param>
        /// <param name="left">������λ��</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, int width, int height)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }

        #region ���º�����ҳ��������AJAX�����,���õ������ű�
        /// <summary>
        /// Ajax�����ű� For ����AJAX�����ҳ
        /// AjaxAlert:�����Ի���
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="msg">�Ի�����ʾ��</param>
        public static void AjaxAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", string.Format("alert('{0}��')", msg), true);
        }

        /// <summary>
        /// Ajax�����ű� For ����AJAX�����ҳ
        /// �����Ի�����ת��URLҳ
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="msg">�Ի�����ʾ��</param>
        /// <param name="url">�����ĵ�ַ</param>
        public static void AjaxAlertAndLocationHref(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", string.Format("alert('{0}��');location.href='{1}';", msg, url), true);

        }
        /// <summary>
        /// Ajax�����ű� For ����AJAX�����ҳ
        /// JS���
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="js">��alert('test');</param>
        public static void AjaxRunJs(System.Web.UI.Page page, string js)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", string.Format("{0}", js), true);
        }

        /// <summary>
        /// Ajax�����ű� For ����AJAX�����ҳ
        /// JS���
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="js">��alert('test');</param>
        public static void AjaxEndRunJs(System.Web.UI.Page page, string js)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "ajaxjs", string.Format("{0}", js), true);
        }  


        /// <summary>
        /// Ajax�����ű� For ����AJAX�����ҳ
        /// ת���µ�URL
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="toURL">���ӵ�ַ</param>
        public static void AjaxRedirect(System.Web.UI.Page page, string toURL)
        {
            #region
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", string.Format("window.location.replace('{0}');", toURL), true);
            #endregion
        }

        /// <summary>
        ///  Ajax�����ű� For ����AJAX�����ҳ
        /// ��ʾģ̬����
        /// </summary>
        /// <param name="page">һ����this</param>
        /// <param name="webFormUrl">������ҳ���ַ</param>
        /// <param name="width">����ҳ��Ŀ��</param>
        /// <param name="height">����ҳ��ĸ߶�</param>
        public static void AjaxShowModalDialogWindow(System.Web.UI.Page page, string webFormUrl, int width, int height)
        {
            #region

            string CommandStr = "window.showModalDialog('{0}','tempdialog','dialogWidth:{1}px;dialogHeight:{2}px;center:yes;help=no;resizable:no;status:no;scroll=yes')";
            CommandStr = string.Format(CommandStr, webFormUrl, width, height);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", CommandStr, true);
            #endregion
        }

        #endregion        


        /// <summary>
        /// ��������ҳ��
        /// </summary>
        /// <param name="errStr">�������</param>
        public static void ToError(System.Web.UI.Page page,string errStr)
        {
            page.Response.Redirect(string.Format("Error.aspx?ErrStr={0}", errStr));
        }

        public static void BindDateCalendar(System.Web.UI.WebControls.TextBox txtDate)
        {
            txtDate.Attributes.Add("onclick", "setday(this)");
        }

        /// <summary>
        /// �趨Checkbox����TextBox�¼�,
        /// </summary>
        /// <param name="ChkObj">Checkbox�ؼ�</param>
        /// <param name="HtmlObjId">�ؼ�TextBox ID,���ID��,�ŷֿ�</param>
        public static void SetChkAttrib(System.Web.UI.WebControls.CheckBox ChkObj, string HtmlObjId)
        {
            #region
            string[] StrId = HtmlObjId.Split(',');
            string JsCode = "";
            for (int i = 0; i < StrId.Length; i++)
            {
                JsCode = JsCode + string.Format("EnableCtrl(this,'{0}');", StrId[i]);
            }
            ChkObj.Attributes.Add("onclick", JsCode);
            #endregion
        }      
    }
