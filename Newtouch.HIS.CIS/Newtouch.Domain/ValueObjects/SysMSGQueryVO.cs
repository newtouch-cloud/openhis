using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class SysMSGQueryVO
    {
        public int? msgtypecode { get; set; }
        public string msgcontent { get; set; }
        public string patno { get; set; }
        public string ywlsh { get; set; }
        public string ks { get; set; }
        public string bq { get; set; }
        public int? shzs { get; set; }
        public int? zxzs { get; set; }
    }
}
