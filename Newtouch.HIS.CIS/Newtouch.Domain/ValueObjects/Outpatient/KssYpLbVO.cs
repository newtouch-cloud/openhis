using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 抗生素类别
    /// </summary>
    public class KssYpLbVO
    {
        /// <summary>
        /// 抗生素类别id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 抗生素类别名称
        /// </summary>
        public string typeName { get; set; }
    }
}
