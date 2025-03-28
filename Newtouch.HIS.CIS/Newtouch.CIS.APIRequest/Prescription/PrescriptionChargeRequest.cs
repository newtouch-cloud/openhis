using Newtouch.CIS.APIRequest.Dto;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Prescription
{
    public class PrescriptionChargeRequest : RequestBase
    {
        /// <summary>
        /// 处方号List
        /// </summary>
        public List<PrescriptionChargeDto> cfList { get; set; }

        /// <summary>
        /// 收费标志
        /// </summary>
        [Required]
        public bool sfbz { get; set; }

        ///// <summary>
        ///// 操作时间
        ///// </summary>
        //[Required]
        //public DateTime OperateTime { get; set; }
        public string OrganizeId { get; set; } = null;
    }

    public class PrescriptionReturnChargeRequest : RequestBase
    {
        /// <summary>
        /// 处方号List
        /// </summary>
        public List<string> cfList { get; set; }

        /// <summary>
        /// 收费标志
        /// </summary>
        [Required]
        public bool tbz { get; set; }
    }
}
