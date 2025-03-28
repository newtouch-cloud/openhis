
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class RegScheduleVO: OutpatientRegistScheduleEntity
    {
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// 科室拼音
        /// </summary>
        public string kspy { get; set; }
        /// <summary>
        /// 人员拼音
        /// </summary>
        public string rypy { get; set; }
        /// <summary>
        /// 收费项目拼音
        /// </summary>
        public string sfxmpy { get; set; }
        /// <summary>
        /// xt_ghzb挂号专病
        /// </summary>
        public new string ghzb { get; set; }
        /// <summary>
        /// 挂号专病名称
        /// </summary>
        public string ghzbmc { get; set; }
        /// <summary>
        /// 挂号专病拼音
        /// </summary>
        public string ghzbpy { get; set; }

        /// <summary>
        /// 诊疗项目名称
        /// </summary>
        public string zlxmmc { get; set; }

    }

	
}
