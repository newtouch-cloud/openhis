using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;


namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统配置模板
    /// </summary>
    public interface ISystemConfigTemplateRepo: IRepositoryBase<SysConfigTemplateEntity>
    {
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysConfigTemplateEntity> GetListTmp(string keyword);
        /// <summary>
        /// 获取配置模板明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysConfigTemplateEntity> GetTemplateInfo(string keyValue);
        /// <summary>
        /// 新增/修改模板
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitFormTmp(SysConfigTemplateEntity entity, string keyValue);
    }
}
