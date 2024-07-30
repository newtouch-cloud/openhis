using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.EMR
{
    public class MedicalApplyApproveVo
    {
        /// <summary>
        /// 审批申请号
        /// </summary>
        [Required(ErrorMessage = "ApplyNos不可为空")]
        
        public string ApplyNos { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        [Required(ErrorMessage = "ApprovePerson不可为空")]
        public string ApprovePerson { get; set; }
        /// <summary>
        /// 审批人部门/科室
        /// </summary>
        [Required(ErrorMessage = "ApproveDept不可为空")]
        public string ApproveDept { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        [Required(ErrorMessage = "ApproveStatus不可为空")]
        public int ApproveStatus { get; set; }
    }
}
