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
    using Kalman.Data;
    using Kalman.Data.SchemaObject;
    using Kalman.Database;
    using ToolManager.Infrustructure;
    using ToolManager.Utility.Alert;

    [FormAttribute("代码生成", "按模板组生成代码")]
    public partial class GroupGenerateForm : BaseForm
    {
        /// <summary>
        /// 当前操作的数据库
        /// </summary>
        SODatabase nowDb;

        /// <summary>
        /// 当前操作的数据库连接名
        /// </summary>
        String nowConnectionName;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupGenerateForm()
        {
            InitializeComponent();
        }

        #region 事件处理

        /// <summary>
        /// 加载时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupGenerateForm_Load(object sender, EventArgs e)
        {
            LoadConnection();
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
            var dal = new DbConnDAL();
            var model = dal.FindOne(cmbConnectionList.SelectedItem.ToString());

            var dbSchema = DbSchemaFactory.Create(model.Name);

            try
            {
                nowDb = dbSchema.GetDatabase(model.DefaultDb);
            }
            catch (Exception e1)
            {
                MsgBox.ShowErrorMessage("数据库打开失败:" + e1.Message);
            }
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
        /// 代码生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {

        }

        #endregion  事件处理

        /// <summary>
        /// 加载连接
        /// </summary>
        private void LoadConnection()
        {
            var dal = new DbConnDAL();
            var connList = dal.FindAll();

            cmbConnectionList.Items.Clear();
            foreach (var item in connList)
            {
                cmbConnectionList.Items.Add(item.Name);
            }

            // 加载连接数据
            var nowConn = connList.FirstOrDefault(tmp => tmp.Name == nowConnectionName);
            if (nowConn != null)
            {
                LoadTable(nowConn.Name);
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
            var dal = new DbConnDAL();
            var model = dal.FindOne(connectionName);
            var dbSchema = DbSchemaFactory.Create(model.Name);
            try
            {
                nowDb = dbSchema.GetDatabase(model.DefaultDb);
            }
            catch (Exception e1)
            {
                MsgBox.ShowErrorMessage("数据库打开失败:" + e1.Message);
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

    }
}
