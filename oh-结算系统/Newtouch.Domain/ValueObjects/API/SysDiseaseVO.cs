using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDiseaseVO
    {
        /// <summary>
        /// 疾病ID
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// ICD10
        /// </summary>
        public string icd10 { get; set; }
    }
}
