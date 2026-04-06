using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CaptTranslate.TextRecognizers;
using CaptTranslate.Translators;

namespace CaptTranslate;

public partial class Form1 : Form
{
    private readonly HotKeyManager _hotKeyManager;
    
    private readonly JsonAction<Settings> _saveSettings;
    private readonly JsonAction<string[]> _ollamaModels;
    
    public Form1()
    {
        InitializeComponent();
        _saveSettings = new JsonAction<Settings>("settings.json", a => Settings.Singleton = a);
        _ollamaModels = new JsonAction<string[]>("ollamaModels.json", a => Ollama.Models = a);
        
        _hotKeyManager = new HotKeyManager(Handle);
        _hotKeyManager.AddHotKey(Settings.Singleton.ModKey, Settings.Singleton.Key, OpenSelectForm);
        UpdateView();
        UpdateStatusLabel();
        
        checkBoxTranslate.BindSetting(Settings.Singleton.Translate, (b) => Settings.Singleton.Translate = b);
        checkBoxRemember.BindSetting(Settings.Singleton.RememberCapt, (b) => Settings.Singleton.RememberCapt = b);
        checkBoxKey.Text = "Hot Key: " + GetHotkeyString() + (Keys)Settings.Singleton.Key;
    }

    private bool _firstCapt = true;
    
    private void OpenSelectForm()
    {
        if (Settings.Singleton.SelectedEngine == null || checkBoxKey.Checked) return;

        if (_firstCapt || !checkBoxRemember.Checked)
        {
            FormManager.OpenForm<SelectForm>();
            FormManager.AfterClose = () =>
            {
                if (!ImageData.IsSuccess) return;
            
                _firstCapt = !Settings.Singleton.RememberCapt;
                CaptureAndShow();
            };
        }
        else
        {
            CaptureAndShow();
        }
    }
    private void CaptureAndShow()
    {
        var filePath = ScreenManager.CaptureScreen();
        if (filePath != null) 
        {
            FormManager.OpenForm<TextForm>();
        }
    }
    
    protected override void WndProc(ref Message m)
    {
        const int wmHotkey = 0x0312;

        if (m.Msg == wmHotkey)
        {
            _hotKeyManager.ProcessHotKey((int)m.WParam);
        }

        base.WndProc(ref m);
    }

    private void FormClose()
    {
        notifyIcon1.Visible = false;
        _hotKeyManager.Dispose();
        _saveSettings.Export(Settings.Singleton);
    }
    
    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        FormClose();
    }

    private void Form1_Move(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Minimized)
        {
            this.Hide();
            notifyIcon1.Visible = true;
        }
    }

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;
        if (this.Visible)
        {
            Hide();
        }
        else
        {
            Show();
            this.WindowState = FormWindowState.Normal; 
            this.BringToFront();                       
            this.Activate();
        }
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void UpdateView()
    {
        treeViewModel.BeginUpdate();
        treeViewModel.Nodes.Clear();
        var mainNode = new TreeNode("Models");
        foreach (var engine in StaticData.Engines)
        {
            var engineNode = new TreeNode(engine.Name);
            mainNode.Nodes.Add(engineNode);
            
            var models = engine.GetAvailableModels();

            if (models == null) continue;
            foreach (var model in models)
            {
                var modelNode = new TreeNode(model) {Tag = engine };
                engineNode.Nodes.Add(modelNode);
                
                if (model == Settings.Singleton.SelectedModel)
                {
                    HighlightNode(modelNode);
                }
            }
        }
        treeViewModel.Nodes.Add(mainNode);
        
        mainNode = new TreeNode("Translators");
        foreach (var translator in StaticData.Translators)
        {
            var translatorNode = new TreeNode(translator.Name) {Tag = translator};
            mainNode.Nodes.Add(translatorNode);
            if (translator.Name == Settings.Singleton.SelectedTranslator)
            {
                HighlightNode(translatorNode);
            }
        }
        treeViewModel.Nodes.Add(mainNode);
        
        treeViewModel.ExpandAll();
        treeViewModel.EndUpdate();
    }
    
    private void HighlightNode(TreeNode node, Color backColor = default)
    {
        if (backColor == default) backColor = Color.LightGoldenrodYellow;
        node.BackColor = backColor;
        node.ForeColor = Color.Black;
    }
    
    private async void OllamaButton_Click(object sender, EventArgs e)
    {
        var ollama = StaticData.GetEngine<Ollama>();
        Cursor.Current = Cursors.WaitCursor;
        this.Cursor = Cursors.WaitCursor; 
        try 
        {
            var models = await ollama.GetModelList();
            Ollama.Models = models;
            _ollamaModels.Export(Ollama.Models);
            UpdateView();
        }
        finally 
        {
            this.Cursor = Cursors.Default;
        }
    }
    
    private void treeViewModel_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if(e.Node == null) return;
        
        switch (e.Node.Tag)
        {
            case null:
                return;
            case IRecognizer engine:
            {
                var modelName = e.Node.Text;
                engine.SelectModel(modelName);
                
                Settings.Singleton.SelectedEngine = engine.Name;
                Settings.Singleton.SelectedModel = modelName;
                break;
            }
            case ITranslator translator:
                Settings.Singleton.SelectedTranslator = translator.Name;
                    break;
            }
        UpdateView();
        UpdateStatusLabel();
    }
    
    private void UpdateStatusLabel()
    {
        var model = Settings.Singleton.SelectedModel ?? "None";
        var translator = Settings.Singleton.SelectedTranslator ?? "None";
        labelModel.Text = $@"{model} + {translator}";
    }
    
    private void checkBoxKey_CheckedChanged(object sender, EventArgs e)
    {
        KeyPreview = checkBoxKey.Checked;
        if (checkBoxKey.Checked)
        {
            KeyDown += Form_KeyDown;
        }
        else
        {
            KeyDown -= Form_KeyDown;
            _hotKeyManager.UpdateHotKey(Settings.Singleton.ModKey, Settings.Singleton.Key, OpenSelectForm);
        }
    }
    
    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            checkBoxKey.Checked = false;
            return;
        }
        
        Settings.Singleton.ModKey = 0;
        if (e.Control)
        {
            Settings.Singleton.ModKey += 2;
        }
        if (e.Shift)
        {
            Settings.Singleton.ModKey += 4;
        }
        if (e.Alt)
        {
            Settings.Singleton.ModKey += 1;
        }
        Settings.Singleton.Key = e.KeyValue;
        checkBoxKey.Text = GetHotkeyString() + e.KeyCode;
    }
    
    private string GetHotkeyString()
    {
        var mod = Settings.Singleton.ModKey;
        
        var res = "";
        if ((mod & 2) != 0) res += "Ctrl + ";
        if ((mod & 4) != 0) res += "Shift + ";
        if ((mod & 1) != 0) res += "Alt + ";

        return res;
    }
}
