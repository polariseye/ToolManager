using System;
using System.Linq;

namespace ToolManager
{
    using Autofac;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ToolManager.Infrustructure;
    using ToolManager.Module;
    using ToolManager.Utility.Alert;

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
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        public TreeView GetTreeObj()
        {
            return this.treeView1;
        }

        /// <summary>
        /// չʾ�Ĵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DummySolutionExplorer_Load(object sender, EventArgs e)
        {
            // �����й�����ӵ��ڵ���
            var formList = ModuleManager.GetAllFormInfos();
            var groupedData = formList.GroupBy(tmp => tmp.AttributeInfo.GroupTitle);
            foreach (var groupObj in groupedData)
            {
                TreeNode groupNode = this.treeView1.Nodes.Add(groupObj.Key);

                // ����ӽڵ�
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
        /// ���һ����
        /// </summary>
        /// <param name="formList"></param>
        public void AddNodes(List<FormInfo> formList)
        {
            var groupedData = formList.GroupBy(tmp => tmp.AttributeInfo.GroupTitle);
            foreach (var groupObj in groupedData)
            {
                var groupNode = this.FindGroupNode(groupObj.Key);
                if (groupNode == null)
                {
                    // ֱ���������
                    groupNode = new TreeNode(groupObj.Key);
                    this.treeView1.Nodes.Add(groupNode);

                    // ����ӽڵ�
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
                    // �����е����������
                    // ����ӽڵ�
                    var groupItemList = groupObj.OrderBy(tmp => tmp.AttributeInfo.Title).ToList();
                    foreach (var item in groupItemList)
                    {
                        // ����Ӧ�÷��õ�λ��
                        var preItemIndex = 0;
                        for (Int32 i = 0; i < groupNode.Nodes.Count; i++)
                        {
                            if (String.Compare(groupNode.Nodes[i].Text, item.AttributeInfo.Title) > 0)
                            {
                                preItemIndex = i;
                            }
                        }

                        // ��Ӵ���
                        var nodeItem = new TreeNode(item.AttributeInfo.Title);
                        nodeItem.Tag = item;
                        groupNode.Nodes.Insert(preItemIndex, nodeItem);
                    }
                }
            }
        }

        /// <summary>
        /// �Ƴ�������Ϣ
        /// </summary>
        /// <param name="formList">������</param>
        public void RemoveNodes(List<FormInfo> formList)
        {
            foreach (var formItem in formList)
            {
                var tmpSameNodes = this.FindNode(formItem.AttributeInfo.Title);
                if (tmpSameNodes == null || tmpSameNodes.Count <= 0)
                {
                    continue;
                }

                // �Ƴ���Ӧ�Ľڵ�
                foreach (var node in tmpSameNodes)
                {
                    if (node.Tag == formItem)
                    {
                        var parent = node.Parent;
                        parent.Nodes.Remove(node);
                        if (parent != null && parent.Nodes.Count <= 0)
                        {
                            if (parent.Parent != null)
                            {
                                parent.Parent.Nodes.Remove(parent);
                            }
                            else
                            {
                                this.treeView1.Nodes.Remove(parent);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ˫������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                // ֻ����������
                return;
            }

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

        /// <summary>
        /// �����ı�������
        /// </summary>
        /// <param name="text">�ı�</param>
        /// <returns></returns>
        private List<TreeNode> FindNode(String text)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            foreach (TreeNode item in this.treeView1.Nodes)
            {
                queue.Enqueue(item);
            }

            var result = new List<TreeNode>();
            while (true)
            {
                if (queue.Count <= 0)
                {
                    break;
                }

                var nowItem = queue.Dequeue();
                if (nowItem.Text == text)
                {
                    result.Add(nowItem);
                }

                foreach (TreeNode item in nowItem.Nodes)
                {
                    queue.Enqueue(item);
                }
            }

            return result;
        }

        /// <summary>
        /// ������ڵ�
        /// </summary>
        /// <param name="text">�����ı�</param>
        /// <returns></returns>
        private TreeNode FindGroupNode(String text)
        {
            foreach (TreeNode item in this.treeView1.Nodes)
            {
                if (item.Text == text)
                {
                    return item;
                }
            }

            return null;
        }
    }
}