using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysDepartmentBindingController : ControllerBase
    {
        private readonly ISysDepartmentBindingRepo _sysDepartmentBindingRepo;

        private readonly ISysDepartmentBindingDmnService _sysDepartmentBindingDmnService;

        // GET: SystemManage/SysDepartmentBinding
        public ActionResult SysDepartmentBindingIndex()
        {
            return View();
        }

        //[HandlerAuthorize]
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetSysDepartmentBindingList(string keyword)
        {
            var data = _sysDepartmentBindingRepo.GetSysDepartmentBindingList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string bddm)
        {
            var entity = _sysDepartmentBindingRepo.GetSysDepartmentBindingEntity(bddm, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysDepartmentBindingEntity sysDepartmentBindgEntity, string bddm)
        {
            sysDepartmentBindgEntity.zt = sysDepartmentBindgEntity.zt == "true" ? "1" : "0";
            sysDepartmentBindgEntity.OrganizeId = this.OrganizeId;
            var Screen = _sysDepartmentBindingRepo.FindEntity(o => o.ks == sysDepartmentBindgEntity.ks);
            if (Screen!=null && !string.IsNullOrEmpty(Screen.bddm))
            {
                return Error("请一一对应，不能绑定多个！");
            }
           
            var saffentity = _sysDepartmentBindingDmnService.GetSaffEntity(sysDepartmentBindgEntity.gh, this.OrganizeId);
            if (string.IsNullOrEmpty(saffentity.zjh))
            {
                return Error("请维护人员身份证信息！");
            }
            sysDepartmentBindgEntity.xb = saffentity.xb; ;
            sysDepartmentBindgEntity.zjlx = saffentity.zjlx;
            sysDepartmentBindgEntity.zjh = saffentity.zjh;
           
            _sysDepartmentBindingRepo.SubmitForm(sysDepartmentBindgEntity, bddm);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string bddm)
        {
            _sysDepartmentBindingRepo.DeleteForm(bddm, this.OrganizeId);
            return Success("操作成功。");
        }
       
    }
}