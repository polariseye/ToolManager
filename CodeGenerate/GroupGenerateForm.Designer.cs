namespace CodeGenerate
{
    partial class GroupGenerateForm
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbConnectionList = new System.Windows.Forms.ComboBox();
            this.btnSetConnection = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveOne = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(549, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(86, 43);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "连接地址:";
            // 
            // cmbConnectionList
            // 
            this.cmbConnectionList.FormattingEnabled = true;
            this.cmbConnectionList.Location = new System.Drawing.Point(84, 19);
            this.cmbConnectionList.Name = "cmbConnectionList";
            this.cmbConnectionList.Size = new System.Drawing.Size(353, 20);
            this.cmbConnectionList.TabIndex = 2;
            this.cmbConnectionList.SelectedIndexChanged += new System.EventHandler(this.cmbConnectionList_SelectedIndexChanged);
            // 
            // btnSetConnection
            // 
            this.btnSetConnection.Location = new System.Drawing.Point(454, 8);
            this.btnSetConnection.Name = "btnSetConnection";
            this.btnSetConnection.Size = new System.Drawing.Size(86, 43);
            this.btnSetConnection.TabIndex = 3;
            this.btnSetConnection.Text = "设置";
            this.btnSetConnection.UseVisualStyleBackColor = true;
            this.btnSetConnection.Click += new System.EventHandler(this.btnSetConnection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "模版组:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(518, 104);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(151, 20);
            this.comboBox2.TabIndex = 7;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(518, 78);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(151, 20);
            this.comboBox3.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(453, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "语言:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(453, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "输出目录:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(518, 166);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 21);
            this.textBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(453, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "命名规则:";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(518, 134);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(151, 20);
            this.comboBox4.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveOne);
            this.groupBox1.Controls.Add(this.btnRemoveAll);
            this.groupBox1.Controls.Add(this.btnSelectOne);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 289);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "要生成的表";
            // 
            // btnRemoveOne
            // 
            this.btnRemoveOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveOne.Location = new System.Drawing.Point(198, 176);
            this.btnRemoveOne.Name = "btnRemoveOne";
            this.btnRemoveOne.Size = new System.Drawing.Size(28, 25);
            this.btnRemoveOne.TabIndex = 26;
            this.btnRemoveOne.Text = "<";
            this.btnRemoveOne.UseVisualStyleBackColor = true;
            this.btnRemoveOne.Click += new System.EventHandler(this.btnRemoveOne_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAll.Location = new System.Drawing.Point(198, 226);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(28, 25);
            this.btnRemoveAll.TabIndex = 25;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectOne.Location = new System.Drawing.Point(198, 126);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(28, 25);
            this.btnSelectOne.TabIndex = 24;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(232, 64);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox2.Size = new System.Drawing.Size(180, 196);
            this.listBox2.TabIndex = 23;
            this.listBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDoubleClick);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectAll.Location = new System.Drawing.Point(198, 76);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(28, 25);
            this.btnSelectAll.TabIndex = 22;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(180, 196);
            this.listBox1.TabIndex = 21;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(46, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(366, 21);
            this.textBox2.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "筛选:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(455, 194);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(214, 146);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // GroupGenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSetConnection);
            this.Controls.Add(this.cmbConnectionList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Name = "GroupGenerateForm";
            this.Text = "按照模版组生成代码";
            this.Load += new System.EventHandler(this.GroupGenerateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbConnectionList;
        private System.Windows.Forms.Button btnSetConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemoveOne;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnGenerate;
    }
}