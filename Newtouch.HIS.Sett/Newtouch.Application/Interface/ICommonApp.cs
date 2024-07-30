using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappEntity> GetAllSysFailedCodeMessageMapList();



    }
}
