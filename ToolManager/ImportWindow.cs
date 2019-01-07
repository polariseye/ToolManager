using System;
using System.IO;
using System.Windows.Forms;

namespace ToolManager
{
    using Autofac;
    using System.Collections.Generic;
    using ToolManager.Infrustructure;
    using ToolManager.Module;
    using ToolManager.Utility.Alert;

    /// <summary>
    /// 导入处理
    /// </summary>
    public partial class ImportWindow : Form
    {
        /// <summary>
        /// 列表对象
        /// </summary>
        private DummySolutionExplorer solutionExplorer;

        public ImportWindow(DummySolutionExplorer solutionExplorer)
        {
            this.solutionExplorer = solutionExplorer;
            InitializeComponent();
        }

        /// <summary>
        /// 浏览窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.Filter = "dll files(*.dll)|*.dll;exe files(*.exe)|*.exe";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var name = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            var targetPath = Path.GetDirectoryName(openFileDialog1.FileName).ToLower().TrimEnd(new char[] { '/', '\\' });
            var currentPath = AppDomain.CurrentDomain.BaseDirectory.ToLower().TrimEnd(new char[] { '/', '\\' });
            if (targetPath.Contains(currentPath) == false)
            {
                MsgBox.Show("只能导入当前目录的文件");
                return;
            }

            txtModulePath.Text = openFileDialog1.FileName;
        }

        /// <summary>
        /// 窗口列表
        /// </summary>
        public List<FormInfo> FormInfoList { get; set; }

        /// <summary>
        /// 提交处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var logObj = Singleton.Container.Resolve<IOutput>();
            var container = Singleton.Container.Resolve<IWindowContainer>();


            var moduleType = ModuleTypeEnum.DllModule;
            var moduleName = txtModuleName.Text;
            var description = txtModuleDescription.Text;
            var modulePath = txtModulePath.Text;
            var groupName = txtModuleGroup.Text;

            var targetPath = Path.GetDirectoryName(modulePath).ToLower().TrimEnd(new char[] { '/', '\\' });
            var currentPath = AppDomain.CurrentDomain.BaseDirectory.ToLower().TrimEnd(new char[] { '/', '\\' });
            if (targetPath.Contains(currentPath) == false)
            {
                MsgBox.Show("只能导入当前目录的文件");
                return;
            }

            // 注册可执行程序
            this.FormInfoList = ModuleManager.RegisterExe(moduleName, modulePath, description, moduleType, groupName, logObj, container);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
