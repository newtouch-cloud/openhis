using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.PharmacyDepartment;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class SysMedicineController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicineBaseDmnService _sysMedicineBaseDmnService;
        private readonly ISysPharmacyDepartmentBaseDmnService _sysPharmacyDepartmentBaseDmnService;
        private readonly ISysPharmacyDepartmentDmnService _sysPharmacyDepartmentDmnService;
        //private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;
        //private readonly IPharmacyDmnService _pharmacyDmnService;


        public SysMedicineController(
            ISysOrganizeDmnService sysOrganizeDmnService
            , ISysMedicineDmnService sysMedicineDmnService
            ,ISysMedicineBaseDmnService sysMedicineBaseDmnService
            , ISysPharmacyDepartmentBaseDmnService sysPharmacyDepartmentBaseDmnService
            ,ISysPharmacyDepartmentDmnService sysPharmacyDepartmentDmnService
            //, ISysPharmacyDepartmentApp sysPharmacyDepartmentApp
            , ISysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo
            //, IPharmacyDmnService pharmacyDmnService
            )
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysMedicineDmnService = sysMedicineDmnService;
            this._sysMedicineBaseDmnService = sysMedicineBaseDmnService;
            this._sysPharmacyDepartmentBaseDmnService = sysPharmacyDepartmentBaseDmnService;
            this._sysPharmacyDepartmentDmnService = sysPharmacyDepartmentDmnService;
            //this._sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
            this._sysPharmacyDepartmentMedicineRepo = sysPharmacyDepartmentMedicineRepo;
            //this._pharmacyDmnService = pharmacyDmnService;
        }

        public ActionResult SysMedicineAdd()
        {
            return View();
        }

        public ActionResult YbbxblForm()
        {
            return View();
        }
        //grid json 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId, string zt, string ypflCode)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineDmnService.GetPaginationList(organizeId, pagination, zt, ypflCode, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #region 绑定下拉控件的值

        /// <summary>
        /// 根据组织机构获取药品信息列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetFloatingYPXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbMedicineList(organizeId, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetYbName(string organizeId, string keyword, string lx)
        {
            var list = _sysMedicineDmnService.GetYbName(organizeId, lx, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFloatingYPXNHXXJson(string organizeId, string keyword)
        {
            var list = _sysMedicineDmnService.GetYbXNHMedicineList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// 提交数据信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="p"></param>
        /// <param name="ypCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult submitForm(SysMedicineVO model, List<string> p, string ypCode, int? keyValue)
        {
            model.zt = model.zt == "true" ? "1" : "0";
            model.ycmc = model.ycmc ?? "";
            //model.ybbz = model.ybbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(model.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }

            if (!_sysOrganizeDmnService.IsMedicalOrganize(model.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _sysMedicineDmnService.SubmitMedicine(model, keyValue);
            var t = SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);

            var msg = string.IsNullOrWhiteSpace(t) ? "操作成功。" : ("保存药品信息成功，但授权药房药库失败，" + t);
            return Success(msg);
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _sysMedicineDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 药品信息同步医保
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult YibaoUploadApi(int ypId, string flag)
        {
            if (ypId <= 0)
            {
                return Success("请选择同步的药品！");
            }
            var ypEntity = _sysMedicineDmnService.GetFormJson(ypId);
            if (!string.IsNullOrWhiteSpace(ypEntity.ybdm))
            {
                var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "0", ypEntity.ybdm, ypEntity.ypCode, ypEntity.ypmc, ypEntity.jxmc, ypEntity.ycmc, ypEntity.ypgg, ypEntity.bzdw, ypEntity.lsj, flag);
                if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
                {
                    string error = "";
                    if (!_sysMedicineDmnService.YibaoUpload(ypId, out error))
                    {
                        return Error(error);
                    }
                    return Success(string.Format("[{0}]医保同步成功！", ypEntity.ypmc));
                }
                else
                {
                    return Error(string.Format("[{0}]医保同步失败：{1}", ypEntity.ypmc, Result.ErrorMsg));
                }
            }
            else
            {
                return Success("缺少医保代码！");
            }
        }

        public ActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            List<SysMedicalOrderFrequencyVO> entityList = _sysMedicineBaseDmnService.GetOrderFrequencyList(this.OrganizeId);
            foreach (var item in entityList)
            {
                var obj = new
                {
                    id = item.yzpcCode,
                    text = item.yzpcmc
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取抗生素信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetKssById(string Id)
        {
            SysMedicineAntibioticInfoVO entity = _sysMedicineBaseDmnService.GetKssInfo(Id, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存抗生素信息
        /// </summary>
        /// <param name="AntibioticInfo"></param>
        /// <returns></returns>
        public ActionResult SubmitKssForm(SysMedicineAntibioticInfoVO AntibioticInfo)
        {
            AntibioticInfo.OrganizeId = OrganizeId;
            AntibioticInfo.zt = "1";
            AntibioticInfo.LastModifierCode = UserIdentity.UserCode;
            AntibioticInfo.LastModifyTime = DateTime.Now;
            var getId = _sysMedicineBaseDmnService.SubmitForm(AntibioticInfo);
            return string.IsNullOrWhiteSpace(getId) ? Error("保存抗生素失败") : Success("", getId);
        }

        #region 自动授权药房药库


        /// <summary>
        /// 获取所有药房药库
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllYfbm()
        {
            var result = _sysPharmacyDepartmentBaseDmnService.GetEffectiveList(OrganizeId, null);
            return Success("", result.ToJson());
        }

        /// <summary>
        /// 获取该药品已授权的药房药库
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        public ActionResult GetEmpowermentPharmacyDepartment(string ypId)
        {
            var result = _sysPharmacyDepartmentDmnService.SelectEmpowermentYfbmByYp(ypId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 提交授权药房药库
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypCode">药品代码</param>
        /// <param name="keyValue">药品ID</param>
        /// <returns></returns>
        //public ActionResult SubmitEmpowermentPharmacyDepartment(List<string> p, string ypCode, int? keyValue)
        //{
        //    var result = _sysPharmacyDepartmentApp.SubmitEmpowermentPharmacyDepartment(keyValue, ypCode, OrganizeId, UserIdentity.UserCode, p);
        //    return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        //}

        public ActionResult GetItemDetailsByCode(string name)
        {
            string result = _sysMedicineDmnService.GetTradePricePlus(name);
            return Content(result); ;
        }

        #endregion


        //药品用法下拉浮层
        public ActionResult GetUsageFloat(string keyword = null)
        {
            var list = _sysMedicineDmnService.GetUsageFloat(keyword);
            return Content(list.ToJson());
        }


        #region base基础数据

        /// <summary>
        /// 根据关键字查询药品分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetValidList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //下拉框数据绑定
        public ActionResult GetSelectJson(string code)
        {
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, "");
            var list = new List<object>();
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        //收费大类树形（医院） 下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _sysMedicineBaseDmnService.GetsfdlValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlCode;
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                        : null;
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlId.ToString();
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        public JsonResult GetypjxListSelectJson(string keyword)
        {
            var list = _sysMedicineBaseDmnService.GetypjxValidList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取药品单位列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GetypdwListSelectJson(string keyword)
        {
            var data = _sysMedicineBaseDmnService.GetypdwValidList(keyword);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GettsypbzSelectJson(string code, string keyword, string OrganizeId)
        {
            List<object> list = new List<object>();
            if (code == "tsypbz")
            {
                foreach (var item in System.Enum.GetValues(typeof(EnumYpsx)))
                {
                    object name = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                    list.Add(new { id = (int)item, text = (name as DescriptionAttribute).Description });
                }
                return Content(list.ToJson());
            }
            var data = _sysMedicineBaseDmnService.GetValidListByItemCode(code, keyword, OrganizeId);
            foreach (SysItemsDetailVO item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        #endregion


        #region 系统药品_药房部门

        /// <summary>
        /// 授权药房部门
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        public string SubmitEmpowermentPharmacyDepartment(int? ypId, string ypCode, string organizeId, string userCode, List<string> epds)
        {
            if (epds == null || epds.Count == 0) return "";

            var ypxx = _sysMedicineDmnService.SelectMedicineInfo(ypCode, organizeId);
            if (ypxx == null) return string.Format("授权药房失败，根据药品代码【{0}】未找到药品信息", ypCode);
            var yfbmyp = _sysMedicineBaseDmnService.SelectDepartmentMedicine(ypxx.dlCode, organizeId);

            return ypId == null ? NewEpd(yfbmyp, ypxx, epds) : UpdateEpd(yfbmyp, ypxx, epds);
        }

        /// <summary>
        /// 新增药房部门授权
        /// </summary>
        /// <param name="yfbmyp"></param>
        /// <param name="ypxx"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        private string NewEpd(IList<PharmacyDepartmentOpenMedicineRepoVO> yfbmyp, SysMedicineVO ypxx, List<string> epds)
        {
            var result = new StringBuilder();
            epds.Distinct().ToList().ForEach(p =>
            {
                if (yfbmyp == null || yfbmyp.Count == 0)
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                else if (yfbmyp.All(o => o.yfbmCode != p))
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                var reqObj = new EmpowermentPharmacyDepartmentRequestDto
                {
                    bmypId = Guid.NewGuid().ToString(),
                    yfbmCode = p,
                    Ypdm = ypxx.ypCode,
                    OrganizeId = ypxx.OrganizeId,
                    Ypkw = "",
                    Zcxh = "",
                    px = null,
                    Pxfs1 = "",
                    Pxfs2 = "",
                    Kcsx = 0,
                    Kcxx = 0,
                    Jhd = 0,
                    Jhl = 0,
                    Ypsxdm = null,
                    Sysx = 0,
                    Ylsx = 0,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = ypxx.CreatorCode,
                    LastModifierCode = "",
                    LastModifyTime = null,
                    Timestamp = DateTime.Now
                };

                //var apiResp = SitePdsApiHelper.Request<APIRequestHelper.DefaultResponse>("api/PharmacyDepartment/EmpowermentPharmacyDepartment", reqObj);
                //var response = apiResp.data.ToString();
                var response = EmpowermentPharmacyDepartment(reqObj).ToString();
                if (!string.IsNullOrWhiteSpace(response)) result.Append(response);
            });
            return result.ToString();
        }

        /// <summary>
        /// 新增药房部门授权
        /// </summary>
        /// <param name="yfbmyp"></param>
        /// <param name="ypxx"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        private string UpdateEpd(IList<PharmacyDepartmentOpenMedicineRepoVO> yfbmyp, SysMedicineVO ypxx, List<string> epds)
        {
            var result = new StringBuilder();
            var request = new EmpowermentPharmacyDepartmentAndRemoveOldRequestDto
            {
                epds = new List<EmpowermentPharmacyDepartment>(),
                Timestamp = DateTime.Now
            };
            epds.Distinct().ToList().ForEach(p =>
            {
                if (yfbmyp == null || yfbmyp.Count == 0)
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                else if (yfbmyp.All(o => o.yfbmCode != p))
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                request.epds.Add(new EmpowermentPharmacyDepartment
                {
                    bmypId = Guid.NewGuid().ToString(),
                    yfbmCode = p,
                    Ypdm = ypxx.ypCode,
                    OrganizeId = ypxx.OrganizeId,
                    Ypkw = "",
                    Zcxh = "",
                    px = null,
                    Pxfs1 = "",
                    Pxfs2 = "",
                    Kcsx = 0,
                    Kcxx = 0,
                    Jhd = 0,
                    Jhl = 0,
                    Ypsxdm = null,
                    Sysx = 0,
                    Ylsx = 0,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = ypxx.CreatorCode,
                    LastModifierCode = "",
                    LastModifyTime = null
                });
            });
            //var apiResp = SitePdsApiHelper.Request<APIRequestHelper.DefaultResponse>("api/PharmacyDepartment/EmpowermentPharmacyDepartmentAndRemoveOld", request);
            //var response = apiResp.data.ToString();
            var response = EmpowermentPharmacyDepartmentAndRemoveOld(request).ToString();
            if (!string.IsNullOrWhiteSpace(response)) result.Append(response);
            return result.ToString();
        }


        /// <summary>
        /// 插入药房部门药品
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypxx"></param>
        /// <returns></returns>
        private string InsertSysPharmacyDrug(string p, SysMedicineVO ypxx)
        {
            var yfbmypEntity = new SysPharmacyDepartmentOpenMedicineVO
            {
                Id = Guid.NewGuid().ToString(),
                yfbmCode = p,
                dlCode = ypxx.dlCode,
                OrganizeId = OrganizeId,
                CreatorCode = ypxx.CreatorCode,
                CreateTime = DateTime.Now,
                zt = "1",
                px = null
            };

            var r1 = _sysMedicineBaseDmnService.Insertyfbmyp(yfbmypEntity);
            return r1 <= 0 ? string.Format("药房部门{0}赋予药品大类{1}权限失败;", p, ypxx.dlCode) : "";
        }


        /// <summary>
        /// 药品授权药房部门
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.bmypId)) return "请传入有效的本部门医药品信息；";
            var oldData = _sysPharmacyDepartmentMedicineRepo.SelectData(req.Ypdm, req.OrganizeId, req.yfbmCode);
            if (oldData != null && oldData.Count > 0)
            {
                if (oldData.Any(p => p.zt == "1")) return "";
                var item = oldData.FirstOrDefault();
                if (item != null)
                {
                    var t = _sysPharmacyDepartmentMedicineRepo.UpdateZt(item.bmypId, "1", req.CreatorCode, req.CreateTime);
                    return t > 0 ? "" : string.Format("修改授权部门药品为【{0}】的药品【{1}】失败；", req.yfbmCode, req.Ypdm);
                }
            }
            var bmypxx = new SysPharmacyDepartmentMedicineEntity
            {
                bmypId = req.bmypId,
                yfbmCode = req.yfbmCode,
                Ypdm = req.Ypdm,
                OrganizeId = req.OrganizeId,
                Ypkw = req.Ypkw,
                Zcxh = req.Zcxh,
                px = req.px,
                Pxfs1 = req.Pxfs1,
                Pxfs2 = req.Pxfs2,
                Kcsx = req.Kcsx,
                Kcxx = req.Kcxx,
                Jhd = req.Jhd,
                Jhl = req.Jhl,
                Ypsxdm = req.Ypsxdm,
                Sysx = req.Sysx,
                Ylsx = req.Ylsx,
                zt = req.zt,
                CreateTime = req.CreateTime,
                CreatorCode = req.CreatorCode,
                LastModifierCode = req.LastModifierCode,
                LastModifyTime = req.LastModifyTime
            };
            var rt = _sysPharmacyDepartmentMedicineRepo.Insert(bmypxx);
            return rt > 0 ? "" : string.Format("新增授权部门药品为【{0}】的药品【{1}】失败；", req.yfbmCode, req.Ypdm);
        }

        /// <summary>
        /// 药品授权药房部门 与EmpowermentPharmacyDepartment不同，该方法会先取消该药品所有药房的授权，在重新赋予权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto req)
        {
            var result = new StringBuilder();
            if (req == null || req.epds == null || req.epds.Count == 0) return "请传入有效的本部门医药品信息；";
            req.epds.Select(p => p.Ypdm).Distinct().ToList().ForEach(o =>
            {
                _sysPharmacyDepartmentMedicineRepo.DeleteItem(o, req.epds[0].OrganizeId);
            });
            req.epds.ForEach(p =>
            {
                var tp = p.ToJson().ToObject<EmpowermentPharmacyDepartmentRequestDto>();
                result.Append(EmpowermentPharmacyDepartment(tp));
            });
            return result.ToString();
        }

        #endregion

        #region 限用

        public ActionResult Getybbxbldata(string keyValue)
        {
            var list = _sysMedicineBaseDmnService.Getybbxbldata(keyValue, this.OrganizeId);
            return Content(list.ToJson());
        }

        public ActionResult SaveYbblValue(List<Sh_YbfyxzblVO> entity, string xmbm, string xmmc)
        {
            _sysMedicineBaseDmnService.SaveYbblValue(entity, xmbm, xmmc, this.OrganizeId, this.UserIdentity.rygh);
            return Success();
        }
        #endregion
    }
}