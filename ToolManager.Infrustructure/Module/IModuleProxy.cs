using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 可以跨越应用程序域的模块处理接口
    /// </summary>
    public interface IModuleProxy
    {
        /// <summary>
        /// 获取所有窗口
        /// </summary>
        /// <param name="assemblyFilePath">文件名</param>
        /// <param name="logObj">日志记录对象</param>
        /// <param name="windowContainer">窗口容器</param>
        /// <returns></returns>
        List<FormInfo> LoadModule(String assemblyFilePath, IOutput logObj, IWindowContainer windowContainer);

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="typeString">类型字符串</param>
        void Show(String assemblyFilePath, String typeString);
    }
}
