using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;

namespace CaptTranslate
{
    internal class ListData
    {
        public enum Translator
        {
            Google,
            MyMemory
        };

        public static Func<string, Task<string>> GetTranslator(Translator translator)
        {
            switch (translator)
            {
                default:
                case Translator.Google:
                    return TranslateManager.TranslateGoogle;
                case Translator.MyMemory:
                    return TranslateManager.TranslateMyMemory;
            }
        }

        public enum Language
        {
            English,
            Chinese,
            Korean
        };
        public static string GetLanguage(Language language)
        {
            switch (language)
            {
                default:
                case Language.English:
                    return "en";
                case Language.Chinese:
                    return "cn";
                case Language.Korean:
                    return "kr";
            }
        }
    }
}
