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
    using ToolManager.Utility;

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
        public static List<AssemblyInfo> GetAllModule()
        {
            lock (lockObj)
            {
                return new List<AssemblyInfo>(moduleInfos);
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
        /// 按照模块名获取模块对象
        /// </summary>
        /// <param name="moduleName">模块名</param>
        /// <returns></returns>
        public static AssemblyInfo GetAssembly(String moduleName, Boolean ifTriggerException = true)
        {
            lock (lockObj)
            {
                var result = moduleInfos.FirstOrDefault(tmp => tmp.ModuleInfo.Name == moduleName);
                if (result == null)
                {
                    if (ifTriggerException)
                    {
                        throw new Exception($"模块不存在 ModuleName:{moduleName}");
                    }

                    return null;
                }

                return result;
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
            // 配置即将创建的应用程序域
            AppDomainSetup domainSetup = new AppDomainSetup();
            domainSetup.ApplicationName = "Module_" + moduleInfo.Name;
            domainSetup.ApplicationBase = Path.GetDirectoryName(moduleInfo.ModulePath);
            //// 子目录（相对形式）在AppDomainSetup中加入外部程序集的所在目录，多个目录用分号间隔
            domainSetup.PrivateBinPath = Path.GetDirectoryName(moduleInfo.ModulePath);
            //// 设置缓存目录
            //domainSetup.CachePath = domainSetup.ApplicationBase;
            //// 获取或设置指示影像复制是打开还是关闭
            //domainSetup.ShadowCopyFiles = "true";
            //// 获取或设置目录的名称，这些目录包含要影像复制的程序集
            //domainSetup.ShadowCopyDirectories = domainSetup.ApplicationBase;
            domainSetup.DisallowBindingRedirects = false;
            domainSetup.DisallowCodeDownload = true;
            //// 配置文件
            var configFilePath = moduleInfo.ModulePath + ".config";
            if (File.Exists(configFilePath))
            {
                domainSetup.ConfigurationFile = configFilePath;
            }
            var moduleDomain = AppDomain.CreateDomain(domainSetup.ApplicationName, AppDomain.CurrentDomain.Evidence, domainSetup);

            // 创建代理类实例
            var proxyType = typeof(DefaultModuleProxy);
            var proxyObj = (IModuleProxy)moduleDomain.CreateInstanceAndUnwrap(proxyType.Assembly.FullName, proxyType.FullName);

            // 加载模块
            var result = proxyObj.LoadModule(moduleInfo.ModulePath, logObj, windowContainer);

            // 创建程序集
            var assemblyInfo = new AssemblyInfo()
            {
                TargetDomain = moduleDomain,
                ProxyObj = proxyObj,
                FormInfoList = new List<FormInfo>(result),
                ModuleInfo = moduleInfo,
            };
            foreach (var item in result)
            {
                item.ModuleName = assemblyInfo.ModuleInfo.Name;
            }

            // 添加到程序集中
            lock (lockObj)
            {
                moduleInfos.Add(assemblyInfo);
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
