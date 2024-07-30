using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// 预约挂号信息
    /// </summary>
    public class MzyyghVO
    {
        /// <summary>
        /// 预约挂号ID mz_yygh表主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 门诊类型  1：普通门诊    2：急症     3：专家门诊
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 时段
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 专家工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 专家名称
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// 门诊预约挂号明细
    /// </summary>
    public class MzyyghDetailVo
    {
        /// <summary>
        /// 最大支持预约数
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// 已预约数
        /// </summary>
        public int bespeakedCount { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// 专家工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime regDate { get; set; }
    }

    /// <summary>
    /// 挂号预约排班
    /// </summary>
    public class BespeakRegisterScheduling
    {
        /// <summary>
        /// 日历日期
        /// </summary>
        public DateTime CalendarDate { get; set; }

        public MzyyghDetailVo schedulingData { get; set; }
    }
}
