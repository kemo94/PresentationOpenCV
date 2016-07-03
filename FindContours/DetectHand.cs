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
    
    class DetectHand
    {

        MCvConvexityDefect[] defectArray;
        Image<Bgr, Byte> colorImage;
        int fingerNum = 1;
        
        int hMin , sMin, vMin, hMax, sMax, vMax;
        int min = 10000000, indxMn = 0; 
         
        public void setHSV(string[] HSV){
            hMin = Int32.Parse(HSV[0]);
            sMin = Int32.Parse(HSV[1]);
            vMin = Int32.Parse(HSV[2]);
            hMax = Int32.Parse(HSV[3]);
            sMax = Int32.Parse(HSV[4]);
            vMax = Int32.Parse(HSV[5]);
        }
        public MCvConvexityDefect[] getDefectArray()
        {
            return  defectArray;
        }
        public int getMinIndx()
        {

            for (int i = 0; i < defectArray.Length; i++)
            {
                if (min > (int)defectArray[0].EndPoint.Y)
                {
                    indxMn = i;
                    min = (int)defectArray[0].EndPoint.Y;
                }
            }
            return indxMn;

        }
        public int getFingeresNum()
        {

            return fingerNum;
        }
        public Image<Bgr, Byte> getBinaryImage()
        {
            return colorImage;
        }
        public void convertToBinary(Image<Bgr, Byte> originColorImage)
        {
                colorImage = originColorImage;
                Image<Hsv, byte> imgHsv = colorImage.Convert<Hsv, Byte>();
                Hsv lowerLimit = new Hsv(hMin, sMin, vMin);
                Hsv upperLimit = new Hsv(hMax, sMax, vMax);

                Image<Gray, byte> imageHSVDest = imgHsv.InRange(lowerLimit, upperLimit);
            // morphological operators
                Image<Gray, byte> erroded = imageHSVDest.Erode(2);
                Image<Gray, byte> dilated = erroded.Dilate(2);
                Image<Gray, byte> binary = dilated.SmoothBlur(7, 7);

                erroded = binary.Erode(2);
                dilated = erroded.Dilate(2);
                binary = dilated.SmoothBlur(7, 7);

                Image<Gray, Byte> dst = new Image<Gray, Byte>(binary.Width, binary.Height);
                StructuringElementEx element = new StructuringElementEx(3, 3, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);

                CvInvoke.cvMorphologyEx(binary, dst, IntPtr.Zero, element, CV_MORPH_OP.CV_MOP_CLOSE, 1);

                colorImage = binary.Convert<Bgr, Byte>();
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


                }
        }

        private void DrawAndComputeFingersNum(MCvBox2D box)
        {
            MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 1d, 1d);
            fingerNum = 1;
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

                if (getAngle(startPoint, depthPoint, endPoint) > 20 && getAngle(startPoint, depthPoint, endPoint) < 80)
                {
                    fingerNum++;
                    int angle = (int)getAngle(startPoint, depthPoint, endPoint);

                    colorImage.Draw(angle.ToString(), ref font, new Point(depthPoint.X, depthPoint.Y), new Bgr(Color.White));
                    colorImage.Draw(startDepthLine, new Bgr(Color.Green), 2);
                    colorImage.Draw(depthEndLine, new Bgr(Color.Magenta), 2);
                }

            }

            colorImage.Draw(fingerNum.ToString(), ref font, new Point(50, 150), new Bgr(Color.White));

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

    
    }
}
