﻿using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;
using System.Runtime;
using System.Text.RegularExpressions;

namespace CaptTranslate
{
    internal class TextRecognizer
    {
        private static FullOcrModel _currentModel;
        private static PaddleOcrAll _paddleOcrAll;
        public static void InitializeModel()
        {
            _currentModel = LocalFullModels.EnglishV4;

            _paddleOcrAll = new PaddleOcrAll(_currentModel, PaddleDevice.Onnx(2))
            {
                AllowRotateDetection = false, 
                Enable180Classification = false,
            };
        }


        public static string Recognize(byte[] imageData)
        {
            using var src = Cv2.ImDecode(imageData, ImreadModes.Color);
            if(Settings.ScaleImage)
                Cv2.Resize(src, src, new OpenCvSharp.Size(src.Width * 0.6, src.Height * 0.6));
            return _paddleOcrAll.Run(src).Text;
        }

        public static string ClearText(string text)
        {
            text = text.Trim().Replace("\n", " ")
.Replace(",.", ",")
.Replace(".,", ".")
.Replace("?.", "?")
.Replace(".?", "?")
.Replace("!.", "!")
.Replace(".!", "!")

.Replace("..", ".")

.Replace("..", "...");
            return text;
        }
    }
}
