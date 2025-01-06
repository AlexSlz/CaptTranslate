using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            English
        };
    }
}
