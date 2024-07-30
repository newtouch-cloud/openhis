using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 取消住院结算 接口
    /// </summary>
    public interface IHospSettCancelApp
    {
        /// <summary>
        /// 取消住院结算，查看患者住院 状态信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        HospSettCancelPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh);

        /// <summary>
        /// 取消住院结算 提交保存操作
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        void DoCancel(string zyh, int expectedjsnm, string cancelReason);

    }
}
