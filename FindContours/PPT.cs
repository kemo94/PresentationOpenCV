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
using Microsoft.Office.Core;
using PPt = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;


namespace FindContours
{
    public partial class PPT : Form
    {
        PPt.Application pptApplication;
        PPt.Presentation presentation;
        PPt.Presentations objPresSet;
        PPt.Slides slides;
        MCvConvexityDefect[] defectArray;
       // PPt.Slide slide;

        Painter painter = new Painter();  
        int slidescount;
        int slideIndex=1;


        int indxMn = 0;
        bool first = true;
        string[] HSV = new string[6];
        DetectHand hand = new DetectHand();
        Emgu.CV.Capture c;
        Image<Bgr, Byte> colorImage;
        int fingerNum = 1;
        startPresentation mainWindow = new startPresentation(); 
        int hMin , sMin, vMin, hMax, sMax, vMax;

        string strPres ;
        public PPT(int hMin, int sMin, int vMin, int hMax, int sMax, int vMax)
        {

            InitializeComponent();

            painter.initX = -1;
            painter.initY = -1;
            this.hMin = hMin;
            this.sMin = sMin;
            this.vMin = vMin;
            this.hMax = hMax;
            this.sMax = sMax;
            this.vMax = vMax;

        }

        private void openCam_Click(object sender, EventArgs e)
        {


            //Create an instance of PowerPoint.
            pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();

            // Show PowerPoint to the user.
            pptApplication.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;

            objPresSet = pptApplication.Presentations;

            //open the presentation
            presentation = objPresSet.Open(strPres, MsoTriState.msoFalse,
            MsoTriState.msoTrue, MsoTriState.msoTrue);

            presentation.SlideShowSettings.Run();

            if (c == null)
            {
                c = new Emgu.CV.Capture();
            }
            /*
            try
            {
                pptApplication = Marshal.GetActiveObject("PowerPoint.Application") as PPt.Application;
            }
            catch
            {
                MessageBox.Show("Please Run PowerPoint Firstly", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
             * */
            if (pptApplication != null)
            {
                presentation = pptApplication.ActivePresentation;
                slides = presentation.Slides;
                slidescount = slides.Count;
              /*  try
                {
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            */
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
                }
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
                    else if ( painter.initX > 0 && painter.initY > 0)
                    {
                        int c_x = (int)defectArray[indxMn].EndPoint.X;
                        int c_y = (int)defectArray[indxMn].EndPoint.Y;
                        try
                        {
                            presentation.SlideShowWindow.View.DrawLine(c_x, c_y, painter.initX, painter.initY);
                        }
                        catch(Exception){}
                        painter.initX = c_x;
                        painter.initY = c_y;
                    }
                   
                   
                }
                if (fingerNum == 3 )
                {
                    presentationTimer.Interval = 1000;

                    painter.initX = -1;
                    painter.initY = -1;        
                 //   slideIndex = slide.SlideIndex + 1;
                    if (slideIndex < slidescount)
                    {

                        presentation.SlideShowWindow.View.GotoSlide(++slideIndex);
                      //  slide = slides[slideIndex];
                      //  slides[slideIndex].Select();
                    }
                }
                else if (fingerNum == 4 )
                {
                    presentationTimer.Interval = 1000;

                    painter.initX = -1;
                    painter.initY = -1;
                    //slideIndex = slide.SlideIndex - 1;
                    if (slideIndex > 1)
                    {

                        presentation.SlideShowWindow.View.GotoSlide(--slideIndex);
                        //slide = slides[slideIndex];
                        //slides[slideIndex].Select();

                    }
                    
                }

                textBox1.Text = fingerNum + "";

        }
        private void button1_Click(object sender, EventArgs e)
        { 
        }

        private void browse_Click(object sender, EventArgs e)
        {
             
            openFileDialog1.Multiselect = true;

            openFileDialog1.Title = "Select PPT";



            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {


                    try
                    {
                        strPres = openFileDialog1.FileName;
                     
                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show("Error: " + ex.Message);

                    }

            }
        }

    }
}
