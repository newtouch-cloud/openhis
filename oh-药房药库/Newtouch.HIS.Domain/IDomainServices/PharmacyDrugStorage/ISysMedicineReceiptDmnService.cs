namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 系统药品单据
    /// </summary>
    public interface ISysMedicineReceiptDmnService
    {
        /// <summary>
        /// 出入库单据 审核
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        bool ApprovalStorageIoReceipt(string crkId, int djlx, string shzt);
        bool SubmitDrupreparation(string byId, string shzt);
        /// <summary>
        /// 获取转换因子
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        int GetZhyz(string organizeId, string yfbmCode, string ypCode);
    }
}
