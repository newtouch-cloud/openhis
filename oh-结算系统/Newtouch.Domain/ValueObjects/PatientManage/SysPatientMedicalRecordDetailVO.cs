using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 病历明细
    /// </summary>
    public class SysPatientMedicalRecordDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int attachType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string attachName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string attachPath { get; set; }

        /// <summary>
        /// 网络访问路径
        /// </summary>
        public string attachUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}
