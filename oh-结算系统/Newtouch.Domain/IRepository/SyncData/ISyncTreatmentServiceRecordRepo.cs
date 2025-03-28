using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISyncTreatmentServiceRecordRepo : IRepositoryBase<SyncTreatmentServiceRecordEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<SyncTreatmentServiceRecordEntity> GetList(string orgId, int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<SyncTreatmentServiceRecordEntity> GetPagintionList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string gh, bool? isDeleted, bool? isAdmini, string brlx, int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "", string blh = "", string xm = "");


        /// <summary>
        ///获取待确认和已确认的门诊号/住院号distinct
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="type"></param>
        List<OptimaAccLeftDto> GetUnitList(string orgId, string type, string rygh, string kssj = null, string jssj = null);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SyncTreatmentServiceRecordEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SyncTreatmentServiceRecordEntity SyncTreatmentServiceRecordEntity, int? keyValue, string orgId);

        /// <summary>
        /// 批量作废
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="Id"></param>
        void SaveCancel(string orgId, List<string> Ids);
    }
}
