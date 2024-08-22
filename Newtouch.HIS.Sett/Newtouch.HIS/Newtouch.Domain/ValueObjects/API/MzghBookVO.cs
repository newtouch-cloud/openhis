using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzghBookVO
    {
        public string OrganizeId { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        /// <returns></returns>
        public decimal BookId { get; set; }

        /// <summary>
        /// 门诊排班日程
        /// </summary>
        /// <returns></returns>
        public decimal SchedId { get; set; }

        /// <summary>
        /// brxz 挂号时 自费 医保
        /// </summary>
        /// <returns></returns>
        public string ghxz { get; set; }

        /// <summary>
        /// patid
        /// </summary>
        /// <returns></returns>
        public int? patid { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        /// <returns></returns>
        public string kh { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        /// <returns></returns>
        public string ys { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        /// <returns></returns>
        public DateTime OutDate { get; set; }

        /// <summary>
        /// 挂号类型 门诊 急诊 专家
        /// </summary>
        /// <returns></returns>
        public string RegType { get; set; }

        /// <summary>
        /// 时间段 0无 1全天 2上午 3下午
        /// </summary>
        /// <returns></returns>
        public int Period { get; set; }

        /// <summary>
        /// PeriodStart
        /// </summary>
        /// <returns></returns>
        public string PeriodStart { get; set; }

        /// <summary>
        /// PeriodEnd
        /// </summary>
        /// <returns></returns>
        public string PeriodEnd { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        /// <returns></returns>
        public int? QueueNo { get; set; }

        /// <summary>
        /// 挂号总费用
        /// </summary>
        /// <returns></returns>
        public decimal RegFee { get; set; }

        /// <summary>
        /// 已付费用
        /// </summary>
        /// <returns></returns>
        public decimal? PayFee { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        /// <returns></returns>
        public string PayLsh { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        /// <returns></returns>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        /// <returns></returns>
        public string IsCancel { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        /// <returns></returns>
        public string CancelReason { get; set; }

        /// <summary>
        /// 取消时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        /// <returns></returns>
        public string mzh { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        public string lxdh { get; set; }

        /// <summary>
        /// 预约来源
        /// </summary>
        /// <returns></returns>
        public string AppName { get; set; }

        /// <summary>
        /// 预约来源
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }
    }
}
