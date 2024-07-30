using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    [NotMapped]
    public class OutpatientRegistScheduleVO: OutpatientRegistScheduleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string staffName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string departmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ghzbmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlxmmc { get; set; }
    }
}
