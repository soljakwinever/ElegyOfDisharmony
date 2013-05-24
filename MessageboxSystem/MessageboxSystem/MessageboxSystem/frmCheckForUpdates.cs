using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MessageboxSystem
{
    public partial class frmCheckForUpdates : Form
    {
        public frmCheckForUpdates()
        {
            InitializeComponent();
            const string Update_Url = @"http://localhost/Equestriengine/update.xml";
            string downloadUrl = "";
            Version newVersion = new Version("0.0.0.0");
            string updateDetails = "";
            btnUpdates.Hide();
            Dictionary<Version, string> _otherVersions = new Dictionary<Version,string>();
            //bool latestVersion = false;
            try
            {
                System.Xml.XmlReader reader = new System.Xml.XmlTextReader(Update_Url);
                reader.MoveToContent();
                string element = "";
                if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name == "data")
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            element = reader.Name;
                        }
                        else
                        {
                            if (reader.NodeType == System.Xml.XmlNodeType.Text && reader.HasValue)
                            {
                                switch (element)
                                {
                                    case "latestversion":
                                        //latestVersion = true;
                                        break;
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "updateinfo":
                                        updateDetails = reader.Value;
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        break;
                                    default:
                                        //latestVersion = false;
                                        break;
                                }
                            }
                        }
                        if (newVersion != new Version("0.0.0.0") && updateDetails != "")
                        {
                            _otherVersions[newVersion] = updateDetails;
                            newVersion = new Version("0.0.0.0");
                            updateDetails = "";
                        }
                    }
                    foreach (var kvp in _otherVersions)
                    {
                        lblUpdates.Text += string.Format("--{0}--\n{1}\n", kvp.Key, kvp.Value);
                    }
                    btnUpdates.Show();
                }
            }
            catch
            {
                lblUpdates.Text = "No new updates";
            }
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
