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

namespace STalkServer.Client
{
    /// <summary>
    /// 客户端连接对象
    /// 每个连进的客户端为一个实例
    /// </summary>
    class XmppServerConnection
    {
        private StreamParser m_StreamParser;
        private Socket m_Socket;
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        private const int BUFFERSIZE = 1024;
        private Jid m_JID;
        private byte[] m_Buffer = new byte[BUFFERSIZE];

        public XmppServerConnection()
        {
            m_StreamParser = new StreamParser();
            m_StreamParser.OnStreamStart += new StreamHandler(m_StreamParser_OnStreamStart);
            m_StreamParser.OnStreamError += new StreamError(m_StreamParser_OnStreamError);
            m_StreamParser.OnStreamEnd += new StreamHandler(m_StreamParser_OnStreamEnd);
            m_StreamParser.OnStreamElement += new StreamHandler(m_StreamParser_OnStreamElement);
        }

        public XmppServerConnection(Socket socket):this()
        {
            m_Socket = socket;

            //开始接收
            try
            {
                m_Socket.BeginReceive(m_Buffer, 0, m_Buffer.Length, SocketFlags.None, new AsyncCallback(OnDataReceive), null);
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
                int ret = m_Socket.EndReceive(iar);
                if (ret > 0)
                {
                    m_StreamParser.Push(m_Buffer, 0, ret);

                    //重置缓冲
                    m_Buffer = new byte[BUFFERSIZE];
                    m_Socket.BeginReceive(m_Buffer, 0, m_Buffer.Length, SocketFlags.None, new AsyncCallback(OnDataReceive), null);
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
                int ret = m_Socket.EndSend(iar);
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
                m_Socket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, new AsyncCallback(OnDataSend), null);
            }
            catch
            {
            }
        }

        private void Send(Element elm)
        {
            Send(elm.ToString());
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void DisConnect()
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
            //throw new NotImplementedException();
        }

        #endregion


    }


}
