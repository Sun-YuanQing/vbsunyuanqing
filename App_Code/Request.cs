using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace YYCMS
{
    public class Request
    {
        /// <summary>
        /// 获取get请求的变量值,返回string类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetQueryString(string name)
        {
            string values = "";
            try
            {
                values = HttpContext.Current.Request.QueryString[name].ToString();
            }
            catch
            {
                values = "";
            }
            return values;
        }

        /// <summary>
        /// 获取get请求的变量值，返回int类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetQueryInt(string name,int defaultValue)
        {
            int values = defaultValue;
            try
            {
                string _values = HttpContext.Current.Request.QueryString[name].ToString();
                if (!string.IsNullOrEmpty(_values))
                {
                    values = Convert.ToInt32(_values);
                }
            }
            catch
            {
                values = defaultValue;
            }

            return values;
        }

        /// <summary>
        /// 获取post请求的变量值，返回string类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFormString(string name)
        {
            string values ="";
            try
            {
                values = HttpContext.Current.Request.Form[name].ToString();
            }
            catch
            {
                values ="";
            }
            return values;
        }

        /// <summary>
        /// 获取post请求的变量值，返回string类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetFormInt(string name, int defaultValue)
        {
            int values = defaultValue;
            try
            {
                values =Convert.ToInt32(HttpContext.Current.Request.Form[name]);
            }
            catch
            {
                values = defaultValue;
            }
            return values;
        }

        /// <summary>
        /// 获取int类型的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetPageInt(string name, int defaultValue)
        {
            int values = defaultValue;
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[name]))
                {
                    values = Convert.ToInt32(HttpContext.Current.Request.Form[name]);
                }
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[name]))
                {
                    values = Convert.ToInt32(HttpContext.Current.Request.QueryString[name]);
                }
                else
                {
                    values = defaultValue;
                }
            }
            catch
            {
                values = defaultValue;
            }
            return values;
        }

        /// <summary>
        /// 获取string类型的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetPageString(string name)
        {
            string values ="";
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[name]))
                {
                    values = HttpContext.Current.Request.Form[name].ToString();
                }
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[name]))
                {
                    values = HttpContext.Current.Request.QueryString[name].ToString();
                }
                else
                {
                    values = "";
                }
            }
            catch
            {
                values ="";
            }
            return values;
        } 


        
    }
}
