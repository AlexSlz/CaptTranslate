using System;
using System.IO;
using Newtonsoft.Json;

namespace CaptTranslate;

public class JsonAction<T>
{
    private readonly string _filePath;
    private Action<T> _onImport;
    
    public JsonAction(string FileName, Action<T> onImport)
    {
        _filePath = $"{FileName}";
        _onImport = onImport;
        Import();
    }
    
    public void Export(T objectToExport)
    {
        try
        {
            var json = JsonConvert.SerializeObject(objectToExport);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            OperationResult.Failure<JsonAction<T>>(ex.Message);
        }
    }

    private void Import()
    {
        try
        {
            if (!File.Exists(_filePath)) return;

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json)) return;

            var imported = JsonConvert.DeserializeObject<T>(json);

            if (imported != null)
            {
                _onImport(imported);
            }
        }
        catch (JsonException ex)
        {
            OperationResult.Failure<JsonAction<T>>(ex.Message);
            File.Delete(_filePath);
        }
        catch (Exception ex)
        {
            OperationResult.Failure<JsonAction<T>>(ex.Message);
        }
    }
}