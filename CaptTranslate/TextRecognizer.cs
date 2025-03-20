using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;
using System.Linq;
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
            if (_currentModel == null)
                return "";
            using (var src = Cv2.ImDecode(imageData, ImreadModes.Color))
            using (var paddleOcrAll = new PaddleOcrAll(_currentModel, PaddleDevice.Onnx(2)))
            {
                var result = paddleOcrAll.Run(src);

                var regions = result.Regions;
                var yArray = regions.Select(x => x.Rect.Center.Y);

                double mean = yArray.Average();
                double variance = yArray.Sum(num => Math.Pow(num - mean, 2)) / yArray.Count();
                double clusterSize = Math.Sqrt(variance);


                List<List<PaddleOcrResultRegion>> clusters = new List<List<PaddleOcrResultRegion>>();
                List<PaddleOcrResultRegion> currentCluster = new List<PaddleOcrResultRegion>();

                foreach (var region in regions)
                {
                    if (currentCluster.Count == 0 || region.Rect.Center.Y <= currentCluster.Last().Rect.Center.Y + clusterSize)
                    {
                        currentCluster.Add(region);
                    }
                    else
                    {
                        clusters.Add(new List<PaddleOcrResultRegion>(currentCluster));
                        currentCluster.Clear();
                        currentCluster.Add(region);
                    }
                }
                if (currentCluster.Count > 0)
                {
                    clusters.Add(currentCluster);
                }

                List<PaddleOcrResultRegion> sortClusters = new List<PaddleOcrResultRegion>();
                foreach (var region in clusters)
                {
                    sortClusters.AddRange(region.OrderBy(x => x.Rect.Center.X));
                }
                currentCluster.Clear();
                clusters.Clear();
                List<string> resultText = sortClusters.Select(x => x.Text).ToList();
                sortClusters.Clear();

                return string.Join(" ", resultText).Trim();
            }
        }

    }
}
