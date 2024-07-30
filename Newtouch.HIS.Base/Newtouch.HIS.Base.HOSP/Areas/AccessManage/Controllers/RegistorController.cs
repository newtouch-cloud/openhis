using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.AccessManage.Controllers
{
    public class RegistorController : ControllerBase
    {
        // GET: AccessManage/Registor
        private readonly IAccessAuthRepo _accessAuthRepo;
        public RegistorController(IAccessAuthRepo accessAuthRepo)
        {
            this._accessAuthRepo = accessAuthRepo;
        }

        public ActionResult Form()
        {
            return View();
        }
        public ActionResult GetRegistorList(Pagination pagination, string keyword,string org)
        {
            var data = new
            {
                rows = _accessAuthRepo.FindList(pagination, keyword,org),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public ActionResult GetRegistorForm(string keyValue)
        {
            var data = _accessAuthRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }


        public ActionResult submitForm(AccessAuthEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.RegCode))
            {
                throw new FailedException("请填写编码");
            }
            if (string.IsNullOrWhiteSpace(entity.RegName))
            {
                throw new FailedException("请填写名称");
            }
            _accessAuthRepo.RegistProcess(entity, keyValue);
            return Success("操作成功");
        }
        /// <summary>
        /// 获取授权密钥
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetAccessKey(string keyValue)
        {             
            var ety = _accessAuthRepo.FindEntity(keyValue,"1");
            if (ety != null)
            {
                AccessIdentity au = new AccessIdentity();
                au.accesskey = "API_" + ety.RegCode + "_" + ety.Id;
                au.AuthorizedLev = ety.AuthorizedLev.ToString();
                au.AuthorizedPeriod = ety.AuthorizedPeriod.ToString();
                au.RegCode = ety.RegCode;
                au.RegName = ety.RegName;
                au.Timestamp =string.IsNullOrWhiteSpace(ety.accesskey)==true? DateTime.Now:Convert.ToDateTime(ety.accesskey);
                au.AppId= ety.RegCode;
                au.UserId = au.accesskey;
                au.UserName= ety.RegName;                
                au.Account = au.RegCode;
                au.OrganizeId = ety.OrganizeId;
                var auth = "";
                string token = auth.GetType().GetProperty("token").GetValue(auth, null).ToString() ;
                string tokenType = auth.GetType().GetProperty("tokentype").GetValue(auth, null).ToString();
                au.TokenType = tokenType;
                var data = new { token = token, tokenType = tokenType, AppId = ety.RegCode, regtime=au.Timestamp };                
                return Success("生成密钥成功", data.ToJson());
            }
            return Error("注册信息无效");
        }
    }
}