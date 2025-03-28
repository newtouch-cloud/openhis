using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{

    /// <summary>
    /// 预交金
    /// </summary>
    public class InpatientReserveDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public InpatientReservePatientInfoDto patInfo;

        /// <summary>
        /// 账户信息
        /// </summary>
        public List<InpatientPatAccPayVO> accPayInfoList;
    }
    [NotMapped]
    public class InpatientReservePatientInfoDto : InpatientOutpatAccInfoDto
    {
        public int zhCode { get; set; }
        /// <summary>
        /// 账户性质 区分院内账户时使用
        /// </summary>
        public int? zhxz { get; set; }
    }
}
