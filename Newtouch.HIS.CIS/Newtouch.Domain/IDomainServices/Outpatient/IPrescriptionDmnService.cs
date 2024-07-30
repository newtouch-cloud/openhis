using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrescriptionDmnService
    {
        /// <summary>
        /// 历史处方树明细
        /// </summary>
        /// <param name="cfId"></param>
        IList<HistoryPresFormSelectVO> GetHistoryPresDetailByCfId(string cfId);

        /// <summary>
        /// 根据cfId或cfmxIdStr查询明细
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="cfId"></param>
        /// <param name="cfmxIdStr"></param>
        /// <returns></returns>
        List<PrescriptionDetailQueryVO> GetPresDetailList(int cflx, string cfId, string cfmxIdStr);

        /// <summary>
        /// 医保患者审核收费项目是否含自费
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        List<string> ValidateMedicalInsurance(Dictionary<int, string> dic, string orgId);

		/// <summary>
		/// 开立物资项目冻结库存数量
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int Sumwzdj(string[] cfh,string orgId,string rygh);

		/// <summary>
		/// 开立物资项目冻结库存数量作废
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int ZUOFwzdj(string cfh, string orgId, string rygh);

	}
}
