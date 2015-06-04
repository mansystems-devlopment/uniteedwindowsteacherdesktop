namespace UniteEDTeacher.Views
{
    partial class ClassRoomForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassRoomForm));
            this.classRoomwebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // classRoomwebBrowser
            // 
            this.classRoomwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classRoomwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.classRoomwebBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.classRoomwebBrowser.MinimumSize = new System.Drawing.Size(15, 16);
            this.classRoomwebBrowser.Name = "classRoomwebBrowser";
            this.classRoomwebBrowser.ScriptErrorsSuppressed = true;
            this.classRoomwebBrowser.Size = new System.Drawing.Size(1138, 603);
            this.classRoomwebBrowser.TabIndex = 0;
            // 
            // ClassRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 603);
            this.Controls.Add(this.classRoomwebBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "ClassRoomForm";
            this.ShowInTaskbar = false;
            this.Text = "ClassRoom";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser classRoomwebBrowser;
    }
}