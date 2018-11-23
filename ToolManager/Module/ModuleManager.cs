﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Module
{
    using System.IO;
    using ToolManager.Infrustructure;

    public class ModuleManager
    {
        /// <summary>
        /// 模块列表
        /// </summary>
        private static List<AssemblyInfo> moduleInfos = new List<AssemblyInfo>();

        /// <summary>
        /// 锁对象
        /// </summary>
        private static Object lockObj = new Object();

        /// <summary>
        /// 本地程序集路径
        /// </summary>
        private const string ASSEMBLY_LOCAL_PATH = "Module/";

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(IOutput logObj, IWindowContainer windowContainer)
        {
            var dal = new ModuleInfoDAL();
            var moduleList = dal.FindAll();

            foreach (var item in moduleList)
            {
                try
                {
                    LoadModule(item, logObj, windowContainer);
                }
                catch (Exception e1)
                {
                    logObj.PrintLine($"模块加载出错 ModuleName:{item.Name} ModulePath:{item.ModulePath} Message:{e1.Message}");
                }
            }
        }

        /// <summary>
        /// 注册一个新模块
        /// </summary>
        /// <param name="name">模块名</param>
        /// <param name="filePath">需要注册的文件名</param>
        /// <param name="description">描述信息</param>
        /// <param name="logObj">日志对象</param>
        /// <param name="windowContainer">窗口容器</param>
        /// <returns></returns>
        public static List<FormInfo> Register(String name, String filePath, String description, IOutput logObj, IWindowContainer windowContainer)
        {
            var destPath = SaveToLocal(filePath);

            try
            {
                var moduleInfo = new ModuleInfo() { Name = name, ModulePath = destPath, Description = description };
                var result = LoadModule(moduleInfo, logObj, windowContainer);

                // 添加到数据库
                var dal = new ModuleInfoDAL();
                dal.Insert(moduleInfo);

                return result;
            }
            catch (Exception e1)
            {
                File.Delete(destPath);
                throw e1;
            }
        }

        /// <summary>
        /// 加载一个模块
        /// </summary>
        /// <param name="moduleInfo">模块信息</param>
        /// <returns></returns>
        private static List<FormInfo> LoadModule(ModuleInfo moduleInfo, IOutput logObj, IWindowContainer windowContainer)
        {
            var result = new List<FormInfo>();

            // 寻找模块中的所有窗口和模块初始化类型
            var moduleInterfaceType = typeof(IModule);
            Type moduleInitType = null;
            var assemblyObj = Assembly.LoadFile(moduleInfo.ModulePath);
            foreach (var typeItem in assemblyObj.ExportedTypes)
            {
                // 找到所有的窗口
                var attrItem = typeItem.GetCustomAttribute<FormAttribute>();
                if (attrItem != null)
                {
                    result.Add(new FormInfo() { AttributeInfo = attrItem, FormType = typeItem });
                }

                // 寻找模块初始化类
                if (typeItem.IsSubclassOf(moduleInterfaceType))
                {
                    moduleInitType = typeItem;
                }
            }

            if (moduleInitType == null && result.Count <= 0)
            {
                throw new Exception("这不是一个有效的插件:没有IModule的实现，也没有任何窗口");
            }

            // 调用初始化函数
            if (moduleInitType == null)
            {
                var initObj = (IModule)Activator.CreateInstance(moduleInitType);
                initObj.Init(logObj, windowContainer);
            }

            // 添加到程序集中
            lock (lockObj)
            {
                moduleInfos.Add(new AssemblyInfo()
                {
                    AssemblyObj = assemblyObj,
                    FormInfoList = new List<FormInfo>(result),
                    ModuleInfo = moduleInfo,
                });
            }

            return result;
        }

        /// <summary>
        /// 获取所有窗口
        /// </summary>
        /// <returns></returns>
        public static List<FormInfo> GetAllFormInfos()
        {
            var result = new List<FormInfo>();

            lock (lockObj)
            {
                foreach (var item in moduleInfos)
                {
                    result.AddRange(item.FormInfoList);
                }
            }

            return result;
        }

        /// <summary>
        /// 检查是否存在对应的模块（避免重复加载）
        /// </summary>
        /// <param name="filePath">模块路径</param>
        /// <returns></returns>
        public static Boolean ExistPath(String filePath)
        {
            var tmpPath = Path.Combine(ASSEMBLY_LOCAL_PATH, Path.GetFileName(filePath));

            lock (lockObj)
            {
                return moduleInfos.Any(tmp => tmp.ModuleInfo.ModulePath == tmpPath);
            }
        }

        /// <summary>
        /// 检查是否存在对应的模块（避免重复加载）
        /// </summary>
        /// <param name="filePath">模块路径</param>
        /// <returns></returns>
        public static Boolean ExistName(String moduleName)
        {
            lock (lockObj)
            {
                return moduleInfos.Any(tmp => tmp.ModuleInfo.Name == moduleName);
            }
        }

        /// <summary>
        /// 把指定文件拷贝到指定位置
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static String SaveToLocal(String filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var destName = Path.Combine(ASSEMBLY_LOCAL_PATH, fileName);
            File.Copy(filePath, destName);

            return destName;
        }
    }
}
