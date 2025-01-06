namespace CaptTranslate
{
    public partial class Form1 : Form
    {
        private HotKeyManager _hotKeyManager;
        public static Rectangle SelectedArea;
        public static Point TextPoint;

        public Form1()
        {
            InitializeComponent();
            TextRecognizer.InitializeModel();
            _hotKeyManager = new HotKeyManager(Handle);
            _hotKeyManager.AddHotKey(HotKeyManager.MOD_CTRL | HotKeyManager.MOD_SHIFT, (int)Keys.S, OpenScreenForm);


            comboBox1.Items.AddRange(Enum.GetNames(typeof(ListData.Translator)));
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(Settings.Translator.ToString());

            comboBox2.Items.AddRange(Enum.GetNames(typeof(ListData.Language)));
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(Settings.Language.ToString());

            numericUpDown1.Value = Settings.FontSize;

            checkBox1.Checked = Settings.Translate;
            checkBox2.Checked = Settings.ScaleImage;
        }

        private void OpenScreenForm()
        {
            FormManager.OpenForm<ScreenForm>();
            FormManager.AfterClose += FormManager.OpenForm<TextForm>;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                _hotKeyManager.ProcessHotKey((int)m.WParam);
            }

            base.WndProc(ref m);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Translator = (ListData.Translator)comboBox1.SelectedIndex;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.FontSize = (int)numericUpDown1.Value;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Language = (ListData.Language)comboBox2.SelectedIndex;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Translate = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ScaleImage = checkBox2.Checked;
        }
    }
}
