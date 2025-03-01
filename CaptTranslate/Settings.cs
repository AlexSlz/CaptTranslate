using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptTranslate
{
    internal static class Settings
    {
        public static int FontSize = 14;
        public static bool Translate = true;
        public static bool RememberCapt = true;

        public static ListData.Translator Translator = ListData.Translator.Google;
        public static ListData.Language Language = ListData.Language.English;

        public static int ModKEY = 0; //6;
        public static int Key = 0x2C; // 83;
    }
}
