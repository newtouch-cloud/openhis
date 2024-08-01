using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 基础数据
    /// </summary>
    [AutoResolveIgnore]
    public class BaseDataController : OrgControllerBase
    {
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="baseDataDmnService"></param>
        /// <param name="sysConfigRepo"></param>
        /// <param name="sysChargeCategoryVRepo"></param>
        /// <param name="sysChargeItem"></param>
        /// <param name="sysDepartmentRepo"></param>
        public BaseDataController(IBaseDataDmnService baseDataDmnService
            , ISysConfigRepo sysConfigRepo
            , ISysDepartmentRepo sysDepartmentRepo
            )
        {
            this._baseDataDmnService = baseDataDmnService;
            this._sysConfigRepo = sysConfigRepo;
            this._sysDepartmentRepo = sysDepartmentRepo;
        }

        /// <summary>
        /// 检索药品项目
        /// （动态参数配置 药品是否关联药房库存 IS_MedicineSearchRelatedKC true or false 默认不关联）
        /// </summary>
        /// <param name="reqDto"></param>
        /// <returns></returns>
        public ActionResult SelectSfxmYp(SelectSfxmYpFilterDTO reqDto)
        {
            reqDto.orgId = this.OrganizeId;
            var relatedKc = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", this.OrganizeId);
            reqDto.useypckflag = relatedKc ?? false;

            var data = _baseDataDmnService.SelectSfxmYp(reqDto);
            return Content(data == null ? "[]" : data.ToJson());
        }

        /// <summary>
        /// （授权的）获取病区树的Json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAuthedWardTree()
        {
            var gh = this.UserIdentity.rygh;
            var data = _baseDataDmnService.GetWardListByStaffGh(gh, this.OrganizeId);
            var treeList = data.Select(item => new TreeViewModel()
            {
                id = item.bqCode,
                value = item.bqCode,
                text = item.bqmc,
                parentId = null,
                hasChildren = false,
                isexpand = false,
                complete = true,
                showcheck = true,
            }).ToList();
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// （授权的）获取病区下拉的Json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAuthedWardSelectJson()
        {
            var gh = this.UserIdentity.rygh;
            var data = _baseDataDmnService.GetWardListByStaffGh(gh, this.OrganizeId);
            var list = new List<object>();
            foreach (var item in data)
            {
                list.Add(new { id = item.bqCode, text = item.bqmc });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 医嘱频次 检索
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOrderFrequencyList()
        {
            var list = _baseDataDmnService.GetOrderFrequencyList(this.OrganizeId);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取收费项目的执行科室列表xt_sfxm_zxks（未绑定时返回所有科室）
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        public ActionResult GetSfxmZxksSelectJson(string sfxmCode)
        {
            var list = _baseDataDmnService.GetSfxmZxksList(this.OrganizeId, sfxmCode);

            if (list.Count == 0)
            {
                list = _sysDepartmentRepo.GetList(this.OrganizeId, "1", null);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有收费项目
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllSfdl()
        {
            var data = _baseDataDmnService.SelectSfdl(OrganizeId);
            if (data != null && data.Count > 0) return Content(data.ToJson());
            return Content(null);
        }

        /// <summary>
        /// 根据大类代码获取收费项目
        /// </summary>
        /// <param name="sfdlCode"></param>
        /// <returns></returns>
        public ActionResult GetGetSfmxBySfdl(string sfdlCode)
        {
            var data = _baseDataDmnService.SelectSfxm(sfdlCode, OrganizeId);
            if (data != null && data.Count > 0) return Content(data.ToJson());
            return Content(null);
        }

    }
}