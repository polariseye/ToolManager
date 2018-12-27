using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Plugn.CodeGenerate.Data.SchemaObject
{
    using ToolManager.Utility;

    /// <summary>
    /// 架构对象基类
    /// </summary>
    [Serializable]
    public abstract class SOBase
    {
        /// <summary>
        /// 获取所属数据库对象
        /// </summary>
        public abstract SODatabase Database { get; }

        /// <summary>
        /// 用来标识对象的名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 完整名称，比如"[dbo].[user]"
        /// </summary>
        public virtual string FullName { get { return this.Name; } }

        /// <summary>
        /// 获取数据库对象的映射名称（代码中的类名或属性名）
        /// </summary>
        /// <param name="prefixLevel">前缀层次，默认1</param>
        /// <param name="separator">分隔符，默认下划线</param>
        /// <returns></returns>
        public string GetMappingName(int prefixLevel = 1, string separator = "_")
        {
            string mappingName = StringUtil.ToPascalName(StringUtil.RemovePrefix(this.Name, prefixLevel), separator);
            return mappingName;
        }

        /// <summary>
        /// 对象的注释信息
        /// </summary>
        public virtual string Comment { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
