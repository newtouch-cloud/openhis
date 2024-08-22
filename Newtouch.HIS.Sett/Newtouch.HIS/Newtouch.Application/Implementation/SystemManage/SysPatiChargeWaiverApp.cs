using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeWaiverApp : AppBase, ISysPatiChargeWaiverApp
    {
        private readonly ISysPatientChargeWaiverRepo _sysPatiChargeWaiverRepo;
        private readonly ISysFeeDmnService _sysFeeDmnService;

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysPatiChargeWaiverVo> GetEffectiveList(string keyValue, int? bh = null)
        {
            return _sysFeeDmnService.GetSysPatiChargeWaiverList(keyValue, bh);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysPatiChargeWaiverRepo.DeleteForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeWaiverEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue)
        {
            _sysPatiChargeWaiverRepo.SubmitForm(sysPatiChargeWaiverEntity, keyValue, this.OrganizeId);
        }

        /// <summary>
        /// 计算收费项目减免金额
        /// 病人状态：1；--有效
        /// 变更标志：0；--未变更
        /// </summary>
        /// <param name="parmBrxz">病人性质</param>
        /// <param name="parmDl">大类</param>
        /// <param name="parmSfxm">收费项目</param>
        /// <param name="parmJe">金额</param>
        /// <param name="outJmbl">减免比例</param>
        /// <param name="outJmje">减免金额</param>
        /// <returns>count:减免后金额</returns>
        public decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje)
        {
            outJmbl = 0;
            outJmje = 0;
            return _sysPatiChargeWaiverRepo.Get_Calcjm(parmBrxz, parmDl, parmSfxm,parmJe,out outJmbl,out outJmje, this.OrganizeId);
        }
    }
}
