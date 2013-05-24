using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsGame1
{
    public partial class NameForm : Form
    {
        string _name;

        public string AreaName
        {
            get { return _name; }
        }

        public NameForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _name = textBox1.Text;
        }
    }
}
