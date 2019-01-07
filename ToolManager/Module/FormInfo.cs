using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Module
{
    using System.Reflection;
    using ToolManager.Infrustructure;

    /// <summary>
    /// 窗口信息
    /// </summary>
    public class FormInfo
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        public Assembly AssemblyInfo { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        public FormAttribute AttributeInfo { get; set; }

        /// <summary>
        /// 窗口的类型
        /// </summary>
        public Type FormType { get; set; }

        /// <summary>
        /// 模块信息
        /// </summary>
        public ModuleInfo ModuleInfo { get; set; }
    }
}
