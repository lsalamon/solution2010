using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace STalk.Lib
{
    /// <summary>
    /// 公用方法类
    /// </summary>
    public class Function
    {
        /// <summary>
        /// html目录位置
        /// </summary>
        public static string GetViewPath(string fileName)
        {
            string ViewPath = AppDomain.CurrentDomain.BaseDirectory + "View\\";
            return string.Concat(ViewPath, fileName);
        }

        public static string EncryptMD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
}
