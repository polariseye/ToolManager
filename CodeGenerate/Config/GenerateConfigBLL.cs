using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate.Config
{
    /// <summary>
    /// 代码生成的构造函数
    /// </summary>
    public class GenerateConfigBLL
    {
        private GenerateConfig data;
        private GenerateConfigDAL dalObj = new GenerateConfigDAL();

        static GenerateConfigBLL()
        {
            GenerateConfigDAL.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenerateConfigBLL()
        {
            var configData = dalObj.FindAll();
            if (configData.Any() == false)
            {
                data = new GenerateConfig();
                dalObj.Insert(data);
                return;
            }

            data = configData.First();
        }

        /// <summary>
        /// 获取数据项
        /// </summary>
        /// <returns></returns>
        public GenerateConfig GetData()
        {
            return data;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            dalObj.Update(data);
        }
    }
}
