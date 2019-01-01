using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ToolManager
{
    using Autofac;
    using System.Globalization;
    using ToolManager.Infrustructure;
    using ToolManager.Module;
    using ToolManager.Utility.Alert;
    using WeifenLuo.WinFormsUI.Docking;

    public partial class MainForm : Form
    {
        private DummySolutionExplorer solutionExplorer = new DummySolutionExplorer();
        private DummyOutputWindow outputWindow = new DummyOutputWindow();

        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            // 设置窗口样式
            SetTheme(this.vS2015BlueTheme1, VisualStudioToolStripExtender.VsVersion.Vs2015);
            this.IsMdiContainer = true;

            this.InitBaseObj();
        }

        private void InitBaseObj()
        {
            outputWindow.Show(this.dockPanel1, DockState.DockBottom);
            solutionExplorer.Show(this.dockPanel1, DockState.DockLeft);

            // 注册单例
            Singleton.Builder.RegisterInstance<IWindowContainer>(this);
            // 注册单例
            Singleton.Builder.RegisterInstance<IOutput>(outputWindow);

            // 初始化
            ModuleManager.Init(outputWindow, this);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// 寻找找不到的dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName nameInfo = new AssemblyName(args.Name);
            var fileName = nameInfo.Name + ".dll";

            // 从当前位置查找
            Assembly result = null;
            if ((result = LoadAssembly(AppDomain.CurrentDomain.BaseDirectory, fileName)) != null)
            {
                return result;
            }
            // 从语言包加载
            result = LoadAssemblyByCulture(AppDomain.CurrentDomain.BaseDirectory, fileName, nameInfo.CultureInfo);
            if (result != null)
            {
                return result;
            }

            // 从父程序集加载
            if (args.RequestingAssembly != null)
            {
                if ((result = LoadAssembly(args.RequestingAssembly.CodeBase, fileName)) != null)
                {
                    return result;
                }
                // 从语言包加载
                result = LoadAssemblyByCulture(args.RequestingAssembly.CodeBase, fileName, nameInfo.CultureInfo);
                if (result != null)
                {
                    return result;
                }
            }

            // 从模块列表加载
            var moduleList = ModuleManager.GetAllAssemblyInfo();
            foreach (var moduleItem in moduleList)
            {
                var searchPath = Path.GetDirectoryName(moduleItem.ModuleInfo.ModulePath);
                if ((result = LoadAssembly(searchPath, fileName)) != null)
                {
                    return result;
                }
                // 从语言包加载
                result = LoadAssemblyByCulture(searchPath, fileName, nameInfo.CultureInfo);
                if (result != null)
                {
                    return result;
                }
            }

            // 未找到需要加载的文件(则加载对应的语言包文件)
            System.Diagnostics.Debug.WriteLine($"未找到文件:{args.Name}");
            throw new FileNotFoundException($"File Not Found :{args.Name}");
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Assembly LoadAssembly(String dirPath, String fileName)
        {
            var searchPath = Path.Combine(dirPath, fileName);
            if (File.Exists(searchPath))
            {
                return Assembly.LoadFrom(searchPath);
            }

            return null;
        }

        private Assembly LoadAssemblyByCulture(String dirPath, String fileName, CultureInfo cultureInfo)
        {
            while (true)
            {
                if (cultureInfo == null || String.IsNullOrWhiteSpace(cultureInfo.Name))
                {
                    return null;
                }

                var searchPath = Path.Combine(dirPath, cultureInfo.Name, fileName);
                if (File.Exists(searchPath))
                {
                    return Assembly.LoadFrom(searchPath);
                }

                cultureInfo = cultureInfo.Parent;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="theme">主题名</param>
        /// <param name="themeVersion">主题版本</param>
        private void SetTheme(ThemeBase theme, VisualStudioToolStripExtender.VsVersion themeVersion)
        {
            this.dockPanel1.Theme = theme;
            visualStudioToolStripExtender1.SetStyle(menuStrip1, themeVersion, theme);
            visualStudioToolStripExtender1.SetStyle(toolStrip1, themeVersion, theme);
            visualStudioToolStripExtender1.SetStyle(statusStrip1, themeVersion, theme);
        }

        /// <summary>
        /// 新建文本文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewFileWindow_ButtonClick(object sender, EventArgs e)
        {
            DummyDoc txtWindow = new DummyDoc();
            int count = 1;
            string text = $"Document{count}";
            while (this.FindForm(text) != null)
            {
                count++;
                text = $"Document{count}";
            }

            txtWindow.Text = text;
            txtWindow.Show(this.dockPanel1);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTextFile_ButtonClick(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter = "rtf files (*.rtf)|*.rtf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullName = openFile.FileName;
                string fileName = Path.GetFileName(fullName);

                if (this.FindForm(fileName) != null)
                {
                    MessageBox.Show("The document: " + fileName + " has already opened!");
                    return;
                }

                DummyDoc dummyDoc = new DummyDoc();
                dummyDoc.Text = fileName;
                try
                {
                    dummyDoc.FileName = fullName;
                }
                catch (Exception exception)
                {
                    dummyDoc.Close();
                    MessageBox.Show(exception.Message);
                }

                // 显示
                dummyDoc.Show(this.dockPanel1);
            }
        }

        /// <summary>
        /// 导入模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuImportModule_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = Application.ExecutablePath;
            openFile.Filter = "dll files(*.dll)|*.dll";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = false;

            var logObj = Singleton.Container.Resolve<IOutput>();
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    var name = Path.GetFileNameWithoutExtension(openFile.FileName);
                    var targetPath = Path.GetDirectoryName(openFile.FileName).ToLower().TrimEnd(new char[] { '/', '\\' });
                    var currentPath = AppDomain.CurrentDomain.BaseDirectory.ToLower().TrimEnd(new char[] { '/', '\\' });
                    if (targetPath.Contains(currentPath) == false)
                    {
                        MsgBox.Show("只能导入当前目录的文件");
                        return;
                    }

                    var formList = ModuleManager.Register(name, openFile.FileName, "", logObj, this);
                    if (formList.Count > 0)
                    {
                        this.solutionExplorer.AddNodes(formList);
                    }
                }
            }
            catch (Exception e1)
            {
                MsgBox.ShowErrorMessage("导入失败，错误信息:" + e1.Message);
            }
        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        private void CloseAllDocuments()
        {
            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in this.dockPanel1.DocumentsToArray())
                {
                    document.DockHandler.DockPanel = null;
                    document.DockHandler.Close();
                }
            }
        }

        /// <summary>
        /// 模块管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemManageModule_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleSettingWindow frm = new ModuleSettingWindow();
                var result = frm.ShowDialog();
                if (result != DialogResult.Yes)
                {
                    return;
                }

                var frmList = new List<FormInfo>();
                foreach (var moduleItem in frm.DeleteList)
                {
                    frmList.AddRange(moduleItem.FormInfoList);
                }

                this.solutionExplorer.RemoveNodes(frmList);
            }
            catch (Exception e1)
            {
                MsgBox.Show($"模块管理出错:{e1.Message}");
            }
        }
    }
}
