using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 系统部门药品
    /// </summary>
    public class DepartmentMedicineVO
    {
        #region 系统药品xt_yp
        /// <summary>
        /// 药品类别
        /// </summary>
        public string yplb { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        
        /// <summary>
        /// 包装数
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// 药库单位 包装单位
        /// </summary>
        public string ykdw { get; set; }
        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }
        #endregion

        #region 系统药品库存 xt_yp_kcxx
        /// <summary>
        /// 库存Id
        /// </summary>
        public string kcId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int xykc { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public int kykc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xykcstr { get; set; }
        /// <summary>
        /// 可用库存大小单位
        /// </summary>
        public string kykcstr { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int kcsl { get; set; }
        /// <summary>
        /// 转换因子
        /// </summary>
        public int zhyz { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime yxq { get; set; }
        #endregion

        #region 出入库明细xt_yp_crkmx 
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime scrq { get; set; }
        /// <summary>
        /// 进价
        /// </summary>
        public decimal jj { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public decimal pfj { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// 扣率
        /// </summary>
        public decimal kl { get; set; }
        #endregion

        #region 药品属性xt_ypsx
        /// <summary>
        /// 批准文号
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }
        #endregion

    }
}
