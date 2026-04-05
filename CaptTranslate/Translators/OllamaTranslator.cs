using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CaptTranslate.Translators;

public class OllamaTranslator : ITranslator
{
    public string Name => "Ollama Translator";
    
    private readonly HttpClient _httpClient = new();
    private const string OllamaUrl = "http://localhost:11434/api/generate";
    
    public async Task<OperationResult> Translate(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return OperationResult.Failure<OllamaTranslator>("Text is empty");

        var payload = new
        {
            model = Settings.Singleton.OllamaTranslationModel,
            prompt = $"Translate the following text to {Settings.Singleton.TargetLanguage}. " +
                     $"Output ONLY the translated text, no explanations, no quotes:\n\n{text}",
            stream = false
        };

        try
        {
            string jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(OllamaUrl, content);
            if (!response.IsSuccessStatusCode)
                return OperationResult.Failure<OllamaTranslator>($"Ollama error: {response.StatusCode}");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            
            using var doc = JsonDocument.Parse(jsonResponse);
            string translatedText = doc.RootElement.GetProperty("response").GetString();

            return OperationResult.Success(translatedText?.Trim());
        }
        catch (Exception ex)
        {
            return OperationResult.Failure<OllamaTranslator>($"Ollama connection failed: {ex.Message}");
        }
    }
}