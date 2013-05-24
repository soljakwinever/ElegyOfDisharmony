namespace MessageboxSystem
{
    partial class frmDownloadClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.progressDownload = new System.Windows.Forms.ProgressBar();
            this.lblDowloadProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressDownload
            // 
            this.progressDownload.Location = new System.Drawing.Point(12, 33);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(405, 19);
            this.progressDownload.TabIndex = 1;
            // 
            // lblDowloadProgress
            // 
            this.lblDowloadProgress.AutoSize = true;
            this.lblDowloadProgress.Location = new System.Drawing.Point(184, 56);
            this.lblDowloadProgress.Name = "lblDowloadProgress";
            this.lblDowloadProgress.Size = new System.Drawing.Size(68, 13);
            this.lblDowloadProgress.TabIndex = 2;
            this.lblDowloadProgress.Text = "Progress 0/0";
            this.lblDowloadProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDownloadClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 111);
            this.Controls.Add(this.lblDowloadProgress);
            this.Controls.Add(this.progressDownload);
            this.Controls.Add(this.button1);
            this.Name = "frmDownloadClient";
            this.Text = "frmDownloadClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressDownload;
        private System.Windows.Forms.Label lblDowloadProgress;
    }
}