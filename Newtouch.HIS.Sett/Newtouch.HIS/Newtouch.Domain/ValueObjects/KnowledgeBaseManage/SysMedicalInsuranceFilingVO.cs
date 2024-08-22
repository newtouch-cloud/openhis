using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.KnowledgeBaseManage
{
    [NotMapped]
    public class SysMedicalInsuranceFilingVO: SysMedicalInsuranceFilingEntity
    {
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl
        {
            get
            {
                if (csny.HasValue)
                {
                    return (DateTime.Now.Year - csny.Value.Year) + 1;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }
    }
}
