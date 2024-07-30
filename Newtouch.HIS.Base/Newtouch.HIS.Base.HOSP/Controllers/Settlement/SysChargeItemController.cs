using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysChargeItemController : ControllerBase
    {
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;

        public SysChargeItemController(ISysChargeItemRepo _sysChargeItemRepo
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysChargeItemDmnService sysChargeItemDmnService)
        {
            this._sysChargeItemRepo = _sysChargeItemRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
            this._sysChargeItemDmnService = sysChargeItemDmnService;
        }

        public ActionResult YbbxblForm()
        {
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string organizeId, string keyword,string sfdl)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (!_SysOrganizeDmnService.IsMedicalOrganize(organizeId))
            {
                return Content(new
                {
                    rows = new List<SysChargeItemVO>(),
                    total = 0,
                    page = pagination.page,
                    records = 0,
                }.ToJson());
            }
            var list = _sysChargeItemDmnService.GetPagintionList(organizeId, pagination, sfdl, keyword);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysChargeItemRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeItemEntity entity, int? keyValue)
        {
            entity.ssbz = entity.ssbz == "true" ? "1" : "0";
            entity.tsbz = entity.tsbz == "true" ? "1" : "0";
            entity.sfbz = entity.sfbz == "true" ? "1" : "0";
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _sysChargeItemRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 收费项目同步医保
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult YibaoUploadApi(int sfxmId, string flag)
        {
            if (sfxmId <= 0)
            {
                return Success("请选择同步的项目！");
            }
            var sfxmEntity = _sysChargeItemRepo.GetForm(sfxmId);
            if (!string.IsNullOrWhiteSpace(sfxmEntity.ybdm))
            {
                var Result = newtouchyibao.YibaoUniteInterface.Mcatalogrl("Z092", "1", sfxmEntity.ybdm, sfxmEntity.sfxmCode, sfxmEntity.sfxmmc, null, null, null, null, sfxmEntity.dj, flag);
                if (Result.Code == 0 && Result.Data.Count > 0 && Result.Data[0].TPCODE == 0)
                {
                    string error = "";
                    if (!_sysChargeItemRepo.YibaoUpload(sfxmId, out error))
                    {
                        return Error(error);
                    }
                    return Success(string.Format("[{0}]医保同步成功！", sfxmEntity.sfxmmc));
                }
                else
                {
                    return Error(string.Format("[{0}]医保同步失败：{1}", sfxmEntity.sfxmmc, Result.ErrorMsg));
                }
            }
            else
            {
                return Success("缺少医保代码！");
            }
        }

        public ActionResult Getybbxbldata(string keyValue)
        {
            var list = _sysChargeItemDmnService.Getybbxbldata(keyValue, this.OrganizeId);
            return Content(list.ToJson());
        }

        public ActionResult SaveYbblValue(List<Sh_YbfyxzblEntity> entity, string xmbm,string xmmc)
        {
            _sysChargeItemDmnService.SaveYbblValue(entity, xmbm, xmmc, this.OrganizeId,this.UserIdentity.rygh);
            return Success();
        }
    }
}