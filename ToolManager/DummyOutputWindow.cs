
using System;
using System.Windows.Forms;

namespace ToolManager
{
    using ToolManager.Infrustructure;

    public partial class DummyOutputWindow : BaseForm
    {
        delegate void AppendTextCallback(string text);

        public DummyOutputWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ׷���ı������
        /// </summary>
        /// <param name="txt"></param>
        private void AppendText(String txt)
        {
            if (this.textBox1.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.Invoke(d, new object[] { txt });
            }
            else
            {
                if (string.IsNullOrEmpty(txt)) return;
                this.textBox1.AppendText(txt);
                if (this.Visible==false)
                {
                    this.Visible = true;
                }
            }
        }
        /// <summary>
        /// �����������ı�
        /// </summary>
        public void ClearText()
        {
            this.textBox1.Clear();
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            this.textBox1.Copy();
        }

        private void menuItemClear_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void menuItemSelectAll_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
        }
    }
}