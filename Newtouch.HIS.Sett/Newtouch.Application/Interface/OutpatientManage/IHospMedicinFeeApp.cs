using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospMedicinFeeApp
    {
        /// <summary>
        /// 根据住院号查询所有的计算记录，把已结和已冲销的记录进行对冲，得到未冲销的数据
        /// </summary>
        /// <param name="zyh"></param>
        DataTable CountYPItemFree(string zyh);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(HospDrugBillingEntity hospMedicinFeeEntity, int? keyValue);
    }
}
