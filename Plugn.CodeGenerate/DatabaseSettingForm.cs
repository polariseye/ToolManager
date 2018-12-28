using System;
using System.Linq;
using System.Windows.Forms;

namespace CodeGenerate
{
    using ToolManager.Infrustructure;
    using Autofac;
    using ToolManager.Utility.Alert;
    using Plugn.CodeGenerate.Config;
    using Plugn.CodeGenerate.DbConn;

    public partial class DatabaseSettingForm : Form
    {
        DbConnConfigDAL dal = new DbConnConfigDAL();
        bool ConnectStringChanged = false;

        public DatabaseSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存数据库连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var logObj = Singleton.Container.Resolve<IOutput>();
            var connectName = cbConnectStringName.Text.Trim();
            var providerName = cbProviderName.Text.Trim();
            var connectString = txtConnectString.Text.Trim();
            var defaultDb = txtDefaultDb.Text.Trim();

            if (string.IsNullOrEmpty(connectName))
            {
                MsgBox.Show("请选择或输入连接名称！");
                return;
            }

            if (string.IsNullOrEmpty(providerName))
            {
                MsgBox.Show("请选择Provider名称！");
                return;
            }

            if (string.IsNullOrEmpty(connectString))
            {
                MsgBox.Show("请输入ConnectString内容！");
                return;
            }

            if (string.IsNullOrEmpty(defaultDb))
            {
                MsgBox.Show("请输入默认数据库！");
                return;
            }

            try
            {
                var model = dal.FindOne(connectName);
                if (model == null)
                {
                    model = new DbConnConfig();
                    model.ConnectionString = connectString;
                    model.Name = connectName;
                    model.ProviderName = providerName;
                    model.DefaultDb = defaultDb;

                    dal.Insert(model);
                }
                else
                {
                    model.ConnectionString = connectString;
                    model.Name = connectName;
                    model.ProviderName = providerName;
                    model.DefaultDb = defaultDb;
                    dal.Update(model);
                }

                this.DialogResult = DialogResult.Yes;
                MsgBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MsgBox.ShowExceptionMessage(ex);
                this.DialogResult = DialogResult.No;
            }
        }

        private void DatabaseSettingForm_Load(object sender, EventArgs e)
        {
            RefreshDatabase();

            //todo: 因为没有实现其他驱动，所以暂时写死
            this.cbProviderName.SelectedIndex = 1;
            this.cbProviderName.Enabled = false;
        }

        private void cbConnectStringName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cbConnectStringName.Text;
            var model = dal.FindOne(item.ToString());
            if (model != null)
            {
                ConnectStringChanged = true;
                txtConnectString.Text = model.ConnectionString;
                cbProviderName.SelectedItem = model.ProviderName;
                txtDefaultDb.Text = model.DefaultDb;
            }
        }

        /// <summary>
        /// 删除数据库项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var name = cbConnectStringName.Text;

            if (!string.IsNullOrEmpty(name))
            {
                var result = dal.Delete(name);
                this.DialogResult = DialogResult.Yes;
                MsgBox.Show("删除成功！");
            }
        }


        /// <summary>
        /// 刷新所有数据库配置
        /// </summary>
        private void RefreshDatabase()
        {
            cbConnectStringName.Items.Clear();

            var list = dal.FindAll().ToList();
            foreach (var item in list)
            {
                if (item.IsActive)
                    cbConnectStringName.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// 选择数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProviderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConnectStringChanged)
            {
                ConnectStringChanged = false;
                return;
            }

            cbConnectStringName.Text = string.Empty;

            switch (cbProviderName.Text)
            {
                case "System.Data.SqlClient":
                    txtConnectString.Text = "server=.;database=loamen;uid=sa;pwd=sa;";
                    break;
                case "MySql.Data.MySqlClient":
                    txtConnectString.Text = "server=localhost;user id=root;password=root;persistsecurityinfo=True;port=3306;database=loamen;SslMode=none";
                    break;
                case "System.Data.SQLite":
                    txtConnectString.Text = @"Data Source=D:\data\sqlite3\loamen.s3db;Version=3;";
                    break;
                case "System.Data.Odbc":
                    txtConnectString.Text = @"Driver={SQL Server};Server=(local);Trusted_Connection=Yes;Database=loamen;";
                    break;
                case "System.Data.OleDb":
                    txtConnectString.Text = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=d:\loamnen.mdb;User Id=admin;Password=;";
                    break;
                case "Devart.Data.Oracle":
                    txtConnectString.Text = "Data Source=orcl;User ID=sa;Password=sa;";
                    break;
                case "Oracle.ManagedDataAccess.Client":
                    txtConnectString.Text = "User Id=root ;Password=root;Data Source=loamen";
                    break;
                case "System.Data.OracleClient":
                    txtConnectString.Text = "Data Source=Oracle8i;Integrated Security=yes";
                    break;
                case "DDTek.Oracle":
                    txtConnectString.Text = "Host=127.0.0.1;Port=1521;User ID=root;Password=root;Service Name=loamen";
                    break;
                case "IBM.Data.DB2":
                    txtConnectString.Text = "Server=localhost:8081;Database=loamen;UID=root;PWD=root;";
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    txtConnectString.Text = "User=SYSDBA;Password=masterkey;Database=SampleDatabase.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
                    break;
                default:
                    txtConnectString.Text = "";
                    break;
            }
        }
    }
}
