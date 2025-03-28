using Newtouch.Herp.Domain.DTO.InputDto;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 出入库单审核
    /// </summary>
    public interface IOutOrInStorageBillApprovalApp
    {
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="billApprovalDTO"></param>
        /// <returns>error message</returns>
        string Approval(BillApprovalDTO billApprovalDTO);
    }
}