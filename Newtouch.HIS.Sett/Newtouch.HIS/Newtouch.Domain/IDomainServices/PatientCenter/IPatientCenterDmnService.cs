using Newtouch.Core.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ReportTemplateVO;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IPatientCenterDmnService
    {
        #region 患者信息
        HosPatientVo PatBrxzInfo(string patid, string orgId);
        PatientCenterVO PatientBasic(string zyh, string mzh, string keyword, string blh, string orgId, string ywlx);
        IList<HosPatientVo> InHospitalHistory(string patid, string zyh, string orgId);
        HosPatientVo InHospitalDoctorInfo(string zyh, string orgId, ref HosPatientVo patvo);
        IList<PatientSettleHisVO> InHospitalSett(string zyh, string jsxz, string orgId);
        #endregion

        #region 费用信息
        IList<HospFeeUploadDetailVO> GetHospXmYpFeeVOList(Pagination pagination, string zyh, string orgId);
        /// <summary>
        /// 计费表费用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="group">1 sfdl 2 sfxm 3 sfxm+jfrq 4 无</param>
        /// <param name="sfdl"></param>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj">yyyy-mm-dd</param>
        /// <param name="jssj">yyyy-mm-dd</param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetPatjfbInfo(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj);
        IList<HospFeeChargeCategoryGroupDetailVO> GetPatjfbInfo(string zyh, string group, string sfdl, string sfxm, string orgId, string kssj, string jssj, string keyword);
        #endregion


        #region 预交金
        List<PatAccPayVO> GetAdvancePayment(string zyh, string orgId);
        #endregion


        #region 诊断

        //根据住院号获取诊断信息
        IList<PatZDListVO> GetDiagLsit( string orgId, string zyh);

        /// <summary>
        ///  获取患者出院诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">诊断类别 1入院诊断2出院诊断</param>
        /// <returns></returns>
        //IList<PatZDListVO> GetPatHisZDInfo( string zyh, string orgId, int? zdlb);

        #endregion

        #region 医保信息
        IList<PatMedInsurSettVO> MedInsurPreSettList(string zyh, string orgId);
        PatMedInsurSettVO PreSettbyId(string presettId);
        PatientSettleHisVO InHospitalSettbyJsnm(string jsnm,string settid,string orgId);
        PatientSettleHisVO MedInsurSettbyId(string settId);
        #endregion
    }
}
