using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class HospItemFeeApp : AppBase, IHospItemFeeApp
    {
        private readonly IHospItemBillingRepo _hospItemFeeRepository;

        /// <summary>
        /// 根据住院号计算项目计费
        /// </summary>
        /// <param name="zyh"></param>
        public DataTable CountZYItemFree(string zyh)
        {
            return this.CountZYItemFree(zyh);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(HospItemBillingEntity hospItemFeeEntity, int? keyValue)
        {

             _hospItemFeeRepository.SubmitForm(hospItemFeeEntity, keyValue);
        }
    }
}
