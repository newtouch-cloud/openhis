using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientRegChargeDetailDto
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<OutPatientRegChargeVO> ghRecords;
        public IList<OutPatientRegChargeDetailsVO> ghRecordsDetails;

        /// <summary>
        /// 
        /// </summary>
        public IList<OutPatientRegChargeMajorClassGroupBO> dlhj { get; set; }

    }
}
