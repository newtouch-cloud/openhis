using Newtouch.Domain.ValueObjects.API;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects.API
{
    public class MedicalRecordInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime rq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MedicalRecordFileVO> Details { get; set; }
    }
}
