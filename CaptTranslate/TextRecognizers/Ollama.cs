using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptTranslate.TextRecognizers;

public class Ollama : IRecognizer, IDisposable
{
    public string Name => "Ollama";
    private readonly HttpClient _httpClient = new() {Timeout = TimeSpan.FromSeconds(30)};
    private string _currentModel = "glm-ocr:latest";
    public static string[] Models = [];
    
    public string[] GetAvailableModels()
    {
        return Models;
    }
    
    public async Task<string[]> GetModelList()
    {
        try 
        {
            using var response = await _httpClient.GetAsync("http://localhost:11434/api/tags");

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (json.TryGetProperty("models", out var modelsProp) && modelsProp.ValueKind == JsonValueKind.Array)
            {
                return modelsProp.EnumerateArray()
                    .Select(m => m.GetProperty("name").GetString())
                    .Where(name => name != null)
                    .ToArray();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, this.ToString());
        }

        return [];
    }
    
    public void SelectModel(string modelName)
    {
        _currentModel = modelName;
    }
    
    public async Task<OperationResult> ARecognize(byte[] imageData, CancellationToken ct = default)
    {
        var requestBody = new
        {
            model = _currentModel,
            prompt = Settings.Singleton.Prompt,
            images = new[] { Convert.ToBase64String(imageData) }, 
            stream = false,
            think = Settings.Singleton.Think,
            keep_alive = 0,
            options = new {
                temperature = 0
            }
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(requestBody), 
            Encoding.UTF8, 
            "application/json"
        );

        try
        {
            using var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", content, ct);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return OperationResult.Failure<Ollama>(error);
            }

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (json.TryGetProperty("response", out var respProp))
            {
                return OperationResult.Success(respProp.GetString());
            }

            return OperationResult.Failure<Ollama>("Not Found");
        }
        catch (OperationCanceledException)
        {
            return OperationResult.Success("");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure<Ollama>(ex.Message);
        }
    }
    
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
