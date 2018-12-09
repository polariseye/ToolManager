namespace ToolManager
{
    partial class ModuleSettingWindow
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
            this.moduleGrid = new System.Windows.Forms.DataGridView();
            this.colModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModulePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFunctionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.moduleGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // moduleGrid
            // 
            this.moduleGrid.AllowUserToAddRows = false;
            this.moduleGrid.AllowUserToDeleteRows = false;
            this.moduleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moduleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moduleGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colModuleName,
            this.colModulePath,
            this.colFunctionCount,
            this.colDelete});
            this.moduleGrid.Location = new System.Drawing.Point(12, 5);
            this.moduleGrid.Name = "moduleGrid";
            this.moduleGrid.ReadOnly = true;
            this.moduleGrid.RowTemplate.Height = 23;
            this.moduleGrid.Size = new System.Drawing.Size(645, 411);
            this.moduleGrid.TabIndex = 0;
            this.moduleGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.moduleGrid_CellContentClick);
            // 
            // colModuleName
            // 
            this.colModuleName.DataPropertyName = "ModuleName";
            this.colModuleName.HeaderText = "模块名";
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.ReadOnly = true;
            // 
            // colModulePath
            // 
            this.colModulePath.DataPropertyName = "ModulePath";
            this.colModulePath.HeaderText = "模块路径";
            this.colModulePath.Name = "colModulePath";
            this.colModulePath.ReadOnly = true;
            this.colModulePath.Width = 300;
            // 
            // colFunctionCount
            // 
            this.colFunctionCount.DataPropertyName = "FunctionCount";
            this.colFunctionCount.HeaderText = "功能数量";
            this.colFunctionCount.Name = "colFunctionCount";
            this.colFunctionCount.ReadOnly = true;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "删除";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDelete.Text = "删除";
            this.colDelete.UseColumnTextForButtonValue = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(582, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ModuleSettingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.moduleGrid);
            this.Name = "ModuleSettingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模块管理";
            this.Load += new System.EventHandler(this.ModuleSettingWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.moduleGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView moduleGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModulePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFunctionCount;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
    }
}