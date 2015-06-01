namespace UniteEDTeacher.Views
{
    partial class MediaPage
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
            this.MediawebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // MediawebBrowser
            // 
            this.MediawebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediawebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MediawebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.MediawebBrowser.Name = "MediawebBrowser";
            this.MediawebBrowser.Size = new System.Drawing.Size(1484, 740);
            this.MediawebBrowser.TabIndex = 0;
            // 
            // MediaPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 740);
            this.Controls.Add(this.MediawebBrowser);
            this.Name = "MediaPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Media";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser MediawebBrowser;
    }
}