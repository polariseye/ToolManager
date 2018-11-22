using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Module
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        public ModuleInfo ModuleInfo { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly AssemblyObj { get; set; }

        /// <summary>
        /// 窗口列表
        /// </summary>
        public List<FormInfo> FormInfoList { get; set; }
    }
}
