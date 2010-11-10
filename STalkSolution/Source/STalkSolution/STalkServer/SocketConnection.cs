using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using STalkServer.Lib;
using XMPPProtocol;
using XMPPProtocol.Protocol;
using XMPPProtocol.Protocol.iq;
using XMPPProtocol.Protocol.iq.auth;
using XMPPProtocol.Protocol.iq.roster;
using XMPPProtocol.Protocol.iq.browse;
using XMPPProtocol.Protocol.client;
using XMPPProtocol.Protocol.x;
using XMPPProtocol.Xml;
using XMPPProtocol.Xml.Dom;

namespace STalkServer
{
    /// <summary>
    /// 客户端连接对象
    /// 每个连进的客户端为一个实例
    /// </summary>
    class SocketConnection
    {
        private StreamParser m_StreamParser;
        private Socket m_Socket;
        private NetworkStream m_NetStream;
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        private const int BUFFERSIZE = 1024;
        private Jid m_JID=null;
        private string m_SessionID = string.Empty;
        private byte[] m_Buffer = new byte[BUFFERSIZE];

        public Jid JID
        {
            get { return m_JID; }
            set { m_JID = value; }
        }

        public string RemoteHostIP
        {
            get { return m_Socket.RemoteEndPoint.ToString(); }
        }

        public string SessionID
        {
            get { return m_SessionID; }
        }

        public SocketConnection()
        {
            m_StreamParser = new StreamParser();
            m_StreamParser.OnStreamStart += new StreamHandler(m_StreamParser_OnStreamStart);
            m_StreamParser.OnStreamError += new StreamError(m_StreamParser_OnStreamError);
            m_StreamParser.OnStreamEnd += new StreamHandler(m_StreamParser_OnStreamEnd);
            m_StreamParser.OnStreamElement += new StreamHandler(m_StreamParser_OnStreamElement);
        }

        public SocketConnection(Socket socket):this()
        {
            m_Socket = socket;
            m_NetStream = new NetworkStream(m_Socket);
            try
            {
                m_NetStream.BeginRead(m_Buffer, 0, m_Buffer.Length,  new AsyncCallback(OnDataReceive), null);
            }
            catch
            { 
            }
        }

        /// <summary>
        /// Socket数据接收
        /// </summary>
        /// <param name="iar"></param>
        private void OnDataReceive(IAsyncResult iar)
        {
            try
            {
                int ret = m_NetStream.EndRead(iar);
                if (ret > 0)
                {
                    m_StreamParser.Push(m_Buffer, 0, ret);

                    //重置缓冲
                    m_Buffer = new byte[BUFFERSIZE];
                    m_NetStream.BeginRead(m_Buffer, 0, m_Buffer.Length, new AsyncCallback(OnDataReceive), null);
                }
                else
                {
                    //断开连接
                    DisConnect();
                }
            }
            catch
            {
                DisConnect();
            }
        }

        private void OnDataSend(IAsyncResult iar)
        {
            try
            {
               m_NetStream.EndWrite(iar);
            }
            catch
            { 
            }
        }

        private void Send(string xml)
        {
            //异步发送
            try
            {
                byte[] sendBuffer = Encoding.UTF8.GetBytes(xml);
                m_NetStream.BeginWrite(sendBuffer, 0, sendBuffer.Length, new AsyncCallback(OnDataSend), null);
            }
            catch
            {

            }
        }

        public void Send(Element elm)
        {
            Send(elm.ToString());
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            try
            {
                m_Socket.Shutdown(SocketShutdown.Both);
                m_Socket.Close();
            }
            catch
            { 

            }

            //这里需要离线逻辑处理
        }

        #region StreamParser 事件
        void m_StreamParser_OnStreamElement(object sender, Node e)
        {
            //throw new NotImplementedException();
            StreamFactory.Add(new StreamInfo(e, this));
        }

        void m_StreamParser_OnStreamEnd(object sender, Node e)
        {
            //throw new NotImplementedException();
        }

        void m_StreamParser_OnStreamError(object sender, Exception ex)
        {
            //throw new NotImplementedException();
        }

        void m_StreamParser_OnStreamStart(object sender, Node e)
        {
            //string ServerDomain = "dcboy.vicp.net";
            m_SessionID = Function.CreateSessionID();

            StringBuilder sb = new StringBuilder();
            sb.Append("<stream:stream from='");
            sb.Append(IMServer.SERVERNAME);
            sb.Append("' xmlns='");
            sb.Append(XMPPProtocol.Uri.CLIENT);
            sb.Append("' xmlns:stream='");
            sb.Append(XMPPProtocol.Uri.STREAM);
            sb.Append("' id='");
            sb.Append(m_SessionID);
            sb.Append("'>");
            Send(sb.ToString());
        }

        #endregion


    }


}
