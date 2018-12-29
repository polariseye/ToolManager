using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.NameRule
{
    /// <summary>
    /// 替换项
    /// </summary>
    public class ReplaceItem
    {
        /// <summary>
        /// 原始值
        /// </summary>
        public String OriginValue { get; set; }

        /// <summary>
        /// 新的值
        /// </summary>
        public String NewValue { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ReplaceItem()
        {
            this.OriginValue = String.Empty;
            this.NewValue = String.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="originValue"></param>
        /// <param name="newValue"></param>
        public ReplaceItem(String originValue, String newValue)
        {
            this.OriginValue = originValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// 获取一个拷贝
        /// </summary>
        /// <returns></returns>
        public ReplaceItem Clone()
        {
            return this.MemberwiseClone() as ReplaceItem;
        }
    }
}
