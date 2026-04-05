namespace CaptTranslate;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
        OllamaButton = new System.Windows.Forms.Button();
        labelModel = new System.Windows.Forms.Label();
        treeViewModel = new System.Windows.Forms.TreeView();
        checkBoxTranslate = new System.Windows.Forms.CheckBox();
        checkBoxRemember = new System.Windows.Forms.CheckBox();
        checkBoxKey = new System.Windows.Forms.CheckBox();
        contextMenuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { closeToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
        // 
        // closeToolStripMenuItem
        // 
        closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
        closeToolStripMenuItem.Text = "Close";
        closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
        // 
        // notifyIcon1
        // 
        notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        notifyIcon1.Icon = ((System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon"));
        notifyIcon1.Text = "Alex";
        notifyIcon1.Visible = true;
        notifyIcon1.MouseClick += notifyIcon1_MouseClick;
        // 
        // OllamaButton
        // 
        OllamaButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        OllamaButton.Location = new System.Drawing.Point(369, 13);
        OllamaButton.Margin = new System.Windows.Forms.Padding(4);
        OllamaButton.Name = "OllamaButton";
        OllamaButton.Size = new System.Drawing.Size(152, 33);
        OllamaButton.TabIndex = 4;
        OllamaButton.Text = "GetOllamaModels";
        OllamaButton.UseVisualStyleBackColor = true;
        OllamaButton.Click += OllamaButton_Click;
        // 
        // labelModel
        // 
        labelModel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        labelModel.Location = new System.Drawing.Point(12, 365);
        labelModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        labelModel.Name = "labelModel";
        labelModel.Size = new System.Drawing.Size(350, 33);
        labelModel.TabIndex = 5;
        labelModel.Text = "Model";
        labelModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // treeViewModel
        // 
        treeViewModel.Location = new System.Drawing.Point(12, 12);
        treeViewModel.Name = "treeViewModel";
        treeViewModel.Size = new System.Drawing.Size(350, 350);
        treeViewModel.TabIndex = 7;
        treeViewModel.NodeMouseDoubleClick += treeViewModel_NodeMouseDoubleClick;
        // 
        // checkBoxTranslate
        // 
        checkBoxTranslate.Location = new System.Drawing.Point(369, 53);
        checkBoxTranslate.Name = "checkBoxTranslate";
        checkBoxTranslate.Size = new System.Drawing.Size(152, 24);
        checkBoxTranslate.TabIndex = 8;
        checkBoxTranslate.Text = "Translate";
        checkBoxTranslate.UseVisualStyleBackColor = true;
        // 
        // checkBoxRemember
        // 
        checkBoxRemember.Location = new System.Drawing.Point(369, 83);
        checkBoxRemember.Name = "checkBoxRemember";
        checkBoxRemember.Size = new System.Drawing.Size(152, 24);
        checkBoxRemember.TabIndex = 9;
        checkBoxRemember.Text = "RememberCapt";
        checkBoxRemember.UseVisualStyleBackColor = true;
        // 
        // checkBoxKey
        // 
        checkBoxKey.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        checkBoxKey.Location = new System.Drawing.Point(12, 401);
        checkBoxKey.Name = "checkBoxKey";
        checkBoxKey.Size = new System.Drawing.Size(350, 35);
        checkBoxKey.TabIndex = 10;
        checkBoxKey.Text = "Key";
        checkBoxKey.UseVisualStyleBackColor = true;
        checkBoxKey.CheckedChanged += checkBoxKey_CheckedChanged;
        // 
        // Form1
        // 
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        ClientSize = new System.Drawing.Size(534, 511);
        Controls.Add(checkBoxKey);
        Controls.Add(checkBoxRemember);
        Controls.Add(checkBoxTranslate);
        Controls.Add(treeViewModel);
        Controls.Add(labelModel);
        Controls.Add(OllamaButton);
        Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        Margin = new System.Windows.Forms.Padding(4);
        MaximizeBox = false;
        Text = "Alex";
        FormClosed += Form1_FormClosed;
        Move += Form1_Move;
        contextMenuStrip1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.CheckBox checkBoxKey;

    private System.Windows.Forms.CheckBox checkBoxRemember;

    private System.Windows.Forms.CheckBox checkBoxTranslate;

    private System.Windows.Forms.TreeView treeViewModel;

    private System.Windows.Forms.Label labelModel;

    private System.Windows.Forms.Button OllamaButton;

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.NotifyIcon notifyIcon1;

    #endregion
}
