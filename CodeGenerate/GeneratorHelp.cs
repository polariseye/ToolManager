using CodeGenerate.T4Template.Golang.Model;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerate
{
    public class GeneratorHelp
    {
        /// <summary>
        /// 按照语言的组生成代码
        /// </summary>
        /// <param name="language"></param>
        /// <param name="groupName"></param>
        public void GenerateGroup(String language, String groupName)
        {
            T4TemplateEngineHost.TableHost host = new T4TemplateEngineHost.TableHost();

            Engine engine = new Engine();
            Model model = new Model();
            model.TransformText();
        }

        public void GenerateClass(String language, String className, String groupName)
        {

        }

        /// <summary>
        /// 生成模版文件
        /// </summary>
        public void GenerateFile()
        {
        }
    }
}
