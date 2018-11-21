using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager
{
    using ToolManager.Infrustructure;

    public partial class DummyOutputWindow : IOutput
    {
        /// <summary>
        /// 输出行
        /// </summary>
        public void PrintLine(String content)
        {
            this.AppendText(content + Environment.NewLine);
        }

        /// <summary>
        /// 输出
        /// </summary>
        public void Print(String content)
        {
            this.AppendText(content);
        }

        /// <summary>
        /// 格式化输出行
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public void PrintFormatLine(String format, params Object[] arg)
        {
            this.AppendText(String.Format(format, arg) + Environment.NewLine);
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        public void PrintFormat(String format, params Object[] arg)
        {
            this.AppendText(String.Format(format, arg));
        }
    }
}
