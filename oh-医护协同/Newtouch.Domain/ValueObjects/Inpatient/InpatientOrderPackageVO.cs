using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    [NotMapped]
    public class InpatientOrderPackageVO : InpatientOrderPackageDetailEntity
    {
        public string yfmc { get; set; }
        public string pcmc { get; set; }
        public string yslb { get; set; }
        public string yszsval { get; set; }
        public string yszs { get; set; }
        public string nlmd { get; set; }
        public string yslbdm { get; set; }
        
        /// <summary>
        /// 医嘱类别
        /// </summary>
        public string yzlb { get; set; }
    }
}
