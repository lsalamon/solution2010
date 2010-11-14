using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using WebKit;

namespace TestWebKit
{
    public class WebKitBrowserEx : WebKitBrowser
    {
        [DllImport("user32.dll")]
        public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("user32.dll")]
        public extern static uint
        SetWindowLong(IntPtr hwnd,
        int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        public extern static uint
        GetWindowLong(IntPtr hwnd, int nIndex);

        public enum WindowStyle : int
        {
            GWL_EXSTYLE = -20
        }

        public enum ExWindowStyle : uint
        {
            WS_EX_LAYERED = 0x00080000
        }
        private void SetWindowTransparent(byte bAlpha)
        {
            try
            {
                SetWindowLong(
                this.Handle,
                (int)WindowStyle.GWL_EXSTYLE,
                GetWindowLong(
                this.Handle,
                (int)WindowStyle.GWL_EXSTYLE) |
                (uint)ExWindowStyle.WS_EX_LAYERED);

                SetLayeredWindowAttributes(
                this.Handle, 0, bAlpha,
                 0x00000001 | 0x00000002);
            }
            catch
            {
            }
        }  

        public WebKitBrowserEx(bool canDebug)
            : base(canDebug)
        {
            // Application.AddMessageFilter(this);
         //   SetStyle(ControlStyles.SupportsTransparentBackColor
         //| ControlStyles.UserPaint
         //| ControlStyles.AllPaintingInWmPaint
         //| ControlStyles.Opaque, true);
         //   BackColor = Color.Transparent;
            SetWindowTransparent(0);
        }

        protected override void WndProc(ref System.Windows.Forms.Message message)
        {
            // Console.WriteLine(message);
            base.WndProc(ref message);
        }
    }
}
