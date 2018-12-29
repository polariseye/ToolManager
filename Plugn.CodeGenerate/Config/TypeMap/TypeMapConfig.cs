using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.TypeMap
{
    /// <summary>
    /// 替换项
    /// </summary>
    public class TypeMapConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 目标语言
        /// </summary>
        public String Language { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public Int32 Length { get; set; }

        /// <summary>
        /// 新的值
        /// </summary>
        public String TargetTypeString { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TypeMapConfig()
        {
            this.DbType = String.Empty;
            this.TargetTypeString = String.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="targetTypeString"></param>
        public TypeMapConfig(String dbType, String targetTypeString)
        {
            this.DbType = dbType;
            this.TargetTypeString = targetTypeString;
        }

        /// <summary>
        /// 获取一个拷贝
        /// </summary>
        /// <returns></returns>
        public TypeMapConfig Clone()
        {
            return this.MemberwiseClone() as TypeMapConfig;
        }
    }
}
