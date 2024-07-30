using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IMainRecordDmnService
    {

        BasyVO GetMainRecord(string orgId,string mainId);
        BasyVO GetMainRecordbybrjbxx(string orgId, string zyh);
        /// <summary>
        /// 住院费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BasyVO GetMainRecordZyFee(string orgId, string zyh);
        BasyVO GetMainRecordZyFeeDetail(BasyVO vo, string orgId, string zyh);
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
        void PatBasicSubmit(BasyVO vo, BasyRelVO reldto, string orgId, string user);
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
        IList<BasyVO> PatMainList(Pagination pagination, string orgId, string cyksrq, string cyjsrq, string bazt, string bq, string keyword, string sfzh,string cykb);
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
        
        #region 结算病人不能保存诊断
        IList<PatZDListVO> SettlementQuery(string zyh,string orgId);
        List<ButtonEnableVO> DiagnosticSave(string Code, string orgId);
        #endregion
        string MedicalRecordExportQuery(string kssj, string jssj, string orgId);
        string MedicalRecordExportFYQuery(string kssj, string jssj, string orgId);
    }
}
