using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 门诊输液药品信息
    /// </summary>
    public interface IMzsyypxxDmnService
    {

        /// <summary>
        /// 根据卡号获取输液信息
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">true：已结束输液；false：未结束输液</param>
        /// <returns></returns>
        IList<MzsyypxxVO> SelectMzsyypxxByKh(Pagination pagination, string kh,string mzh, string organizeId, bool isEnd);

        /// <summary>
        /// 根据卡号获取输液信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzsyypxxVO SelectMzsyypxxById(long id, string organizeId);

        /// <summary>
        /// 未结束的输液患者
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">是否结束  false：输液未结束  true：输液已结束</param>
        /// <returns></returns>
        IList<MzsyhzxxVO> SelectKhAndXm(Pagination pagination, string kh,string mzh, string fph, DateTime kssj, DateTime jssj, string organizeId, bool isEnd = false);

        /// <summary>
        /// 插入新输液信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        int InsertNewMzsyypxx(OutpatientSettledRpQueryResponseDTO item, string organizeId, string creatorCode);

		/// <summary>
		/// 已结算处方明细查询
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="fph"></param>
		/// <param name="kh"></param>
		/// <param name="yfCode">用法代码</param>
		/// <returns></returns>
		List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode, string mzh);
		/// <summary>
		/// 获取所有组号
		/// </summary>
		/// <param name="kh"></param>
		/// <param name="fph"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		IList<string> SelectAllGroupNoByKh(string kh, string organizeId,string mzh);
    }
}
