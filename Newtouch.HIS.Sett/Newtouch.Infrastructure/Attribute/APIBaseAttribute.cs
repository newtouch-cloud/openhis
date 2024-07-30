using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure.Attributes
{
    /// <summary>
    /// 对应Xml节点 节点名称（不加该特性时 默认取属性名/类名）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class APIBaseAttribute : Attribute
    {
        public APIBaseAttribute(string name) {
            this.Name = name;
        }
        public string Name { get; set; }
        public string ErrorMsg { get; set; }

        
    }
}
