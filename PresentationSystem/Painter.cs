using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace FindContours
{
    class Painter
    {
        public ColorDialog color;
        public Graphics graph;
        public Pen pen;
        public bool startPaint = false;
        public int initX ;
        public int initY ;
        
    }
}
