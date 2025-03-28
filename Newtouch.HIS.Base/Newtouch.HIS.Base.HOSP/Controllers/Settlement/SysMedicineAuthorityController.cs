using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.Controllers.Settlement
{
    public class SysMedicineAuthorityController : ControllerBase
    {
        private readonly ISysMedicineAuthorityRepo _sysMedicineAuthorityRepo;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;

        public SysMedicineAuthorityController(ISysMedicineAuthorityRepo sysMedicineAuthorityRepo, ISysOrganizeDmnService SysOrganizeDmnService, ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            this._sysMedicineAuthorityRepo = sysMedicineAuthorityRepo;
            this._sysChargeCategoryRepo = sysChargeCategoryRepo;
            //this._SysOrganizeDmnService = SysOrganizeDmnService;
        }
        // GET: SysMedicineAuthority
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string organizeId, string keyword)
        {
            var MedicineAuthorityRepo = _sysMedicineAuthorityRepo.GetList(organizeId, keyword);
            List<SysMedicineAuthorityVO> List = new List<SysMedicineAuthorityVO>();
            SysMedicineAuthorityVO obj= new SysMedicineAuthorityVO();
            foreach (var repo in MedicineAuthorityRepo) {
                obj = repo.MapperTo<SysMedicineAuthorityEntity, SysMedicineAuthorityVO>();
                if (repo.rel_lxcode == "1") {
                    obj.rel_lxname = "特殊药品";
                    obj.rel_name = ((Newtouch.Infrastructure.EnumYpsx)int.Parse(obj.rel_value)).GetDescription();
                }
                else if (repo.rel_lxcode == "2")
                {
                    obj.rel_lxname = "收费大类";
                    var sfdl = _sysChargeCategoryRepo.FindEntity(p => p.dlCode == obj.rel_value && p.OrganizeId == OrganizeId && p.zt == "1");
                    if(sfdl!=null)
                        obj.rel_name = sfdl.dlmc;
                    else
                        obj.rel_name = "";
                }
                else if (repo.rel_lxcode == "3")
                {
                    obj.rel_lxname = "抗生素";
                    obj.rel_name=   ((Newtouch.Infrastructure.EnumKss)int.Parse(obj.rel_value)).GetDescription();
                }
                List.Add(obj);
            }
            var data = List;
            return Content(data.ToJson());
        }


        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysMedicineAuthorityRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicineAuthorityEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            //if (entity.rel_lxcode == "1") {  //lx: 1药品属性 2收费大类
            //    entity.sfdlCode = null;
            //}
            //else if(entity.lx == "2") {
            //    entity.ypsxCode = null;
            //}
            _sysMedicineAuthorityRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}