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
    public partial class Form1 : Form,IMessageFilter
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        IntPtr hWnd, // handle to destination window
        int Msg, // message
        int wParam, // first message parameter
        int lParam
        );

        private WebKitBrowser m_WebBrowser;
        public Form1()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }


        public bool PreFilterMessage(ref Message m)
        {
            // throw new NotImplementedException();
            Console.WriteLine(m);
            if (m.Msg == 0x200) //鼠标移动
            {
                Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                // vPoint = PointToClient(vPoint);
               
                Console.WriteLine(vPoint);

                int lP =( MousePosition.Y << 16 )+ MousePosition.X;
                SendMessage(Handle, Win32.WM_NCHITTEST, 0, lP);
                //Message msg = Message.Create(this.Handle, Win32.WM_NCHITTEST, (IntPtr)0, (IntPtr)lP);
                //WndProc(ref msg);
             //   SendMessage(this.Handle, Win32.WM_NCHITTEST, 0, lP);
                //if (vPoint.X <= 3)
                //{
                //    if (vPoint.Y <= 3)
                //    {
                //      //  Message msg = Message.Create(this.Handle,Win32.WM_NCHITTEST, (IntPtr)Win32.HTLEFT, (IntPtr)lP);
                //      //  WndProc(ref msg);
                //    }
                //    else if (vPoint.Y >= Height - 3)
                //        m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                //    else
                //    {
                //       // SendMessage(Handle, Win32.WM_NCHITTEST, Win32.HTLEFT, (int)lP);
                //        m.Result = (IntPtr)Win32.HTLEFT;
                //    }
                //}
                //else if (vPoint.X >= Width - 3)
                //{
                //    if (vPoint.Y <= 3)
                //        m.Result = (IntPtr)Win32.HTTOPRIGHT;
                //    else if (vPoint.Y >= Height - 3)
                //        m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                //    else
                //        m.Result = (IntPtr)Win32.HTRIGHT;
                //}
                //else if (vPoint.Y <= 3)
                //{
                //    m.Result = (IntPtr)Win32.HTTOP;
                //}
                //else if (vPoint.Y >= Height - 3)
                //    m.Result = (IntPtr)Win32.HTBOTTOM;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            Console.WriteLine(this.Handle);
            Console.WriteLine(m);
            switch (m.Msg)
            {
                case Win32.WM_NCPAINT:
                    break;
                case Win32.WM_NCACTIVATE:
                    if (m.WParam == (IntPtr)0)
                    {
                        m.Result = (IntPtr)1;
                    }
                    if (m.WParam == (IntPtr)2097152)
                    {
                        m.Result = (IntPtr)1;
                    }
                    break;
                case Win32.WM_NCCALCSIZE:
                    break;
                case Win32.WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    Console.WriteLine(vPoint);
                    vPoint = PointToClient(vPoint);
                    Console.WriteLine(vPoint);
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        if (vPoint.X <= 3)
                        {
                            if (vPoint.Y <= 3)
                                m.Result = (IntPtr)Win32.HTTOPLEFT;
                            else if (vPoint.Y >= Height - 3)
                                m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                            else
                                m.Result = (IntPtr)Win32.HTLEFT;
                        }
                        else if (vPoint.X >= Width - 3)
                        {
                            if (vPoint.Y <= 3)
                                m.Result = (IntPtr)Win32.HTTOPRIGHT;
                            else if (vPoint.Y >= Height - 3)
                                m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                            else
                                m.Result = (IntPtr)Win32.HTRIGHT;
                        }
                        else if (vPoint.Y <= 3)
                        {
                            m.Result = (IntPtr)Win32.HTTOP;
                        }
                        else if (vPoint.Y >= Height - 3)
                            m.Result = (IntPtr)Win32.HTBOTTOM;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_WebBrowser = new WebKitBrowser(false);
            m_WebBrowser.BackColor = Color.Black;
            //m_WebBrowser.Dock = DockStyle.None;
            m_WebBrowser.Left = 1;
           // m_WebBrowser.Right = 1;
            m_WebBrowser.Top = 1;
          //  m_WebBrowser.Bottom = 1;
          //  m_WebBrowser.Width = 100;
          //  m_WebBrowser.Height = 100;
            m_WebBrowser.JavaScriptExternalCall += new JavaScriptExternalCallEventHandler(webBrowser_JavaScriptExternalCall);
            Controls.Add(m_WebBrowser);
          //  m_WebBrowser.MouseMove += new MouseEventHandler(m_WebBrowser_MouseMove);
           // m_WebBrowser.Url = new Uri("c:\\Html\\main.htm");
            //m_WebBrowser.MouseDown += new MouseEventHandler(m_WebBrowser_MouseDown);
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
