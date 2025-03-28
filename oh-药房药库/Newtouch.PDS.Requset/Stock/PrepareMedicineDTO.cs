using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 单据内容
    /// </summary>
    public class PrepareMedicineDTO : RequestBase
    {
        /// <summary>
        /// 入库部门
        /// </summary>
        public string OrganizeId { get; set; }
        public string yhgh { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public BYDjInfoDTO yplist { get; set; }
    }

    /// <summary>
    /// 单据内容
    /// </summary>
    public class BYDjInfoDTO
    {
        /// <summary>
        /// 入库部门
        /// </summary>
        public string rkbm { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string bqbm { get; set; }
        public string issavesubmit { get; set; }
        public string shzt { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public List<DjDetailDTO> mx { get; set; }
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
        /// 批发价  部门单位
        /// </summary>
        public string pfj { get; set; }
        /// <summary>
        ///规格
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// 零售价  部门单位
        /// </summary>
        public string lsj { get; set; }

        /// <summary>
        /// 进价  药库单位
        /// </summary>
        public string jj { get; set; }

        /// <summary>
        /// 药库单位批发价
        /// </summary>
        public string ykpfj { get; set; }

        /// <summary>
        /// 药库单位零售价
        /// </summary>
        public string yklsj { get; set; }

        /// <summary>
        /// 总金额  进价总金额
        /// </summary>
        public string zje { get; set; }

        /// <summary>
        /// 数量 部门单位数量 与出库转化因子对应
        /// </summary>
        public string sl { get; set; }

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
        public string kprq { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string scrq { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public string thyy { get; set; }
        /// <summary>
        /// 药库单位批发价
        /// </summary>
        public decimal pfjze { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksbm { get; set; }
        /// <summary>
        /// 药品类别
        /// </summary>
        public string yplb { get; set; }

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
        public string yxq { get; set; }
        /// <summary>
        /// 转化因子
        /// </summary>
        public string zhyz { get; set; }
    }
}
