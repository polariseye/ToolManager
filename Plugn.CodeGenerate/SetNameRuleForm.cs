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
    using Plugn.CodeGenerate.Config.NameRule;
    using ToolManager.Utility.Alert;

    public partial class SetNameRuleForm : Form
    {
        /// <summary>
        /// 业务逻辑对象
        /// </summary>
        private NameRuleConfigBLL bllObj = new NameRuleConfigBLL();

        /// <summary>
        /// 当前处理的项
        /// </summary>
        private NameRuleConfig nowConfigItem;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SetNameRuleForm()
        {
            InitializeComponent();

            this.LoadRuleList();
        }

        #region 事件处理

        /// <summary>
        /// 变更要编辑的项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbRuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ruleName = cbRuleName.SelectedText;
            if (String.IsNullOrEmpty(ruleName))
            {
                this.nowConfigItem = null;
                this.Clear();
                return;
            }

            this.nowConfigItem = this.bllObj.GetItem(ruleName);
            LoadItem(this.nowConfigItem);
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var ruleName = cbRuleName.Text.Trim();
            if (String.IsNullOrWhiteSpace(ruleName))
            {
                MsgBox.Show("规则名不能为空");
                return;
            }

            if (this.nowConfigItem == null)
            {
                return;
            }

            this.bllObj.Delete(this.nowConfigItem.Id);
            this.nowConfigItem = null;
            this.Clear();

            this.cbRuleName.Items.RemoveAt(this.cbRuleName.SelectedIndex);
        }

        /// <summary>
        /// 保存项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (nowConfigItem == null)
            {
                // 添加
                btnAdd_Click(null, null);
                return;
            }

            var ruleName = cbRuleName.Text.Trim();
            if (String.IsNullOrWhiteSpace(ruleName))
            {
                MsgBox.Show("规则名不能为空");
                return;
            }

            var ruleList = this.bllObj.GetData();
            var nowRule = ruleList.FirstOrDefault(tmp => tmp.Name == ruleName && tmp.Id != nowConfigItem.Id);
            if (nowRule != null)
            {
                MsgBox.Show("存在重复的规则名");
                return;
            }

            this.nowConfigItem.Name = ruleName;
            this.nowConfigItem.Seperator = txtSeperator.Text.Trim();
            if (this.rdioFirstLower.Checked)
            {
                this.nowConfigItem.FirstCharHandleType = FirstCharHandleType.FirstCharLower;
            }
            else
            {
                this.nowConfigItem.FirstCharHandleType = FirstCharHandleType.FirstCharUp;
            }

            this.nowConfigItem.PrefixList = ((BindingList<ReplaceItem>)this.gridPrefix.DataSource).ToList();
            this.nowConfigItem.StuffixList = ((BindingList<ReplaceItem>)this.gridSuffix.DataSource).ToList();

            // 更新项
            this.bllObj.Save(this.nowConfigItem);
        }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var ruleName = cbRuleName.Text;
            if (String.IsNullOrWhiteSpace(ruleName))
            {
                MsgBox.Show("规则名不能为空");
                return;
            }

            var ruleList = this.bllObj.GetData();
            var nowRule = ruleList.FirstOrDefault(tmp => tmp.Name == ruleName);
            if (nowRule != null)
            {
                MsgBox.Show("存在重复的规则名");
                return;
            }

            var tmpOldItem = this.nowConfigItem;

            this.nowConfigItem = new NameRuleConfig();
            this.nowConfigItem.Id = GetNextId();
            this.nowConfigItem.Name = ruleName.Trim();
            this.nowConfigItem.Seperator = txtSeperator.Text.Trim();
            if (this.rdioFirstLower.Checked)
            {
                this.nowConfigItem.FirstCharHandleType = FirstCharHandleType.FirstCharLower;
            }
            else
            {
                this.nowConfigItem.FirstCharHandleType = FirstCharHandleType.FirstCharUp;
            }

            this.nowConfigItem.PrefixList = ((BindingList<ReplaceItem>)this.gridPrefix.DataSource).ToList();
            this.nowConfigItem.StuffixList = ((BindingList<ReplaceItem>)this.gridSuffix.DataSource).ToList();

            // 添加项
            this.bllObj.Insert(this.nowConfigItem);

            if (tmpOldItem != null)
            {
                this.cbRuleName.Items.Add(tmpOldItem.Name);
            }
        }

        #endregion 事件处理

        /// <summary>
        /// 加载规则列表
        /// </summary>
        private void LoadRuleList()
        {
            var data = bllObj.GetData();
            this.cbRuleName.Items.Clear();

            foreach (var item in data)
            {
                this.cbRuleName.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// 清理所有数据
        /// </summary>
        private void Clear()
        {
            this.txtSeperator.Text = "";
            this.gridPrefix.DataSource = null;
            this.gridSuffix.DataSource = null;
            this.rdoFirstUpper.Checked = true;
        }

        /// <summary>
        /// 加载项
        /// </summary>
        private void LoadItem(NameRuleConfig configItem)
        {
            this.txtSeperator.Text = configItem.Seperator;
            if (configItem.FirstCharHandleType == FirstCharHandleType.FirstCharLower)
            {
                this.rdioFirstLower.Checked = true;
            }
            else
            {
                this.rdoFirstUpper.Checked = true;
            }

            this.gridPrefix.DataSource = new BindingList<ReplaceItem>(configItem.PrefixList);
            this.gridSuffix.DataSource = new BindingList<ReplaceItem>(configItem.StuffixList);
        }

        /// <summary>
        /// 获取下一个Id
        /// </summary>
        /// <returns></returns>
        private Int32 GetNextId()
        {
            var ruleList = this.bllObj.GetData();
            if (ruleList.Count <= 0)
            {
                return 1;
            }

            return ruleList.Max(tmp => tmp.Id) + 1;
        }
    }
}
