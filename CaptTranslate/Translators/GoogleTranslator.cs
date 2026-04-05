using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using System.Web;

namespace CaptTranslate.Translators;

public class GoogleTranslator : ITranslator
{
    public string Name => "Google";
    
    private static readonly HttpClient HttpClient = new();

    public async Task<OperationResult> Translate(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return OperationResult.Failure<GoogleTranslator>("No text.");

        try
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=" +
                         $"{Settings.Singleton.TargetLanguage}&dt=t&q={HttpUtility.UrlEncode(text)}";
            var response = await HttpClient.GetStringAsync(url);
            var json = JArray.Parse(response);
            var result = string.Join(string.Empty, json[0].Select(x => x[0]));
            return OperationResult.Success(result);
        }
        catch (Exception ex)
        {
            return OperationResult.Failure<GoogleTranslator>(ex.Message);
        }
    }
}