using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class HospMedicinFeeApp : AppBase, IHospMedicinFeeApp
    {
        private readonly IHospDrugBillingRepo _hospMedicinFeeRepository;

        /// <summary>
        /// 根据住院号查询所有的计算记录，把已结和已冲销的记录进行对冲，得到未冲销的数据
        /// </summary>
        /// <param name="zyh"></param>
        public DataTable CountYPItemFree(string zyh)
        {
            return this.CountYPItemFree(zyh);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(HospDrugBillingEntity hospMedicinFeeEntity, int? keyValue)
        {
            _hospMedicinFeeRepository.SubmitForm(hospMedicinFeeEntity, keyValue);
        }
    }
}
