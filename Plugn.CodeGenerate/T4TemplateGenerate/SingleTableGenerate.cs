
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plugn.CodeGenerate.T4TemplateGenerate
{
    using Microsoft.VisualStudio.TextTemplating;
    using Plugn.CodeGenerate.Config;
    using Plugn.CodeGenerate.Config.NameRule;
    using Plugn.CodeGenerate.Config.TypeMap;
    using Plugn.CodeGenerate.Data.SchemaObject;
    using Plugn.CodeGenerate.T4TemplateEngineHost;
    using Plugn.CodeGenerate.TemplateMange;

    /// <summary>
    /// 单表生成
    /// </summary>
    public class SingleTableGenerate : GenerateBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="database">要使用的数据库</param>
        /// <param name="tableList">要使用的表列表</param>
        /// <param name="templateInfoList">要使用的模板列表</param>
        /// <param name="nameRuleConfig">命名规则</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="paramList">生成的参数信息</param>
        /// <param name="typeMapList">类型映射</param>
        public SingleTableGenerate(SODatabase database, List<SOTable> tableList, List<TemplateInfo> templateInfoList,
            List<TypeMapConfig> typeMapConfigList, NameRuleConfig nameRuleConfig, List<ParamItem> paramList, String savePath) :
            base(database, tableList, templateInfoList, typeMapConfigList, nameRuleConfig, paramList, savePath)
        {
        }

        /// <summary>
        /// 参数检查
        /// </summary>
        /// <returns>错误信息</returns>
        public override String CheckParam()
        {
            if (this.Database == null)
            {
                return "请选择使用的数据库";
            }

            if (this.TableList == null || this.TableList.Count <= 0)
            {
                return "请选择要生成的表";
            }

            if (this.TemplateInfoList == null || this.TemplateInfoList.Count <= 0)
            {
                return "请选择要使用的模板";
            }

            if (this.TypeMapConfigList == null || this.TypeMapConfigList.Count <= 0)
            {
                return "请配置类型转换";
            }

            if (String.IsNullOrWhiteSpace(this.SavePath))
            {
                return "请选择存储目录";
            }

            return String.Empty;
        }

        /// <summary>
        /// 获取需要进行生成的总次数
        /// </summary>
        /// <returns></returns>
        public override Int32 GetGenenrateTotalCount()
        {
            return this.TableList.Count * this.TemplateInfoList.Count;
        }

        /// <summary>
        /// 代码生成
        /// </summary>
        /// <returns>错误信息</returns>
        public override Boolean Generate()
        {
            Boolean isHaveError = false;

            //遍历选中的表，一张表对应生成一个代码文件
            foreach (SOTable table in this.TableList)
            {
                foreach (var templateItem in this.TemplateInfoList)
                {
                    var fileName = String.Empty;
                    var errMsg = BuildTable(table, templateItem, out fileName);
                    if (String.IsNullOrWhiteSpace(errMsg) == false)
                    {
                        isHaveError = true;
                    }

                    this.InvokeFinishOneGenerate(fileName, errMsg);
                }
            }

            return isHaveError;
        }

        /// <summary>
        /// 生成一个表
        /// </summary>
        /// <param name="table">表对象</param>
        /// <param name="templateItem">模板列表</param>
        /// <param name="savedFileName">保存到的文件名</param>
        /// <param name="paramData">参数数据</param>
        private String BuildTable(SOTable table, TemplateInfo templateItem, out String savedFileName)
        {
            List<SOColumn> columnList = table.ColumnList;//可能传入的是从PDObject对象转换过来的SODatabase对象

            //生成代码文件
            TableHost host = new TableHost(this.NameRuleConfig, this.TypeMapConfigList, table);
            host.Table = table;
            host.ColumnList = columnList;
            host.TemplateFile = templateItem.FilePath;

            // 额外参数追加
            if (this.ParamList != null && this.ParamList.Count > 0)
            {
                foreach (var item in this.ParamList)
                {
                    host.SetValue(item.ParamName, item.ParamValue);
                }
            }

            Engine engine = new Engine();
            string templateContent = File.ReadAllText(templateItem.FilePath);
            var outputContent = engine.ProcessTemplate(templateContent, host);
            savedFileName = Path.Combine(this.SavePath, string.Format("{0}{1}", table.Name, host.FileExtention));
            if (String.IsNullOrWhiteSpace(host.OutputFileName) == false)
            {
                savedFileName = Path.Combine(this.SavePath, host.OutputFileName);
            }

            StringBuilder sb = new StringBuilder();
            if (host.ErrorCollection != null && host.ErrorCollection.HasErrors)
            {
                foreach (CompilerError err in host.ErrorCollection)
                {
                    sb.AppendLine(err.ToString());
                }

                return sb.ToString();
            }

            if (Directory.Exists(this.SavePath) == false)
            {
                Directory.CreateDirectory(this.SavePath);
            }

            File.WriteAllText(savedFileName, outputContent, host.FileEncoding);

            return String.Empty;
        }

    }
}
