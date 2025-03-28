using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysMedicalOrderFrequencyController : ControllerBase
    {
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyRepo;

        public SysMedicalOrderFrequencyController(ISysMedicalOrderFrequencyRepo sysMedicalOrderFrequencyRepo)
        {
            this._sysMedicalOrderFrequencyRepo = sysMedicalOrderFrequencyRepo;
        }
        // GET: SysMedicalOrderFrequency
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId)
        {
            entity.zbz = entity.zbz ?? "";  //数据类型问题
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysMedicalOrderFrequencyRepo.SubmitForm(entity, yzpcId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public ActionResult GetGridJson(string keyword,string orgId)
        {
            var data = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyList(orgId,keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        public ActionResult GetFormJson(int? yzpcId,string orgId)
        {
            var entity = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyEntity(yzpcId,orgId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        public ActionResult DeleteForm(int yzpcId,string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysMedicalOrderFrequencyRepo.DeleteForm(orgId,yzpcId);
            return Success("操作成功。");
        }


    }
}