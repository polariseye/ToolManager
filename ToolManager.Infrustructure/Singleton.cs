using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Infrustructure
{
    using Autofac;

    /// <summary>
    /// 所有单例
    /// </summary>
    public static class Singleton
    {
        /// <summary>
        /// 创造对象
        /// </summary>
        public static ContainerBuilder Builder { get; set; }

        private static IContainer container;
        private static Object containerLock = new object();
        /// <summary>
        /// 容器对象
        /// </summary>
        public static IContainer Container
        {
            get
            {
                if (container != null)
                {
                    return container;
                }

                lock (containerLock)
                {
                    if (container != null)
                    {
                        return container;
                    }

                    container = Builder.Build();
                }

                return container;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        static Singleton()
        {
            Builder = new ContainerBuilder();
        }
    }
}
