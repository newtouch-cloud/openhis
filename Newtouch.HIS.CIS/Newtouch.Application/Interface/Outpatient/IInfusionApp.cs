using Newtouch.Domain.DTO;
using System;
using System.Collections.Generic;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 注射管理
    /// </summary>
    public interface IInfusionApp
    {
        /// <summary>
        /// 同步已结算处方信息
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh, string mzh);

        /// <summary>
        /// 已结算处方明细查询
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh);

        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="tags">输液明细ID</param>
        /// <param name="groupNo">组号</param>
        /// <returns></returns>
        string Grouping(List<long> tags, string groupNo);

        /// <summary>
        /// 输液开始
        /// </summary>
        /// <param name="tags">输液明细ID</param>
        /// <param name="startDateTime">开始时间</param>
        /// <param name="seatNum">座号/床号</param>
        /// <returns></returns>
        string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum);

        /// <summary>
        /// 输液结束
        /// </summary>
        /// <param name="tags">输液明细ID</param>
        /// <param name="endDateTime">结束时间</param>
        /// <returns></returns>
        string EndInfusion(List<long> tags, DateTime endDateTime);

        /// <summary>
        /// 创建组号
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        string CreateGroupNo(string kh,string mzh);

        /// <summary>
        /// 设置座位号
        /// </summary>
        /// <param name="tag">目标ID</param>
        /// <param name="seatNum">座位号</param>
        /// <returns></returns>
        string SetSeatNum(long tag, string seatNum);

        /// <summary>
        /// 查询输液信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzsyypxxVO> MzsyxxQuery(List<long> ids, string organizeId);
    }
}
