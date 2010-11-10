using System;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        private Queue<IQueryWork> m_Work = new Queue<IQueryWork>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new Thread(new ThreadStart(
           // ProcessData();
            //ProcessData();
           // IQueryWork work=
            //ProcessData(new CallBack(delegate(string name) {
            //    Console.WriteLine(name);
            //}));
        }

        delegate void CallBack(string name);

        public void ProcessData(Delegate d)
        {
            Invoke(d, new object[] { "dcboy"});
        }
    }
}
