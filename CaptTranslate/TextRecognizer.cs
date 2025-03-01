using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;
using static CaptTranslate.ListData;

namespace CaptTranslate
{
    internal class TextRecognizer
    {
        private static FullOcrModel _currentModel;
        public static void SelectModel(Language language)
        {
            switch (language)
            {
                default:
                case Language.English:
                    _currentModel = LocalFullModels.EnglishV4;
                    break;
                case Language.Chinese:
                    _currentModel = LocalFullModels.ChineseV4;
                    break;
                case Language.Korean:
                    _currentModel = LocalFullModels.KoreanV4;
                    break;
            }
        }

        public static string Recognize(byte[] imageData)
        {
            if(_currentModel == null) 
                return "";
            using (var src = Cv2.ImDecode(imageData, ImreadModes.Color))
            using (var paddleOcrAll = new PaddleOcrAll(_currentModel, PaddleDevice.Onnx(2)))
            {
                var result = paddleOcrAll.Run(src);
                return result.Text;
            }
        }

        public static string ClearText(string text)
        {
            text = text.Trim().Replace("\n", " ");
            return text;
        }
    }
}
