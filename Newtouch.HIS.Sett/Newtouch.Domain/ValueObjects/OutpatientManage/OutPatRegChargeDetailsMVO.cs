using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 含退费显示的收费明细
    /// </summary>
    public class OutPatRegChargeDetailsMVO
    {
        #region 挂号收费查询明细(含退费查询)
        public int jsmxnm { get; set; }
        /// <summary>
        /// from xt_sfdl 收费大类
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 处方号  在挂号收费查询中给空值
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 收费项目 from mz_xm
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 药品名称  在挂号收费查询指的是收费项目名称
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 单价 from mz_xm
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// 收费数量
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// 开立数量
        /// </summary>
        public decimal klsl { get; set; }
        /// <summary>
        /// 开立金额
        /// </summary>
        public decimal klje { get; set; }
        /// <summary>
        /// 已退数量
        /// </summary>
        public decimal ytsl { get; set; }
        /// <summary>
        /// 已退金额
        /// </summary>
        public decimal ytje { get; set; }
        #endregion
    }
}
