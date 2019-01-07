namespace ToolManager
{
    partial class ImportWindow
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
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModulePath = new System.Windows.Forms.TextBox();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbModuleType = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModuleDescription = new System.Windows.Forms.TextBox();
            this.txtModuleGroup = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "模块名:";
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(72, 39);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(265, 21);
            this.txtModuleName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "模块路径:";
            // 
            // txtModulePath
            // 
            this.txtModulePath.Location = new System.Drawing.Point(72, 99);
            this.txtModulePath.Name = "txtModulePath";
            this.txtModulePath.Size = new System.Drawing.Size(227, 21);
            this.txtModulePath.TabIndex = 3;
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Location = new System.Drawing.Point(297, 98);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(40, 23);
            this.btnOpenDir.TabIndex = 4;
            this.btnOpenDir.Text = "浏览";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(262, 218);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "模块类型:";
            // 
            // cmbModuleType
            // 
            this.cmbModuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModuleType.FormattingEnabled = true;
            this.cmbModuleType.Location = new System.Drawing.Point(72, 9);
            this.cmbModuleType.Name = "cmbModuleType";
            this.cmbModuleType.Size = new System.Drawing.Size(265, 20);
            this.cmbModuleType.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "模块描述:";
            // 
            // txtModuleDescription
            // 
            this.txtModuleDescription.Location = new System.Drawing.Point(72, 132);
            this.txtModuleDescription.Multiline = true;
            this.txtModuleDescription.Name = "txtModuleDescription";
            this.txtModuleDescription.Size = new System.Drawing.Size(265, 78);
            this.txtModuleDescription.TabIndex = 9;
            // 
            // txtModuleGroup
            // 
            this.txtModuleGroup.Location = new System.Drawing.Point(72, 69);
            this.txtModuleGroup.Name = "txtModuleGroup";
            this.txtModuleGroup.Size = new System.Drawing.Size(265, 21);
            this.txtModuleGroup.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "模块组名:";
            // 
            // ImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 244);
            this.Controls.Add(this.txtModuleGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtModuleDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbModuleType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.txtModulePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtModuleName);
            this.Controls.Add(this.label1);
            this.Name = "ImportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导入配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModulePath;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbModuleType;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModuleDescription;
        private System.Windows.Forms.TextBox txtModuleGroup;
        private System.Windows.Forms.Label label5;
    }
}