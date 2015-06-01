namespace UniteEDTeacher.Views
{
    partial class BookStorePage
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
            this.StorewebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // StorewebBrowser
            // 
            this.StorewebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorewebBrowser.Location = new System.Drawing.Point(0, 0);
            this.StorewebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.StorewebBrowser.Name = "StorewebBrowser";
            this.StorewebBrowser.Size = new System.Drawing.Size(1484, 739);
            this.StorewebBrowser.TabIndex = 0;
            // 
            // BookStorePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 739);
            this.Controls.Add(this.StorewebBrowser);
            this.Name = "BookStorePage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Store";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser StorewebBrowser;
    }
}