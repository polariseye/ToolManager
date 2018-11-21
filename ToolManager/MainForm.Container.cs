using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager
{
    using System.Windows.Forms;
    using ToolManager.Infrustructure;
    using WeifenLuo.WinFormsUI.Docking;

    /// <summary>
    /// 容器的处理
    /// </summary>
    public partial class MainForm : IWindowContainer
    {
        /// <summary>
        /// 获取主窗口的菜单项
        /// </summary>
        public MenuStrip GetMainMenu()
        {
            return this.menuStrip1;
        }

        /// <summary>
        /// 获取状态栏对象
        /// </summary>
        /// <returns></returns>
        public StatusStrip GetStatusStrip()
        {
            return this.statusStrip1;
        }

        /// <summary>
        /// 获取工具栏对象
        /// </summary>
        /// <returns></returns>
        public ToolStrip GetToolStrip()
        {
            return this.toolStrip1;
        }

        /// <summary>
        /// 获取解决方案树的根节点
        /// </summary>
        /// <returns></returns>
        public TreeView GetSolutionTree()
        {
            return this.solutionExplorer.GetTreeObj();
        }

        /// <summary>
        /// 获取主显示对象
        /// </summary>
        /// <returns></returns>
        public WeifenLuo.WinFormsUI.Docking.DockPanel GetMainView()
        {
            return this.dockPanel1;
        }


        /// <summary>
        /// 按照名字查找窗口
        /// </summary>
        /// <param name="title">窗口名</param>
        /// <returns></returns>
        public BaseForm FindForm(String title)
        {
            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form.Text == title)
                    {
                        return form as BaseForm;
                    }
                }

                return null;
            }
            else
            {
                foreach (IDockContent content in this.dockPanel1.Documents)
                {
                    if (content.DockHandler.TabText == title)
                    {
                        return content as BaseForm;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 按照类型查找窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>() where T : Form, IDockContent
        {
            var result = new List<T>();
            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form is T)
                    {
                        result.Add(form as T);
                    }
                }
            }
            else
            {
                foreach (IDockContent content in this.dockPanel1.Documents)
                {
                    if (content is T)
                    {
                        result.Add(content as T);
                    }
                }
            }

            return result;
        }
    }
}
