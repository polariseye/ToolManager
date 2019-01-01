using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.T4TemplateGenerate
{
    using Plugn.CodeGenerate.Config;
    using Plugn.CodeGenerate.Config.NameRule;
    using Plugn.CodeGenerate.Config.TypeMap;
    using Plugn.CodeGenerate.Data.SchemaObject;
    using Plugn.CodeGenerate.TemplateMange;

    /// <summary>
    /// 代码生成基类
    /// </summary>
    public abstract class GenerateBase
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        public SODatabase Database { get; private set; }

        /// <summary>
        /// 数据表列表
        /// </summary>
        public List<SOTable> TableList { get; private set; }

        /// <summary>
        /// 要使用的模板列表
        /// </summary>
        public List<TemplateInfo> TemplateInfoList { get; private set; }

        /// <summary>
        /// 保存目录
        /// </summary>
        public String SavePath { get; private set; }

        /// <summary>
        /// 类型映射配置列表
        /// </summary>
        public List<TypeMapConfig> TypeMapConfigList { get; private set; }

        /// <summary>
        /// 命名规则
        /// </summary>
        public NameRuleConfig NameRuleConfig { get; private set; }

        public List<ParamItem> ParamList { get; private set; }

        /// <summary>
        /// 完成一次生成的事件
        /// Param1:保存到的文件名
        /// Param2:错误信息
        /// </summary>
        public event Action<String, String> OnFinishOneGenerateEvent;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database">要使用的数据库</param>
        /// <param name="tableList">要使用的表列表</param>
        /// <param name="templateInfoList">要使用的模板列表</param>
        /// <param name="nameRuleConfig">命名规则</param>
        /// <param name="paramList">生成的参数信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="typeMapConfigList">类型映射</param>
        public GenerateBase(SODatabase database, List<SOTable> tableList, List<TemplateInfo> templateInfoList,
            List<TypeMapConfig> typeMapConfigList, NameRuleConfig nameRuleConfig, List<ParamItem> paramList, String savePath)
        {
            this.TableList = tableList;
            this.Database = database;
            this.TemplateInfoList = templateInfoList;
            this.TypeMapConfigList = typeMapConfigList;
            this.NameRuleConfig = nameRuleConfig;
            this.ParamList = paramList;
            this.SavePath = savePath;
        }

        /// <summary>
        /// 参数检查
        /// </summary>
        /// <returns>错误信息</returns>
        public abstract String CheckParam();

        /// <summary>
        /// 获取需要进行生成的总次数
        /// </summary>
        /// <returns></returns>
        public abstract Int32 GetGenenrateTotalCount();

        /// <summary>
        /// 代码生成
        /// </summary>
        /// <param name="paramData">生成过程的参数信息</param>
        /// <returns>错误信息</returns>
        public abstract bool Generate();

        /// <summary>
        /// 触发完成一次生成的事件
        /// </summary>
        /// <param name="fileName">保存的文件名</param>
        /// <param name="errMessage">错误信息</param>
        protected void InvokeFinishOneGenerate(String fileName, String errMessage)
        {
            if (this.OnFinishOneGenerateEvent != null)
            {
                this.OnFinishOneGenerateEvent(fileName, errMessage);
            }
        }
    }
}
