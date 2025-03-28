using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// 门诊预约查询返回报文
    /// </summary>
    public class MzBespeakRegisterQueryResponseDTO
    {
        /// <summary>
        /// 门诊预约挂号ID
        /// </summary>
        public string mzyyghId { get; set; }
        
        /// <summary>
        /// 证件类型
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        
        /// <summary>
        /// 门诊住院标志 1：普通门诊  2：急诊   3：专家门诊
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// 挂号日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 最大预约数
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// 赴约时间
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// 现场处理挂号人员
        /// </summary>
        public string arrivalOpereater { get; set; }

        /// <summary>
        /// 预约操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 预约操作人员
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
