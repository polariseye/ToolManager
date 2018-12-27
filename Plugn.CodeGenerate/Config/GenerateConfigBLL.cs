using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config
{
    /// <summary>
    /// 代码生成的构造函数
    /// </summary>
    public class GenerateConfigBLL
    {
        private GenerateConfig data;
        private GenerateConfigDAL dalObj = new GenerateConfigDAL();

        static GenerateConfigBLL()
        {
            GenerateConfigDAL.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenerateConfigBLL()
        {
            // 刷新
            Refresh();
        }

        /// <summary>
        /// 获取数据项
        /// </summary>
        /// <returns></returns>
        public GenerateConfig GetData()
        {
            return data;
        }

        /// <summary>
        /// 获取参数配置项
        /// </summary>
        /// <param name="language">语言</param>
        /// <param name="templateGroupName">模板组名</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public TemplateParamConfig GetParamConfigItem(String language, String templateGroupName, Boolean ifTriggerException = true)
        {
            var configData = this.GetData();
            language = language.ToLower();
            templateGroupName = templateGroupName.ToLower();
            var targetItem = configData.TemplateParamConfigData.FirstOrDefault(tmp => tmp.Language.ToLower() == language && tmp.TemplateGroupName.ToLower() == templateGroupName);
            if (targetItem == null)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到 Language={language} TemplateGroupName={templateGroupName} 的参数配置项");
                }

                return null;
            }

            return targetItem;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Refresh()
        {
            var configData = dalObj.FindAll();
            if (configData.Any() == false)
            {
                data = new GenerateConfig();
                dalObj.Insert(data);
                return;
            }

            data = configData.First();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            dalObj.Update(data);
        }
    }
}
