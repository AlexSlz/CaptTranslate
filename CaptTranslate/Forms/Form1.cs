using System.Diagnostics;

namespace CaptTranslate
{
    public partial class Form1 : Form
    {
        private HotKeyManager _hotKeyManager;

        public Form1()
        {
            InitializeComponent();
            _hotKeyManager = new HotKeyManager(Handle);
            _hotKeyManager.AddHotKey(Settings.ModKEY, Settings.Key, OpenScreenForm);


            comboBox1.Items.AddRange(Enum.GetNames(typeof(ListData.Translator)));
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(Settings.Translator.ToString());

            comboBox2.Items.AddRange(Enum.GetNames(typeof(ListData.Language)));
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(Settings.Language.ToString());

            numericUpDown1.Value = Settings.FontSize;

            checkBox1.Checked = Settings.Translate;
            checkBox4.Checked = Settings.RememberCapt;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCombination = "";
            Settings.ModKEY = 0;
            if (e.Control)
            {
                keyCombination += "Ctrl + ";
                Settings.ModKEY += 2;
            }
            if (e.Shift)
            {
                keyCombination += "Shift + ";
                Settings.ModKEY += 4;
            }
            if (e.Alt)
            {
                keyCombination += "Alt + ";
                Settings.ModKEY += 1;
            }
            keyCombination += e.KeyCode.ToString();
            Settings.Key = e.KeyValue;

            label4.Text = keyCombination;
        }
        bool CanCapt = true;
        private void OpenScreenForm()
        {
            if (checkBox3.Checked)
                return;

            if (CanCapt)
            {
                FormManager.OpenForm<SelectForm>();
                FormManager.AfterClose += () =>
                {
                    if (!ImageData.CanCaptScreen)
                        return;
                    FormManager.OpenForm<TextForm>();
                    if(Settings.RememberCapt)
                        CanCapt = false;
                };
            }
            else
            {
                FormManager.OpenForm<TextForm>();
            }
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
            TextRecognizer.SelectModel(Settings.Language);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Translate = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _hotKeyManager.UpdateHotKey(Settings.ModKEY, Settings.Key, OpenScreenForm);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.KeyPreview = checkBox3.Checked;
            if (checkBox3.Checked)
            {
                this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
            }
            else
            {
                this.KeyDown -= new KeyEventHandler(MainForm_KeyDown);
                _hotKeyManager.UpdateHotKey(Settings.ModKEY, Settings.Key, OpenScreenForm);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Settings.RememberCapt = checkBox4.Checked;
            CanCapt = true;
        }
    }
}
