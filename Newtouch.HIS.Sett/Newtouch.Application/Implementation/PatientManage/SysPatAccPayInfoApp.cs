using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class SysPatAccPayInfoApp : AppBase, ISysPatAccPayInfoApp
    {
        private readonly ISysPatientAccountRevenueAndExpenseRepo _sysPatAccPayInfoRepository;

    }
}
