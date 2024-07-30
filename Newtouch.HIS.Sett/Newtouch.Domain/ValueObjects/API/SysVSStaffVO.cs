using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysVSStaffVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string departmentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string departmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string staffName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DutyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dutyName { get; set; }
    }

    public class SysDoctorVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DutyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dutyName { get; set; }
    }
}
