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
    public partial class frmMain : BaseForm
    {
        private string m_UrlFile = Function.GetViewPath("Main.htm");
        private XmppClientConnection m_Xmpp = new XmppClientConnection();

        public frmMain()
        {
            InitializeComponent();
            InitXmpp();
            base.Browser.Url = new System.Uri(m_UrlFile);
            frmLogin frm = new frmLogin(m_Xmpp);
            frm.ShowDialog();
        }

        private void InitXmpp()
        {
            m_Xmpp.OnRosterItem += new XmppClientConnection.RosterHandler(m_Xmpp_OnRosterItem);
            m_Xmpp.OnAgentItem += new XmppClientConnection.AgentHandler(m_Xmpp_OnAgentItem);
            m_Xmpp.OnPresence += new XMPPProtocol.Protocol.client.PresenceHandler(m_Xmpp_OnPresence);
            m_Xmpp.OnMessage += new XMPPProtocol.Protocol.client.MessageHandler(m_Xmpp_OnMessage);
            m_Xmpp.OnIq += new XMPPProtocol.Protocol.client.IqHandler(m_Xmpp_OnIq);
            m_Xmpp.OnError += new ErrorHandler(m_Xmpp_OnError);
            m_Xmpp.OnSocketError += new ErrorHandler(m_Xmpp_OnSocketError);
            m_Xmpp.OnClose += new ObjectHandler(m_Xmpp_OnClose);
        }

        void m_Xmpp_OnClose(object sender)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnSocketError(object sender, Exception ex)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnError(object sender, Exception ex)
        {
            //throw new NotImplementedException();
        }

        

        void m_Xmpp_OnIq(object sender, XMPPProtocol.Protocol.client.IQ iq)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnMessage(object sender, XMPPProtocol.Protocol.client.Message msg)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnPresence(object sender, XMPPProtocol.Protocol.client.Presence pres)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnAgentItem(object sender, XMPPProtocol.Protocol.iq.agent.Agent agent)
        {
            //throw new NotImplementedException();
        }

        void m_Xmpp_OnRosterItem(object sender, XMPPProtocol.Protocol.iq.roster.RosterItem item)
        {
            //throw new NotImplementedException();
        }
    }
}
