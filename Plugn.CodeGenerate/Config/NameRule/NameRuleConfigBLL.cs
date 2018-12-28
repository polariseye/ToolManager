using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.NameRule
{
    public class NameRuleConfigBLL
    {
        private List<NameRuleConfig> data;

        private NameRuleConfigDAL dalObj = new NameRuleConfigDAL();

        /// <summary>
        /// 初始化
        /// </summary>
        static NameRuleConfigBLL()
        {
            NameRuleConfigDAL.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NameRuleConfigBLL()
        {
            this.Refresh();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<NameRuleConfig> GetData()
        {
            return data;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Refresh()
        {
            var configData = dalObj.FindAll();
            this.data = configData.ToList();
        }

        /// <summary>
        /// 获取参数配置项
        /// </summary>
        /// <param name="id">语言</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public NameRuleConfig GetItem(Int32 id, Boolean ifTriggerException = true)
        {
            var configData = this.GetData();
            var result = configData.FirstOrDefault(tmp => tmp.Id == id);
            if (result == null)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到 Id={id} 的命名规则配置项");
                }

                return null;
            }

            return result;
        }

        /// <summary>
        /// 获取参数配置项
        /// </summary>
        /// <param name="name">项</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public NameRuleConfig GetItem(String name, Boolean ifTriggerException = true)
        {
            var configData = this.GetData();
            var result = configData.FirstOrDefault(tmp => tmp.Name == name);
            if (result == null)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到 Name={name} 的命名规则配置项");
                }

                return null;
            }

            return result;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save(NameRuleConfig model)
        {
            dalObj.Update(model);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Insert(NameRuleConfig model)
        {
            dalObj.Insert(model);
            this.data.Add(model);
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Int32 id)
        {
            dalObj.Delete(id);
            this.data.RemoveAll(tmp => tmp.Id == id);
        }
    }
}
