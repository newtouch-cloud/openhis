using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO.InputDto;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.ValueObjects.MRHomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IMRHomePageDmnService
    {
        BasyVO GetMainRecord(string orgId, string mainId);
        BasyVO GetMainRecordbybrjbxx(string orgId, string zyh);
        /// <summary>
        /// 住院费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BasyVO GetMainRecordZyFee(string orgId, string zyh);
        IList<PatZDListVO> GetDiagLsit(string orgId, string bah, string zyh);
        IList<BasyOpDto> GetOPLsit(string orgId, string bah, string zyh);
        /// <summary>
        /// 保存诊断列表
        /// </summary>
        /// <param name="list"></param>
        void ZdListSubmit(List<BasyZdDto> list, string orgId);
        /// <summary>
        /// 保存手术列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orgId"></param>
        void OpListSubmit(List<BasyOpDto> list, string orgId);
        void PatBasicSubmit(BasyVO vo, BasyRelVO reldto, string blmbId, string orgId, string user, string username = null);
        /// <summary>
        /// 病案列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="cyksrq"></param>
        /// <param name="cyjsrq"></param>
        /// <param name="bazt"></param>
        /// <param name="bq"></param>
        /// <param name="keyword"></param>
        /// <param name="cyts"></param>
        /// <returns></returns>
        IList<BasyVO> PatMainList(Pagination pagination, string orgId, string cyksrq, string cyjsrq, string bazt, string bq, string keyword, string cyts);
        /// <summary>
        ///  获取患者出院诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">诊断类别 1入院诊断2出院诊断</param>
        /// <returns></returns>
        IList<PatZDListVO> GetPatHisZDInfo(string bah, string zyh, string orgId, int? zdlb);
        /// <summary>
        /// 获取患者手术列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<BasyOpDto> GetPatHisOperInfo(string bah, string zyh, string orgId);
        /// <summary>
        /// 获取上传医保病案信息
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<YB_7600> GetHomePageforYB(string OrgId, string zyh);
        BasyVO GetMainRecordZyFeeDetail(BasyVO vo, string orgId, string zyh);

        #region 结算病人不能保存诊断
        IList<PatZDListVO> SettlementQuery(string zyh, string orgId);
        List<ButtonEnableVO> DiagnosticSave(string Code, string orgId);
        /// <summary>
        /// 把病案的诊断同步到出区诊断表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        void SynchronizationZD(string zyh, string orgId);
        #endregion

        string GetYbdmByGh(string rygh);
    }
}
