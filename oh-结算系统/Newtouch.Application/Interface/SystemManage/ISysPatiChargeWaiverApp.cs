using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeWaiverApp
    {
        List<SysPatiChargeWaiverVo> GetEffectiveList(string keyValue, int? bh = null);

        void DeleteForm(int keyValue);
        void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue);

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
        decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje);

    }
}
