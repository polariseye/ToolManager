using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.NameRule
{
    public class NameRuleConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 规则名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        public String Seperator { get; set; }

        /// <summary>
        /// 首字母处理方式
        /// </summary>
        public FirstCharHandleType FirstCharHandleType { get; set; }

        /// <summary>
        /// 前缀处理列表
        /// </summary>
        public List<ReplaceItem> PrefixList { get; set; }

        /// <summary>
        /// 后缀处理列表
        /// </summary>
        public List<ReplaceItem> StuffixList { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NameRuleConfig()
        {
            this.PrefixList = new List<ReplaceItem>();
            this.StuffixList = new List<ReplaceItem>();
        }
    }
}
