using System;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using STalk.DataModule;
using STalk.DataFactory;
using XMPPProtocol.Protocol;
using XMPPProtocol.Protocol.stream;
using XMPPProtocol.Protocol.iq;
using XMPPProtocol.Protocol.iq.auth;
using XMPPProtocol.Protocol.iq.roster;
using XMPPProtocol.Protocol.iq.browse;
using XMPPProtocol.Protocol.client;
using XMPPProtocol.Protocol.x;
using XMPPProtocol.Protocol.extensions.compression;
using XMPPProtocol.Xml;
using XMPPProtocol.Xml.Dom;

namespace STalkServer
{
    /// <summary>
    /// 数据处理工厂
    /// </summary>
    class StreamFactory
    {
        /// <summary>
        /// 处理队列
        /// </summary>
        private static Queue m_StreamQueue = Queue.Synchronized(new Queue());
        private static object m_LockStatus = new object();
        
        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="sInfo"></param>
        public static void Add(StreamInfo sInfo)
        {
            BackgroundWorker ProcessWorker = new BackgroundWorker();
            ProcessWorker.DoWork += new DoWorkEventHandler(ProcessWorker_DoWork);
            ProcessWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessWorker_RunWorkerCompleted);
            m_StreamQueue.Enqueue(sInfo);
            ProcessWorker.RunWorkerAsync();
        }

        static void ProcessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                StreamInfo sInfo = (StreamInfo)m_StreamQueue.Dequeue();
                ProcessStreamInfo(sInfo);
            }
            catch
            { 

            }
        }

        static void ProcessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1);
        }

        private static void ProcessStreamInfo(StreamInfo sInfo)
        {
            if (sInfo == null)
                return;

            Console.WriteLine("Recv:{0}", sInfo.Node.ToString());
            //开始一系列的逻辑处理
           
            if(sInfo.Node.GetType() == typeof(IQ))
            {
                ProcessIQ(sInfo);
            }
            else if (sInfo.Node.GetType() == typeof(Message))
            { 
            }
            else if (sInfo.Node.GetType() == typeof(Presence))
            {
                ProcessPresence(sInfo);
            }
            else if (sInfo.Node.GetType() == typeof(Features))
            { 
            }
            else if (sInfo.Node.GetType() == typeof(Compressed))
            { 
            }
        }


        private static void ProcessIQ(StreamInfo sInfo)
        {
            IQ iq = (IQ)sInfo.Node;
            if (iq.Query != null)
            {
                //用户登录
                if (iq.Query.GetType() == typeof(Auth))
                {
                    ProcessIQAuth(sInfo);
                }
            }
        }

        private static void ProcessPresence(StreamInfo sInfo)
        {

        }

        private static void ProcessIQAuth(StreamInfo sInfo)
        {
            IQ iq = (IQ)sInfo.Node;
            Auth auth = (Auth)iq.Query;
            switch (iq.Type)
            {
                case IqType.get:
                    iq.SwitchDirection();
                    iq.Type = IqType.result;
                    auth.AddChild(new Element("password"));
                    auth.AddChild(new Element("digest"));
                    sInfo.Client.Send(iq);
                    break;
                case IqType.set:
                    User user = DataFactory.UserProvider.GetUserByUserName(auth.Username);
                    string digest = XMPPProtocol.Util.Hash.Sha1Hash(sInfo.Client.SessionID + user.UserPwd);
                    if (auth.Digest == digest && !string.IsNullOrEmpty(user.UserName)) //登录验证通过
                    {
                        sInfo.Client.JID = new XMPPProtocol.Jid(auth.Username, IMServer.SERVERNAME, "STalk");
                        //添加到全局客户端字典
                        ClientFactory.AddClient(sInfo.Client);
                        //update 数据库 修改用户lastLoginIP lastLoginTime Server
                        
                        //通知其他IMServer 逼在线的下线

                        //写入到登录日志

                        //发送登录成功iq
                        iq.SwitchDirection();
                        iq.Type = IqType.result;
                        iq.Query = null;
                        sInfo.Client.Send(iq);
                    }
                    else
                    {
                        //验证失败，发送失败iq
                        iq.SwitchDirection();
                        iq.Type = IqType.error;
                        iq.Query = null;
                        iq.Error.Message = "用户名或者密码错误！";
                        sInfo.Client.Send(iq);
                    }
                    break;
            }
        }

    }
}
