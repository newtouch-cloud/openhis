using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO
{
    public class DiagnoseDTO
    {
        public string zdCode { get; set; }
        public string zdName { get; set; }
        public string orgId { get; set; }
        public string icd10 { get; set; }
        public string zdlx { get; set; }
    }

    public class DiagnoseDocDTO : DiagnoseDTO
    {
        /// <summary>
        /// 诊断分类 1	初步诊断 2	修正诊断 3	确定诊断 4	补充诊断
        /// </summary>
        public string zdfl { get; set; }
        public int? px { get; set; }
    }
}
