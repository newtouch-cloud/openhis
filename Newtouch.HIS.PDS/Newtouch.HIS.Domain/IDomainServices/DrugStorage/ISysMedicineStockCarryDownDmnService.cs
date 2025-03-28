using System;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 结转
    /// </summary>
    public interface ISysMedicineStockCarryDownDmnService
    {
        /// <summary>
        /// 查询已结转药品
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryDownMedicineList(Pagination pagination, string jzsj, string keyWork);

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj">结转时间</param>
        string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj);

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">结转时间</param>
        /// <returns></returns>
        string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj, string userCode);

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">结转时间</param>
        /// <returns></returns>
         string CarryOverMedicine(string yfbmCode, string organizeId, DateTime jzsj, string userCode);
    }
}
