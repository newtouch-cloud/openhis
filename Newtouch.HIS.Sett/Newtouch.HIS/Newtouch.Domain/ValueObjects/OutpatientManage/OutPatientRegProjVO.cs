
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 挂号项目
    /// </summary>
    [NotMapped]
    public class OutPatientRegProjVO: OutpatientRegistItemEntity
    {
        /// <summary>
        /// 人员名称
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 农保大类
        /// </summary>
        public string nbdl { get; set; }
    }
}
