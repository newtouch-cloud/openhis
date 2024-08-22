using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonApp : AppBase, ICommonApp
    {
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMapRepository;
        public CommonApp(ISysFailedCodeMessageMappRepo sysFailedCodeMessageMapRepository)
        {
            this._sysFailedCodeMessageMapRepository = sysFailedCodeMessageMapRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysFailedCodeMessageMappEntity> GetAllSysFailedCodeMessageMapList()
        {
            return _sysFailedCodeMessageMapRepository.GetList(this.OrganizeId);
        }


    }
}
