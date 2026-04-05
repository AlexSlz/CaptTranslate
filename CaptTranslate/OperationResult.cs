using System.Windows.Forms;

namespace CaptTranslate;

public class OperationResult
{
    public readonly bool IsError;
    public string Text { get; }

    private OperationResult(string text, bool isError = false)
    {
        Text = text;
        IsError = isError;
    }

    public static OperationResult Success(string text) => new(text);

    public static OperationResult Failure<T>(string errorMessage)
    {
        var className = typeof(T).Name;
        MessageBox.Show(errorMessage, className, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return new OperationResult(errorMessage, true);
    }
}