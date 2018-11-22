using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

namespace Kalman
{
    /// <summary>
    /// ���û���
    /// </summary>
    public abstract class ConfigBase
    {
        /// <summary>
        /// ��ȡ���ԣ�string���ͣ�
        /// </summary>
        public string GetStringAttribute(XmlNode node, string key, string defaultValue)
        {
            XmlAttributeCollection attributes = node.Attributes;
            if (attributes[key] != null && !string.IsNullOrEmpty(attributes[key].Value))
                return attributes[key].Value;
            return defaultValue;
        }

        /// <summary>
        /// ��ȡ���ԣ�string���ͣ���Ĭ��ֵstring.Empty
        /// </summary>
        public string GetStringAttribute(XmlNode node, string key)
        {
            return GetStringAttribute(node,key,string.Empty);
        }

        /// <summary>
        /// ��ȡ���ԣ�int���ͣ�
        /// </summary>
        public int GetIntAttribute(XmlNode node, string key, int defaultValue)
        {
            int val = defaultValue;
            XmlAttributeCollection attributes = node.Attributes;

            if (attributes[key] != null && !string.IsNullOrEmpty(attributes[key].Value))
            {
                int.TryParse(attributes[key].Value, out val);
            }
            return val;
        }

        /// <summary>
        /// ��ȡ���ԣ�bool���ͣ�
        /// </summary>
        public bool GetBoolAttribute(XmlNode node, string key, bool defaultValue)
        {
            bool val = defaultValue;
            XmlAttributeCollection attributes = node.Attributes;

            if (attributes[key] != null && !string.IsNullOrEmpty(attributes[key].Value))
            {
                bool.TryParse(attributes[key].Value, out val);
            }
            return val;
        }

        /// <summary>
        /// �����ýڵ��ȡ����ֵ
        /// </summary>
        /// <param name="isMandatory">�Ƿ�Ϊǿ������</param>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public string GetAttribute(XmlNode node, string key, bool isMandatory)
        {
            string errMsg = string.Format("���ýڵ�[{0}]������������[{1}]", node.Name, key);

            if (node.Attributes == null)
                throw new ConfigurationErrorsException(errMsg);

            XmlAttribute attribute = node.Attributes[key];
            if (attribute == null && isMandatory)
                throw new ConfigurationErrorsException(errMsg);

            return attribute == null ? null : attribute.InnerText;
        }

        /// <summary>
        /// �����ýڵ��ȡ����ֵ
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">�����ýڵ��ֵΪkey������û�����ã��򷵻�һ��Ĭ��ֵ</param>
        /// <returns>���ýڵ��ֵΪkey������ֵ</returns>
        public string GetAttribute(XmlNode node, string key, string defaultValue)
        {
            if (node.Attributes.Count == 0)return defaultValue;

            XmlAttribute attribute = node.Attributes[key];
            if (attribute == null)return defaultValue;

            return attribute.InnerText;
        }

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, T> LoadModules<T>(XmlNode node)
        {
            Dictionary<string, T> modules = new Dictionary<string, T>();

            if (node != null)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        switch (n.Name)
                        {
                            case "clear":
                                modules.Clear();
                                break;
                            case "remove":
                                XmlAttribute removeNameAtt = n.Attributes["name"];
                                string removeName = removeNameAtt == null ? null : removeNameAtt.Value;

                                if (!string.IsNullOrEmpty(removeName) && modules.ContainsKey(removeName))
                                {
                                    modules.Remove(removeName);
                                }

                                break;
                            case "add":

                                XmlAttribute en = n.Attributes["enabled"];
                                if (en != null && en.Value == "false")
                                    continue;

                                XmlAttribute nameAtt = n.Attributes["name"];
                                XmlAttribute typeAtt = n.Attributes["type"];
                                string name = nameAtt == null ? null : nameAtt.Value;
                                string itype = typeAtt == null ? null : typeAtt.Value;

                                if (string.IsNullOrEmpty(name))
                                {
                                    continue;
                                }

                                if (string.IsNullOrEmpty(itype))
                                {
                                    continue;
                                }

                                Type type = Type.GetType(itype);

                                if (type == null)
                                {
                                    continue;
                                }

                                T mod = default(T);

                                try
                                {
                                    mod = (T)Activator.CreateInstance(type);
                                }
                                catch { 
                                    //todo: log
                                }

                                if (mod == null)
                                {
                                    continue;
                                }

                                modules.Add(name, mod);
                                break;

                        }
                    }
                }
            }
            return modules;
        }

        /// <summary>
        /// �����ýڵ�ת����NameValueCollection����
        /// </summary>
        /// <param name="node">���ýڵ�</param>
        /// <returns></returns>
        protected NameValueCollection XmlNode2NameValueCollection(XmlNode node)
        {
            NameValueCollection nvc = new NameValueCollection();

            foreach (XmlNode item in node)
            {
                nvc.Add(item.Name, item.InnerXml);
            }

            return nvc;
        }
    }
}
