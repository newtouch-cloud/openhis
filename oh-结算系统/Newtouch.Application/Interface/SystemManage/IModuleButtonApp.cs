using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 按钮App
    /// </summary>
    public interface IModuleButtonApp
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysModuleButtonEntity GetForm(string keyValue);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

    }
}
