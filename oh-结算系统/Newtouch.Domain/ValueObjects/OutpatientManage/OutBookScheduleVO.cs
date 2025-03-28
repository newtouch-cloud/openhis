using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    [NotMapped]
    public class OutBookScheduleVO : OutBookScheduleEntity
    {
        /// <summary>
        /// 挂号项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 诊疗项目名称
        /// </summary>
        public string zlxmmc { get; set; }
    }
}
