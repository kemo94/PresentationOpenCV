namespace FindContours
{
    partial class startPresentation
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
            this.slidesList = new System.Windows.Forms.ListBox();
            this.draw = new System.Windows.Forms.Button();
            this.erase = new System.Windows.Forms.Button();
            this.addSlide = new System.Windows.Forms.Button();
            this.colorBtn = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.fontsList = new System.Windows.Forms.ComboBox();
            this.sizeFontList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.hMin = new System.Windows.Forms.TrackBar();
            this.sMax = new System.Windows.Forms.TrackBar();
            this.hMax = new System.Windows.Forms.TrackBar();
            this.vMin = new System.Windows.Forms.TrackBar();
            this.sMin = new System.Windows.Forms.TrackBar();
            this.vMax = new System.Windows.Forms.TrackBar();
            this.lblHMin = new System.Windows.Forms.Label();
            this.lblSMin = new System.Windows.Forms.Label();
            this.lblVMin = new System.Windows.Forms.Label();
            this.lblHMax = new System.Windows.Forms.Label();
            this.lblSMax = new System.Windows.Forms.Label();
            this.lblVMax = new System.Windows.Forms.Label();
            this.openCam = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.slides = new System.Windows.Forms.PictureBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.pictBoxColor = new System.Windows.Forms.PictureBox();
            this.camerStream = new System.Windows.Forms.Timer(this.components);
            this.marker = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMax)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slides)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxColor)).BeginInit();
            this.SuspendLayout();
            // 
            // slidesList
            // 
            this.slidesList.FormattingEnabled = true;
            this.slidesList.Location = new System.Drawing.Point(666, 39);
            this.slidesList.Name = "slidesList";
            this.slidesList.Size = new System.Drawing.Size(93, 69);
            this.slidesList.TabIndex = 1;
            this.slidesList.SelectedIndexChanged += new System.EventHandler(this.slidesList_SelectedIndexChanged);
            // 
            // draw
            // 
            this.draw.Location = new System.Drawing.Point(1017, 12);
            this.draw.Name = "draw";
            this.draw.Size = new System.Drawing.Size(75, 23);
            this.draw.TabIndex = 5;
            this.draw.Text = "draw";
            this.draw.UseVisualStyleBackColor = true;
            this.draw.Click += new System.EventHandler(this.draw_Click);
            // 
            // erase
            // 
            this.erase.Location = new System.Drawing.Point(1098, 12);
            this.erase.Name = "erase";
            this.erase.Size = new System.Drawing.Size(75, 23);
            this.erase.TabIndex = 6;
            this.erase.Text = "erase";
            this.erase.UseVisualStyleBackColor = true;
            this.erase.Click += new System.EventHandler(this.erase_Click);
            // 
            // addSlide
            // 
            this.addSlide.Location = new System.Drawing.Point(1122, 52);
            this.addSlide.Name = "addSlide";
            this.addSlide.Size = new System.Drawing.Size(51, 23);
            this.addSlide.TabIndex = 7;
            this.addSlide.Text = "+";
            this.addSlide.UseVisualStyleBackColor = true;
            this.addSlide.Click += new System.EventHandler(this.addSlide_Click);
            // 
            // colorBtn
            // 
            this.colorBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorBtn.Location = new System.Drawing.Point(1017, 52);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(75, 23);
            this.colorBtn.TabIndex = 8;
            this.colorBtn.UseVisualStyleBackColor = false;
            this.colorBtn.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(1008, 395);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(188, 69);
            this.txtInput.TabIndex = 9;
            // 
            // fontsList
            // 
            this.fontsList.FormattingEnabled = true;
            this.fontsList.Location = new System.Drawing.Point(793, 12);
            this.fontsList.Name = "fontsList";
            this.fontsList.Size = new System.Drawing.Size(121, 21);
            this.fontsList.TabIndex = 10;
            // 
            // sizeFontList
            // 
            this.sizeFontList.FormattingEnabled = true;
            this.sizeFontList.Location = new System.Drawing.Point(666, 12);
            this.sizeFontList.Name = "sizeFontList";
            this.sizeFontList.Size = new System.Drawing.Size(121, 21);
            this.sizeFontList.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(994, 575);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 75);
            this.button1.TabIndex = 12;
            this.button1.Text = "Start Presentation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveTimer
            // 
            this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
            // 
            // hMin
            // 
            this.hMin.Location = new System.Drawing.Point(7, 535);
            this.hMin.Maximum = 255;
            this.hMin.Name = "hMin";
            this.hMin.Size = new System.Drawing.Size(204, 45);
            this.hMin.TabIndex = 14;
            this.hMin.Value = 60;
            // 
            // sMax
            // 
            this.sMax.Location = new System.Drawing.Point(342, 586);
            this.sMax.Maximum = 255;
            this.sMax.Name = "sMax";
            this.sMax.Size = new System.Drawing.Size(204, 45);
            this.sMax.TabIndex = 15;
            this.sMax.Value = 60;
            // 
            // hMax
            // 
            this.hMax.Location = new System.Drawing.Point(342, 535);
            this.hMax.Maximum = 255;
            this.hMax.Name = "hMax";
            this.hMax.Size = new System.Drawing.Size(204, 45);
            this.hMax.TabIndex = 16;
            this.hMax.Value = 60;
            // 
            // vMin
            // 
            this.vMin.Location = new System.Drawing.Point(7, 637);
            this.vMin.Maximum = 255;
            this.vMin.Name = "vMin";
            this.vMin.Size = new System.Drawing.Size(204, 45);
            this.vMin.TabIndex = 17;
            this.vMin.Value = 60;
            // 
            // sMin
            // 
            this.sMin.Location = new System.Drawing.Point(7, 586);
            this.sMin.Maximum = 255;
            this.sMin.Name = "sMin";
            this.sMin.Size = new System.Drawing.Size(204, 45);
            this.sMin.TabIndex = 18;
            this.sMin.Value = 60;
            // 
            // vMax
            // 
            this.vMax.Location = new System.Drawing.Point(342, 627);
            this.vMax.Maximum = 255;
            this.vMax.Name = "vMax";
            this.vMax.Size = new System.Drawing.Size(204, 45);
            this.vMax.TabIndex = 19;
            this.vMax.Value = 60;
            // 
            // lblHMin
            // 
            this.lblHMin.AutoSize = true;
            this.lblHMin.Location = new System.Drawing.Point(217, 535);
            this.lblHMin.Name = "lblHMin";
            this.lblHMin.Size = new System.Drawing.Size(33, 13);
            this.lblHMin.TabIndex = 20;
            this.lblHMin.Text = "h Min";
            // 
            // lblSMin
            // 
            this.lblSMin.AutoSize = true;
            this.lblSMin.Location = new System.Drawing.Point(217, 586);
            this.lblSMin.Name = "lblSMin";
            this.lblSMin.Size = new System.Drawing.Size(32, 13);
            this.lblSMin.TabIndex = 21;
            this.lblSMin.Text = "s Min";
            // 
            // lblVMin
            // 
            this.lblVMin.AutoSize = true;
            this.lblVMin.Location = new System.Drawing.Point(217, 637);
            this.lblVMin.Name = "lblVMin";
            this.lblVMin.Size = new System.Drawing.Size(33, 13);
            this.lblVMin.TabIndex = 22;
            this.lblVMin.Text = "v Min";
            // 
            // lblHMax
            // 
            this.lblHMax.AutoSize = true;
            this.lblHMax.Location = new System.Drawing.Point(552, 535);
            this.lblHMax.Name = "lblHMax";
            this.lblHMax.Size = new System.Drawing.Size(36, 13);
            this.lblHMax.TabIndex = 23;
            this.lblHMax.Text = "h Max";
            // 
            // lblSMax
            // 
            this.lblSMax.AutoSize = true;
            this.lblSMax.Location = new System.Drawing.Point(552, 586);
            this.lblSMax.Name = "lblSMax";
            this.lblSMax.Size = new System.Drawing.Size(35, 13);
            this.lblSMax.TabIndex = 24;
            this.lblSMax.Text = "s Max";
            // 
            // lblVMax
            // 
            this.lblVMax.AutoSize = true;
            this.lblVMax.Location = new System.Drawing.Point(552, 637);
            this.lblVMax.Name = "lblVMax";
            this.lblVMax.Size = new System.Drawing.Size(36, 13);
            this.lblVMax.TabIndex = 25;
            this.lblVMax.Text = "v Max";
            // 
            // openCam
            // 
            this.openCam.Location = new System.Drawing.Point(12, 488);
            this.openCam.Name = "openCam";
            this.openCam.Size = new System.Drawing.Size(114, 41);
            this.openCam.TabIndex = 26;
            this.openCam.Text = "Start Camera";
            this.openCam.UseVisualStyleBackColor = true;
            this.openCam.Click += new System.EventHandler(this.openCam_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(616, 637);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "v Max";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(616, 586);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "s Max";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(616, 535);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "h Max";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(281, 637);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "v Min";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(281, 586);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "s Min";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(281, 535);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "h Min";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.slides);
            this.panel1.Location = new System.Drawing.Point(661, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 275);
            this.panel1.TabIndex = 33;
            // 
            // slides
            // 
            this.slides.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.slides.Location = new System.Drawing.Point(12, 8);
            this.slides.Name = "slides";
            this.slides.Size = new System.Drawing.Size(512, 255);
            this.slides.TabIndex = 4;
            this.slides.TabStop = false;
            this.slides.MouseDown += new System.Windows.Forms.MouseEventHandler(this.slides_MouseDown);
            this.slides.MouseMove += new System.Windows.Forms.MouseEventHandler(this.slides_MouseMove);
            this.slides.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slides_MouseUp);
            // 
            // stopBtn
            // 
            this.stopBtn.BackgroundImage = global::FindContours.Properties.Resources.stop1;
            this.stopBtn.Location = new System.Drawing.Point(726, 424);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(52, 40);
            this.stopBtn.TabIndex = 35;
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.BackgroundImage = global::FindContours.Properties.Resources.plays1;
            this.playBtn.Location = new System.Drawing.Point(673, 424);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(47, 40);
            this.playBtn.TabIndex = 34;
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // pictBoxColor
            // 
            this.pictBoxColor.Location = new System.Drawing.Point(12, 6);
            this.pictBoxColor.Name = "pictBoxColor";
            this.pictBoxColor.Size = new System.Drawing.Size(640, 480);
            this.pictBoxColor.TabIndex = 13;
            this.pictBoxColor.TabStop = false;
            // 
            // camerStream
            // 
            this.camerStream.Tick += new System.EventHandler(this.camerStream_Tick);
            // 
            // marker
            // 
            this.marker.Location = new System.Drawing.Point(936, 12);
            this.marker.Name = "marker";
            this.marker.Size = new System.Drawing.Size(75, 23);
            this.marker.TabIndex = 36;
            this.marker.Text = "mm";
            this.marker.UseVisualStyleBackColor = true;
            this.marker.Click += new System.EventHandler(this.marker_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(687, 528);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(701, 575);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 75);
            this.button2.TabIndex = 38;
            this.button2.Text = "Control Running Presentation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // startPresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 681);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.marker);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.openCam);
            this.Controls.Add(this.lblVMax);
            this.Controls.Add(this.lblSMax);
            this.Controls.Add(this.lblHMax);
            this.Controls.Add(this.lblVMin);
            this.Controls.Add(this.lblSMin);
            this.Controls.Add(this.lblHMin);
            this.Controls.Add(this.vMax);
            this.Controls.Add(this.sMin);
            this.Controls.Add(this.vMin);
            this.Controls.Add(this.hMax);
            this.Controls.Add(this.sMax);
            this.Controls.Add(this.hMin);
            this.Controls.Add(this.pictBoxColor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sizeFontList);
            this.Controls.Add(this.fontsList);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.colorBtn);
            this.Controls.Add(this.addSlide);
            this.Controls.Add(this.erase);
            this.Controls.Add(this.draw);
            this.Controls.Add(this.slidesList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "startPresentation";
            this.Text = "Contour Extraction";
            ((System.ComponentModel.ISupportInitialize)(this.hMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vMax)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slides)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox slides;
        private System.Windows.Forms.ListBox slidesList;
        private System.Windows.Forms.Button draw;
        private System.Windows.Forms.Button erase;
        private System.Windows.Forms.Button addSlide;
        private System.Windows.Forms.Button colorBtn;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox fontsList;
        private System.Windows.Forms.ComboBox sizeFontList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer saveTimer;
        private System.Windows.Forms.PictureBox pictBoxColor;
        private System.Windows.Forms.TrackBar hMin;
        private System.Windows.Forms.TrackBar sMax;
        private System.Windows.Forms.TrackBar hMax;
        private System.Windows.Forms.TrackBar vMin;
        private System.Windows.Forms.TrackBar sMin;
        private System.Windows.Forms.TrackBar vMax;
        private System.Windows.Forms.Label lblHMin;
        private System.Windows.Forms.Label lblSMin;
        private System.Windows.Forms.Label lblVMin;
        private System.Windows.Forms.Label lblHMax;
        private System.Windows.Forms.Label lblSMax;
        private System.Windows.Forms.Label lblVMax;
        private System.Windows.Forms.Button openCam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Timer camerStream;
        private System.Windows.Forms.Button marker;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}

