using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace STalkServer
{
    /// <summary>
    /// 客户端工厂,存储全局数据
    /// </summary>
    class ClientFactory
    {
        /// <summary>
        /// 用户集合
        /// </summary>
        private static Dictionary<string, ClientConnection> m_Clients = new Dictionary<string, ClientConnection>();
        private static object m_Status = new object();

        /// <summary>
        /// 添加客户端到集合
        /// </summary>
        /// <param name="client"></param>
        public static void AddClient(ClientConnection client)
        {
            lock (m_Status)
            {
                m_Clients.Add(client.JID.ToString(), client);
            }
        }

        /// <summary>
        /// 在线客户端个数
        /// </summary>
        public static int ClientCount
        {
            get { return m_Clients.Count; }
        }

        /// <summary>
        /// 删除客户端对象
        /// </summary>
        /// <param name="jid"></param>
        public static void RemoveClient(string jid)
        {
            lock (m_Status)
            {
                m_Clients.Remove(jid);
            }
        }
    }
}
