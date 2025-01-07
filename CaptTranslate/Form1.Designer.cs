namespace CaptTranslate
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            closeToolStripMenuItem = new ToolStripMenuItem();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            numericUpDown1 = new NumericUpDown();
            label3 = new Label();
            comboBox2 = new ComboBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            label4 = new Label();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { closeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(103, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(119, 6);
            comboBox1.Margin = new Padding(5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(188, 33);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 9);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 2;
            label1.Text = "Translator";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 49);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(83, 25);
            label2.TabIndex = 3;
            label2.Text = "FontSIze";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(119, 47);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(188, 33);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(317, 9);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 6;
            label3.Text = "Language";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(422, 6);
            comboBox2.Margin = new Padding(5);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(188, 33);
            comboBox2.TabIndex = 5;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(14, 86);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(106, 29);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Translate";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(12, 121);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(127, 29);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "ScaleImage";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(244, 87);
            label4.Name = "label4";
            label4.Size = new Size(63, 25);
            label4.TabIndex = 9;
            label4.Text = "label4";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(244, 121);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(156, 29);
            checkBox3.TabIndex = 11;
            checkBox3.Text = "ChangeHotKey";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Enabled = false;
            checkBox4.Location = new Point(313, 48);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(105, 29);
            checkBox4.TabIndex = 12;
            checkBox4.Text = "AutoSize";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 156);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(label4);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(numericUpDown1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "Form1";
            Text = "Form1";
            Move += Form1_Move;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private ComboBox comboBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label label4;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
    }
}
