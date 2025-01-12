using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CaptTranslate
{
    internal class TranslateManager
    {
        public static async Task<string> TranslateGoogle(string text)
        {
            var to = "ru";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={to}&dt=t&q={HttpUtility.UrlEncode(text)}";

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return string.Join(string.Empty, JArray.Parse(response)[0].Select(x => x[0]));
        }
        public static async Task<string> TranslateMyMemory(string text)
        {
            string url = $"http://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair={ListData.GetLanguage(Settings.Language)}|ru";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(url);
                res.EnsureSuccessStatusCode();

                string responseJson = await res.Content.ReadAsStringAsync();
                var translationResult = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);

                if (translationResult.ResponseStatus == 200)
                {
                    return translationResult.TranslatedText;
                }

                return string.Empty;
            }
        }
        public static string Translate(string input, Func<string, Task<string>> translateMethod)
        {
            return Task.Run(async () => await translateMethod(input)).Result;
        }
    }

    public class TranslationResponse
    {
        [JsonProperty("responseStatus")]
        public int ResponseStatus { get; set; }

        [JsonProperty("responseData")]
        public TranslationData ResponseData { get; set; }

        public string TranslatedText => ResponseData?.TranslatedText;
    }

    public class TranslationData
    {
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
    }
}
