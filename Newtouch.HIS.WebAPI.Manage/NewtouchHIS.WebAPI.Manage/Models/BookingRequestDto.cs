using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingRequestDto : RequestBase
    {
        public string OrgId { get; set; }
        /// <summary>
        /// 患者唯一标识
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        public string xm { get; set; }
        public DateTime? csrq { get; set; }
        public string xb { get; set; }
        public string dh { get; set; }
        public string dz { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        public string ksmc { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 预约就诊日期
        /// </summary>
        public string outDate { get; set; }
        /// <summary>
        /// 起始就诊日期
        /// </summary>
        public string FromoutDate { get; set; }
        public string ys { get; set; }
        /// <summary>
        /// 门诊类型 普通专家副高等
        /// </summary>
        public string regType { get; set; }
        /// <summary>
        /// mz_ghpb_config  排班id
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// mz_ghpb_schedule 预约既定日程Id
        /// </summary>
        public string scheduId { get; set; }
        /// <summary>
        /// 挂号性质 自费医保等 brxz 等同
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// 预约号
        /// </summary>
        public string bookId { get; set; }
        /// <summary>
        /// 门诊预约状态
        /// </summary>
        public int? yyzt { get; set; }
        /// <summary>
        /// 已支付费用
        /// </summary>
        public string PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string lxdh { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string ksrq { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string jsrq { get; set; }
        /// <summary>
        /// 挂号日期
        /// </summary>
        public string ghrq { get; set; }
        /// <summary>
        /// 挂号来源辅助标识 区分同一来源的不同标记患者
        /// 体检用
        /// </summary>
        public string ghlybz { get; set; }
    }
}
