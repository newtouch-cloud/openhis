using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class SysObjectActionRecordVO
    {
        public string objectType { get; set; }
        public string objectKey { get; set; }
        public string actionType { get; set; }
        public DateTime? zhczsj { get; set; }
        public string result { get; set; }
    }
}
