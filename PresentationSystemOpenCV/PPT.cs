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
        PPt.Application pptApplication;
        PPt.Presentation presentation;
        PPt.Slides slides;
        PPt.Slide slide;

        int slidescount;
        int slideIndex=1;

        
        string[] HSV = new string[6];
        DetectHand hand = new DetectHand();
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
                pptApplication = Marshal.GetActiveObject("PowerPoint.Application") as PPt.Application;
            }
            catch
            {
                MessageBox.Show("Please Run PowerPoint Firstly", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            if (pptApplication != null)
            {
                presentation = pptApplication.ActivePresentation;
                slides = presentation.Slides;
                slidescount = slides.Count;
                try
                {
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
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
                
                    HSV[0] = hMin + "";
                    HSV[1] = sMin + "";
                    HSV[2] = vMin + "";
                    HSV[3] = hMax + "";
                    HSV[4] = sMax + "";
                    HSV[5] = vMax + "";

                    hand.setHSV(HSV);
                    hand.convertToBinary(colorImage);
                    fingerNum = hand.getFingeresNum();
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
        private void button1_Click(object sender, EventArgs e)
        { 
        }

    }
}
