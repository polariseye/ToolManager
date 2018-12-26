using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    [Serializable]
    public class FormInfo
    {
        /// <summary>
        /// 所对应的模块名
        /// </summary>
        public String ModuleName { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        public FormAttribute AttributeInfo { get; set; }

        /// <summary>
        /// 类型字符串(用于类型反射)
        /// </summary>
        public String TypeString { get; set; }
    }
}
