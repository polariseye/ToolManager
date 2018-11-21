using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    using WeifenLuo.WinFormsUI.Docking;

    /// <summary>
    /// 窗口基类
    /// </summary>
    public class BaseForm : DockContent
    {
        public BaseForm()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        /// <summary>
        /// 用于左边展示的树结构
        /// </summary>
        public TreeView SolutionTree { get; set; }

        /// <summary>
        /// 展示的时候处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
    }
}
