using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.DbConnConfig
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    public class DbConn
    {
        /// <summary>
        /// 数据库连接ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 数据库连接名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 默认的数据库
        /// </summary>
        public String DefaultDb { get; set; }

        /// <summary>
        /// 驱动名
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 是否是当前使用的
        /// </summary>
        public bool IsActive { get; set; }
    }
}
