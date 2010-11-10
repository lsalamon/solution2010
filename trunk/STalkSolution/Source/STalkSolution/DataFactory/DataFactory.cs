using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using STalk.DataModule;
using STalk.IDataProvider;

namespace STalk.DataFactory
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public class DataFactory
    {
        private static readonly string m_ProviderName = ConfigurationManager.AppSettings["DataProvider"];
        private static Hashtable m_CacheProvider = Hashtable.Synchronized(new Hashtable());

        public static IUserProvider UserProvider
        {
            get {
                //查询缓存是否存在实例
                if (m_CacheProvider["UserProvider"] == null)
                {
                    string className = string.Concat(m_ProviderName, ".UserProvider");
                    m_CacheProvider.Add("UserProvider", (IUserProvider)Assembly.Load(m_ProviderName).CreateInstance(className));
                }

                return (IUserProvider)m_CacheProvider["UserProvider"];
            }
        }


    }
}
