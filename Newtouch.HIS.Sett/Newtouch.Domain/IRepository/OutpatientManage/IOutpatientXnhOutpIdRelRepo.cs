using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 新农合门诊补偿序号关联表
    /// </summary>
    public interface IOutpatientXnhOutpIdRelRepo : IRepositoryBase<OutpatientXnhOutpIdRelEntity>
    {
        /// <summary>
        /// 修改状态无效
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateZtDisable(string mzh, string organizeId, string userCode);

        /// <summary>
        /// 修改状态无效
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateZtDisable(string mzh, string outpId, string organizeId, string userCode);

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">处理状态 0-未处理 1-明细已上传 2-已结算 3-已红冲  4-门诊回退 </param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateProcessingStatus(string mzh, int processingStatus, string organizeId, string userCode);

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">处理状态 0-未处理 1-明细已上传 2-已结算 3-已红冲  4-门诊回退 </param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateProcessingStatus(string mzh, int processingStatus, string outpId, string organizeId, string userCode);
    }
}