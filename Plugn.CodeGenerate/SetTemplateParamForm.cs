using Plugn.CodeGenerate.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolManager.Utility.Alert;

namespace CodeGenerate
{
    public partial class SetTemplateParamForm : Form
    {
        /// <summary>
        /// 语言名
        /// </summary>
        public String Langugage { get; private set; }

        /// <summary>
        /// 模板组名
        /// </summary>
        public String TemplateGroupName { get; private set; }

        /// <summary>
        /// 配置管理对象
        /// </summary>
        private GenerateConfigBLL configBllObj = new GenerateConfigBLL();

        /// <summary>
        /// 参数配置项
        /// </summary>
        private TemplateParamConfig paramConfigData = null;

        /// <summary>
        /// 参数列表
        /// </summary>
        private BindingList<ParamItem> paramList = new BindingList<ParamItem>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="language">编程语言</param>
        /// <param name="templateGroupName">模板组名</param>
        public SetTemplateParamForm(String language, String templateGroupName)
        {
            this.Langugage = language;
            this.TemplateGroupName = templateGroupName;
            InitializeComponent();

            this.lbLanguage.Text = this.Langugage;
            this.lbTemplateGroup.Text = this.TemplateGroupName;

            paramConfigData = configBllObj.GetParamConfigItem(language, templateGroupName, false);
            if (paramConfigData == null)
            {
                paramConfigData = new TemplateParamConfig();
                paramConfigData.Language = language;
                paramConfigData.TemplateGroupName = templateGroupName;
                paramConfigData.ParamData = new List<ParamItem>();
            }

            paramList = new BindingList<ParamItem>(paramConfigData.ParamData);
        }

        /// <summary>
        /// 加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTemplateParamForm_Load(object sender, EventArgs e)
        {
            this.paramGrid.DataSource = this.paramList;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (paramList.Any(tmp => String.IsNullOrWhiteSpace(tmp.ParamName)))
            {
                MsgBox.Show("存在空的参数名，请确认", "提示");
                return;
            }

            if (paramList.Select(tmp => tmp.ParamName.ToLower()).Distinct().Count() != this.paramList.Count)
            {
                MsgBox.Show("存在重复的参数项", "提示");
                return;
            }

            try
            {
                paramConfigData.ParamData = paramList.ToList();
                var resultIndex = configBllObj.GetData().TemplateParamConfigData.FindIndex(tmp => tmp.Language == this.Langugage && tmp.TemplateGroupName == this.TemplateGroupName);
                if (resultIndex < 0)
                {
                    configBllObj.GetData().TemplateParamConfigData.Add(paramConfigData);
                }

                // 保存数据
                configBllObj.Save();

                this.Close();
            }
            catch (Exception e1)
            {
                MsgBox.ShowExceptionDialog(e1, "数据存在出错");
            }
        }

        private void paramGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void paramGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var rowItem = this.paramList[e.RowIndex];
            if (e.ColumnIndex == 0)
            {
                //确保key不重复
            }

        }
    }
}
