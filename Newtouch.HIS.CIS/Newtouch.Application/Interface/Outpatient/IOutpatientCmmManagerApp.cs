using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 门诊中医馆管理
    /// </summary>
    public interface IOutpatientCmmManagerApp
    {
        /// <summary>
        /// 患者信息上传
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        Result TcmHis01(TreatEntityObj jzxx, string organizeId, string userCode);

        /// <summary>
        /// 电子病历集成模块
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateEmrUrl(string mzh, string jzys, string organizeId, out string response);

        /// <summary>
        /// 辩证论治
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateAeUrl(GetAeRequestDto aeRequest, out string response);

        /// <summary>
        /// 知识库
        /// </summary>
        /// <returns></returns>
        string GetIntegrateKUrl(out string response);

        /// <summary>
        /// 集成治未病
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh, string organizeId, out string response);

        /// <summary>
        /// 提取电子病历
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        Record ExtractIntegrateEmr(string mzh, string organizeId);

        /// <summary>
        /// 提取处方
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz">门诊住院标志 0-药库  1-门诊  2-住院  3-通用(取门诊单位) </param>
        /// <returns></returns>
        List<DrugDataEx> ExtractIntegrateRp(string mzh, string organizeId, string mzzybz);

        /// <summary>
        /// 提取诊断信息
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        DiagInfo ExtractIntegrateDiagnosis(string mzh, string organizeId);

        /// <summary>
        /// 推送草药
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string PushMedicineInfo(string organizeId, string userCode);

        /// <summary>
        /// 组装远程医疗参数
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IntegrateRcDto GetIntegrateRCRequsetParam(string blh, string mzh, string jzys, string organizeId);
    }
}