using System;

namespace Newtouch.HIS.Proxy.Attribute
{
    /// <summary>
    /// 接口编码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InterfaceCodeAttribute : System.Attribute
    {
        /// <summary>
        /// 接口编码
        /// </summary>
        public string InterfaceCode { get; set; }

        public InterfaceCodeAttribute(string interfaceCode)
        {
            this.InterfaceCode = interfaceCode;
        }
    }
}