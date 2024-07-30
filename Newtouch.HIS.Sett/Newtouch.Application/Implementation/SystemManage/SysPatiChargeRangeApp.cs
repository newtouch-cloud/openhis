using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeRangeApp : AppBase, ISysPatiChargeRangeApp
    {
        private readonly ISysPatientChargeRangeRepo _sysPatiChargeRangeRepo;
        private readonly ISysFeeDmnService _sysFeeDmnService;

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysPatientChargeRangeVO> GetEffectiveList(string keyValue, int? bh = null)
        {
            return _sysFeeDmnService.GetSysPatientChargeRangeList(keyValue, bh);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysPatiChargeRangeRepo.DeleteForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeRangeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue)
        {
            _sysPatiChargeRangeRepo.SubmitForm(sysPatiChargeRangeEntity, keyValue, this.OrganizeId);
        }


    }
}
