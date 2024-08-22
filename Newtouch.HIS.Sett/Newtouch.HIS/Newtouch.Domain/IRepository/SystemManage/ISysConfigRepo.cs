using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysConfigRepo : IRepositoryBase<SysConfigEntity>
    {
        /// <summary>
        /// 根据Code获取配置Entity
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysConfigEntity GetByCode(string code, string orgId);

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetValueByCode(string code, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);

        IList<SysConfigEntity> GetList(string keyword, string organizeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysConfigEntity GetForm(string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysConfigEntity entity, string keyValue);



    }
}
