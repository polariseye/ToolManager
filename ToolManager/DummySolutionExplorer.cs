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
                var tmpGroupNodes = this.treeView1.Nodes.Find(groupObj.Key, false);
                TreeNode groupNode = null;
                if (tmpGroupNodes == null || tmpGroupNodes.Length <= 0)
                {
                    // ֱ���������
                    groupNode = this.treeView1.Nodes.Add(groupObj.Key);

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
                    groupNode = tmpGroupNodes[0];

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
                var tmpGroupNodes = this.treeView1.Nodes.Find(formItem.AttributeInfo.Title, false);
                if (tmpGroupNodes == null || tmpGroupNodes.Length <= 0)
                {
                    continue;
                }

                // �Ƴ���Ӧ�Ľڵ�
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
        /// ˫������
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