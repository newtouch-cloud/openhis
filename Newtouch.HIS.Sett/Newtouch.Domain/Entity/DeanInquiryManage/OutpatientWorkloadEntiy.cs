using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientWorkloadEntiy
    {
        /// <summary>
        /// 门诊科室
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// 门诊诊疗人次
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// 处方数量
        /// </summary>
        public int? cfsl { get; set; }
        /// <summary>
        /// 开药处方数量
        /// </summary>
        public int? kycfs { get; set; }
        /// <summary>
        /// 发药处方数量
        /// </summary>
        public int? fycfs { get; set; }
        /// <summary>
        /// 康复处方数量
        /// </summary>
        public int? kfcfs { get; set; }
        public string DepartmentCode { get; set; }
    }

    public class OutpatientWorkloadEntiy_ysgzl
    {
        /// <summary>
        /// 医生
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 门诊诊疗人次
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// 处方数量
        /// </summary>
        public int? cfsl { get; set; }
        /// <summary>
        /// 开药处方数量
        /// </summary>
        public int? kycfs { get; set; }
        /// <summary>
        /// 发药处方数量
        /// </summary>
        public int? fycfs { get; set; }
        /// <summary>
        /// 康复处方数量
        /// </summary>
        public int? kfcfs { get; set; }
    }
}
