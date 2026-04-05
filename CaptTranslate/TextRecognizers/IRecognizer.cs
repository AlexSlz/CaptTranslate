using System.Threading;
using System.Threading.Tasks;

namespace CaptTranslate.TextRecognizers;

public interface IRecognizer
{
    string Name  { get; }

    string[] GetAvailableModels();
    void SelectModel(string modelName);
    Task<OperationResult> ARecognize(byte[] imageData, CancellationToken ct = default);
}