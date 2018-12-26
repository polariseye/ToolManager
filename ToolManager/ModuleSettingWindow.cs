using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolManager
{
    using ToolManager.Infrustructure;
    using ToolManager.Module;

    public partial class ModuleSettingWindow : Form
    {
        /// <summary>
        /// 需要删除的列表
        /// </summary>
        public List<AssemblyInfo> DeleteList { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ModuleSettingWindow()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;

            this.DeleteList = new List<AssemblyInfo>();
        }

        /// <summary>
        /// 窗口加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModuleSettingWindow_Load(object sender, EventArgs e)
        {
            var moduleList = ModuleManager.GetAllModule().Select(tmp => new ShownInfo(tmp)).ToList();
            this.moduleGrid.DataSource = new BindingList<ShownInfo>(moduleList);
        }

        /// <summary>
        /// 删除一项数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moduleGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var colItem = this.moduleGrid.Columns[e.ColumnIndex];
            if (colItem.Name != colDelete.Name)
            {
                // 不是点击的删除列
                return;
            }

            var tmpData = this.moduleGrid.DataSource as BindingList<ShownInfo>;

            this.DeleteList.Add(tmpData[e.RowIndex].TargetModule);
            tmpData.RemoveAt(e.RowIndex);
            //this.moduleGrid.Refresh();
        }

        /// <summary>
        /// 保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.DeleteList.Count <= 0)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            ModuleManager.DeleteModule(DeleteList);

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }

    public class ShownInfo
    {
        public String ModuleName { get; set; }

        public String ModulePath { get; set; }

        public Int32 FunctionCount { get; set; }

        public AssemblyInfo TargetModule { get; set; }

        public ShownInfo(AssemblyInfo moduleItem)
        {
            this.ModuleName = moduleItem.ModuleInfo.Name;
            this.ModulePath = moduleItem.ModuleInfo.ModulePath;
            this.FunctionCount = moduleItem.FormInfoList.Count;
            this.TargetModule = moduleItem;
        }
    }
}
