using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolManager.Infrustructure;
using Autofac;
using CodeGenerate;

namespace TollManager.TestWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 注册单例
            Singleton.Builder.RegisterInstance<IOutput>(new Output());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroupGenerateForm frm = new GroupGenerateForm();
            frm.Show();
        }
    }
}
