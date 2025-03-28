using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects.SysManage
{
    /// <summary>
    /// 系统字典
    /// </summary>
    public class SysItemExtendVO
    {
        public string Type { get; set; }
        public List<SysItemDetailVO>? Items { get;set; }
    }

    public class SysItemDetailVO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? zt { get; set; }
    }
}
