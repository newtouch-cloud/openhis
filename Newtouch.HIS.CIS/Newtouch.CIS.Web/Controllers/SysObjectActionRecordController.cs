using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 通用对象操作记录
    /// </summary>
    public class SysObjectActionRecordController : OrgControllerBase
    {
        private readonly ISysObjectActionRecordRepo _sysObjectActionRecordRepo;

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(SysObjectActionRecordEntity entity)
        {
            entity.OrganizeId = this.OrganizeId;
            _sysObjectActionRecordRepo.Add(entity);
            return Success();
        }

    }
}