using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ToolManager
{
    using ToolManager.Infrustructure;
    using ToolManager.Module;

    public partial class ExeWindow : BaseForm
    {
        /// <summary>
        /// 进程对象
        /// </summary>
        private Process process;

        /// <summary>
        /// 窗口对象
        /// </summary>
        private FormInfo formObj = null;

        /// <summary>
        /// 目标路径
        /// </summary>
        private String targetPath = String.Empty;

        public ExeWindow(FormInfo formInfo, String targetPath)
        {
            this.formObj = formInfo;
            this.Text = this.formObj.AttributeInfo.Title;

            InitializeComponent();
        }

        private void ShowProcess()
        {
            this.process = new Process();

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = targetPath;
            this.process.StartInfo = startInfo;

            // 窗口关联
            SetParent(this.process.MainWindowHandle, this.Handle);

            this.process.Start();
            this.process.Exited += Process_Exited;
        }

        /// <summary>
        /// 窗口退出处理s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_Exited(object sender, EventArgs e)
        {
            this.process.Exited -= Process_Exited;
            MessageBox.Show("子窗口已退出");
        }

        /// <summary>
        /// 关闭时，退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExeWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.process.Exited -= Process_Exited;
            try
            {
                this.process.Kill();
            }
            catch (Exception e1)
            {
                //todo:打印信息
            }
        }

        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
