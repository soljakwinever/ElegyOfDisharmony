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
    public partial class TextureSelection : Form
    {
        string path;

        public TextureSelection()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(path))
            {
                MessageBox.Show("No Texture selected");
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            Game1.FilePath = path;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Texture Files|*.png;*.jpg;*.jpeg";
                ofd.Multiselect = false;
                result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtTexture.Text = ofd.FileName;
                    path = ofd.FileName;
                }
            }
        }
    }
}
