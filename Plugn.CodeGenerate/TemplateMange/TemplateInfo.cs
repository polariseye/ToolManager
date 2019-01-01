using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.TemplateMange
{
    /// <summary>
    /// 模板信息
    /// </summary>
    public class TemplateInfo
    {
        /// <summary>
        /// 使用的宿主名
        /// </summary>
        public String HostName { get; set; }

        /// <summary>
        /// 语言名
        /// </summary>
        public String Language { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public String GroupName { get; set; }

        /// <summary>
        /// 模板名
        /// </summary>
        public String TemplateName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">宿主名</param>
        /// <param name="language">语言名</param>
        /// <param name="groupName">组名</param>
        /// <param name="templateName">模板名</param>
        public TemplateInfo(String hostName, string language, String groupName, String templateName, String filePath)
        {
            this.HostName = hostName;
            this.Language = language;
            this.GroupName = groupName;
            this.TemplateName = templateName;
            this.FilePath = filePath;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.TemplateName;
        }
    }
}
