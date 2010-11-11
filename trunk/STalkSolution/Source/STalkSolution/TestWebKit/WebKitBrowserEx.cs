using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using WebKit;

namespace TestWebKit
{
    public class WebKitBrowserEx:WebKitBrowser
    {
        public WebKitBrowserEx(bool canDebug) : base(canDebug) {
           // Application.AddMessageFilter(this);
        }

        protected override void WndProc(ref System.Windows.Forms.Message message)
        {
           // Console.WriteLine(message);
            base.WndProc(ref message);
        }


    }
}
