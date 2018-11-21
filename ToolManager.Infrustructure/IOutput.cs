using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    /// <summary>
    /// 使用输出窗口输出数据
    /// </summary>
    public interface IOutput
    {
        /// <summary>
        /// 输出行
        /// </summary>
        void PrintLine(String content);

        /// <summary>
        /// 输出
        /// </summary>
        void Print(String content);

        /// <summary>
        /// 格式化输出行
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        void PrintFormatLine(String format, params Object[] arg);

        /// <summary>
        /// 格式化输出
        /// </summary>
        void PrintFormat(String format, params Object[] arg);
    }
}
