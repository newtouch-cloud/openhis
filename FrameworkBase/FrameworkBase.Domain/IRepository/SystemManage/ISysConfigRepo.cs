using FrameworkBase.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.Domain.IRepository
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
        /// <returns></returns>
        SysConfigEntity GetByCode(string code);

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string GetValueByCode(string code);

        /// <summary>
        /// 根据Code获取配置Int Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        int GetIntValueByCode(string code, int defaultValue = 0);

        /// <summary>
        /// 根据Code获取配置Bool Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool? GetBoolValueByCode(string code, bool? defaultValue = null);

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysConfigEntity> GetList(string keyword);

        /// <summary>
        /// 提交保存配置
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysConfigEntity entity, string keyValue);


    }
}
