using System;


namespace Newtouch.EMR.Domain.DTO
{
    /// <summary>
    /// 住院患者一览 在病区的病人返回对象集合
    /// </summary>
    public class PatientzbqResponseDto
    {
        public string id { get; set; }
        public string cwmc { get; set; }
        public string xm { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string zyh { get; set; }
        public DateTime ryrq { get; set; }
        public string hljb { get; set; }
        public string wzjb { get; set; }
        public string brzt { get; set; }
        public string brxzmc { get; set; }
        public string ysmc { get; set; }
        public string zdmc { get; set; }
        public string nlshow { get; set; }
        public string gms { get; set; }
    }
    /// <summary>
    /// 住院患者一览 已出区的病人返回对象集合
    /// </summary>
    public class PatientycqResponseDto
    {
        public string id { get; set; }
        public string xm { get; set; }
        public string zyh { get; set; }
        public string cardno { get; set; }
        public string cwmc { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime? cqrq { get; set; }
        public string ysmc { get; set; }
        public string zdmc { get; set; }
        public string nlshow { get; set; }
    }

    public class PatientmyResponseDto : PatientzbqResponseDto
    {
        public string cardno { get; set; }
        public DateTime? cqrq { get; set; }
        public int zybz { get; set; }
        public string blh { get; set; }
        /// <summary>
        /// 文档状态
        /// </summary>
        public string RecordStu { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? CommitTime { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Commitor { get; set; }
    }
}
