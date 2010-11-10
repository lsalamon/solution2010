using System;
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
            m_WebBrowser.Dock = DockStyle.Fill;
            m_WebBrowser.JavaScriptExternalCall += new JavaScriptExternalCallEventHandler(webBrowser_JavaScriptExternalCall);
            panel1.Controls.Add(m_WebBrowser);
            m_WebBrowser.Url = new Uri("c:\\Html\\main.htm");
            m_WebBrowser.MouseDown += new MouseEventHandler(m_WebBrowser_MouseDown);
        }

        void m_WebBrowser_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void webBrowser_JavaScriptExternalCall(object sender, JavaScriptExternalEventArgs args)
        {
            //throw new NotImplementedException();
            MessageBox.Show("js call");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_WebBrowser.InvokeScript("Test('dcboy')", true);
        }
    }
}
