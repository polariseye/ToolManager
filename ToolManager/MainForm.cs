﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolManager
{
    using Autofac;
    using System.IO;
    using ToolManager.Infrustructure;
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
    }
}