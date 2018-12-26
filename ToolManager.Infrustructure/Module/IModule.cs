using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 模块初始化
        /// </summary>
        /// <param name="logObj"></param>
        /// <param name="windowContainer"></param>
        void Init(IOutput logObj, IWindowContainer windowContainer);
    }
}
