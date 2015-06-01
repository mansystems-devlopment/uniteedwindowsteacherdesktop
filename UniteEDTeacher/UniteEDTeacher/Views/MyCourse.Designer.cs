namespace UniteEDTeacher.Views
{
    partial class MyCourses
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
            this.MyCoursewebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // MyCoursewebBrowser
            // 
            this.MyCoursewebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyCoursewebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MyCoursewebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.MyCoursewebBrowser.Name = "MyCoursewebBrowser";
            this.MyCoursewebBrowser.Size = new System.Drawing.Size(1483, 731);
            this.MyCoursewebBrowser.TabIndex = 0;
            this.MyCoursewebBrowser.Url = new System.Uri("http://google.com", System.UriKind.Absolute);
            // 
            // MyCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1483, 731);
            this.Controls.Add(this.MyCoursewebBrowser);
            this.Name = "MyCourses";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "My Courses";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser MyCoursewebBrowser;
    }
}