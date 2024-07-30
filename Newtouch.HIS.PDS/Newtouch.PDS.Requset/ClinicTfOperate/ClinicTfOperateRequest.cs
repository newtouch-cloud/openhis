using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.PDS.Requset.ClinicTfOperate
{
    public class ClinicTfOperateRequest : RequestBase
    {
        /// <summary>
        /// 处方列表
        /// </summary>
        public List<CfDetial> cfhs { get; set; }
    }

    /// <summary>
    /// 退费处方信息
    /// </summary>
    public class CfDetial
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
