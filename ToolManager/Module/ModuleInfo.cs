using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Module
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// 数据库连接ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public ModuleTypeEnum ModuleType { get; set; }

        /// <summary>
        /// 模块存储位置
        /// </summary>
        public String ModulePath { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public String GroupTitle { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public String Description { get; set; }
    }

    /// <summary>
    /// 模块类型枚举
    /// </summary>
    public enum ModuleTypeEnum
    {
        /// <summary>
        /// 内部组件
        /// </summary>
        DllModule = 0,

        /// <summary>
        /// 可执行组件s
        /// </summary>
        ExeModule = 1
    }
}
