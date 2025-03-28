using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 综合材料项目
    /// </summary>
    public interface IIntegratedMaterialDmnService
    {
        /// <summary>
        /// 根据收费项目获取 一次性材料支付标准代码
        /// </summary>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        SysChargeMaterialItemSynthesisEntity GetSfclxmzhEntity(string sfxm, string orgId);

    }

}
