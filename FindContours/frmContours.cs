using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System.Media;
using System.Runtime.InteropServices;
namespace FindContours
{ 
    public partial class startPresentation : Form
    {
        public int slideIndex; 
        SoundPlayer soundPlayer;
        Painter painter = new Painter();
        Files files = new Files();
        DetectHand hand = new DetectHand();
        Emgu.CV.Capture c;
        Image<Bgr, Byte> colorImage;
        string[] HSV;
        public startPresentation()
        {
            InitializeComponent();
            painter.graph = slides.CreateGraphics();
            painter.color = new ColorDialog();
            painter.color.Color = colorBtn.BackColor = Color.Black;

            for (int i = 1; i <= files.sizeSlides; i++)
                slidesList.Items.Add(i);

            HSV = files.HSV;
            painter.initX = -1;
            painter.initY = -1;
            hMin.Value = Int32.Parse(HSV[0]);
            sMin.Value = Int32.Parse(HSV[1]);
            vMin.Value = Int32.Parse(HSV[2]);
            hMax.Value = Int32.Parse(HSV[3]);
            sMax.Value = Int32.Parse(HSV[4]);
            vMax.Value = Int32.Parse(HSV[5]);

            slidesList.SelectedIndex = slideIndex = files.curIndx - 1;
            fill_comboBox();
        }

        void fill_comboBox()
        {
            var fonts = File.ReadAllText("fonts.txt");
            string[] tmpFonts = fonts.Split('\n', '\r');

            for (int i = 0; i < tmpFonts.Length; i++)
                if (tmpFonts[i] != "")
                    fontsList.Items.Add(tmpFonts[i]);
            for (int i = 8; i <= 100; i += 2)
                sizeFontList.Items.Add(i);

            sizeFontList.Text = "12";
            fontsList.Text = "Italic";
        } 
          
        private void erase_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";

            painter.pen = new Pen(Color.White, 20); 
        }

        private void draw_Click(object sender, EventArgs e)
        {

            txtInput.Text = "";

            painter.pen = new Pen(painter.color.Color, 3); 
         }

        private void slides_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (painter.startPaint && painter.pen != null )
            
                painter.graph.DrawLine(painter.pen, new Point(painter.initX , painter.initY ), new Point(e.X, e.Y));
                
            
            painter.initX = e.X;
            painter.initY = e.Y;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (painter.color.ShowDialog() == DialogResult.OK)
            {
                colorBtn.BackColor = painter.color.Color;
                painter.pen = new Pen(painter.color.Color, 3);

            }
        }

        private void addSlide_Click(object sender, EventArgs e)
        {

            int newSize = files.addNewSlide();
            slidesList.Items.Add(newSize);
            slidesList.SelectedIndex = slideIndex = newSize - 1;
        }

        private void slidesList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string indx = slidesList.SelectedItem.ToString();
            slideIndex = Int32.Parse(indx);
            slides.ImageLocation = files.imgsPath + "//" + slidesList.SelectedItem.ToString() + ".jpg";
            slides.SizeMode = PictureBoxSizeMode.Zoom;
            files.updetNewIndex(indx);
            
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            this.Hide();
            SlideShow slideShow = new SlideShow(hMin.Value, sMin.Value, vMin.Value, hMax.Value, sMax.Value, vMax.Value);
            slideShow.ShowDialog();

        }

        private void slides_MouseDown(object sender, MouseEventArgs e)
        {

            saveTimer.Start();
            painter.startPaint = true;
            if (txtInput.Text != "" )
            {
                painter.initX = e.X;
                painter.initY = e.Y;

                Font font = new Font(fontsList.Text, Int32.Parse(sizeFontList.Text));

                painter.graph.DrawString(txtInput.Text, font, Brushes.Black, new Point(e.X, e.Y));

            }

            painter.initX = e.X;
            painter.initY = e.Y;
        }

        private void slides_MouseUp(object sender, MouseEventArgs e)
        {

            saveTimer.Stop();
            painter.startPaint = false;
            painter.initX = -1;
            painter.initY = -1;
        }

        private void openCam_Click(object sender, EventArgs e)
        {

             
            if (c == null)
            {
                c = new Emgu.CV.Capture();
            }
            camerStream.Enabled = true;
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            Bitmap bitMap = new Bitmap(slides.Width, slides.Height, painter.graph);
            Graphics graphics = Graphics.FromImage(bitMap);
            Rectangle rec = slides.RectangleToScreen(slides.ClientRectangle);
            graphics.CopyFromScreen(rec.Location, Point.Empty, slides.Size);
            graphics.DrawImage(bitMap, rec);
            bitMap.Save("../../images/" + slideIndex.ToString() + ".jpg");
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if (files.soundsPathes[slideIndex - 1] != "EMPTY")
            {
                soundPlayer = new SoundPlayer(files.soundsPathes[slideIndex - 1]);
                soundPlayer.Play();
                playBtn.Enabled = false;
                stopBtn.Enabled = true;
            }

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            stopBtn.Enabled = false;
            playBtn.Enabled = true;
        }

        private void camerStream_Tick(object sender, EventArgs e)
        {

            colorImage = c.QueryFrame().Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
            if (colorImage != null)
            {
                Image<Hsv, byte> imgHsv = colorImage.Convert<Hsv, Byte>();
                Hsv lowerLimit = new Hsv(hMin.Value, sMin.Value, vMin.Value);
                Hsv upperLimit = new Hsv(hMax.Value, sMax.Value, vMax.Value);

                HSV[0] = lblHMin.Text = hMin.Value + "";
                HSV[1] = lblSMin.Text = sMin.Value + "";
                HSV[2] = lblVMin.Text = vMin.Value + "";
                HSV[3] = lblHMax.Text = hMax.Value + "";
                HSV[4] = lblSMax.Text = sMax.Value + "";
                HSV[5] = lblVMax.Text = vMax.Value + "";
                files.updateHSV(HSV);

                hand.setHSV(HSV);
                hand.convertToBinary(colorImage);
                pictBoxColor.Image = hand.getBinaryImage().ToBitmap();
            }
        }


        private void marker_Click(object sender, EventArgs e)
        {

            Color newColor = Color.FromArgb(100, painter.color.Color);
            painter.pen = new Pen(newColor, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            PPT slideShow = new PPT(hMin.Value, sMin.Value, vMin.Value, hMax.Value, sMax.Value, vMax.Value);
            slideShow.ShowDialog();
        }
         
    }
}
