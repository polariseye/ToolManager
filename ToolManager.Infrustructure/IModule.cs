using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 模块
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 模块初始化，只会调用一次
        /// </summary>
        void Init();
    }
}
