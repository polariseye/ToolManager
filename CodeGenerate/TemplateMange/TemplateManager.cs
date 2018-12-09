using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.TemplateMange
{
    public class TemplateManager
    {
        /// <summary>
        /// 模板所在目录
        /// </summary>
        private readonly String templateDir;

        /// <summary>
        /// 模板列表
        /// </summary>
        private List<TemplateInfo> templateInfos = new List<TemplateInfo>();

        /// <summary>
        /// 模板管理对象
        /// </summary>
        public TemplateManager()
        {
            templateDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "T4Template");
        }

        public void Init()
        {
            var result = new List<TemplateInfo>();
            var languateDirList = Directory.GetDirectories(this.templateDir);

            foreach (var languateDir in languateDirList)
            {
                var language = Path.GetFileNameWithoutExtension(languateDir);

                foreach (var groupPath in Directory.GetDirectories(languateDir))
                {
                    var groupName = Path.GetFileNameWithoutExtension(groupPath);

                    result.AddRange(LoadLanguageGroup(groupPath, language, groupName));
                }
            }

            this.templateInfos = result;
        }

        /// <summary>
        /// 获取语言列表
        /// </summary>
        /// <returns>语言列表</returns>
        public List<String> GetLanguageList()
        {
            return templateInfos.Select(tmp => tmp.Language).Distinct().ToList();
        }

        /// <summary>
        /// 获取组名列表
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns></returns>
        public List<String> GetGroupList(String language)
        {
            return templateInfos.Where(tmp => tmp.Language == language).Select(tmp => tmp.GroupName).Distinct().ToList();
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="language">模板语言</param>
        /// <param name="groupName">组名</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public List<TemplateInfo> GetGroupTemplate(String language, String groupName, Boolean ifTriggerException = true)
        {
            var result = this.templateInfos.Where(tmp => tmp.Language == language && tmp.GroupName == groupName).ToList();
            if (result.Count <= 0)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到language={language} groupname={groupName}的模板文件");
                }

                return result;
            }

            return result;
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="language">模板语言</param>
        /// <param name="groupName">组名</param>
        /// <param name="templateName">模板名</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public TemplateInfo GetGroupTemplate(String language, String groupName, String templateName, Boolean ifTriggerException = true)
        {
            var result = this.templateInfos.FirstOrDefault(tmp =>
                tmp.Language == language &&
                tmp.GroupName == groupName &&
                tmp.TemplateName == templateName);
            if (result == null)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到language={language} groupname={groupName} TemplateName={templateName} 的模板文件");
                }

                return result;
            }

            return result;
        }

        /// <summary>
        /// 加载模板组
        /// </summary>
        /// <param name="groupPath">组所在路径</param>
        /// <param name="language">语言</param>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        private List<TemplateInfo> LoadLanguageGroup(String groupPath, String language, String groupName)
        {
            var result = new List<TemplateInfo>();

            var fileList = Directory.GetFiles(groupPath, "*.tt");
            foreach (var item in fileList)
            {
                var templateName = Path.GetFileNameWithoutExtension(item);
                result.Add(new TemplateInfo(language, groupName, templateName, item));
            }

            return result;
        }
    }
}
