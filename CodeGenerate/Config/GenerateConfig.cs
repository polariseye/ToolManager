using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.Config
{
    /// <summary>
    /// 代码生成配置s
    /// </summary>
    public class GenerateConfig
    {
        /// <summary>
        /// 数据库连接ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 保存位置
        /// </summary>
        public String SavePath { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenerateConfig()
        {
            this.SavePath = String.Empty;
        }
    }
}
