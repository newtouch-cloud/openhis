using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutPatientChargeQueryDmnService
    {
        /// <summary>
        /// 查询收费项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="syy"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <returns></returns>
        IList<OutPatientRegChargeVO> SelectRegChargeQuery(Pagination pagination,string kh, string fph, string xm, string syy, DateTime? createTimestart, DateTime? createTimeEnd);

        /// <summary>
        /// 查询收费项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="syy"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <returns></returns>
        IList<OutPatientRegChargeMVO> RegChargeQuery(Pagination pagination, string kh, string fph,string jsfph, string xm, string syy, DateTime? createTimestart, DateTime? createTimeEnd, DateTime? sfrqTimestart, DateTime? sfrqTimeEnd,string zxlsh);

        /// <summary>
        /// 获取结算支付方式
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnms"></param>
        /// <returns></returns>
        IList<SettZffsResult> GetSettZffsResultList(string orgId, string jsnms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        IList<OutPatientRegChargeDetailsVO> GetRecordsByJsnm(string jsnm);

        IList<OutPatientReprintOrSuppPrintSettleVO> LoadMzjsRecords(Pagination pagination, string jsnm, DateTime? startDate, DateTime? endDate, string kh, string yfph);
        List<OutPatientReprintOrSuppPrintSettleDetailVO> GetOutpatientRegItemList(string jsnmStr);
        List<OutPatientReprintOrSuppPrintSettleDetailVO> GetOutpatientItemList(string jsnmStr);
        List<OutPatientReprintOrSuppPrintSettleDetailVO> GetOutpatientPrescriptDetailList(string jsnmStr);
        void SaveInvoicePrintRecords(OutpatientSettlementEntity js, string fph, string xfph, Enumdyfs dyfs);
        List<OutpatientPrescriptionDetailVO> GetOutpatientPrescriptDetailList(int jsnm);
        List<OutpatienItemVO> GetOutpatientItemList(int jsnm);
        List<OutpatientRegistItemVO> GetOutpatientRegItemList(int jsnm);
        List<OutpatientSettlementPaymentModelVO> GetSettlementPaymentModel(int jsnm);
        void UpdateInvoiceNo(OutpatientSettlementEntity settlementEntity, string pageFph);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<string> GetSettOperByOrg(string orgId);
        IList<RoleUnionUser> GetCurUserIdListRoleId(string roleId);
    }
}
