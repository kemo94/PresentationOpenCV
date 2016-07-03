using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;
namespace FindContours
{
    public partial class SlideShow : Form
    {
         DetectHand hand = new DetectHand();
        MCvConvexityDefect[] defectArray;
        Emgu.CV.Capture c;
        Image<Bgr, Byte> colorImage;
        SoundPlayer soundPlayer; 
        Files files = new Files(); 
        int fingerNum ;
        bool isPlay = false;
        startPresentation mainWindow = new startPresentation();
        Painter painter = new Painter();  
        int slideIndex = 1;
        int hMin , sMin, vMin, hMax, sMax, vMax;
        int indxMn = 0;
        bool first = true;
        string[] HSV = new string[6];
        
        public SlideShow(int hMin, int sMin, int vMin, int hMax, int sMax, int vMax)
        {

            InitializeComponent();
            this.hMin =hMin;
            this.sMin = sMin ;
            this.vMin = vMin ;
            this.hMax = hMax ;
            this.sMax = sMax;
            this.vMax = vMax;

            painter.initX = -1;
            painter.initY = -1;
            
            if (c == null)
            {
                c = new Emgu.CV.Capture();
            }
            presentationTimer.Enabled = true;
            painter.graph = slides.CreateGraphics();
            painter.color = new ColorDialog();
            painter.color.Color = Color.Black; 
        }

        private void SlideShow_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            slides.Dock = DockStyle.Fill;
            slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
            slides.SizeMode = PictureBoxSizeMode.Zoom;
            painter.graph = slides.CreateGraphics();
            painter.color = new ColorDialog();
            painter.color.Color = Color.Black; 
         
        }

        private void SlideShow_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (presentationTimer.Enabled)
                        presentationTimer.Stop();
                    else
                        presentationTimer.Start();

                    break;
                case Keys.Left:
                case Keys.Up:
                    presentationTimer.Stop();
                    if (slideIndex > 1)
                        slideIndex--;
                    slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                    break;
                case Keys.Right:
                case Keys.Down:
                    presentationTimer.Stop();
                    if (slideIndex < files.sizeSlides)
                    {
                        slideIndex++;
                        slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                    }
                    break;
                case Keys.Escape:
                    presentationTimer.Stop();
                    this.Hide();
                    mainWindow.ShowDialog();
                    break;
                case Keys.Home:

                    presentationTimer.Stop();
                    slideIndex = 1;
                    slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";

                    break;
                case Keys.End:

                    presentationTimer.Stop();
                    slideIndex = files.sizeSlides;
                    slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";

                    break;
            }
           /* if (files.soundsPathes[slideIndex - 1] != "EMPTY")
            {
                soundPlayer = new SoundPlayer(files.soundsPathes[slideIndex - 1]);
                soundPlayer.Play();
            }*/

        }

        private void pen_Click(object sender, EventArgs e)
        { 
            painter.pen = new Pen(painter.color.Color, 3);
        }

        private void SlideShow_MouseDown(object sender, MouseEventArgs e)
        {

            if (painter.startPaint)
            {
                painter.graph.DrawLine(painter.pen, new Point(painter.initX, painter.initY), new Point(e.X, e.Y));
                painter.initX = e.X;
                painter.initY = e.Y;
            }
        }

        private void color_Click(object sender, EventArgs e)
        {

            if (painter.color.ShowDialog() == DialogResult.OK)
            {
                //color.BackColor = painter.color.Color;
                painter.pen = new Pen(painter.color.Color, 3);

            }
        }

        private void slides_MouseDown(object sender, MouseEventArgs e)
        {
            //startPaint = true; 
        }

        private void slides_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (startPaint && painter.initX > 0 && painter.initY > 0 )
            {
                painter.graph.DrawLine(painter.pen, new Point(painter.initX, painter.initY), new Point(e.X, e.Y));

                painter.initX = e.X;
                painter.initY = e.Y;
            }

            painter.initX = e.X;
            painter.initY = e.Y;*/
        }

        private void slides_MouseUp(object sender, MouseEventArgs e)
        {
            /*
            startPaint = false;

            painter.initX = -1;
            painter.initY = -1;
             */
        }

        private void eraser_Click(object sender, EventArgs e)
        {

            painter.pen = new Pen(Color.White, 13);
        }

        private void marker_Click(object sender, EventArgs e)
        {

            Color newColor = Color.FromArgb(50, Color.Blue);
            painter.pen = new Pen(newColor, 20);
        }

        private void presentationTimer_Tick(object sender, EventArgs e)
        {
            Bitmap bitMap = new Bitmap(slides.Width, slides.Height, painter.graph);
            Graphics graphics = Graphics.FromImage(bitMap);
            Rectangle rec = slides.RectangleToScreen(slides.ClientRectangle);
            graphics.CopyFromScreen(rec.Location, Point.Empty, slides.Size);
            graphics.DrawImage(bitMap, rec);
          //  bitMap.Save("../../images/" + slideIndex.ToString() + ".jpg");
            
                colorImage = c.QueryFrame().Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
                if (colorImage != null)
                {
                    Image<Hsv, byte> imgHsv = colorImage.Convert<Hsv, Byte>();
                    Hsv lowerLimit = new Hsv(hMin, sMin, vMin);
                    Hsv upperLimit = new Hsv(hMax, sMax, vMax);

                    HSV[0] = hMin + "";
                    HSV[1] = sMin + "";
                    HSV[2] = vMin + "";
                    HSV[3] = hMax + "";
                    HSV[4] = sMax + "";
                    HSV[5] = vMax + "";

                    hand.setHSV(HSV);
                    hand.convertToBinary(colorImage);
                    fingerNum = hand.getFingeresNum();
                    defectArray = hand.getDefectArray();
                   

                    if (fingerNum != 1 && fingerNum != 2)

                        presentationTimer.Interval = 1000;
                    if (fingerNum == 1)
                    {
                        presentationTimer.Interval = 10;

                        if (first || defectArray.Length <= indxMn)
                        {
                            indxMn = hand.getMinIndx();
                            first = false;
                        }
                        if (painter.initX < 0 && painter.initY < 0)
                        {
                            painter.initX = (int)defectArray[indxMn].EndPoint.X;
                            painter.initY = (int)defectArray[indxMn].EndPoint.Y;
                        }
                        else if (painter.pen != null && painter.initX > 0 && painter.initY > 0)
                        {
                            int c_x = (int)defectArray[indxMn].EndPoint.X;
                            int c_y = (int)defectArray[indxMn].EndPoint.Y;
                            try{
                                painter.graph.DrawLine(painter.pen, new Point(painter.initX, painter.initY), new Point(c_x, c_y));
                              }
                            catch(Exception){}
                            Cursor.Position = new Point((int)defectArray[indxMn].EndPoint.X, (int)defectArray[indxMn].EndPoint.Y);

                            painter.initX = c_x;
                            painter.initY = c_y;
                        }
                    }
                    else if (fingerNum == 2)
                    {

                        presentationTimer.Interval = 10;
                        Cursor.Position = new Point((int)defectArray[indxMn].EndPoint.X, (int)defectArray[indxMn].EndPoint.Y);

                        painter.initX = -1;
                        painter.initY = -1;
                        if (first || defectArray.Length <= indxMn)
                        {
                            indxMn = hand.getMinIndx();
                            first = false;
                        }
                        DoMouseClick();
                    }


                    else if (fingerNum == 3 && slideIndex < files.sizeSlides/*&& files.soundsPathes[slideIndex - 1] != "EMPTY"*/)
                    {
                        slideIndex++;
                        slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                        first = true;
                    }
                    else if (fingerNum == 4 && slideIndex > 1)
                    {
                        slideIndex--;
                        slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                        first = true;
                    }
                    else if (fingerNum == 5 && files.soundsPathes[slideIndex - 1] != "EMPTY")
                    {
                        if (!isPlay)
                        {
                            soundPlayer = new SoundPlayer(files.soundsPathes[slideIndex - 1]);
                            soundPlayer.Play();
                            isPlay = true;
                        }
                        else
                        {
                            soundPlayer.Stop();
                            isPlay = false;
                        }
                        first = true;
                    }

                    textBox1.Text = fingerNum + "";
                }
        }
        

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
           int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;          // mouse left button pressed 
        private const int MOUSEEVENTF_LEFTUP = 0x04;            // mouse left button unpressed
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;         // mouse right button pressed
        private const int MOUSEEVENTF_RIGHTUP = 0x10;           // mouse right button unpressed


        public void DoMouseClick()
        {
            uint X = Convert.ToUInt32(Cursor.Position.X);
            uint Y = Convert.ToUInt32(Cursor.Position.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        private void prev_Click(object sender, EventArgs e)
        {

        }

        private void next_Click(object sender, EventArgs e)
        {
            
        }
    }
}
