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
            InitializeComponent();
            m_Xmpp = xmpp;
            m_Xmpp.OnLogin += new ObjectHandler(m_Xmpp_OnLogin);
            m_Xmpp.OnAuthError += new XmppElementHandler(m_Xmpp_OnAuthError);
            base.Browser.Url = new System.Uri(m_UrlFile);
        }

        void m_Xmpp_OnAuthError(object sender, XMPPProtocol.Xml.Dom.Element e)
        {
            //throw new NotImplementedException();
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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Minimized;
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


            }
            catch
            { 
            }
        }

    }
}
