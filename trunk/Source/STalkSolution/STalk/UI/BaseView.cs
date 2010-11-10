﻿using System;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WebKit;
using STalk.Lib;

namespace STalk.UI
{
    public class BaseView : Form
    {
        private Queue m_InvokeQueue=Queue.Synchronized(new Queue());
        private object m_Status = new object(); //线程锁定对象;
        public const bool DEBUG = false;
        /// <summary>
        /// 边框的大小
        /// </summary>
        private const int BORDER_SIZE = 1;
        protected WebKitBrowser m_WebBrowser = new WebKitBrowser(DEBUG);
        public BaseView()
        {
            InitBaseForm();
            InitWebKit();
            this.Hide();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region winform初始化
        private void InitBaseForm()
        {
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Width = 300;
            base.Height = 300;
            base.Hide();
            base.MinimumSize = new Size(100, 100);
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        protected override void WndProc(ref Message m)
        {
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
                        base.WndProc(ref m);
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (MaximizeBox)
                        {
                            if (vPoint.X <= 3)
                            {
                                if (vPoint.Y <= 3)
                                    m.Result = (IntPtr)Win32.HTTOPLEFT;
                                else if (vPoint.Y >= Height - 3)
                                    m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                                else m.Result = (IntPtr)Win32.HTLEFT;
                            }
                            else if (vPoint.X >= Width - 3)
                            {
                                 if (vPoint.Y <= 3)
                                    m.Result = (IntPtr)Win32.HTTOPRIGHT;
                                else if (vPoint.Y >= Height - 3)
                                    m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                                else m.Result = (IntPtr)Win32.HTRIGHT;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            m_WebBrowser.Width = base.Width - (BORDER_SIZE*2);
            m_WebBrowser.Height = base.Height - (BORDER_SIZE * 2);
        }
        #endregion

        #region webkit初始化

        private void InitWebKit()
        {
            m_WebBrowser.Location = new System.Drawing.Point(BORDER_SIZE, BORDER_SIZE);
            m_WebBrowser.Margin = new System.Windows.Forms.Padding(0);
            m_WebBrowser.TabIndex = 0;
            m_WebBrowser.AllowDownloads = false;
            m_WebBrowser.AllowNewWindows = false;
            m_WebBrowser.Width = base.Width - (BORDER_SIZE * 2);
            m_WebBrowser.Height = base.Height - (BORDER_SIZE * 2);
            m_WebBrowser.BorderStyle = BorderStyle.None;
            m_WebBrowser.HorizontalScroll.Visible = false;
            m_WebBrowser.VerticalScroll.Visible = false;
            m_WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(m_WebBrowser_DocumentCompleted);
            m_WebBrowser.JavaScriptExternalCall += new JavaScriptExternalCallEventHandler(m_WebBrowser_JavaScriptExternalCall);
            this.Controls.Add(m_WebBrowser);
        }

        void m_WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Show();
        }

        void m_WebBrowser_JavaScriptExternalCall(object sender, JavaScriptExternalEventArgs args)
        {
            Console.WriteLine("ExternalCall:{0} {1} {2}", args.strPage, args.strId, args.strArg);
            //基本窗口控制处理

            switch (args.strId)
            {
                case "FormClose":
                    this.Close();
                    break;
                case "FormMinSize":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "FormMaxSize":
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "FormNormalSize":
                    this.WindowState = FormWindowState.Normal;
                    break;
                //处理窗口拖动
                default:
                    break;
            }

            OnExternalCall(args);
        }

        protected virtual void OnExternalCall(JavaScriptExternalEventArgs args) { }

        protected void JSCall(string method, params object[] argv)
        {
            //把参数转换成json
            JSONObject jObj = new JSONObject();
            jObj.Add("Cmd", method);
            jObj.Add("Param", argv);
            //统一调用javascript 中的 ProcessCmd
            string script = string.Format("ProcessCmd({0})", JSONConvert.SerializeObject(jObj));

            //使用后台工作线程
            BackgroundWorker JSCallWorker = new BackgroundWorker();
            JSCallWorker.DoWork += new DoWorkEventHandler(JSCallWorker_DoWork);
            JSCallWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(JSCallWorker_RunWorkerCompleted);
            m_InvokeQueue.Enqueue(script);
            JSCallWorker.RunWorkerAsync();
        }

        void JSCallWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1);
        }

        void JSCallWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                string script = (string)m_InvokeQueue.Dequeue();
                m_WebBrowser.InvokeScript(script, true);
            }
            catch
            { 
            }
        }

        #endregion





    }
}