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
            if (ImageData.SelectedArea.IsEmpty)
                return null;

            using Bitmap screenshot = new Bitmap(ImageData.SelectedArea.Width, ImageData.SelectedArea.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(ImageData.SelectedArea.Left, ImageData.SelectedArea.Top, 0, 0, ImageData.SelectedArea.Size, CopyPixelOperation.SourceCopy);
            }
            screenshot.Save(FileName);
            GC.Collect();
            return FileName;
        }
    }
}
