using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager
{
    /// <summary>
    /// 本地配置
    /// </summary>
    public class LocalConfig
    {
        /// <summary>
        /// 设置目录
        /// </summary>
        public static string SettingPath
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ToolConfig");
                var di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }

                return di.FullName;
            }
        }

        /// <summary>
        /// 配置数据文件
        /// </summary>
        public static string SettingDataFileName
        {
            get
            {
                var result = Path.Combine(SettingPath, @"setting.db");
                return result;
            }
        }
    }
}
