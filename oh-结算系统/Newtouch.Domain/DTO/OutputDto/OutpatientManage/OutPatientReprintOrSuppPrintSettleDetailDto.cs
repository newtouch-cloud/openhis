using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientReprintOrSuppPrintSettleDetailDto
    {
        public List<OutPatientReprintOrSuppPrintSettleDetailVO> mz_ghxmList { get; set; }
        public List<OutPatientReprintOrSuppPrintSettleDetailVO> mz_xmList { get; set; }
        public List<OutPatientReprintOrSuppPrintSettleDetailVO> mz_cfmxList { get; set; }
        public SysConfigVO pzmzEntity { get; set; }
        public bool IsGh { get; set; }
    }
}
