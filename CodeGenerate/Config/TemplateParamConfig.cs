using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.Config
{
    /// <summary>
    /// 模板参数配置
    /// </summary>
    public class TemplateParamConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public String Language { get; set; }

        /// <summary>
        /// 模板组名
        /// </summary>
        public String TemplateGroupName { get; set; }

        /// <summary>
        /// 参数信息
        /// </summary>
        public List<ParamItem> ParamData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TemplateParamConfig()
        {
            this.ParamData = new List<ParamItem>();
        }
    }

    /// <summary>
    /// 参数配置项
    /// </summary>
    public class ParamItem
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public String ParamName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public String ParamValue { get; set; }
    }
}
