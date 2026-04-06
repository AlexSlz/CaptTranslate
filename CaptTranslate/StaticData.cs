using System;
using System.Linq;
using CaptTranslate.Translators;
using CaptTranslate.TextRecognizers;

namespace CaptTranslate;

public static class StaticData
{
    public static IRecognizer[] Engines { get; set; } =
    [
        new Ollama(),
        new PaddleOCR()
    ];

    public static T GetEngine<T>() where T : IRecognizer
    {
        return (T) Engines.FirstOrDefault(e => e.Name == typeof(T).Name);
    }
    
    public static IRecognizer GetRecognizer()
    {
        var engine = Engines.FirstOrDefault(e => e.Name == Settings.Singleton.SelectedEngine);
        engine.SelectModel(Settings.Singleton.SelectedModel);
        return engine;
    }
    
    public static ITranslator[] Translators { get; } = [new GoogleTranslator(), new OllamaTranslator()];

    public static ITranslator GetTranslator()
    {
        var engine = Translators.FirstOrDefault(e => e.Name == Settings.Singleton.SelectedTranslator);
        return engine;
    }
}