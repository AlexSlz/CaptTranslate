using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaptTranslate;

public class MyLabel : Control
{
    private string _fullText = "";

    public override string Text
    {
        get => _fullText;
        set => SetText(value);
    }

    private readonly StringFormat _stringFormat;
    
    public MyLabel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        
        _stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.LineLimit
        };
    }

    private void SetText(string text)
    {
        _fullText = text;
        UpdateFontSize();
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateFontSize();
    }

    private void UpdateFontSize()
    {
        if (string.IsNullOrWhiteSpace(_fullText)) return;

        using var g = this.CreateGraphics();
        float bestSize = 12;
        for (float s = 12; s < 52; s += 2)
        {
            using var testFont = new Font("Segoe UI", s, FontStyle.Bold);
            var size = g.MeasureString(_fullText, testFont, this.Width - 12, _stringFormat);
            if (size.Height >= this.Height - 12) break;
            bestSize = s;
        }
        this.Font = new Font("Segoe UI", bestSize, FontStyle.Bold);
        GC.Collect();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_fullText)) return;

        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        
        RectangleF drawRect = this.ClientRectangle; //RectangleF drawRect = new RectangleF(0, 0, Width, Height);

        e.Graphics.DrawString(_fullText, Font, Brushes.White, drawRect, _stringFormat);
    }
}