using System;
using System.Linq;

namespace ToolManager
{
    using Autofac;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ToolManager.Infrustructure;
    using ToolManager.Module;

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

        /// <summary>
        /// 展示的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DummySolutionExplorer_Load(object sender, EventArgs e)
        {
            // 把所有功能添加到节点上
            var formList = ModuleManager.GetAllFormInfos();
            var groupedData = formList.GroupBy(tmp => tmp.AttributeInfo.GroupTitle);
            foreach (var groupObj in groupedData)
            {
                TreeNode groupNode = this.treeView1.Nodes.Add(groupObj.Key);

                // 添加子节点
                var groupItemList = groupObj.OrderBy(tmp => tmp.AttributeInfo.Title).ToList();
                foreach (var item in groupItemList)
                {
                    var nodeItem = new TreeNode(item.AttributeInfo.Title);
                    nodeItem.Tag = item;
                    groupNode.Nodes.Add(nodeItem);
                }
            }
        }

        /// <summary>
        /// 添加一个组
        /// </summary>
        /// <param name="formList"></param>
        public void AddNodes(List<FormInfo> formList)
        {
            var groupedData = formList.GroupBy(tmp => tmp.AttributeInfo.GroupTitle);
            foreach (var groupObj in groupedData)
            {
                var tmpGroupNodes = this.treeView1.Nodes.Find(groupObj.Key, false);
                TreeNode groupNode = null;
                if (tmpGroupNodes == null || tmpGroupNodes.Length <= 0)
                {
                    // 直接添加新组
                    groupNode = this.treeView1.Nodes.Add(groupObj.Key);

                    // 添加子节点
                    var groupItemList = groupObj.OrderBy(tmp => tmp.AttributeInfo.Title).ToList();
                    foreach (var item in groupItemList)
                    {
                        var nodeItem = new TreeNode(item.AttributeInfo.Title);
                        nodeItem.Tag = item;
                        groupNode.Nodes.Add(nodeItem);
                    }

                    continue;
                }
                else
                {
                    // 往已有的组中添加项
                    groupNode = tmpGroupNodes[0];

                    // 添加子节点
                    var groupItemList = groupObj.OrderBy(tmp => tmp.AttributeInfo.Title).ToList();
                    foreach (var item in groupItemList)
                    {
                        // 查找应该放置的位置
                        var preItemIndex = 0;
                        for (Int32 i = 0; i < groupNode.Nodes.Count; i++)
                        {
                            if (String.Compare(groupNode.Nodes[i].Text, item.AttributeInfo.Title) > 0)
                            {
                                preItemIndex = i;
                            }
                        }

                        // 添加此项
                        var nodeItem = new TreeNode(item.AttributeInfo.Title);
                        nodeItem.Tag = item;
                        groupNode.Nodes.Insert(preItemIndex, nodeItem);
                    }
                }
            }
        }

        /// <summary>
        /// 移除窗口信息
        /// </summary>
        /// <param name="formList">窗口项</param>
        public void RemoveNodes(List<FormInfo> formList)
        {
            foreach (var formItem in formList)
            {
                var tmpGroupNodes = this.treeView1.Nodes.Find(formItem.AttributeInfo.Title, false);
                if (tmpGroupNodes == null || tmpGroupNodes.Length <= 0)
                {
                    continue;
                }

                // 移除对应的节点
                foreach (var node in tmpGroupNodes)
                {
                    if (node.Tag == formItem)
                    {
                        node.Parent.Nodes.Remove(node);
                        if (node.Parent.Nodes.Count <= 0)
                        {
                            node.Parent.Parent.Nodes.Remove(node.Parent);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 双击处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;
            TreeNode tn = tv.GetNodeAt(e.X, e.Y);

            if (tn == null)
            {
                return;
            }

            FormInfo formInfoObj = tn.Tag as FormInfo;
            if (formInfoObj == null)
            {
                return;
            }

            var formObj = (BaseForm)Activator.CreateInstance(formInfoObj.FormType);
            var container = Singleton.Container.Resolve<IWindowContainer>();
            formObj.Show(container.GetMainView());
        }
    }
}