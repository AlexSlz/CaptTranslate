﻿
using static System.Net.Mime.MediaTypeNames;
using Point = System.Drawing.Point;

namespace CaptTranslate
{
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
            this.KeyDown += ScreenForm_KeyDown;
            this.Location = Form1.TextPoint;
            this.Size = new System.Drawing.Size(Form1.SelectedArea.Width, Form1.SelectedArea.Height);
            string text = TextRecognizer.Recognize(ScreenManager.ImageToByte(ScreenManager.FileName));
            text = TextRecognizer.ClearText(text);
            float fontSize = Settings.FontSize;

            label1.Text = text;
            label1.AutoSize = false;
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Arial", fontSize);
            //label1.ForeColor = Color.Black;
            //label1.BackColor = Color.Transparent;
            
            if (Settings.Translate && text != string.Empty)
                label1.Text = TranslateManager.Translate(text, ListData.GetTranslator(Settings.Translator));
        }

        private void ScreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if(e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(label1.Text);
                this.Close();
            }
        }

        private void TextForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
