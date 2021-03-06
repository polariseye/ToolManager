﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugn.CodeGenerate.Config.NameRule
{
    using ToolManager.Utility;
    using ToolManager.Utility.Extensions;

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

        /// <summary>
        /// 按照规则格式化名字
        /// </summary>
        /// <param name="obj">规则对象</param>
        /// <param name="val">结果名字</param>
        /// <returns></returns>
        public static String FormatName(NameRuleConfig obj, String val)
        {
            if (obj == null || String.IsNullOrEmpty(val))
            {
                return val;
            }

            // 前缀处理
            var prefix = String.Empty;
            if (obj.PrefixList != null && obj.PrefixList.Count > 0)
            {
                foreach (var item in obj.PrefixList)
                {
                    if (val.StartsWith(item.OriginValue))
                    {
                        prefix = item.NewValue;
                        val = val.Substring(item.OriginValue.Length);
                        break;
                    }
                }
            }

            // 后缀处理
            var stuffix = String.Empty;
            if (obj.StuffixList != null && obj.StuffixList.Count > 0)
            {
                foreach (var item in obj.StuffixList)
                {
                    if (val.EndsWith(item.OriginValue))
                    {
                        stuffix = item.NewValue;
                        val = val.Substring(0, val.Length - item.OriginValue.Length);
                        break;
                    }
                }
            }

            // 转换为正式的名字
            if (String.IsNullOrWhiteSpace(obj.Seperator) == false)
            {
                val = StringUtil.ToPascalName(val, obj.Seperator);
            }
            val = $"{prefix.FirstUpper()}{val}{stuffix.FirstUpper()}";

            //// 首字母大小写处理
            if (String.IsNullOrWhiteSpace(val) == false)
            {
                switch (obj.FirstCharHandleType)
                {
                    case FirstCharHandleType.FirstCharLower:
                        val = StringUtil.FirstLower(val);
                        break;
                    case FirstCharHandleType.FirstCharUp:
                        val = StringUtil.FirstUpper(val);
                        break;
                }
            }

            return val;
        }
    }
}
