namespace CodeGenerate
{
    partial class SetTemplateParamForm
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
            this.lbLanguage = new System.Windows.Forms.Label();
            this.lbTemplateGroup = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            paramGrid = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(paramGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "语言：";
            // 
            // lbLanguage
            // 
            this.lbLanguage.AutoSize = true;
            this.lbLanguage.Location = new System.Drawing.Point(47, 0);
            this.lbLanguage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lbLanguage.Name = "lbLanguage";
            this.lbLanguage.Size = new System.Drawing.Size(41, 12);
            this.lbLanguage.TabIndex = 2;
            this.lbLanguage.Text = "csharp";
            // 
            // lbTemplateGroup
            // 
            this.lbTemplateGroup.AutoSize = true;
            this.lbTemplateGroup.Location = new System.Drawing.Point(153, 0);
            this.lbTemplateGroup.Name = "lbTemplateGroup";
            this.lbTemplateGroup.Size = new System.Drawing.Size(41, 12);
            this.lbTemplateGroup.TabIndex = 4;
            this.lbTemplateGroup.Text = "csharp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "模板组：";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lbLanguage);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.lbTemplateGroup);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 18);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(343, 27);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(280, 309);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colParamName
            // 
            this.colParamName.DataPropertyName = "ParamName";
            this.colParamName.HeaderText = "参数名";
            this.colParamName.Name = "colParamName";
            // 
            // colParamValue
            // 
            this.colParamValue.DataPropertyName = "ParamValue";
            this.colParamValue.HeaderText = "参数值";
            this.colParamValue.MinimumWidth = 200;
            this.colParamValue.Name = "colParamValue";
            this.colParamValue.Width = 200;
            // 
            // paramGrid
            // 
            paramGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            paramGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            paramGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamName,
            this.colParamValue});
            paramGrid.Location = new System.Drawing.Point(12, 51);
            paramGrid.Name = "paramGrid";
            paramGrid.RowTemplate.Height = 23;
            paramGrid.Size = new System.Drawing.Size(343, 252);
            paramGrid.TabIndex = 0;
            paramGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.paramGrid_CellEndEdit);
            paramGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.paramGrid_CellValueChanged);
            // 
            // SetTemplateParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 335);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(paramGrid);
            this.Name = "SetTemplateParamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模板参数设置";
            this.Load += new System.EventHandler(this.SetTemplateParamForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(paramGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView paramGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbLanguage;
        private System.Windows.Forms.Label lbTemplateGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamValue;
    }
}