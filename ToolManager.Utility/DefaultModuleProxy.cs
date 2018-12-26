using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Utility
{
    using System.Windows.Forms;
    using ToolManager.Infrustructure;
    using Autofac;
    using System.Threading;
    using System.Reflection;

    /// <summary>
    /// 默认的窗口代理对象
    /// </summary>
    public class DefaultModuleProxy : MarshalByRefObject, IModuleProxy
    {
        /// <summary>
        /// 加载所有模块
        /// </summary>
        /// <param name="assemblyFilePath">程序集路径</param>
        /// <param name="logObj">日志对象</param>
        /// <param name="windowContainer">窗口容器对象</param>
        /// <returns></returns>
        public List<FormInfo> LoadModule(String assemblyFilePath, IOutput logObj, IWindowContainer windowContainer)
        {
            List<FormInfo> result = new List<FormInfo>();
            var assemblyObj = Assembly.LoadFile(assemblyFilePath);
            Type moduleInitType = null;
            var moduleInterfaceType = typeof(IModule);
            foreach (var typeItem in assemblyObj.ExportedTypes)
            {
                // 找到所有的窗口
                var attrItem = typeItem.GetCustomAttribute<FormAttribute>();
                if (attrItem != null)
                {
                    result.Add(new FormInfo() { AttributeInfo = attrItem, TypeString = typeItem.ToString() });
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

            return result;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="typeString"></param>
        public void Show(String assemblyFilePath, String typeString)
        {
            var assemblyObj = Assembly.LoadFrom(assemblyFilePath);
            var typeObj = assemblyObj.GetType(typeString);
            var formObj = (BaseForm)Activator.CreateInstance(typeObj);
            var container = Singleton.Container.Resolve<IWindowContainer>();
            formObj.Show(container.GetMainView());
        }
    }
}
