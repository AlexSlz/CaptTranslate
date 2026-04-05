using System;
using System.Windows.Forms;

namespace CaptTranslate;

[Serializable]
internal class Settings
{
    public static Settings Singleton = new();
    
    public string SelectedEngine = "Ollama";
    public string SelectedModel = "glm-ocr:latest";
    
    public bool Translate = true;
    public string TargetLanguage = "ru";
    public string SelectedTranslator = "Google";
    public string OllamaTranslationModel = "glm-ocr:latest";
    
    public int Key = 32; // 67 // 83 0x2C
    public int ModKey = 2; //6
    
    public string Prompt = "OCR Mode: Extract all text from the image. " +
                           "Output ONLY the recognized text. " +
                           "No preamble, no commentary, no 'Here is the text'. " +
                           "NO LaTeX, NO Markdown, NO HTML tag" +
                           "Just the raw text from the image.";

    public bool RememberCapt = false;
    public bool Think = false;

    public Settings()
    {
        Singleton = this;
    }
}

public static class SettingsBinder
{
    public static void BindSetting(this CheckBox control, bool init, Action<bool> onChange)
    {
        control.Checked = init;
        
        control.CheckedChanged += (s, e) =>
        {
            onChange?.Invoke(control.Checked);
        };
    }
}