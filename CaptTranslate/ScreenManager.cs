using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models.Local;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CaptTranslate
{
    internal class ScreenManager
    {
        public static string FileName = "screenshot.png";
        public static byte[] ImageToByte(string image)
        {
            return File.ReadAllBytes(image);
        }

        public static string CaptureScreen()
        {
            Rectangle selectedArea = ImageData.SelectedArea;
            if (selectedArea.IsEmpty)
                return null;

            using Bitmap screenshot = new Bitmap(selectedArea.Width, selectedArea.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(selectedArea.Left, selectedArea.Top, 0, 0, selectedArea.Size, CopyPixelOperation.SourceCopy);
            }
            ImageData.BackgroundColor = GetBackgroundColor(screenshot);
            ImageData.TextColor = InvertColor(ImageData.BackgroundColor);
            screenshot.Save(FileName);
            GC.Collect();
            return FileName;
        }
        private static Color InvertColor(Color color)
        {
            double luminance = (0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B) / 255;
            return luminance < 0.5 ? Color.White : Color.Black;
            //return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }

        private static Color GetBackgroundColor(Bitmap bmp)
        {
            var colorGroups = from x in Enumerable.Range(0, bmp.Width)
                              from y in Enumerable.Range(0, bmp.Height)
                              group bmp.GetPixel(x, y) by bmp.GetPixel(x, y) into g
                              orderby g.Count() descending
                              select g.Key;

            return colorGroups.First();
        }
    }
}
