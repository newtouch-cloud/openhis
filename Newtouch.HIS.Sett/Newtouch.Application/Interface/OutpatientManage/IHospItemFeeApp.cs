using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospItemFeeApp
    {
        /// <summary>
        /// 根据住院号计算项目计费
        /// </summary>
        /// <param name="zyh"></param>
        DataTable CountZYItemFree(string zyh);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
         void SubmitForm(HospItemBillingEntity hospItemFeeEntity, int? keyValue);
    }
}
