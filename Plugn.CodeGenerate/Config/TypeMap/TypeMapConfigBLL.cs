using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.TypeMap
{
    /// <summary>
    /// 类型映射业务处理类
    /// </summary>
    public class TypeMapConfigBLL
    {
        private Dictionary<String, List<TypeMapConfig>> data;

        private TypeMapConfigDAL dalObj = new TypeMapConfigDAL();

        /// <summary>
        /// 初始化
        /// </summary>
        static TypeMapConfigBLL()
        {
            TypeMapConfigDAL.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TypeMapConfigBLL()
        {
            this.Refresh();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, List<TypeMapConfig>> GetData()
        {
            return data;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Refresh()
        {
            var configData = dalObj.FindAll();

            var result = new Dictionary<String, List<TypeMapConfig>>();
            foreach (var item in configData)
            {
                if (result.ContainsKey(item.Language.ToLower()) == false)
                {
                    result[item.Language] = new List<TypeMapConfig>();
                }

                result[item.Language.ToLower()].Add(item);
            }
            this.data = result;
        }

        /// <summary>
        /// 获取参数配置项
        /// </summary>
        /// <param name="id">语言</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public TypeMapConfig GetItem(Int32 id, Boolean ifTriggerException = true)
        {
            var configData = this.GetData();
            foreach (var dataByLanguage in configData.Values)
            {
                var result = dataByLanguage.FirstOrDefault(tmp => tmp.Id == id);
                if (result != null)
                {
                    return result;
                }
            }

            if (ifTriggerException)
            {
                throw new Exception($"未找到 Id={id} 的类型映射配置项");
            }

            return null;
        }

        /// <summary>
        /// 获取参数配置项
        /// </summary>
        /// <param name="language">项</param>
        /// <param name="ifTriggerException">是否触发异常</param>
        /// <returns></returns>
        public List<TypeMapConfig> GetList(String language, Boolean ifTriggerException = true)
        {
            var configData = this.GetData();
            language = language.ToLower();
            if (configData.ContainsKey(language) == false)
            {
                if (ifTriggerException)
                {
                    throw new Exception($"未找到 language={language} 的命名规则配置项");
                }

                return null;
            }

            return configData[language];
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save(TypeMapConfig model)
        {
            dalObj.Update(model);
        }

        /// <summary>
        /// 更新所有项
        /// </summary>
        /// <param name="language"></param>
        /// <param name="typeMapConfigs"></param>
        public void UpdateAll(String language, List<TypeMapConfig> typeMapConfigs)
        {
            dalObj.DeleteByLanguage(language);
            foreach (var item in typeMapConfigs)
            {
                dalObj.Insert(item);
            }

            this.data[language.ToLower()] = new List<TypeMapConfig>(typeMapConfigs);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Insert(TypeMapConfig model)
        {
            dalObj.Insert(model);
            if (this.data.ContainsKey(model.Language.ToLower()) == false)
            {
                this.data[model.Language.ToLower()] = new List<TypeMapConfig>();
            }

            this.data[model.Language.ToLower()].Add(model);
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Int32 id)
        {
            dalObj.Delete(id);

            var nowItem = this.GetItem(id, false);
            if (nowItem == null)
            {
                return;
            }

            this.data[nowItem.Language.ToLower()].RemoveAll(tmp => tmp.Id == id);
            if (this.data[nowItem.Language.ToLower()].Count <= 0)
            {
                this.data.Remove(nowItem.Language.ToLower());
            }
        }

        /// <summary>
        /// 转换为指定语言的类型
        /// </summary>
        /// <param name="colItem">列项</param>
        /// <param name="language">语言</param>
        /// <returns></returns>
        public static String ConvertToLanguageType(List<TypeMapConfig> typeMapConfigList, String dbType, Int32 length)
        {
            if (typeMapConfigList == null || typeMapConfigList.Count <= 0)
            {
                return dbType;
            }

            // 先找类型匹配的项
            var tmpDbType = dbType.ToLower();
            var validList = typeMapConfigList.Where(tmp => tmp.DbType == tmpDbType).ToList();
            if (validList.Count <= 0)
            {
                return dbType;
            }

            // 如果有长度匹配的项，则优先使用长度匹配的项
            var tmpResult = validList.FirstOrDefault(tmp => tmp.Length == length);
            if (tmpResult != null)
            {
                return tmpResult.TargetTypeString;
            }

            return validList[0].TargetTypeString;
        }
    }
}
