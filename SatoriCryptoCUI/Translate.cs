using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SatoriCryptoCUI
{
    public class Translate
    {
        private JObject? parsedTranslateJson = null;

        public Translate(string translateFileDirectory, string lang)
        {
            if (!Directory.Exists(translateFileDirectory)) return;
            string translateFile = Path.Combine(translateFileDirectory, lang + ".json");
            if (!File.Exists(translateFile)) return;

            try
            {
                using (StreamReader reader = File.OpenText(translateFile))
                {
                    parsedTranslateJson = JObject.Parse(reader.ReadToEnd());
                    reader.Dispose();
                }
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("[X] Cannot translate because translate file '{0}' contains invalid JSON.", lang + ".json");
            }
            catch (Exception)
            {
                Console.WriteLine("[X] Cannot translate because throwed exception when reading translate file '{0}'.", lang + ".json");
            }
        }

        public string t(string text)
        {
            if (parsedTranslateJson == null) return text;
            if (parsedTranslateJson[text] != null) return parsedTranslateJson[text].ToString();
            return text;
        }

        public string t(string baseText, params object[] args)
        {
            string translatedText = baseText;
            if (parsedTranslateJson == null) return string.Format(translatedText, args);
            if (parsedTranslateJson[baseText] != null) translatedText = parsedTranslateJson[baseText].ToString();

            return string.Format(translatedText, args);
        }
    }
}
