using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Application
{
    public class SysStaffApp : ISysStaffApp
    {
        private readonly ISysStaffRepo _sysStaffRepository;

        public SysStaffApp(ISysStaffRepo sysStaffRepository)
        {
            this._sysStaffRepository = sysStaffRepository;
        }

        /// <summary>
        /// 有效员工自动提示
        /// </summary>
        /// <returns></returns>
        public object GetValidStaffList()
        {
            return _sysStaffRepository.GetValidStaffList();
        }

        /// <summary>
        /// 根据Account（工号）获取人员编号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int GetSysRybhByAccont(string account)
        {
            var entity = _sysStaffRepository.IQueryable().Where(p => p.zt == ((int)EnumZT.Valid).ToString() && p.gh == account).FirstOrDefault();
            if (entity != null)
            {
                return entity.rybh;
            }
            return 0;
        }

    }
}
