using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class AntibioticTypeController : ControllerBase
    {
        /// <summary>
        /// 创建抗生素类别总览页面视图
        /// </summary>
        ISysMedicineAntibioticTypeDmnService _ISysMedicineAntibioticTypeDmnService;
        public ActionResult AntibioticTypeQuery()
        {
            return View();
        }
        /// <summary>
        /// 创建抗生素类别设置页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AntibioticTypeSettingForm()
        {
            return View();
        }


        public AntibioticTypeController(ISysMedicineAntibioticTypeDmnService sysMedicineAntibioticTypeDmnService)
        {
            this._ISysMedicineAntibioticTypeDmnService = sysMedicineAntibioticTypeDmnService;
        }

        /// <summary>
        /// 获取指定类别等级抗生素类别信息
        /// </summary>
        /// <param name="lbdj"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult SelectGridList(string typelevel, string parentId = null)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetValidListByOrg(OrganizeId, typelevel, parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 抗生素类别信息上传
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public ActionResult submitForm_AntibioticType(SysMedicineAntibioticTypeVO AntibioticType)
        {
            SysMedicineAntibioticTypeVO entity = new SysMedicineAntibioticTypeVO();
            entity = AntibioticType.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// 删除抗生素类别
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DeleteData(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById(OrganizeId, Id);
            entity.zt = "0";
            _ISysMedicineAntibioticTypeDmnService.SubmitForm(entity);
            return Success();
        }
        /// <summary>
        /// 通过Id获取实例
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetById(string Id)
        {
            SysMedicineAntibioticTypeVO entity = _ISysMedicineAntibioticTypeDmnService.GetById( OrganizeId,Id);
            return Content(entity.ToJson());
        }

        public ActionResult GetSelectJson(string parentId)
        {
            var data = _ISysMedicineAntibioticTypeDmnService.GetListByParentId(OrganizeId, parentId);
            List<object> list = new List<object>();
            foreach (SysMedicineAntibioticTypeVO item in data)
            {
                list.Add(new { id = item.Id + "|" + item.qxjb, text = item.typeName });//将权限级别信息带入Id中
            }
            return Content(list.ToJson());
        }
    }
}