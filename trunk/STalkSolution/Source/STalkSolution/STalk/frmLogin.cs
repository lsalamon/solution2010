﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using STalk.UI;
using STalk.Lib;

namespace STalk
{
    public partial class frmLogin : BaseForm
    {
        private string m_UrlFile = Function.GetViewPath("Login.htm");

        public frmLogin()
        {
            InitializeComponent();
            base.Browser.Url = new Uri(m_UrlFile);
        }


    }
}
