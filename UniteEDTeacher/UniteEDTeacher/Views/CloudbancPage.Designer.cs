namespace UniteEDTeacher.Views
{
    partial class CloudbancPage
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
            this.CloudbancwebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // CloudbancwebBrowser
            // 
            this.CloudbancwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloudbancwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.CloudbancwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.CloudbancwebBrowser.Name = "CloudbancwebBrowser";
            this.CloudbancwebBrowser.Size = new System.Drawing.Size(1484, 743);
            this.CloudbancwebBrowser.TabIndex = 0;
            // 
            // CloudbancPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 743);
            this.Controls.Add(this.CloudbancwebBrowser);
            this.Name = "CloudbancPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Cloudbanc";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser CloudbancwebBrowser;
    }
}