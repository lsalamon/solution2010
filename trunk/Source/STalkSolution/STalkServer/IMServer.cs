using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using log4net;
using STalkServer.Client;
using STalkServer.Lib;

namespace STalkServer
{
    /// <summary>
    /// 服务类
    /// </summary>
    public class IMServer
    {
        private ILog m_Log = LogManager.GetLogger("Server");
        private Socket m_ListenSocket;
        private IPEndPoint m_LocalEndPoint = new IPEndPoint(IPAddress.Any, 5222);
        private bool m_IsListening = false;
        private Thread m_Thread;
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private int m_MaxConnections = 2000; //最大客户端

        #region 构造函数
        public IMServer()
        {

        }

        public IMServer(int port)
        {
            m_LocalEndPoint = new IPEndPoint(IPAddress.Any, port);
        }

        public IMServer(IPEndPoint locEndPoint)
        {
            m_LocalEndPoint = locEndPoint;
        }
        #endregion

        /// <summary>
        /// 设置/获取最大连接数
        /// </summary>
        public int MaxConnections
        {
            get { return m_MaxConnections; }
            set { m_MaxConnections = value; }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            m_Thread = new Thread(new ThreadStart(RunThread));
            m_Thread.Start();
        }

        private void RunThread()
        {
            m_ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                m_ListenSocket.Bind(m_LocalEndPoint);//监听本地端口
                m_ListenSocket.Listen(128); //挂起队列
                m_IsListening = true;
                //循环接受连接
                while (m_IsListening)
                {
                    allDone.Reset();
                    m_ListenSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                m_IsListening = false;
                m_Log.Error("服务启动失败!",ex);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            try
            {
                Socket socketHandler = m_ListenSocket.EndAccept(ar);
                m_Log.Info(string.Format("来自:{0}的连接...",socketHandler.RemoteEndPoint.ToString()));
               // Console.WriteLine("has connect:" + newSock.RemoteEndPoint.ToString());
               // XmppSeverConnection conn = new XmppSeverConnection(newSock);
            }
            catch { }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        { 
        }
    }
}
