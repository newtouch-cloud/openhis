using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 历史病历
    /// </summary>
    public class SysPatientMedicalRecordDTO
    {
        /// <summary>
        /// 唯一主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string rq { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 创建时间（上传时间）
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public IList<SysPatientMedicalRecordDetailVO> Details { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
