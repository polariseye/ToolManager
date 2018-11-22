using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 窗口的自定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class FormAttribute : Attribute
    {
        /// <summary>
        /// 组名
        /// </summary>
        public String GroupTitle { get; set; }

        /// <summary>
        /// 展示的标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 资源的Icon文件
        /// </summary>
        public String IconFile { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupTitle">组名</param>
        /// <param name="title">组标题</param>
        /// <param name="iconFile">图标名</param>
        public FormAttribute(String groupTitle, String title)
        {
            this.GroupTitle = groupTitle;
            this.Title = title;
        }
    }
}
