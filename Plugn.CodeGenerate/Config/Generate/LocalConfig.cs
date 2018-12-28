using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config
{
    /// <summary>
    /// 本地配置
    /// </summary>
    public class LocalConfig
    {
        /// <summary>
        /// 配置数据文件
        /// </summary>
        public static string SettingDataFileName
        {
            get
            {
                var result = Path.Combine(Assembly.GetExecutingAssembly().Location, @"setting.db");
                return result;
            }
        }
    }
}
