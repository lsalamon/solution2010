using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using STalk.UI;
using XMPPProtocol;

namespace STalk
{
    public partial class frmMain : BaseForm
    {
        private XmppClientConnection m_Xmpp = new XmppClientConnection();

        public frmMain()
        {
            InitializeComponent();
            InitXmpp();
            frmLogin frm = new frmLogin(m_Xmpp);
            frm.ShowDialog();
        }

        private void InitXmpp()
        { 

        }
    }
}
