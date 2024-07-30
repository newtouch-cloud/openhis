using System;

namespace Newtouch.HIS.Proxy.Attribute
{
    /// <summary>
    /// 重命名xml节点名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class XmlNodeRenameAttribute : System.Attribute
    {
        public string NewName { get; set; }

        public XmlNodeRenameAttribute(string newName)
        {
            NewName = newName;
        }
    }
}