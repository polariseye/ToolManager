using System;
using System.Collections.Generic;
using System.Text;

namespace Kalman
{
    /// <summary>
    /// ��־����ί��
    /// </summary>
    /// <param name="msg">��־��Ϣ</param>
    public delegate void LogHandler(string msg);

    /// <summary>
    /// ��Ҫ��¼��־�������̳иýӿ�
    /// </summary>
    public interface ILogable
    {
        /// <summary>
        /// ��־�¼�
        /// </summary>
        event LogHandler OnLog;
    }
}
