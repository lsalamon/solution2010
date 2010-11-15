using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using STalk.UI;
using STalk.Lib;
using XMPPProtocol;

namespace STalk
{
    public partial class frmLogin : BaseForm
    {
        private XmppClientConnection m_Xmpp = null;
        private string m_UrlFile = Function.GetViewPath("Login.htm");
        private bool m_IsLogin = false;

        public frmLogin(XmppClientConnection xmpp)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            m_Xmpp = xmpp;
            m_Xmpp.OnLogin += new ObjectHandler(m_Xmpp_OnLogin);
            m_Xmpp.OnAuthError += new XmppElementHandler(m_Xmpp_OnAuthError);
            m_Xmpp.OnSocketError += new ErrorHandler(m_Xmpp_OnSocketError);
            base.Browser.Url = new System.Uri(m_UrlFile);
        }

        void m_Xmpp_OnSocketError(object sender, Exception ex)
        {
            //这里可能要处理连接其他服务器,可能有好多服务器
            JSCall("OnSocketError");
            MessageBox.Show("无法连接服务器!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void m_Xmpp_OnAuthError(object sender, XMPPProtocol.Xml.Dom.Element e)
        {
            XMPPProtocol.Protocol.client.IQ iq = (XMPPProtocol.Protocol.client.IQ)e;
            MessageBox.Show(iq.Error.Message, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            JSCall("OnAuthError");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!m_IsLogin)
            {
                if (MessageBox.Show("是否退出?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
                else
                    e.Cancel = true;
            }
        }

        void m_Xmpp_OnLogin(object sender)
        {
            //throw new NotImplementedException();
            //Console.WriteLine("");
            m_IsLogin = true;
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           // WindowState = FormWindowState.Minimized;
        }

        protected override void OnExternalCall(WebKit.JavaScriptExternalEventArgs args)
        {
            base.OnExternalCall(args);
            switch (args.strId)
            {
                case "Login":
                    ProcessCmdLogin(args.strArg);
                    break;
                default:
                    break;
            }
        }

        private void ProcessCmdLogin(string argv)
        {
            try
            {
                //获取登录的参数
                JSONArray param = (JSONArray)JSONConvert.DeserializeArray(argv);
                string userName = param[0].ToString();
                string userPwd = param[1].ToString();

                m_Xmpp.Username = userName;
                m_Xmpp.Server = "im1.Stalk.com";
                m_Xmpp.Port = 5222;
                m_Xmpp.Password = Function.EncryptMD5(userPwd).ToUpper();
                m_Xmpp.Resource = "STalkClient";
                m_Xmpp.AutoResolveConnectServer = true;
                m_Xmpp.ConnectServer = null;
                m_Xmpp.AutoAgents = false;
                m_Xmpp.AutoPresence = false;
                m_Xmpp.AutoRoster = false;
                m_Xmpp.SocketConnectionType = XMPPProtocol.net.SocketConnectionType.Direct;
                m_Xmpp.Open();
            }
            catch
            { 

            }
        }

    }
}
