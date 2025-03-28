using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{

    public class AllergyController : OrgControllerBase
    {
        private readonly IAllergyManageDmnService _IAllergyManageDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        #region 视图
        public ActionResult OutHosAllergyManage()
        {
            return View();
        }

        public ActionResult AllergyForm()
        {
            return View();
        }

        public ActionResult AllergyInfoQuery()
        {
            return View();
        }

        public ActionResult InHosAllergyManage()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取门诊患者列表 (待做皮试)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMzPatientListJson(Pagination pagination, string keyword, string ks, string kssj, string jssj, string zxzt)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("请联系管理员配置过敏项目配置动态参数,参数编码：AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetOutPatientPaginationList(pagination, keyword, ks, kssj, jssj, this.OrganizeId, PsItmeCode, zxzt);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取住院患者列表 (待做皮试)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetZyPatientListJson(Pagination pagination, string keyword, string ks, string kssj, string jssj, string zxzt)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("请联系管理员配置过敏项目配置动态参数,参数编码：AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetInPatientPaginationList(pagination, keyword, ks, kssj, jssj, this.OrganizeId, PsItmeCode, zxzt);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取门诊患者皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="mzzybz">1门诊，2住院</param>
        /// <returns></returns>
        public ActionResult GetMzPatientPsItemsJson(Pagination pagination, string Id)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("请联系管理员配置过敏项目配置动态参数,参数编码：AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetMzPatientPsItemsList(pagination, Id, this.OrganizeId, PsItmeCode);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取住院患者皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="mzzybz">1门诊，2住院</param>
        /// <returns></returns>
        public ActionResult GetZyPatientPsItemsJson(Pagination pagination, string zyh)
        {
            var PsItmeCode = _sysConfigRepo.GetValueByCode("AllergyItemSetting", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(PsItmeCode)) throw new Exception("请联系管理员配置过敏项目配置动态参数,参数编码：AllergyItemSetting");
            var patList = _IAllergyManageDmnService.GetZyPatientPsItemsList(pagination, zyh, this.OrganizeId, PsItmeCode);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取过敏信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPatientGmxxListJson(Pagination pagination, string keyword)
        {
            var patList = _IAllergyManageDmnService.GetPatientGmxxList(pagination, keyword, this.OrganizeId);

            var data = new
            {
                rows = patList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取抗生素药品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetKssYpListJson()
        {
            var list = _IAllergyManageDmnService.GetKssYpListData(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取抗生素药品类别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetKssYpLbJson(string ypCode)
        {
            if (string.IsNullOrEmpty(ypCode))
            {
                return Success(null, null);
            }
            var list = _IAllergyManageDmnService.GetKssYpDlData(ypCode, this.OrganizeId);
            return Success(null, list);
        }
        #endregion

        #region 保存数据
        public ActionResult SaveAllergyInfo(AllergyEntity postData)
        {
            postData.Id = Guid.NewGuid().ToString();
            postData.OrganizeId = this.OrganizeId; 
            postData.CreatorCode = UserIdentity.UserCode;
            postData.CreatorName = UserIdentity.UserName;
            postData.CreateTime = DateTime.Now;
            postData.zt = "1";

            _IAllergyManageDmnService.SaveAllergyInfo(postData);
            return Success();
        }


        /// <summary>
        /// 删除执行的过敏信息
        /// </summary>
        /// <param name="xmCode"></param>
        /// <returns></returns>
        public ActionResult DeleteExecutedGmxx(string gmxxId)
        {
            int ret = _IAllergyManageDmnService.DeleteAllergyInfo(gmxxId, this.OrganizeId, UserIdentity.UserCode, UserIdentity.UserName);
            return ret > 0 ? Success("操作成功") : Error("取消执行失败，请联系管理员！");
        }
        #endregion
    }
}