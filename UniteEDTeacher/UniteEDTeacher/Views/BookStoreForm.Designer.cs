namespace UniteEDTeacher.Views
{
    partial class BookStoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookStoreForm));
            this.StorewebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // StorewebBrowser
            // 
            this.StorewebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorewebBrowser.Location = new System.Drawing.Point(0, 0);
            this.StorewebBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.StorewebBrowser.MinimumSize = new System.Drawing.Size(15, 16);
            this.StorewebBrowser.Name = "StorewebBrowser";
            this.StorewebBrowser.ScriptErrorsSuppressed = true;
            this.StorewebBrowser.Size = new System.Drawing.Size(1403, 782);
            this.StorewebBrowser.TabIndex = 0;
            // 
            // BookStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 782);
            this.Controls.Add(this.StorewebBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "BookStoreForm";
            this.ShowInTaskbar = false;
            this.Text = "Store";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser StorewebBrowser;
    }
}