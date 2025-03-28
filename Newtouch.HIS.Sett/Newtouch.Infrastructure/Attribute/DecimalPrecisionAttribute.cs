using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DecimalPrecisionAttribute:Attribute
    {
        private byte _precision;

        /// <summary>
        /// 精确度。
        /// </summary>
        public byte Precision
        {

            get { return _precision; }
            set { _precision = value; }
        }
        
        private byte _scale;

        /// <summary>
        /// 小数保留位数。
        /// </summary>
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        
        /// <summary>
        /// 根据指定的精确度与小数保留位数，初始化 DecimalPrecisionAttribute 的实例。
        /// </summary>
        /// <param name="precision">精确度。</param>
        /// <param name="scale">小数保留位数。</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            this.Precision = precision;
            this.Scale = scale;
        }
    }
}
