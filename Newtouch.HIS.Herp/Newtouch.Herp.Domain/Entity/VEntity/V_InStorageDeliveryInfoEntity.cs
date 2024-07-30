using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 入库配送单信息
    /// </summary>
    public class VInStorageDeliveryInfoEntity
    {

        /// <summary>
        /// 配送单号
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 出入库方式ID
        /// </summary>
        public string crkfsId { get; set; }

        /// <summary>
        /// 出入库方式名称
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
