using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeProjPriceAdjApp : AppBase, ISysChargeProjPriceAdjApp
    {
        private readonly ISysChargeItemPriceAdjustRepo _sysChargeProjPriceAdjRepo;
        //other Repositories or DomainServices

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysChargeItemPriceAdjustEntity> GetEffectiveList(string keyValue)
        {
            return _sysChargeProjPriceAdjRepo.GetEffectiveList(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeItemPriceAdjustEntity GetForm(int? keyValue)
        {
            return _sysChargeProjPriceAdjRepo.GetForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjRepo.DeleteForm(sysChargeProjPriceAdjEntity, this.OrganizeId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysChargeProjPriceAdjEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue)
        {
            _sysChargeProjPriceAdjRepo.SubmitForm(sysChargeProjPriceAdjEntity, keyValue, this.OrganizeId);
        }


    }
}
