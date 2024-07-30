using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysUsageVO
    {
        //
        // 摘要:
        //     Id
        public int yfId { get; set; }
        //
        // 摘要:
        //     用法编码
        public string yfCode { get; set; }
        //
        // 摘要:
        //     名称
        public string yfmc { get; set; }
        //
        // 摘要:
        //     有效标志
        public string zt { get; set; }
        public string yplx { get; set; }
    }
}
