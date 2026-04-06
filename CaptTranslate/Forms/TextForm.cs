using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CaptTranslate;

public partial class TextForm : Form
{
    private readonly MyLabel _myLabel;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private bool _translated = false;
    private string _recognizedText;
    private string _translatedText;
    
    
    public TextForm()
    {
        InitializeComponent();
        this.KeyPreview = true;
        this.KeyDown += ScreenForm_KeyDown;
        this.StartPosition = FormStartPosition.Manual;
        var borderSize = SystemInformation.FrameBorderSize.Width;
        var captionHeight = SystemInformation.CaptionHeight;
        
        this.Location = new Point(
            ImageData.TextPoint.X - borderSize, 
            ImageData.TextPoint.Y - captionHeight - borderSize
        );
        this.ClientSize = ImageData.SelectedArea.Size;
        _myLabel = new MyLabel { Dock = DockStyle.Fill };
        this.Controls.Add(_myLabel);

        this.FormClosing += OnClose;
    }
    
    private void OnClose(object sender, FormClosingEventArgs e)
    {
        this.KeyDown -= ScreenForm_KeyDown;
        _myLabel.MouseClick -= SwitchText;
        this.FormClosing -= OnClose;
    
        _cts.Cancel();
        _cts.Dispose();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Recognize();
    }

    private async void Recognize()
    {
        this.Cursor = Cursors.WaitCursor; 
        try
        {
            var engine = StaticData.GetRecognizer();
            var data = ScreenManager.ImageToByte(ScreenManager.FileName);
            
            var result = await engine.ARecognize(data, _cts.Token);
            if (result.IsError || this.IsDisposed || _myLabel.IsDisposed) return;
            _recognizedText = result.Text;
        }
        finally 
        {
            this.Cursor = Cursors.Default;
            _myLabel.Text = _recognizedText;
        }
        
        if (string.IsNullOrEmpty(_myLabel.Text) || _myLabel.Text.Contains("```markdown")) 
        {
            this.Close();
            return;
        }
        
        
        if (Settings.Singleton.Translate)
        {
            Translate();
            _translated = true;
        }
        _myLabel.MouseClick += SwitchText;
    }

    private void SwitchText(object sender = null, MouseEventArgs e = null)
    {
        _translated = !_translated;
        if(string.IsNullOrEmpty(_translatedText))
        {
            Translate();
            return;
        }
        _myLabel.Text = _translated ? _translatedText : _recognizedText;
    }
    
    private async void Translate()
    {
        this.Cursor = Cursors.WaitCursor;
        try
        {
            var translator = StaticData.GetTranslator();
            var result = await translator.Translate(_myLabel.Text);
            
            if (result.IsError || this.IsDisposed || _myLabel.IsDisposed) return;
            _translatedText = result.Text;
            _myLabel.Text = _translatedText;
        }
        finally 
        { 
            this.Cursor = Cursors.Default;
        }
    }

    private void ScreenForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Close();
        }
        if(e.Control && e.KeyCode == Keys.C)
        {
            Clipboard.SetText(_myLabel.Text);
            this.Close();
        }
    }

    private void TextForm_Deactivate(object sender, EventArgs e)
    {
        //this.Close();
    }
}

