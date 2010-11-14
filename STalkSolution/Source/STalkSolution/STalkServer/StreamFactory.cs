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
using XMPPProtocol.Protocol.iq.avatar;
using XMPPProtocol.Protocol.iq.agent;
using XMPPProtocol.Protocol.iq.disco;
using XMPPProtocol.Protocol.iq.last;
using XMPPProtocol.Protocol.iq.oob;
using XMPPProtocol.Protocol.iq.rpc;
using XMPPProtocol.Protocol.iq.vcard;
using XMPPProtocol.Protocol.iq.time;
using XMPPProtocol.Protocol.iq.auth;
using XMPPProtocol.Protocol.iq.roster;
using XMPPProtocol.Protocol.iq.browse;
using XMPPProtocol.Protocol.client;
using XMPPProtocol.Protocol.x;
using XMPPProtocol.Protocol.extensions.compression;
using XMPPProtocol.Xml;
using XMPPProtocol.Xml.Dom;
using log4net;

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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
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
                ProcessMessage(sInfo);
            }
            else if (sInfo.Node.GetType() == typeof(Presence))
            {
                ProcessPresence(sInfo);
            }
            else if (sInfo.Node.GetType() == typeof(XMPPProtocol.Protocol.stream.Features))
            { 
            }
            else if (sInfo.Node.GetType() == typeof(Compressed))
            { 

            }
        }

        private static void ProcessMessage(StreamInfo sInfo)
        { 

        }

        /// <summary>
        /// 处理IQ逻辑
        /// </summary>
        /// <param name="sInfo"></param>
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
                //视频请求
                else if (iq.Query.GetType() == typeof(Rpc))
                { 

                }
                //语音请求
                else if (iq.Query.GetType() == typeof(Oob))
                { 

                }
                //用户列表
                else if (iq.Query.GetType() == typeof(Roster))
                {
                    ProcessIQRoster(sInfo);
                }
            }
        }

        /// <summary>
        /// 处理好友查询
        /// </summary>
        /// <param name="sInfo"></param>
        private static void ProcessIQRoster(StreamInfo sInfo)
        {
            IQ iq = (IQ)sInfo.Node;
            if (iq.Type == IqType.get)
            { 

            }
        }

        /// <summary>
        /// 处理用户验证
        /// </summary>
        /// <param name="sInfo"></param>
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
                    //密码是MD5的大写
                    string digest = XMPPProtocol.Util.Hash.Sha1Hash(sInfo.Client.SessionID + user.UserPwd.ToUpper());
                    if (auth.Digest == digest && !string.IsNullOrEmpty(user.UserName)) //登录验证通过
                    {
                        if (user.Status == 0)
                        {
                            iq.SwitchDirection();
                            iq.Type = IqType.error;
                            iq.Query = null;
                            iq.Error = new XMPPProtocol.Protocol.client.Error();
                            iq.Error.Message = "账户还没激活！";
                            sInfo.Client.Send(iq);
                        }
                        else if (user.Status == 2)
                        {
                            iq.SwitchDirection();
                            iq.Type = IqType.error;
                            iq.Query = null;
                            iq.Error = new XMPPProtocol.Protocol.client.Error();
                            iq.Error.Message = "账户被禁止登陆！";
                            sInfo.Client.Send(iq);
                        }
                        else
                        {
                            sInfo.Client.JID = new XMPPProtocol.Jid(auth.Username, IMServer.SERVERNAME, auth.Resource);
                            //添加到全局客户端字典
                            ClientFactory.AddClient(sInfo.Client);

                            //update 数据库 修改用户lastLoginIP lastLoginTime Server
                            user.LastLoginIP = sInfo.Client.RemoteHostIP;
                            user.Server = IMServer.SERVERNAME;
                            DataFactory.UserProvider.UpdateUserLoginInfo(user);

                            //
                            sInfo.Client.User = user;
                            //通知其他IMServer 逼在线的下线

                            //写入到登录日志 这里可以用log4net记录

                            //发送登录成功iq
                            iq.SwitchDirection();
                            iq.Type = IqType.result;
                            iq.Query = null;
                            sInfo.Client.Send(iq);
                        }
                    }
                    else
                    {
                        //验证失败，发送失败iq
                        iq.SwitchDirection();
                        iq.Type = IqType.error;
                        iq.Query = null;
                        iq.Error = new XMPPProtocol.Protocol.client.Error();
                        iq.Error.Message = "登录验证失败!";
                        sInfo.Client.Send(iq);
                    }
                    break;
            }
        }

        /// <summary>
        /// 处理用户出席信息
        /// </summary>
        /// <param name="sInfo"></param>
        private static void ProcessPresence(StreamInfo sInfo)
        {

        }

      

    }
}
