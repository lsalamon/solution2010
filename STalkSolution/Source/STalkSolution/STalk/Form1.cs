using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using STalk.UI;

namespace STalk
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 500;
            this.Height = 500;
            base.Browser.Url = new Uri(AppDomain.CurrentDomain.BaseDirectory + "View\\EasyUI.htm");
        }

        protected override void OnExternalCall(WebKit.JavaScriptExternalEventArgs args)
        {
            base.OnExternalCall(args);
        }

        
    }
}
