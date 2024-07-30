using Newtouch.HIS.Application.Interface;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicinApp : AppBase, ISysMedicinApp
    {
        private readonly ISysMedicinDmnService _sysMedicinDmnService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        public IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod)
        {
            return _sysMedicinDmnService.GetYpList(keywrod);
        }

    }
}
