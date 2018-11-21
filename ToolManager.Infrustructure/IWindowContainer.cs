using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolManager.Infrustructure
{
    using WeifenLuo.WinFormsUI.Docking;

    /// <summary>
    /// 窗口容器
    /// </summary>
    public interface IWindowContainer
    {
        /// <summary>
        /// 获取主窗口的菜单项
        /// </summary>
        MenuStrip GetMainMenu();

        /// <summary>
        /// 获取状态栏对象
        /// </summary>
        /// <returns></returns>
        StatusStrip GetStatusStrip();

        /// <summary>
        /// 获取工具栏对象
        /// </summary>
        /// <returns></returns>
        ToolStrip GetToolStrip();

        /// <summary>
        /// 获取解决方案树的根节点
        /// </summary>
        /// <returns></returns>
        TreeView GetSolutionTree();

        /// <summary>
        /// 获取主显示对象
        /// </summary>
        /// <returns></returns>
        WeifenLuo.WinFormsUI.Docking.DockPanel GetMainView();

        /// <summary>
        /// 按照名字查找窗口
        /// </summary>
        /// <param name="name">窗口名</param>
        /// <returns></returns>
        BaseForm FindForm(String name);

        /// <summary>
        /// 按照类型查找窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAll<T>() where T : Form, IDockContent;
    }
}
