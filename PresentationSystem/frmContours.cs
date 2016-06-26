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
        MCvConvexityDefect[] defectArray;
        SoundPlayer soundPlayer;
        Painter painter = new Painter();
        Files files = new Files(); 
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

                Image<Gray, byte> imageHSVDest = imgHsv.InRange(lowerLimit, upperLimit);
                Image<Gray, byte> erroded = imageHSVDest.Erode(2);
                Image<Gray, byte> dilated = erroded.Dilate(2);
                Image<Gray, byte> binary = dilated.SmoothBlur(7, 7);

                erroded = binary.Erode(2);
                dilated = erroded.Dilate(2);
                binary = dilated.SmoothBlur(7, 7);


                Image<Gray, Byte> dst = new Image<Gray, Byte>(binary.Width, binary.Height);
                StructuringElementEx element = new StructuringElementEx(3, 3, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);

                CvInvoke.cvMorphologyEx(binary, dst, IntPtr.Zero, element, CV_MORPH_OP.CV_MOP_CLOSE, 1);
                colorImage = binary.Convert<Bgr , Byte>();
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
                 //       colorImage.Draw(biggestHandContour, new Bgr(Color.LimeGreen), 2);
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


                } MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 1d, 1d);
         
           //     String f = "X : " + defectArray[0].EndPoint.X + "  Y : " + defectArray[0].EndPoint.Y ;
               // colorImage.Draw(f, ref font, new Point(defectArray[0].EndPoint.X, defectArray[0].EndPoint.Y), new Bgr(Color.White));
                 
                //textBox1.Text = "X : " + defectArray[0].EndPoint.X +  "Y : " + defectArray[0].EndPoint.Y;
                
                pictBoxColor.Image = colorImage.ToBitmap();
            }
        }

        private void DrawAndComputeFingersNum(MCvBox2D box)
        {
               MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 1d, 1d);
         
            int fingerNum = 1;  
            for (int i = 0; i < defectArray.Length; i++)
            {
                Point startPoint = new Point((int)defectArray[i].StartPoint.X,
                                                (int)defectArray[i].StartPoint.Y);

                Point depthPoint = new Point((int)defectArray[i].DepthPoint.X,
                                                (int)defectArray[i].DepthPoint.Y);

                Point endPoint = new Point((int)defectArray[i].EndPoint.X,
                                                (int)defectArray[i].EndPoint.Y);

                LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                CircleF startCircle = new CircleF(startPoint, 5f);

                CircleF depthCircle = new CircleF(depthPoint, 5f);

                CircleF endCircle = new CircleF(endPoint, 5f);

                //Custom heuristic based on some experiment, double check it before use
              //  if ((startCircle.Center.Y < box.center.Y || depthCircle.Center.Y < box.center.Y) && (startCircle.Center.Y < depthCircle.Center.Y) && (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > box.size.Height / 6.5))
                if (getAngle(startPoint, depthPoint, endPoint) > 20 && getAngle(startPoint, depthPoint, endPoint) < 80)
                {
                    fingerNum++;
                    int angle =(int) getAngle(startPoint, depthPoint, endPoint);

                    colorImage.Draw(angle.ToString(), ref font, new Point(depthPoint.X, depthPoint.Y), new Bgr(Color.White));
                    colorImage.Draw(startDepthLine, new Bgr(Color.Green), 2);
                    colorImage.Draw(depthEndLine, new Bgr(Color.Magenta), 2);
                }

                                
              //  colorImage.Draw(startCircle, new Bgr(Color.Red), 2);
              //  colorImage.Draw(depthCircle, new Bgr(Color.Yellow), 5);
                //currentFrame.Draw(endCircle, new Bgr(Color.DarkBlue), 4);
            } 

            colorImage.Draw(fingerNum.ToString(), ref font, new Point(50, 150), new Bgr(Color.White));
        }
        
        double getAngle(PointF s, PointF f, PointF e){
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
