using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Module
{
    using System.IO;
    using ToolManager.Infrustructure;

    /// <summary>
    /// 模块管理类
    /// </summary>
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
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public static List<AssemblyInfo> GetAllAssemblyInfo()
        {
            lock (lockObj)
            {
                return new List<AssemblyInfo>(moduleInfos);
            }
        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public static List<ModuleInfo> GetAllModule()
        {
            lock (lockObj)
            {
                return new List<ModuleInfo>(moduleInfos.Select(tmp => tmp.ModuleInfo));
            }
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
            try
            {
                if (ExistName(name))
                {
                    throw new Exception($"存在重复的模块名:{name}");
                }

                if (ExistPath(filePath))
                {
                    throw new Exception($"存在重复的模块加载:{filePath}");
                }

                var moduleInfo = new ModuleInfo() { Name = name, ModulePath = filePath, Description = description };
                var result = LoadModule(moduleInfo, logObj, windowContainer);

                // 添加到数据库
                var dal = new ModuleInfoDAL();
                dal.Insert(moduleInfo);

                return result;
            }
            catch (Exception e1)
            {
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
            var assemblyObj = Assembly.LoadFrom(moduleInfo.ModulePath);
            foreach (var typeItem in assemblyObj.ExportedTypes)
            {
                // 找到所有的窗口
                var attrItem = typeItem.GetCustomAttribute<FormAttribute>();
                if (attrItem != null)
                {
                    result.Add(new FormInfo() { AttributeInfo = attrItem, FormType = typeItem });
                }

                // 寻找模块初始化类
                if (moduleInitType == null)
                {
                    var tmpBaseModule = typeItem.FindInterfaces((tmp, a) => tmp == moduleInterfaceType, null);
                    if (tmpBaseModule != null && tmpBaseModule.Length > 0)
                    {
                        moduleInitType = typeItem;
                    }
                }
            }

            if (moduleInitType == null && result.Count <= 0)
            {
                throw new Exception("这不是一个有效的插件:没有IModule的实现，也没有任何窗口");
            }

            // 调用初始化函数
            if (moduleInitType != null)
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
        /// 加载指定目录的所有dll
        /// (因为有些插件内部是动态加载dll的，而对应的工作目录不对，导致有些功能运行失败)
        /// </summary>
        /// <param name="filePath">文件目录</param>
        /// <returns></returns>
        private static Assembly LoadDirDll(String filePath, IOutput logObj)
        {
            var targetFileName = Path.GetFileName(filePath).ToLower();
            var targetDir = Path.GetDirectoryName(filePath);
            var fls = Directory.GetFiles(targetDir, "*.dll");
            Assembly result = null;
            foreach (var item in fls)
            {
                try
                {
                    var tmpFileName = Path.GetFileName(item).ToLower();
                    if (targetFileName == tmpFileName)
                    {
                        // 如果是当前程序集，则加载相应直接依赖项
                        result = Assembly.LoadFrom(item);
                    }
                    else
                    {
                        // 如果不是当前主要程序集，则只加载这个DLL
                        Assembly.LoadFile(item);
                    }
                }
                catch (Exception e1)
                {
                    logObj.PrintLine($"dll加载失败: {e1.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="deleteModuleList">待删除的模块列表</param>
        public static void DeleteModule(List<AssemblyInfo> deleteModuleList)
        {
            lock (lockObj)
            {
                moduleInfos.RemoveAll(tmp1 => deleteModuleList.Any(tmp2 => tmp2 == tmp1));
                foreach (var item in deleteModuleList)
                {
                    var dal = new ModuleInfoDAL();
                    dal.Delete(item.ModuleInfo.Name);
                }
            }
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
