using System;

namespace ToolManager
{
    using System.Windows.Forms;
    using ToolManager.Infrustructure;

    public partial class DummySolutionExplorer : BaseForm
    {
        public DummySolutionExplorer()
        {
            InitializeComponent();
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            treeView1.RightToLeftLayout = RightToLeftLayout;
        }

        /// <summary>
        /// 获取树对象
        /// </summary>
        /// <returns></returns>
        public TreeView GetTreeObj()
        {
            return this.treeView1;
        }
    }
}