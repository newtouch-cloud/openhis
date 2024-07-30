using Newtouch.HIS.API.Common;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 根据药品代码查询库存
    /// </summary>
    public class StockQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药品信息集合
        /// </summary>
        public List<DrugInfo> yplist { get; set; }
    }

    /// <summary>
    /// 药品信息
    /// </summary>
    public class DrugInfo
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }
    }
}
