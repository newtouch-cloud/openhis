using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// 单据内容
    /// </summary>
    public class DjInfoDTO
    {
        /// <summary>
        /// 单据类型
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// 发药方式
        /// </summary>
        public string fyfs { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string crkId { get; set; }

		/// <summary>
		/// 审核状态
		/// </summary>
		public string shzt { get; set; }
	}

    /// <summary>
    /// 单据明细
    /// </summary>
    public class DjDetailDTO
    {
        /// <summary>
        /// 申领单明细id
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

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
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 批发价  部门单位
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// 零售价  部门单位
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 进价  药库单位
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// 药库单位批发价
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// 药库单位零售价
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 总金额  进价总金额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 数量 部门单位数量 与出库转化因子对应
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 出库部门库存  最小单位
        /// </summary>
        public int? ckbmkc { get; set; }

        /// <summary>
        /// 出库单位（单位名称）
        /// </summary>
        public string ckdw { get; set; }

        /// <summary>
        /// 入库部门库存  最小单位
        /// </summary>
        public int? rkbmkc { get; set; }

        /// <summary>
        /// 入库部门转化因子
        /// </summary>
        public int? rkzhyz { get; set; }

        /// <summary>
        /// 入库单位（单位名称）
        /// </summary>
        public string rkdw { get; set; }

        /// <summary>
        /// 出库部门转化因子
        /// </summary>
        public int? ckzhyz { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 扣率
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? kprq { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// 药库单位批发价
        /// </summary>
        public decimal pfjze { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm {get; set;}

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int sfcl { get; set;}
    }
}
