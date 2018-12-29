namespace Plugn.CodeGenerate
{
    partial class SetNameRuleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridPrefix = new System.Windows.Forms.DataGridView();
            this.colPreFix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreReplace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridSuffix = new System.Windows.Forms.DataGridView();
            this.colSuffix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSuffixReplace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.l = new System.Windows.Forms.Label();
            this.txtSeperator = new System.Windows.Forms.TextBox();
            this.rdioFirstLower = new System.Windows.Forms.RadioButton();
            this.rdoFirstUpper = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbRuleName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrefix)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuffix)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "规则列表：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.gridPrefix);
            this.groupBox1.Location = new System.Drawing.Point(15, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 415);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "前缀替换";
            // 
            // gridPrefix
            // 
            this.gridPrefix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPrefix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPreFix,
            this.colPreReplace});
            this.gridPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPrefix.Location = new System.Drawing.Point(3, 17);
            this.gridPrefix.Name = "gridPrefix";
            this.gridPrefix.RowTemplate.Height = 23;
            this.gridPrefix.Size = new System.Drawing.Size(248, 395);
            this.gridPrefix.TabIndex = 0;
            // 
            // colPreFix
            // 
            this.colPreFix.DataPropertyName = "OriginValue";
            this.colPreFix.HeaderText = "前缀";
            this.colPreFix.Name = "colPreFix";
            // 
            // colPreReplace
            // 
            this.colPreReplace.DataPropertyName = "NewValue";
            this.colPreReplace.HeaderText = "替换为";
            this.colPreReplace.Name = "colPreReplace";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridSuffix);
            this.groupBox2.Location = new System.Drawing.Point(274, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 415);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "后缀替换";
            // 
            // gridSuffix
            // 
            this.gridSuffix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSuffix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSuffix,
            this.colSuffixReplace});
            this.gridSuffix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSuffix.Location = new System.Drawing.Point(3, 17);
            this.gridSuffix.Name = "gridSuffix";
            this.gridSuffix.RowTemplate.Height = 23;
            this.gridSuffix.Size = new System.Drawing.Size(248, 395);
            this.gridSuffix.TabIndex = 0;
            // 
            // colSuffix
            // 
            this.colSuffix.DataPropertyName = "OriginValue";
            this.colSuffix.HeaderText = "后缀";
            this.colSuffix.Name = "colSuffix";
            // 
            // colSuffixReplace
            // 
            this.colSuffixReplace.DataPropertyName = "NewValue";
            this.colSuffixReplace.HeaderText = "替换为";
            this.colSuffixReplace.Name = "colSuffixReplace";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(462, 46);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(62, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "保存";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Location = new System.Drawing.Point(15, 51);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(77, 12);
            this.l.TabIndex = 7;
            this.l.Text = "单词分隔符：";
            // 
            // txtSeperator
            // 
            this.txtSeperator.Location = new System.Drawing.Point(95, 47);
            this.txtSeperator.Name = "txtSeperator";
            this.txtSeperator.Size = new System.Drawing.Size(112, 21);
            this.txtSeperator.TabIndex = 8;
            // 
            // rdioFirstLower
            // 
            this.rdioFirstLower.AutoSize = true;
            this.rdioFirstLower.Location = new System.Drawing.Point(3, 3);
            this.rdioFirstLower.Name = "rdioFirstLower";
            this.rdioFirstLower.Size = new System.Drawing.Size(83, 16);
            this.rdioFirstLower.TabIndex = 10;
            this.rdioFirstLower.Text = "首字母小写";
            this.rdioFirstLower.UseVisualStyleBackColor = true;
            // 
            // rdoFirstUpper
            // 
            this.rdoFirstUpper.AutoSize = true;
            this.rdoFirstUpper.Checked = true;
            this.rdoFirstUpper.Location = new System.Drawing.Point(92, 3);
            this.rdoFirstUpper.Name = "rdoFirstUpper";
            this.rdoFirstUpper.Size = new System.Drawing.Size(83, 16);
            this.rdoFirstUpper.TabIndex = 11;
            this.rdoFirstUpper.TabStop = true;
            this.rdoFirstUpper.Text = "首字母大写";
            this.rdoFirstUpper.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoFirstUpper);
            this.panel1.Controls.Add(this.rdioFirstLower);
            this.panel1.Location = new System.Drawing.Point(212, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 21);
            this.panel1.TabIndex = 12;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(462, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 23);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cbRuleName
            // 
            this.cbRuleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRuleName.FormattingEnabled = true;
            this.cbRuleName.Location = new System.Drawing.Point(95, 20);
            this.cbRuleName.Name = "cbRuleName";
            this.cbRuleName.Size = new System.Drawing.Size(365, 20);
            this.cbRuleName.TabIndex = 13;
            this.cbRuleName.SelectedIndexChanged += new System.EventHandler(this.cbRuleName_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(399, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // SetNameRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 492);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbRuleName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSeperator);
            this.Controls.Add(this.l);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "SetNameRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "命名规则配置";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPrefix)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSuffix)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.TextBox txtSeperator;
        private System.Windows.Forms.RadioButton rdioFirstLower;
        private System.Windows.Forms.RadioButton rdoFirstUpper;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridSuffix;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbRuleName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView gridPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreFix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreReplace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuffix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuffixReplace;
    }
}