using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 盘点信息
    /// </summary>
    public class InventoryInfoVO
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 药厂名称=cd
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 大类代码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 盘点明细ID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// 理论数量
        /// </summary>
        public string llsl { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// 药库数量
        /// </summary>
        public int yksl { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// 转换因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 部门实际数 （与最小单位实际数组合使用）
        /// </summary>
        public int deptSjsl { get; set; }

        /// <summary>
        /// 最小单位实际数 （与部门实际数组合使用）
        /// </summary>
        public int minSjsl { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 理论批发金额
        /// </summary>
        public decimal? llpfje { get; set; }

        /// <summary>
        /// 理论零售金额
        /// </summary>
        public decimal? lllsje { get; set; }

        /// <summary>
        /// 实际批发金额
        /// </summary>
        public decimal? sjpfje { get; set; }

        /// <summary>
        /// 实际零售金额
        /// </summary>
        public decimal? sjlsje { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        public string pdsj { get; set; }

        /// <summary>
        /// 账册序号
        /// </summary>
        public string zcxh { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 理论最小包装数量
        /// </summary>
        public string llsl_zxbz { get; set; }

        /// <summary>
        /// 实际包装数量
        /// </summary>
        public string sjslstr { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 零售差异金额
        /// </summary>
        public decimal? pdlsjcy { get; set; }
        /// <summary>
        /// 批发差异金额
        /// </summary>
        public decimal? pdpfjcy { get; set; }
        /// <summary>
        /// 盘点数差异 = 实际数-理论数
        /// </summary>
        public string pdscy { get; set; }

		/// <summary>
		/// 进价
		/// </summary>
		public decimal jj { get; set; }

        /// <summary>
        /// 数量盈亏 = 实际数 - 理论数
        /// </summary>
        //public int? slyk { get; set; }
        /// <summary>
        /// 实盘金额 = 实际数 * 零售金额(实)
        /// </summary>
        public decimal? spje { get; set; }
        /// <summary>
        /// 盘前金额 = 理论数 * 零售金额(理)
        /// </summary>
        public decimal? pqje { get; set; }
        /// <summary>
        /// 盈亏金额 = 实盘金额 - 盘前金额
        /// </summary>
        public decimal? ykje { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int? sfcl { get; set; }

    }
}
