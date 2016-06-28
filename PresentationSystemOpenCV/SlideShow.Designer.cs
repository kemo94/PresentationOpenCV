namespace FindContours
{
    partial class SlideShow
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
            this.presentationTimer = new System.Windows.Forms.Timer(this.components);
            this.pen = new System.Windows.Forms.Button();
            this.marker = new System.Windows.Forms.Button();
            this.eraser = new System.Windows.Forms.Button();
            this.slides = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.slides)).BeginInit();
            this.SuspendLayout();
            // 
            // presentationTimer
            // 
            this.presentationTimer.Interval = 10;
            this.presentationTimer.Tick += new System.EventHandler(this.presentationTimer_Tick);
            // 
            // pen
            // 
            this.pen.BackgroundImage = global::FindContours.Properties.Resources.pen21;
            this.pen.Location = new System.Drawing.Point(12, 12);
            this.pen.Name = "pen";
            this.pen.Size = new System.Drawing.Size(95, 100);
            this.pen.TabIndex = 3;
            this.pen.UseVisualStyleBackColor = true;
            this.pen.Click += new System.EventHandler(this.pen_Click);
            // 
            // marker
            // 
            this.marker.BackgroundImage = global::FindContours.Properties.Resources.marker1;
            this.marker.Location = new System.Drawing.Point(12, 118);
            this.marker.Name = "marker";
            this.marker.Size = new System.Drawing.Size(95, 100);
            this.marker.TabIndex = 2;
            this.marker.UseVisualStyleBackColor = true;
            this.marker.Click += new System.EventHandler(this.marker_Click);
            // 
            // eraser
            // 
            this.eraser.BackgroundImage = global::FindContours.Properties.Resources.eraser1;
            this.eraser.Location = new System.Drawing.Point(12, 224);
            this.eraser.Name = "eraser";
            this.eraser.Size = new System.Drawing.Size(95, 100);
            this.eraser.TabIndex = 1;
            this.eraser.UseVisualStyleBackColor = true;
            this.eraser.Click += new System.EventHandler(this.eraser_Click);
            // 
            // slides
            // 
            this.slides.Location = new System.Drawing.Point(3, -3);
            this.slides.Name = "slides";
            this.slides.Size = new System.Drawing.Size(388, 327);
            this.slides.TabIndex = 0;
            this.slides.TabStop = false;
            this.slides.MouseDown += new System.Windows.Forms.MouseEventHandler(this.slides_MouseDown);
            this.slides.MouseMove += new System.Windows.Forms.MouseEventHandler(this.slides_MouseMove);
            this.slides.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slides_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 339);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // SlideShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 589);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pen);
            this.Controls.Add(this.marker);
            this.Controls.Add(this.eraser);
            this.Controls.Add(this.slides);
            this.Name = "SlideShow";
            this.Text = "SlideShow";
            this.Load += new System.EventHandler(this.SlideShow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SlideShow_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SlideShow_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.slides)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox slides;
        private System.Windows.Forms.Timer presentationTimer;
        private System.Windows.Forms.Button eraser;
        private System.Windows.Forms.Button marker;
        private System.Windows.Forms.Button pen;
        private System.Windows.Forms.TextBox textBox1;
    }
}