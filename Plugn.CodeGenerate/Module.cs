﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate
{
    using System.Configuration;
    using System.Reflection;
    using ToolManager.Infrustructure;

    /// <summary>
    /// 模块操作
    /// </summary>
    public class Module : IModule
    {
        public void Init(IOutput logObj, IWindowContainer windowContainer)
        {
            //string configPath = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString() + ".config";
            //ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
            //{
            //    ExeConfigFilename = configPath
            //}, ConfigurationUserLevel.None);
        }
    }
}
