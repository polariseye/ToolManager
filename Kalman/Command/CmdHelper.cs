using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Kalman.Command
{
    /// <summary>
    /// �����а�����
    /// </summary>
    public class CmdHelper
    {
        /// <summary>
        /// ����һ���������ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void CreateBat(string fileName,string content)
        {
            if (System.IO.File.Exists(fileName))
                System.IO.File.Delete(fileName);
            File.WriteAllText(fileName, content, Encoding.Default);   //��s�ַ���������д��v_filepathָ����bat�ļ��С�
        }

        /// <summary>
        /// /����cmd.exeִ��һ������
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="workingDirectory"></param>
        /// <returns></returns>
        public static string Execute(string cmdText,string workingDirectory = null)
        {
            return Execute(new string[] { cmdText }, workingDirectory);
        }

        /// <summary>
        /// ����cmd.exeִ�ж�������
        /// </summary>
        /// <param name="cmdTexts"></param>
        /// <param name="workingDirectory"></param>
        /// <returns></returns>
        public static string Execute(string[] cmdTexts,string workingDirectory = null)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            if (!string.IsNullOrEmpty(workingDirectory))
            {
                p.StartInfo.WorkingDirectory = workingDirectory;
            }
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string output = null;
            try
            {
                p.Start();

                foreach (string item in cmdTexts)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        p.StandardInput.WriteLine(item);
                    }
                }
                p.StandardInput.WriteLine("exit");
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit(20 * 1000);
                var ExitCode = p.ExitCode;
               
                p.Close();
            }
            catch (Exception e)
            {
                output = e.Message;
            }

            return output;
        }

        /// <summary>
        /// �����ⲿWindowsӦ�ó������س������
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <returns></returns>
        public static bool RunApp(string appName)
        {
            return RunApp(appName, ProcessWindowStyle.Hidden);
        }
        
        /// <summary>
        /// �����ⲿӦ�ó���
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="style">Ӧ�ó�������ʱ������ʾ��ʽ</param>
        /// <returns></returns>
        public static bool RunApp(string appName, ProcessWindowStyle style)
        {
            return RunApp(appName, null, style);
        }

        /// <summary>
        /// �����ⲿӦ�ó������س������
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static bool RunApp(string appName, string args)
        {
            return RunApp(appName, args, ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// �����ⲿӦ�ó���
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="style"></param>
        /// <param name="workingDirectory"></param>
        /// <returns></returns>
        public static bool RunApp(string appName, ProcessWindowStyle style, string workingDirectory)
        {
            return RunApp(appName, null, style, workingDirectory);
        }

        /// <summary>
        /// �����ⲿӦ�ó���
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="args">����</param>
        /// <param name="style">Ӧ�ó�������ʱ������ʾ��ʽ</param>
        /// <param name="workingDirectory">Ӧ�ó�������ʱ������ʾ��ʽ</param>
        /// <returns></returns>
        public static bool RunApp(string appName, string args, ProcessWindowStyle style, string workingDirectory = null)
        {
            bool flag = false;

            Process p = new Process();
            p.StartInfo.FileName = appName;//exe,bat and so on
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = args;
            if (!string.IsNullOrEmpty(workingDirectory))
            {
                p.StartInfo.WorkingDirectory = workingDirectory;
            }
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
                flag = true;
            }
            catch
            {
            }
            return flag;
        }
    }
}
