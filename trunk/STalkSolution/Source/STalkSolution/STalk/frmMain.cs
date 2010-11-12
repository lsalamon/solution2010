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
    public partial class frmMain : BaseForm
    {
        public frmMain()
        {
            InitializeComponent();

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }
    }
}
