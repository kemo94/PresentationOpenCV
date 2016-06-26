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
using Microsoft.Office.Core;
using PPt = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;


namespace FindContours
{
    public partial class PPT : Form
    {
        // Define PowerPoint Application object
        PPt.Application pptApplication;
        // Define Presentation object
        PPt.Presentation presentation;
        // Define Slide collection
        PPt.Slides slides;
        PPt.Slide slide;

        // Slide count
        int slidescount;
        // slide index
        int slideIndex=1;

        MCvConvexityDefect[] defectArray;
        Emgu.CV.Capture c;
        Image<Bgr, Byte> colorImage;
        int fingerNum = 1;
        startPresentation mainWindow = new startPresentation(); 
        int hMin , sMin, vMin, hMax, sMax, vMax; 
        
        public PPT(int hMin, int sMin, int vMin, int hMax, int sMax, int vMax)
        {

            InitializeComponent();
            this.hMin = hMin;
            this.sMin = sMin;
            this.vMin = vMin;
            this.hMax = hMax;
            this.sMax = sMax;
            this.vMax = vMax;

        }

        private void openCam_Click(object sender, EventArgs e)
        {
            
            if (c == null)
            {
                c = new Emgu.CV.Capture();
            }
            try
            {
                // Get Running PowerPoint Application object
                pptApplication = Marshal.GetActiveObject("PowerPoint.Application") as PPt.Application;
            }
            catch
            {
                MessageBox.Show("Please Run PowerPoint Firstly", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            if (pptApplication != null)
            {
                // Get Presentation Object
                presentation = pptApplication.ActivePresentation;
                // Get Slide collection object
                slides = presentation.Slides;
                // Get Slide count
                slidescount = slides.Count;
                // Get current selected slide 
                try
                {
                    // Get selected slide object in normal view
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
                    // Get selected slide object in reading view
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            
                presentationTimer.Enabled = true;
            }

        }

        private void camStream_Tick(object sender, EventArgs e)
        {

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

                if (fingerNum == 3 )
                {
                    slideIndex = slide.SlideIndex + 1;
                    if (slideIndex <= slidescount)
                    {
                        slide = slides[slideIndex];
                        slides[slideIndex].Select();
                    }
                }
                else if (fingerNum == 4 )
                {

                    slideIndex = slide.SlideIndex - 1;
                    if (slideIndex >= 1)
                    {
                        slide = slides[slideIndex];
                        slides[slideIndex].Select();

                    }
                    
                }

                textBox1.Text = fingerNum + "";

            }
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
                  if (getAngle(startPoint, depthPoint, endPoint) > 20 && getAngle(startPoint, depthPoint, endPoint) < 80)
                
                    fingerNum++;

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

        private void button1_Click(object sender, EventArgs e)
        { 
        }

    }
}
