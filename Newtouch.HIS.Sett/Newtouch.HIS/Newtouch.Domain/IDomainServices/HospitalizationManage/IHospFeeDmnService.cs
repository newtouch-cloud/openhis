using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 住院计费 DmnService
    /// </summary>
    public interface IHospFeeDmnService
    {
        /// <summary>
        /// 根据住院获取住院的项目计费 的 详细信息 列表（所有）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetAllItemFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// 根据住院获取住院的项目计费 的 详细信息 列表（含退费）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// 根据住院获取住院的药品计费 的 详细信息 列表（所有）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospMedicinFeeDetailVO> GetAllMedicinFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// 根据住院获取住院的药品计费 的 详细信息 列表（含退费）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId);
        /// <summary>
        /// 同步最新CPOE项目费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        void SyncPatFee(string orgId, string zyh,int zxtype);

        /// <summary>
        /// check是否 产生非医保相关费用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool CheckHasNonYbFee(string zyh, string orgId);
    }

}
