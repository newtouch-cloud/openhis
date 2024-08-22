using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// 成本信息
    /// </summary>
    public class jgssCostVO
    {
        public string Id { get; set; }
        public string cblb { get; set; }
        public string kmcode { get; set; }
        public string lbmc { get; set; }
        public string kmmc { get; set; }
        public decimal je { get; set; }
    }

    /// <summary>
    /// 机构实收andGRS实收
    /// </summary>
    public class jgssandgrsssVO {
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
    }
}
