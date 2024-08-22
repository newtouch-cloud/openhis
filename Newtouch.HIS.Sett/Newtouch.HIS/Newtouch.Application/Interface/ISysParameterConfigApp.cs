using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ViewModels;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 系统参数设置
    /// </summary>
    public interface ISysParameterConfigApp
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeTemplateInfoVM GetSysChargeTemplateInfo(string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="xmListStr"></param>
        void ChargeTemplate_SubmitForm(SysChargeTemplateEntity entity, string xmListStr);

    }
}
