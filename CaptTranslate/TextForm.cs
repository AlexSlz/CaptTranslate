
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

            Label label1 = new Label
            {
                Text = text,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", Settings.FontSize),
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };
            this.Controls.Add(label1);

            if (Settings.Translate)
                label1.Text = TranslateManager.Translate(text, ListData.GetTranslator(Settings.Translator));
        }


        private void ScreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void TextForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
