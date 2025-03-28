using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MRHomePage
{
    public class ButtonEnableVO
    {
        public string Value { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }

        public int? zs { get; set; }
    }
}
