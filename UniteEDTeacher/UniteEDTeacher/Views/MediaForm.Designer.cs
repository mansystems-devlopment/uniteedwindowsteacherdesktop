namespace UniteEDTeacher.Views
{
    partial class MediaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaForm));
            this.MediawebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // MediawebBrowser
            // 
            this.MediawebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediawebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MediawebBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.MediawebBrowser.MinimumSize = new System.Drawing.Size(15, 16);
            this.MediawebBrowser.Name = "MediawebBrowser";
            this.MediawebBrowser.ScriptErrorsSuppressed = true;
            this.MediawebBrowser.Size = new System.Drawing.Size(1113, 601);
            this.MediawebBrowser.TabIndex = 0;
            // 
            // MediaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 601);
            this.Controls.Add(this.MediawebBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "MediaForm";
            this.ShowInTaskbar = false;
            this.Text = "Media";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser MediawebBrowser;
    }
}