namespace FindContours
{
    partial class PPT
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
            this.components = new System.ComponentModel.Container();
            this.openCam = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.presentationTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openCam
            // 
            this.openCam.Location = new System.Drawing.Point(49, 25);
            this.openCam.Name = "openCam";
            this.openCam.Size = new System.Drawing.Size(75, 23);
            this.openCam.TabIndex = 0;
            this.openCam.Text = "Open Camer";
            this.openCam.UseVisualStyleBackColor = true;
            this.openCam.Click += new System.EventHandler(this.openCam_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // presentationTimer
            // 
            this.presentationTimer.Interval = 1000;
            this.presentationTimer.Tick += new System.EventHandler(this.camStream_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 90);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.openCam);
            this.Name = "PPT";
            this.Text = "PPT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openCam;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer presentationTimer;
        private System.Windows.Forms.Button button1;
    }
}