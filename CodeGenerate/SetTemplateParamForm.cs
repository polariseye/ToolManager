using CodeGenerate.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (paramConfigData != null)
            {
                paramList = new BindingList<ParamItem>(paramConfigData.ParamData);
            }
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

        }
    }
}
