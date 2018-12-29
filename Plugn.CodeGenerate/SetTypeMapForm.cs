using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugn.CodeGenerate
{
    using Plugn.CodeGenerate.Config.TypeMap;
    using ToolManager.Utility.Alert;

    public partial class SetTypeMapForm : Form
    {
        /// <summary>
        /// 语言名
        /// </summary>
        public String Langugage { get; private set; }

        /// <summary>
        /// 配置管理对象
        /// </summary>
        private TypeMapConfigBLL configBllObj = new TypeMapConfigBLL();

        /// <summary>
        /// 当前的映射列表
        /// </summary>
        private List<TypeMapConfig> typeMapList = new List<TypeMapConfig>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="language">编程语言</param>
        public SetTypeMapForm(String language)
        {
            this.Langugage = language;
            InitializeComponent();

            this.lbLanguage.Text = this.Langugage;
        }

        /// <summary>
        /// 加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTemplateParamForm_Load(object sender, EventArgs e)
        {
            var mapList = configBllObj.GetList(this.Langugage);
            if (mapList.Count > 0)
            {
                typeMapList = mapList.Select(tmp => tmp.Clone()).ToList();
            }

            this.typeMapGrid.DataSource = new BindingList<TypeMapConfig>(typeMapList);
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (typeMapList.Any(tmp => String.IsNullOrWhiteSpace(tmp.DbType) || String.IsNullOrWhiteSpace(tmp.TargetTypeString)))
            {
                MsgBox.Show("存在空的配置项，请确认", "提示");
                return;
            }

            if (typeMapList.Select(tmp => tmp.DbType.ToLower()).Distinct().Count() != this.typeMapList.Count)
            {
                MsgBox.Show("存在重复的配置项", "提示");
                return;
            }

            try
            {
                var tmpLanguage = this.Langugage.ToLower().Trim();
                foreach (var item in this.typeMapList)
                {
                    item.Language = tmpLanguage;
                    item.DbType = item.DbType.ToUpper().Trim();
                }

                // 保存数据
                configBllObj.UpdateAll(this.Langugage, this.typeMapList.Select(tmp => tmp.Clone()).ToList());

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
        }
    }
}