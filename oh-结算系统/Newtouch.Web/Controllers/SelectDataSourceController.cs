using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// （下拉）选择 数据源
    /// </summary>
    public class SelectDataSourceController : ControllerBase
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;

        /// <summary>
        /// 获取病人性质有效列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getEffectPatiNatureList()
        {
            var treeList = _sysPatiNatureRepo.getEffectPatiNatureList(this.OrganizeId);
            return Content(treeList);
        }

    }
}