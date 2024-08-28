using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicineReDetailVO
    {
        /// <summary>
        /// 申领单ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// 可用库存，带单位
        /// </summary>
        public string kc { get; set; }

        /// <summary>
        /// 可用库存（最小单位）
        /// </summary>
        public int kcsl { get; set; }
        public string Ypdm { get; set; }
        public string sldmxId { get; set; }

        public string sldh { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }
        public string py { get; set; }
        public string Ph { get; set; }
        public string pc { get; set; }
        public DateTime Yxq { get; set; }
        public string ycmc { get; set; }
        public string bzdw { get; set; }
        public decimal Pfj { get; set; }
        public decimal Lsj { get; set; }

        /// <summary>
        /// 申领数量，带单位
        /// </summary>
        public string Sl { get; set; }
        public decimal Pjze { get; set; }
        public decimal lsje { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 申领数量
        /// </summary>
        public int Slsl { get; set; }

        /// <summary>
        /// 发药数量（申领数量-已发数量， 最小单位）
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// 已发数量（最小单位）
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// 已发数量 带单位
        /// </summary>
        public string yfslStr { get; set; }
    }
}
