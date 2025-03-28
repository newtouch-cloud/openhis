using Newtouch.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.ValueObjects
{
    [NotMapped]
    public class SysDoctorRemarkVO: SysDoctorRemarkEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
    }
}
