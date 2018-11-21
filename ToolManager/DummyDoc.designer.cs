namespace ToolManager
{
    partial class DummyDoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DummyDoc));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mainMenu.SuspendLayout();
            this.contextMenuTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuEdit});
            this.mainMenu.Location = new System.Drawing.Point(0, 4);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(448, 25);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Visible = false;
            // 
            // mMenuEdit
            // 
            this.mMenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuSave,
            this.mMenuSaveAs,
            this.mMenuClose,
            this.mMenuCloseAll});
            this.mMenuEdit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mMenuEdit.MergeIndex = 1;
            this.mMenuEdit.Name = "mMenuEdit";
            this.mMenuEdit.Size = new System.Drawing.Size(44, 21);
            this.mMenuEdit.Text = "编辑";
            // 
            // mMenuSave
            // 
            this.mMenuSave.Name = "mMenuSave";
            this.mMenuSave.Size = new System.Drawing.Size(148, 22);
            this.mMenuSave.Text = "保存";
            this.mMenuSave.Click += new System.EventHandler(this.mMenuSave_Click);
            // 
            // mMenuSaveAs
            // 
            this.mMenuSaveAs.Name = "mMenuSaveAs";
            this.mMenuSaveAs.Size = new System.Drawing.Size(148, 22);
            this.mMenuSaveAs.Text = "另存为";
            this.mMenuSaveAs.Click += new System.EventHandler(this.mMenuSaveAs_Click);
            // 
            // mMenuClose
            // 
            this.mMenuClose.Name = "mMenuClose";
            this.mMenuClose.Size = new System.Drawing.Size(148, 22);
            this.mMenuClose.Text = "关闭";
            this.mMenuClose.Click += new System.EventHandler(this.mMenuClose_Click);
            // 
            // mMenuCloseAll
            // 
            this.mMenuCloseAll.Name = "mMenuCloseAll";
            this.mMenuCloseAll.Size = new System.Drawing.Size(148, 22);
            this.mMenuCloseAll.Text = "关闭所有文档";
            this.mMenuCloseAll.Click += new System.EventHandler(this.mMenuCloseAll_Click);
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSave,
            this.menuSaveAs,
            this.menuClose,
            this.menuCloseAll});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(181, 114);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(180, 22);
            this.menuSave.Text = "保存";
            this.menuSave.Click += new System.EventHandler(this.mMenuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(180, 22);
            this.menuSaveAs.Text = "另存为";
            this.menuSaveAs.Click += new System.EventHandler(this.mMenuSaveAs_Click);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(180, 22);
            this.menuClose.Text = "关闭";
            this.menuClose.Click += new System.EventHandler(this.mMenuClose_Click);
            // 
            // menuCloseAll
            // 
            this.menuCloseAll.Name = "menuCloseAll";
            this.menuCloseAll.Size = new System.Drawing.Size(180, 22);
            this.menuCloseAll.Text = "关闭所有文档";
            this.menuCloseAll.Click += new System.EventHandler(this.mMenuCloseAll_Click);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipTitle = "asdasd";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(448, 389);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // DummyDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(448, 393);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "DummyDoc";
            this.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.TabPageContextMenuStrip = this.contextMenuTabPage;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.contextMenuTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem mMenuSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem menuCloseAll;
        private System.Windows.Forms.ToolStripMenuItem mMenuClose;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem mMenuCloseAll;
        private System.Windows.Forms.ToolStripMenuItem mMenuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
    }
}