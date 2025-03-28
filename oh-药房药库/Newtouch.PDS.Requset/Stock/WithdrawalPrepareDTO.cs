using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 单据内容
    /// </summary>
    public class WithdrawalPrepareDTO : RequestBase
    {
        public string OrganizeId { get; set; }
        public string Djh { get; set; }
        public string yhgh { get; set; }
        public string shzt { get; set; }
    }
}
