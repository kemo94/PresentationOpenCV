using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FindContours
{
    class Files
    {

        public string imgsPath; 
        public string[] soundsPathes;
        public int sizeSlides;
        public int curIndx;
        public string[] HSV; 
        public Files() {
            imgsPath = "../../images";
            var size = File.ReadAllText("size.txt");
            sizeSlides = Int32.Parse(size);
            var indx = File.ReadAllText("indx.txt");
            curIndx = Int32.Parse(indx);

            var soundsFile = File.ReadAllText("sounds.txt");
            soundsPathes = soundsFile.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var arrHSV = File.ReadAllText("hsv.txt");
            HSV = arrHSV.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public int addNewSlide() {

            var newSize = (++sizeSlides).ToString();
            File.Copy("def.jpg", "../../images/" + (sizeSlides).ToString() + ".jpg");
            File.WriteAllText("size.txt", newSize);
            File.AppendAllText("sounds.txt", "\nEMPTY"); 
            var soundsFile = File.ReadAllText("sounds.txt");
            soundsPathes = soundsFile.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    
            return Int32.Parse(newSize);
        }
        public void updetNewIndex(string indx)
        {
            File.WriteAllText("indx.txt", indx);
        }
        public void updatePathesSounds(string[] soundsPathes)
        {
            File.WriteAllLines("sounds.txt", soundsPathes);
            this.soundsPathes = soundsPathes;
        }

        public void updateHSV(string[] HSV)
        {
            File.WriteAllLines("hsv.txt", HSV);
            this.HSV = HSV;
        }
    }
}
