using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.CIS;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.DomainService.CIS
{
    /// <summary>
    /// 门诊病历
    /// </summary>
    public class OutpMedicalRecordService : BaseServices<OutpTreatmentEntity>, IOutpMedicalRecordService
    {
        private readonly IOutpPrescriptionService _outpPrescription;
        public OutpMedicalRecordService(IOutpPrescriptionService outpPrescription)
        {
            _outpPrescription = outpPrescription;
        }
        public async Task<List<OutpPrescriptionDataVO>> OutpPrescriptionDataByMzh(string mzh, string orgId)
        {
            var treatInfo = await baseDal.GetFirstOrDefault(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1");
            if (treatInfo == null || string.IsNullOrWhiteSpace(treatInfo.jzId))
            {
                throw new FailedException("未找到相关就诊信息");
            }
            return await _outpPrescription.GetPresDatabyJzId(treatInfo.jzId);
        }
        public async Task<OutpMedicalRecordVO> OutpMedicalRecordByMzh(string mzh, string orgId)
        {
            var treatInfo = await baseDal.GetFirstOrDefault(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1");
            if (treatInfo == null || string.IsNullOrWhiteSpace(treatInfo.jzId))
            {
                throw new FailedException("未找到相关就诊信息");
            }
            var mrData = treatInfo.Adapt<OutpMedicalRecordVO>();
            var xyzd = await baseDal.GetByWhereWithAttr<WMDiagnosisEntity>(p => p.jzId == treatInfo.jzId && p.zt == "1");
            mrData.xyzd = xyzd.Adapt<List<OutpWMDiagnosisVO>>();
            var zyzd = await baseDal.GetByWhereWithAttr<TCMDiagnosisEntity>(p => p.jzId == treatInfo.jzId && p.zt == "1");
            mrData.zyzd = zyzd.Adapt<List<OutpTCMDiagnosisVO>>();
            return mrData;
        }

    }
}
