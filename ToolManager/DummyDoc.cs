using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace ToolManager
{
    using Autofac;
    using ToolManager.Infrustructure;

    public partial class DummyDoc : BaseForm
    {
        public DummyDoc()
        {
            InitializeComponent();
        }

        private string m_fileName = string.Empty;
        public string FileName
        {
            get { return m_fileName; }
            set
            {
                if (value != string.Empty)
                {
                    Stream s = new FileStream(value, FileMode.Open);

                    FileInfo efInfo = new FileInfo(value);

                    string fext = efInfo.Extension.ToUpper();

                    if (fext.Equals(".RTF"))
                        richTextBox1.LoadFile(s, RichTextBoxStreamType.RichText);
                    else
                        richTextBox1.LoadFile(s, RichTextBoxStreamType.PlainText);
                    s.Close();
                }

                m_fileName = value;
                this.ToolTipText = value;
            }
        }

        // workaround of RichTextbox control's bug:
        // If load file before the control showed, all the text format will be lost
        // re-load the file after it get showed.
        private bool m_resetText = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (m_resetText)
            {
                m_resetText = false;
                FileName = FileName;
            }
        }

        protected override string GetPersistString()
        {
            // Add extra information into the persist string for this document
            // so that it is available when deserialized.
            return GetType().ToString() + "," + FileName + "," + Text;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (FileName == string.Empty)
                this.richTextBox1.Text = Text;
        }

        private void mMenuSave_Click(object sender, EventArgs e)
        {
            var logObj = Singleton.Container.Resolve<IOutput>();
            if (String.IsNullOrWhiteSpace(this.FileName) == false)
            {
                this.richTextBox1.SaveFile(this.FileName);
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "rtf files (*.rtf)|*.rtf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                this.richTextBox1.SaveFile(dlg.FileName);
                m_fileName = dlg.FileName;
            }
            catch (Exception ex)
            {
                logObj.PrintLine($"文件保存失败：" + ex.Message);
                MessageBox.Show("文件保存失败:" + ex.Message);
            }
        }

        private void mMenuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mMenuCloseAll_Click(object sender, EventArgs e)
        {
            var container = Singleton.Container.Resolve<IWindowContainer>();
            var allForm = container.GetAll<DummyDoc>();
            foreach (var item in allForm)
            {
                if (item != this)
                {
                    item.Close();
                }
            }

            this.Close();
        }

        private void mMenuSaveAs_Click(object sender, EventArgs e)
        {
            var logObj = Singleton.Container.Resolve<IOutput>();

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "rtf files (*.rtf)|*.rtf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                this.richTextBox1.SaveFile(dlg.FileName);
                m_fileName = dlg.FileName;
            }
            catch (Exception ex)
            {
                logObj.PrintLine($"文件保存失败：" + ex.Message);
                MessageBox.Show("文件保存失败:" + ex.Message);
            }
        }
    }
}