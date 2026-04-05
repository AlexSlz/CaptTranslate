using System.Threading.Tasks;

namespace CaptTranslate.Translators;

public interface ITranslator
{
    string Name { get; }
    Task<OperationResult> Translate(string text);
}