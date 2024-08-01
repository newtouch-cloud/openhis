using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public interface ISysConfigRepo : IRepositoryBase<SysConfigEntity>
    {
        /// <summary>
        /// 根据Code获取配置Entity
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysConfigVO GetByCode(string code, string orgId);

        /// <summary>
        /// 根据Code获取Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetValueByCode(string code, string orgId);

        /// <summary>
        /// 根据Code获取Value Int
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);

        /// <summary>
        /// 根据Code获取Value Decimal
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        decimal GetDecimalValueByCode(string code, string orgId, decimal defaultValue = 0);

        /// <summary>
        /// 根据Code获取Value Bool
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);

        /// <summary>
        /// 获取列表（检索查询）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysConfigEntity> GetList(string keyword, string organizeId);

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysConfigEntity entity, string keyValue);

        /// <summary>
        /// 修改配置的值（不存在则新增）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void UpdateValue(string code, string orgId, string name, string value);

    }
}
