namespace UniteEDTeacher.Views
{
    partial class SmartLinkPage
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
            this.SmartLinkwebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // SmartLinkwebBrowser
            // 
            this.SmartLinkwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SmartLinkwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.SmartLinkwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.SmartLinkwebBrowser.Name = "SmartLinkwebBrowser";
            this.SmartLinkwebBrowser.Size = new System.Drawing.Size(1518, 742);
            this.SmartLinkwebBrowser.TabIndex = 0;
            // 
            // SmartLinkPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 742);
            this.Controls.Add(this.SmartLinkwebBrowser);
            this.Name = "SmartLinkPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Smart Link";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser SmartLinkwebBrowser;
    }
}