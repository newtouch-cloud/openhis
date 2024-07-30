using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class PrescriptionChargeDto
    {
        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }
    }
}
