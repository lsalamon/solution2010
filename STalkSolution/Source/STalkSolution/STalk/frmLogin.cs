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

        public frmLogin(XmppClientConnection xmpp)
        {
            InitializeComponent();
            m_Xmpp = xmpp;
            base.Browser.Url = new System.Uri(m_UrlFile);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Minimized;
        }

        protected override void OnExternalCall(WebKit.JavaScriptExternalEventArgs args)
        {
            base.OnExternalCall(args);
        }

    }
}
