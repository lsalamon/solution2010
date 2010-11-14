using System;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WebKit;
using STalk.Lib;

namespace STalk.UI
{
    public class BaseForm : Form
    {
        [DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImportAttribute("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        IntPtr hWnd, // handle to destination window
        int Msg, // message
        int wParam, // first message parameter
        int lParam
        );

        private Queue m_InvokeQueue = Queue.Synchronized(new Queue());
        private object m_Status = new object(); //线程锁定对象;
        public const bool DEBUG = true;
        /// <summary>
        /// 边框的大小
        /// </summary>
        private const int BORDER_SIZE = 1;
        protected WebKitBrowser m_WebBrowser = new WebKitBrowser(DEBUG);
        public BaseForm()
        {
            InitBaseForm();
            InitWebKit();
            this.Hide();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region winform初始化
        private void InitBaseForm()
        {
          //  base.ClientSize = new Size(base.ClientSize.Width + 10, base.ClientSize.Height + 10);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Width = 300;
            base.Height = 300;
            base.MinimumSize = new Size(100, 100);
           // base.BackColor = Color.FromArgb(34, 108, 138); //#226C8A
           // this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.TransparencyKey = Color.White;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case Win32.WM_NCPAINT:
        //            break;
        //        case Win32.WM_NCACTIVATE:
        //            if (m.WParam == (IntPtr)0)
        //            {
        //                m.Result = (IntPtr)1;
        //            }
        //            if (m.WParam == (IntPtr)2097152)
        //            {
        //                m.Result = (IntPtr)1;
        //            }
        //            break;
        //        case Win32.WM_NCCALCSIZE:
        //            break;
        //        case Win32.WM_NCHITTEST:
        //            //base.WndProc(ref m);
        //            Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
        //            vPoint = PointToClient(vPoint);
        //            if (this.WindowState == FormWindowState.Normal)
        //            {
        //                if (vPoint.X <= 3)
        //                {
        //                    if (vPoint.Y <= 3)
        //                        m.Result = (IntPtr)Win32.HTTOPLEFT;
        //                    else if (vPoint.Y >= Height - 3)
        //                        m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
        //                    else
        //                        m.Result = (IntPtr)Win32.HTLEFT;
        //                }
        //                else if (vPoint.X >= Width - 3)
        //                {
        //                    if (vPoint.Y <= 3)
        //                        m.Result = (IntPtr)Win32.HTTOPRIGHT;
        //                    else if (vPoint.Y >= Height - 3)
        //                        m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
        //                    else
        //                        m.Result = (IntPtr)Win32.HTRIGHT;
        //                }
        //                else if (vPoint.Y <= 3)
        //                {
        //                    m.Result = (IntPtr)Win32.HTTOP;
        //                }
        //                else if (vPoint.Y >= Height - 3)
        //                    m.Result = (IntPtr)Win32.HTBOTTOM;
        //            }
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }
        //}

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            //调整角度
            //IntPtr regionHandle = CreateRoundRectRgn(0, 0, Width, Height, 10, 10);
            //Region roundRegion = null;
            //roundRegion = Region.FromHrgn(regionHandle);
            //SetWindowRgn(Handle, regionHandle, true);

            base.OnSizeChanged(e);
           // m_WebBrowser.Width = base.Width - (BORDER_SIZE * 2);
           // m_WebBrowser.Height = base.Height - (BORDER_SIZE * 2);

            /* 不要删除！！
            IntPtr WKregionHandle = CreateRoundRectRgn(0, 0, m_WebBrowser.Width, m_WebBrowser.Height, 6, 6);
            Region WKroundRegion = null;
            WKroundRegion = Region.FromHrgn(WKregionHandle);
            SetWindowRgn(m_WebBrowser.Handle, WKregionHandle, true);*/
        }
        #endregion

        #region webkit初始化

        public WebKitBrowser Browser
        {
            get { return m_WebBrowser; }
            set { m_WebBrowser = value; }
        }

        private void InitWebKit()
        {
            m_WebBrowser.Dock = DockStyle.Fill;
            m_WebBrowser.AutoScroll = false;
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
            //this.Show();
            
        }

        void m_WebBrowser_JavaScriptExternalCall(object sender, JavaScriptExternalEventArgs args)
        {
            switch (args.strId)
            {
                case "FormShow":
                    this.Show();
                    break;
                case "FormClose":
                    this.Close();
                    break;
                case "FormMinSize":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "FormMaxSize":
                    if (this.WindowState == FormWindowState.Normal)
                        this.WindowState = FormWindowState.Maximized;
                    else if (this.WindowState == FormWindowState.Maximized)
                        this.WindowState = FormWindowState.Normal;
                    break;
                case "FormNormalSize":
                    this.WindowState = FormWindowState.Normal;
                    break;
                case "FormMove": //移动窗口消息
                    ReleaseCapture();
                    SendMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);
                    break;
                case "FormSetTitle": //设置标题
                    try
                    {
                        JSONArray param = (JSONArray)JSONConvert.DeserializeArray(args.strArg);
                        base.Text = param[0].ToString();
                    }
                    catch
                    {
                    }
                    break;
                case "FormSetMaxSize": //设置标题
                    try
                    {
                        JSONArray param = (JSONArray)JSONConvert.DeserializeArray(args.strArg);
                        int width = Convert.ToInt32(param[0]);
                        int height = Convert.ToInt32(param[1]);
                        this.MaximumSize = new Size(width, height);
                    }
                    catch
                    {
                    }
                    break;
                case "FormSetSize":
                    try
                    {
                        JSONArray param = (JSONArray)JSONConvert.DeserializeArray(args.strArg);
                        int width = Convert.ToInt32(param[0]);
                        int height = Convert.ToInt32(param[1]);
                        this.Width = width;
                        this.Height = height;
                    }
                    catch
                    {
                    }
                    break;
                case "FormSetMinSize": //设置标题
                    try
                    {
                        JSONArray param = (JSONArray)JSONConvert.DeserializeArray(args.strArg);
                        int width = Convert.ToInt32(param[0]);
                        int height = Convert.ToInt32(param[1]);
                        this.MinimumSize = new Size(width, height);
                    }
                    catch
                    {
                    }
                    break;
                case "MessageBox":
                    try
                    {
                        JSONArray param = (JSONArray)JSONConvert.DeserializeArray(args.strArg);
                        JSONObject option = (JSONObject)param[0];
                       // this.Focus();
                        DialogResult result = MessageBox.Show(option["Message"].ToString(),
                            option["Title"].ToString(),(MessageBoxButtons)Convert.ToInt16(option["Button"]),
                            (MessageBoxIcon)Convert.ToInt16(option["Icon"]));
                        if(!string.IsNullOrEmpty(option["CallBack"].ToString()))
                            JSCall(option["CallBack"].ToString(), result.ToString());
                    }
                    catch
                    {
                    }
                    break;
                default:
                    break;
            }

            OnExternalCall(args);
        }

        protected virtual void OnExternalCall(JavaScriptExternalEventArgs args) { }

        protected void JSCall(string method, params object[] argv)
        {
            //把参数转换成json
           // JSONObject jObj = new JSONObject();
            JSONArray param = new JSONArray();
            foreach (object o in argv)
            {
                param.Add(o);
            }
            //jObj.Add("Cmd", method);
           // jObj.Add("Param", argv);
            string script = string.Format("{0}({1})", method, JSONConvert.SerializeArray(param));

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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }

    }
}
