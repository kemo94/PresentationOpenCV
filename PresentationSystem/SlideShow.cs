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
namespace FindContours
{
    public partial class SlideShow : Form
    {
         
        MCvConvexityDefect[] defectArray;
        Emgu.CV.Capture c;
        Image<Bgr, Byte> colorImage;
        SoundPlayer soundPlayer; 
        Files files = new Files(); 
        int fingerNum = 1;
        bool isPlay = false;
        startPresentation mainWindow = new startPresentation();
        Painter painter = new Painter();  
        int slideIndex = 1;
        int hMin , sMin, vMin, hMax, sMax, vMax;
        int min = 10000000, indxMn = 0;
        bool first = true;
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
            painter.initX = -1;
            painter.initY = -1;
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

                Image<Gray, byte> imageHSVDest = imgHsv.InRange(lowerLimit, upperLimit);
                Image<Gray, byte> erroded = imageHSVDest.Erode(4);
                Image<Gray, byte> dilated = erroded.Dilate(4);
                Image<Gray, byte> binary = dilated.SmoothBlur(7, 7);

                erroded = binary.Erode(4);
                dilated = erroded.Dilate(4);
                binary = dilated.SmoothBlur(7, 7);


                Image<Gray, Byte> dst = new Image<Gray, Byte>(binary.Width, binary.Height);
                StructuringElementEx element = new StructuringElementEx(3, 3, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);

                CvInvoke.cvMorphologyEx(binary, dst, IntPtr.Zero, element, CV_MORPH_OP.CV_MOP_CLOSE, 1);

                using (MemStorage storage = new MemStorage())
                {
                    Contour<Point> contours = dst.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, storage);
                    Double biggestArea = 0;
                    
                    Rectangle handRect;
                    MCvBox2D box;
                    Contour<Point> biggestHandContour = null;
                    Contour<Point> temp = null;

                    while (contours != null)
                    {

                        temp = contours.ApproxPoly(contours.Perimeter * 0.00025, storage);
                        if (contours.Area > biggestArea)
                        {
                            biggestArea = contours.Area;
                            biggestHandContour = temp;
                        }
                        contours = contours.HNext;
                    }

                    Seq<Point> hull;
                    if (biggestHandContour != null)
                    {

                        hull = biggestHandContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);

                        box = biggestHandContour.GetMinAreaRect();
                        PointF[] points = box.GetVertices();
                        handRect = box.MinAreaRect();
                        colorImage.Draw(handRect, new Bgr(200, 0, 0), 1);

                        colorImage.DrawPolyline(hull.ToArray(), true, new Bgr(200, 125, 75), 2);
                        box = biggestHandContour.GetMinAreaRect();

                        defectArray = biggestHandContour.GetConvexityDefacts(storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();
                        DrawAndComputeFingersNum(box);
                        
                    }


                }
                if (fingerNum != 1 && fingerNum != 2)

                    presentationTimer.Interval = 1000;
                if (fingerNum == 1)
                {
                    presentationTimer.Interval = 10;
                    if (painter.initX < 0 && painter.initY < 0)
                    {
                        painter.initX = (int)defectArray[indxMn].EndPoint.X;
                        painter.initY = (int)defectArray[indxMn].EndPoint.Y;
                    }
                    if (painter.pen != null && painter.initX > 0 && painter.initY > 0)
                    {
                        int c_x = (int)defectArray[indxMn].EndPoint.X;
                        int c_y = (int)defectArray[indxMn].EndPoint.Y;

                        painter.graph.DrawLine(painter.pen, new Point(painter.initX, painter.initY), new Point(c_x, c_y));
                        Cursor.Position = new Point((int)defectArray[indxMn].EndPoint.X, (int)defectArray[indxMn].EndPoint.Y);

                        painter.initX = c_x;
                        painter.initY = c_y;
                    }
                    if (first || defectArray.Length <= indxMn)
                    {
                        getMinIndx();
                        first = false;
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
                        getMinIndx();
                        first = false;
                    }
                    DoMouseClick();
                }

                else if (fingerNum == 3 && slideIndex < files.sizeSlides/*&& files.soundsPathes[slideIndex - 1] != "EMPTY"*/){
                    slideIndex++;
                     slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                     first = true;
                }
                else if (fingerNum == 4 && slideIndex > 1){
                        slideIndex--;
                    slides.ImageLocation = files.imgsPath + "//" + slideIndex + ".jpg";
                    first = true;
                }
                else if (fingerNum == 5 && files.soundsPathes[slideIndex - 1] != "EMPTY" )
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

                textBox1.Text = fingerNum+"";
            }
        }

        double getAngle(PointF s, PointF f, PointF e)
        {
            double l1 = distanceP2P(f, s);
            double l2 = distanceP2P(f, e);
            double dot = (s.X - f.X) * (e.X - f.X) + (s.Y - f.Y) * (e.Y - f.Y);
            double angle = Math.Acos(dot / (l1 * l2));
            angle = angle * 180 / Math.PI;
            return angle;
        }

        double distanceP2P(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Abs(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2)));
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
        private void DrawAndComputeFingersNum(MCvBox2D box)
        {
            fingerNum = 1;
            for (int i = 0; i < defectArray.Length; i++)
            {
                PointF startPoint = new PointF((float)defectArray[i].StartPoint.X,
                                                (float)defectArray[i].StartPoint.Y);

                PointF depthPoint = new PointF((float)defectArray[i].DepthPoint.X,
                                                (float)defectArray[i].DepthPoint.Y);

                PointF endPoint = new PointF((float)defectArray[i].EndPoint.X,
                                                (float)defectArray[i].EndPoint.Y);

                LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                CircleF startCircle = new CircleF(startPoint, 5f);

                CircleF depthCircle = new CircleF(depthPoint, 5f);

                CircleF endCircle = new CircleF(endPoint, 5f);

                //Custom heuristic based on some experiment, double check it before use
                  if (getAngle(startPoint, depthPoint, endPoint) > 20 && getAngle(startPoint, depthPoint, endPoint) < 80)
                
                    fingerNum++;
//                    int angle =(int) getAngle(startPoint, depthPoint, endPoint);

            } 
        
        }
        void getMinIndx() {

            for (int i = 0; i < defectArray.Length; i ++  ){
                if (min > (int)defectArray[0].EndPoint.Y){
                    indxMn = i;
                    min = (int)defectArray[0].EndPoint.Y;
                }
            }
            
        }
        private void prev_Click(object sender, EventArgs e)
        {

        }

        private void next_Click(object sender, EventArgs e)
        {
            
        }
    }
}
