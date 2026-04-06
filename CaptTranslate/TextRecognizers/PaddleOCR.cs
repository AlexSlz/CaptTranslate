using System;
using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;
using System.Threading;
using System.Threading.Tasks;

namespace CaptTranslate.TextRecognizers;

public class PaddleOCR : IRecognizer
{
    public string Name => "PaddleOCR";
    private FullOcrModel _currentModel;
    public string[] GetAvailableModels()
    {
        return ["English", "Chinese", "Korean"];
    }

    public void SelectModel(string modelName)
    {
        _currentModel = modelName switch
        {
            "Chinese" => LocalFullModels.ChineseV5,
            "Korean" => LocalFullModels.KoreanV4,
            _ => LocalFullModels.EnglishV4
        };
    }

    public async Task<OperationResult> ARecognize(byte[] imageData, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            try
            {
                var localModel = _currentModel;

                using var src = Cv2.ImDecode(imageData, ImreadModes.Color);
                using var all = new PaddleOcrAll(localModel, PaddleDevice.Onnx(2))
                {
                    AllowRotateDetection = true,
                    Enable180Classification = false,
                };
                ct.ThrowIfCancellationRequested();
                var result = all.Run(src);
                return OperationResult.Success(result.Text.Replace("\n", " ").Trim());
            }
            catch (Exception ex)
            {
                return OperationResult.Failure<PaddleOCR>(ex.Message);
            }
            finally
            {
                _currentModel = null; 
            }
        }, ct);
    }
}