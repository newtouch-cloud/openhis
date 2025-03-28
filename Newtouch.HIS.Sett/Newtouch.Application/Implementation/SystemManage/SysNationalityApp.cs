using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class SysNationalityApp : ISysNationalityApp
    {
        private readonly ISysNationalityRepo _SysNationalityRepo;

        public SysNationalityApp(ISysNationalityRepo SysNationalityRepo)
        {
            this._SysNationalityRepo = SysNationalityRepo;
        }

        /// <summary>
        /// 获取所有国籍自动提示
        /// </summary>
        /// <returns></returns>
        public object GetgjList()
        {
            return _SysNationalityRepo.GetgjList();
        }
    }
}
