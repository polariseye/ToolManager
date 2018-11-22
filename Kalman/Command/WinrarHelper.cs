using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Kalman.Command
{
    /// <summary>
    /// ����Winrar��Dos�����й���rar.exeѹ����ѹ���İ�����
    /// </summary>
    public class WinrarHelper
    {
        string _RarExePath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rarExePath"></param>
        public WinrarHelper(string rarExePath)
        {
            _RarExePath = rarExePath;
        }

        /// <summary>
        /// ����rar.exeѹ���ļ�
        /// </summary>
        /// <param name="srcPath">Դ·�����������ļ������ļ���·��</param>
        /// <param name="targetPath">Ŀ��·����ѹ�����ѹ���ļ�·��</param>
        public void Zip(string srcPath, string targetPath)
        {
            string cmdLine = string.Format("a \"{0}\" \"{1}\" -ep1", targetPath, srcPath);
            Rar(cmdLine);
        }

        /// <summary>
        /// ѹ������ļ�
        /// </summary>
        /// <param name="srcFiles">�ļ�·������</param>
        /// <param name="targetPath"></param>
        public void Zip(string[] srcFiles, string targetPath)
        {
            string tmpLstFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp.lst");
            StringBuilder sb = new StringBuilder();
            foreach (string s in srcFiles)
            {
                sb.Append(s);
                sb.Append("\r\n");
            }
            File.AppendAllText(tmpLstFile, sb.ToString());

            string cmdLine = string.Format("a \"{0}\" \"@{1}\"", targetPath, tmpLstFile);
            Rar(cmdLine);

            File.Delete(tmpLstFile);
        }

        /// <summary>
        /// ����rar.exe��ѹ�ļ�������Ŀ���ļ���
        /// </summary>
        /// <param name="srcPath">Ҫ��ѹ����ѹ���ļ�·��</param>
        /// <param name="targetPath">��ѹĿ���ļ���·��</param>
        public void Unzip(string srcPath, string targetPath)
        {
            string cmdLine = string.Format("x \"{0}\" \"{1}\" -o+", srcPath, targetPath);
            Rar(cmdLine);
        }

        /// <summary>
        /// ����rar.exeѹ����ѹ��ִ���Զ����ѹ����ѹ����
        /// </summary>
        /// <param name="cmdLine">�����У�������rar.exe��·����</param>
        public void Rar(string cmdLine)
        {
            cmdLine = string.Format("\"{0}\" {1}", _RarExePath, cmdLine);
            CmdHelper.Execute(cmdLine);
        }
    }
}
