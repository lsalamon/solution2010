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
        [DllImport("user32.dll")]
        static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        private WebKitBrowserEx m_WebBrowser;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       //     m_WebBrowser = new WebKitBrowserEx(false);
       //   //  m_WebBrowser.BackColor = Color.Black;
       ////     m_WebBrowser.Dock = DockStyle.Fill;
       //     m_WebBrowser.Height = 200;
       //     m_WebBrowser.Width = 200;
       //   //  m_WebBrowser.BackColor = Color.Transparent ;
       //     m_WebBrowser.JavaScriptExternalCall += new JavaScriptExternalCallEventHandler(webBrowser_JavaScriptExternalCall);
       //     Controls.Add(m_WebBrowser);

        }
        private static int WM_NCPAINT = 0x0085;
        private static int WM_ERASEBKGND = 0x0014;
        private static int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT || m.Msg == WM_ERASEBKGND || m.Msg == WM_PAINT)
            {
                Graphics graphics = Graphics.FromHdc(this.Handle);
                Rectangle rectangle = new Rectangle(0, 0, this.Width-1 , this.Height-1);
                ControlPaint.DrawBorder(graphics, rectangle, Color.Blue, ButtonBorderStyle.Solid);

              //  m.Result = (IntPtr)1;
              //  ReleaseDC(m.HWnd, this.Handle);
            }
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
