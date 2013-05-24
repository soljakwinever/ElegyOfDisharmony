using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsGame1
{
    public partial class AtlasEditor : Form
    {
        public AtlasArea Selected_Area;
        private string _gameDirectory;

        public string GameDirectory
        {
            get { return _gameDirectory; }
        }

        public AtlasEditor()
        {
            InitializeComponent();
            if (File.Exists("settings.txt"))
            {
                using (StreamReader sr = new StreamReader(File.OpenRead("settings.txt")))
                {
                    _gameDirectory = sr.ReadLine();
                    sr.Close();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selected_Area = (AtlasArea)listBox1.SelectedItem;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Program.Close();
            base.OnFormClosing(e);
        }

        public void SaveFile()
        {

        }

        public void UpdateListbox(List<AtlasArea> areas)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < areas.Count; i++)
            {
                listBox1.Items.Add(areas[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (NameForm form = new NameForm())
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var area = new AtlasArea() { Name = form.AreaName, Area = new Microsoft.Xna.Framework.Rectangle() };
                    Program.Areas.Add(area);
                    UpdateListbox();
                    listBox1.SelectedItem = area;
                    Selected_Area = area;
                }
            }
        }

        public void UpdateListbox()
        {
            listBox1.Items.Clear();
            foreach (var item in Program.Areas)
            {
                listBox1.Items.Add(item);
            }
            listBox1.SelectedItem = Selected_Area;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_Validated(object sender, EventArgs e)
        {
            UpdateListbox();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.SaveFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.Message);
            }
        }

        private void setGameDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
            retry:

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _gameDirectory = dialog.SelectedPath;
                    if (!(Directory.Exists(_gameDirectory + @"\Data")
                        && Directory.Exists(_gameDirectory + @"\Content\Graphics")))
                    {
                        MessageBox.Show("Invalid Game Directory");
                        goto retry;
                    }
                    else
                    {
                        MessageBox.Show("Folder has been Set");
                        using (StreamWriter sr = new StreamWriter(File.OpenWrite("settings.txt")))
                        {
                            sr.WriteLine(_gameDirectory);
                            sr.Close();
                        }

                    }
                }
            }
        }
    }
}
