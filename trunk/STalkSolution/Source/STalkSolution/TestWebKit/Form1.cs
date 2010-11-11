using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WebKit;
using WebKit.DOM;

namespace TestWebKit
{
    public partial class Form1 : Form
    {
        private WebKitBrowser m_WebBrowser;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_WebBrowser = new WebKitBrowser(false);
            m_WebBrowser.BackColor = Color.Black;
            m_WebBrowser.Dock = DockStyle.Fill;
            m_WebBrowser.JavaScriptExternalCall += new JavaScriptExternalCallEventHandler(webBrowser_JavaScriptExternalCall);
            Controls.Add(m_WebBrowser);
        }



        void webBrowser_JavaScriptExternalCall(object sender, JavaScriptExternalEventArgs args)
        {
            MessageBox.Show("js call");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_WebBrowser.InvokeScript("Test('dcboy')", true);
        }
    }
}
