using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerate
{
    using Autofac;
    using Plugn.CodeGenerate.Config;
    using Plugn.CodeGenerate.Data;
    using Plugn.CodeGenerate.Data.DbProvider;
    using Plugn.CodeGenerate.Data.SchemaObject;
    using Plugn.CodeGenerate.DbConn;
    using Plugn.CodeGenerate.T4TemplateEngineHost;
    using Plugn.CodeGenerate.TemplateMange;
    using Microsoft.VisualStudio.TextTemplating;
    using MySql.Data.MySqlClient;
    using ToolManager.Infrustructure;
    using ToolManager.Utility.Alert;
    using Plugn.CodeGenerate;
    using Plugn.CodeGenerate.Config.NameRule;
    using Plugn.CodeGenerate.Config.TypeMap;

    [FormAttribute("代码生成", "按模板组生成代码")]
    public partial class GroupGenerateForm : BaseForm
    {
        /// <summary>
        /// 当前操作的数据库
        /// </summary>
        SODatabase nowDb;

        /// <summary>
        /// 当前的数据库处理项
        /// </summary>
        DbSchema nowDbSchema;

        /// <summary>
        /// 当前操作的数据库连接名
        /// </summary>
        String nowConnectionName;

        /// <summary>
        /// 模板管理对象
        /// </summary>
        private TemplateManager templateManager = new TemplateManager();

        /// <summary>
        /// 配置对象业务处理类
        /// </summary>
        private GenerateConfigBLL configBllObj = new GenerateConfigBLL();

        /// <summary>
        /// 规则配置对象
        /// </summary>
        private NameRuleConfigBLL nameRuleConfigBLLObj = new NameRuleConfigBLL();

        /// <summary>
        /// 类型映射处理
        /// </summary>
        private TypeMapConfigBLL typeMapConfigBLLObj = new TypeMapConfigBLL();

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupGenerateForm()
        {
            InitializeComponent();

            this.templateManager.Init();
        }

        #region 事件处理

        /// <summary>
        /// 加载时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupGenerateForm_Load(object sender, EventArgs e)
        {
            // 初始化存储路径
            var savePath = configBllObj.GetData().SavePath;
            if (String.IsNullOrWhiteSpace(savePath) == false)
            {
                this.txtSavePath.Text = savePath;
            }

            LoadConnection();

            this.LoadLanguage();
        }

        /// <summary>
        /// 设置连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetConnection_Click(object sender, EventArgs e)
        {
            var dbSettingForm = new DatabaseSettingForm();
            if (dbSettingForm.ShowDialog() == DialogResult.Yes)
            {
                LoadConnection();
            }
        }

        /// <summary>
        /// 连接到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbConnectionList.SelectedItem == null)
            {
                MsgBox.Show("请先选择数据库连接");
                return;
            }

            var connName = cmbConnectionList.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(connName))
            {
                MsgBox.Show("请先配置数据库连接");
                return;
            }

            this.LoadTable(connName);
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbConnectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var dal = new DbConnDAL();
            //var model = dal.FindOne(cmbConnectionList.SelectedItem.ToString());

            //var dbSchema = DbSchemaFactory.Create(model.Name);

            //try
            //{
            //    nowDb = dbSchema.GetDatabase(model.DefaultDb);
            //}
            //catch (Exception e1)
            //{
            //    MsgBox.ShowErrorMessage("数据库打开失败:" + e1.Message);
            //}
        }

        /// <summary>
        /// 选择一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectOne();
        }

        /// <summary>
        /// 移除一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveOne();
        }

        /// <summary>
        /// 选中一个的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            this.SelectOne();
        }

        /// <summary>
        /// 选中所有的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }

        /// <summary>
        /// 移除所有的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            this.RemoveAll();
        }

        /// <summary>
        /// 移除一个的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveOne_Click(object sender, EventArgs e)
        {
            this.RemoveOne();
        }

        /// <summary>
        /// 语言下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = this.cmbLanguage.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                MsgBox.Show("请选择语言", "提示");
                return;
            }

            var groupList = this.LoadGroup(language);
            this.cmbTemplateGroup.SelectedIndex = 0;
        }

        /// <summary>
        /// 模板组下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTemplateGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = this.cmbLanguage.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                MsgBox.Show("请选择语言", "提示");
                return;
            }

            var groupName = this.cmbTemplateGroup.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                MsgBox.Show("请选择模板组", "提示");
                return;
            }

            var groupList = this.LoadTemplate(language, groupName);
            this.cmbTemplate.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择模板项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            txtSavePath.Text = folderBrowserDialog1.SelectedPath;

            // 更新配置
            configBllObj.GetData().SavePath = txtSavePath.Text;
            configBllObj.Save();
        }

        /// <summary>
        /// 代码生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var tableList = new List<SOTable>();
            foreach (var item in listBox2.Items)
            {
                tableList.Add((SOTable)item);
            }
            if (tableList.Count <= 0)
            {
                MsgBox.Show("请先选择要生成的表");
                return;
            }

            String language, groupName;
            var templateList = GetSelectedTemplate(out language, out groupName);
            if (templateList.Count <= 0)
            {
                MsgBox.Show("请先选择要使用的模块");
                return;
            }

            var savePath = txtSavePath.Text;
            if (String.IsNullOrWhiteSpace(savePath))
            {
                MsgBox.Show("请先选择存储目录");
                return;
            }

            var arg = new GenerateInfo()
            {
                Language = language,
                GroupName = groupName,
                TemplateInfos = templateList,
                TableList = tableList,
                SavePath = savePath
            };

            // 设置命名规则
            if (String.IsNullOrWhiteSpace(this.cmbRuleList.SelectedText))
            {
                MsgBox.Show("请选择命名规则列表");
                return;
            }
            arg.NameRuleConfig = this.nameRuleConfigBLLObj.GetItem(this.cmbRuleList.Text);

            // 设置类型转换列表
            var typeMapList = typeMapConfigBLLObj.GetList(language, false);
            if (typeMapList == null || typeMapList.Count <= 0)
            {
                MsgBox.Show("请配置类型映射列表");
                return;
            }
            arg.TypeMapConfigList = typeMapList;


            this.UseWaitCursor = true;
            this.Enabled = false;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = templateList.Count * arg.TableList.Count;
            this.backgroundWorker1.RunWorkerAsync(arg);
        }

        /// <summary>
        /// 代码生成信息
        /// </summary>
        class GenerateInfo
        {
            /// <summary>
            /// 语言
            /// </summary>
            public String Language { get; set; }

            /// <summary>
            /// 组名
            /// </summary>
            public String GroupName { get; set; }

            /// <summary>
            /// 模板列表
            /// </summary>
            public List<TemplateInfo> TemplateInfos { get; set; }

            /// <summary>
            /// 要生成的表列表
            /// </summary>
            public List<SOTable> TableList { get; set; }

            /// <summary>
            /// 类型映射配置列表
            /// </summary>
            public List<TypeMapConfig> TypeMapConfigList { get; set; }

            /// <summary>
            /// 命名配置对象
            /// </summary>
            public NameRuleConfig NameRuleConfig { get; set; }

            /// <summary>
            /// 保存路径
            /// </summary>
            public String SavePath { get; set; }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var arg = e.Argument as GenerateInfo;
                Boolean isError = this.DoBuild(arg);
                if (isError)
                {
                    MsgBox.Show("代码生成出错，具体见输出内容");
                }
                else
                {
                    MsgBox.Show("生成成功", "提示");
                }
            }
            catch (Exception e1)
            {
                MsgBox.ShowExceptionDialog(e1, "生成出错");
            }
            finally
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.UseWaitCursor = false;
                    this.Enabled = true;
                }));
            }
        }

        /// <summary>
        /// 打开生成目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtSavePath.Text))
            {
                MsgBox.Show("主先选择生成的目录");
                return;
            }

            Process.Start("explorer.exe", this.txtSavePath.Text);
        }

        /// <summary>
        /// 设置模板参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetArg_Click(object sender, EventArgs e)
        {
            if (this.cmbLanguage.SelectedItem == null)
            {
                MsgBox.Show("请选择模块项");
                return;
            }

            var language = this.cmbLanguage.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                MsgBox.Show("请选择语言", "提示");
                return;
            }

            var groupName = this.cmbTemplateGroup.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                MsgBox.Show("请选择模板组", "提示");
                return;
            }

            SetTemplateParamForm setForm = new SetTemplateParamForm(language, groupName);
            setForm.ShowDialog();

            // 刷新配置
            configBllObj.Refresh();
        }

        /// <summary>
        /// 规则配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetRule_Click(object sender, EventArgs e)
        {
            var frm = new SetNameRuleForm();
            frm.ShowDialog();

            this.nameRuleConfigBLLObj.Refresh();

            this.cmbRuleList.Items.Clear();
            this.cmbRuleList.Items.AddRange(this.nameRuleConfigBLLObj.GetData().Select(tmp => tmp.Name).ToArray());
        }

        /// <summary>
        /// 类型映射配置处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetTypeMap_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.cmbLanguage.Text))
            {
                MsgBox.Show("语言不能为空");
                return;
            }

            // 展示配置窗口
            var frm = new SetTypeMapForm(this.cmbLanguage.Text.Trim());
            frm.ShowDialog();

            typeMapConfigBLLObj.Refresh();
        }

        #endregion  事件处理

        #region 连接处理

        /// <summary>
        /// 加载连接
        /// </summary>
        private void LoadConnection()
        {
            var dal = new DbConnConfigDAL();
            var connList = dal.FindAll().ToList();

            cmbConnectionList.Items.Clear();
            foreach (var item in connList)
            {
                cmbConnectionList.Items.Add(item.Name);
            }

            var tmpIndex = connList.FindIndex(tmp => tmp.Name == nowConnectionName);
            if (tmpIndex >= 0)
            {
                cmbConnectionList.SelectedIndex = tmpIndex;
                LoadTable(connList[tmpIndex].Name);
            }
            else
            {
                //清理相关数据
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
            }
        }

        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="connectionName"></param>
        private void LoadTable(String connectionName)
        {
            // 加载数据库
            nowConnectionName = connectionName;
            var dal = new DbConnConfigDAL();
            var model = dal.FindOne(connectionName);

            this.nowDbSchema = DbSchemaFactory.Create(new MySqlDatabase(model.ConnectionString, MySqlClientFactory.Instance));
            try
            {
                nowDb = this.nowDbSchema.GetDatabase(model.DefaultDb);
            }
            catch (Exception e1)
            {
                MsgBox.ShowErrorMessage("数据库打开失败:" + e1.Message);
                return;
            }

            // 加载表
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            foreach (SOTable t in nowDb.TableList)
            {
                listBox1.Items.Add(t);
            }
        }

        /// <summary>
        /// 选择所有
        /// </summary>
        private void SelectAll()
        {
            if (listBox1.Items.Count > 0)
            {
                listBox2.Items.AddRange(listBox1.Items);
                listBox1.Items.Clear();
            }
        }

        /// <summary>
        /// 选择一个
        /// </summary>
        private void SelectOne()
        {
            object[] items = new object[listBox1.SelectedItems.Count];
            listBox1.SelectedItems.CopyTo(items, 0);
            listBox2.Items.AddRange(items);

            foreach (var item in items)
            {
                listBox1.Items.Remove(item);
            }
        }

        /// <summary>
        /// 移除一个
        /// </summary>
        private void RemoveOne()
        {
            object[] items = new object[listBox2.SelectedItems.Count];
            listBox2.SelectedItems.CopyTo(items, 0);
            listBox1.Items.AddRange(items);

            foreach (var item in items)
            {
                listBox2.Items.Remove(item);
            }
        }

        /// <summary>
        /// 移除所有
        /// </summary>
        private void RemoveAll()
        {
            if (listBox2.Items.Count > 0)
            {
                listBox1.Items.AddRange(listBox2.Items);
                listBox2.Items.Clear();
            }
        }

        #endregion

        #region 模板处理

        /// <summary>
        /// 加载语言
        /// </summary>
        private List<String> LoadLanguage()
        {
            var languageList = this.templateManager.GetLanguageList();

            this.cmbLanguage.Items.Clear();
            foreach (var item in languageList)
            {
                this.cmbLanguage.Items.Add(item);
            }

            return languageList;
        }

        /// <summary>
        /// 加载组
        /// </summary>
        /// <param name="language"></param>
        private List<String> LoadGroup(String language)
        {
            var groupList = this.templateManager.GetGroupList(language);

            this.cmbTemplateGroup.Items.Clear();
            foreach (var item in groupList)
            {
                this.cmbTemplateGroup.Items.Add(item);
            }

            return groupList;
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        /// <param name="language">语言</param>
        private List<TemplateInfo> LoadTemplate(String language, String group)
        {
            var templateList = this.templateManager.GetGroupTemplate(language, group);

            this.cmbTemplate.Items.Clear();
            this.cmbTemplate.Items.Add("所有");
            foreach (var item in templateList)
            {
                this.cmbTemplate.Items.Add(item);
            }

            return templateList;
        }

        /// <summary>
        /// 获取所有选择的模板
        /// </summary>
        /// <returns></returns>
        private List<TemplateInfo> GetSelectedTemplate(out String language, out String groupName)
        {
            var result = new List<TemplateInfo>();

            language = this.cmbLanguage.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                groupName = String.Empty;
                return result;
            }

            groupName = this.cmbTemplateGroup.SelectedItem.ToString();
            if (String.IsNullOrWhiteSpace(language))
            {
                return result;
            }

            if (this.cmbTemplate.SelectedIndex <= 0)
            {
                return this.templateManager.GetGroupTemplate(language, groupName);
            }

            // 获取选择项
            var templateItem = this.templateManager.GetGroupTemplate(language, groupName, this.cmbTemplate.SelectedItem.ToString());
            return new List<TemplateInfo>() { templateItem };
        }

        #endregion

        #region 代码生成

        /// <summary>
        /// 生成处理
        /// </summary>
        private Boolean DoBuild(GenerateInfo generateParam)
        {
            var logObj = Singleton.Container.Resolve<IOutput>();
            Boolean isHaveError = false;

            //遍历选中的表，一张表对应生成一个代码文件
            foreach (SOTable table in generateParam.TableList)
            {
                foreach (var templateItem in generateParam.TemplateInfos)
                {
                    var errMsg = BuildTable(table, generateParam.NameRuleConfig, generateParam.TypeMapConfigList, templateItem, generateParam.SavePath);
                    if (String.IsNullOrWhiteSpace(errMsg) == false)
                    {
                        isHaveError = true;
                        logObj.PrintLine($"Table:{table.Name}  TemplateFile:{templateItem.FilePath} {Environment.NewLine} error:{errMsg}");
                    }

                    this.Invoke(new Action(() =>
                    {
                        this.progressBar1.Value += 1;
                    }));
                }
            }

            return isHaveError;
        }

        /// <summary>
        /// 生成一个表
        /// </summary>
        /// <param name="table">表对象</param>
        /// <param name="templateItem">模板列表</param>
        /// <param name="outputPath">保存到的目标位置</param>
        private String BuildTable(SOTable table, NameRuleConfig nameRuleConfig, List<TypeMapConfig> typeMapConfigList, TemplateInfo templateItem, String outputPath)
        {
            List<SOColumn> columnList = table.ColumnList;//可能传入的是从PDObject对象转换过来的SODatabase对象

            if (columnList == null || columnList.Count == 0)
            {
                columnList = this.nowDbSchema.GetTableColumnList(table);
            }

            //生成代码文件
            TableHost host = new TableHost(nameRuleConfig, typeMapConfigList, table);
            host.Table = table;
            host.ColumnList = columnList;
            host.TemplateFile = templateItem.FilePath;

            // 额外参数追加
            var paramList = this.configBllObj.GetParamConfigItem(templateItem.Language, templateItem.GroupName, false);
            if (paramList != null && paramList.ParamData.Count > 0)
            {
                foreach (var item in paramList.ParamData)
                {
                    host.SetValue(item.ParamName, item.ParamValue);
                }
            }

            Engine engine = new Engine();
            string templateContent = File.ReadAllText(templateItem.FilePath);
            var outputContent = engine.ProcessTemplate(templateContent, host);
            string outputFile = Path.Combine(outputPath, string.Format("{0}{1}", table.Name, host.FileExtention));
            if (String.IsNullOrWhiteSpace(host.OutputFileName) == false)
            {
                outputFile = Path.Combine(outputPath, host.OutputFileName);
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

            if (Directory.Exists(outputPath) == false)
            {
                Directory.CreateDirectory(outputPath);
            }

            File.WriteAllText(outputFile, outputContent, Encoding.UTF8);

            return String.Empty;
        }

        #endregion 代码生成
    }
}
