using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.Settlement;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysWardRoomController : ControllerBase
    {
        private readonly ISysWardRoomRepo _SysWardRoomRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysWardRoomDmnService _SysWardRoomDmnService;

        public SysWardRoomController(ISysWardRoomRepo SysWardRoomRepo, ISysOrganizeDmnService SysOrganizeDmnService,ISysWardRoomDmnService SysWardRoomDmnService)
        {
            this._SysWardRoomRepo = SysWardRoomRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._SysWardRoomDmnService = SysWardRoomDmnService;

        }
        // GET: SysWardRoom
        //public override ActionResult Index()
        //{
        //    return View();
        //}
        //public override ActionResult Form()
        //{
        //    return View();
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _SysWardRoomDmnService.GetPagintionList(pagination, organizeId, keyword),
                //rows = _SysWardRoomRepo.GetPagintionList(pagination, organizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _SysWardRoomRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysWardRoomEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _SysWardRoomRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }

        #region 提供字典查询
        /// <summary>
        /// 病放下拉
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public ActionResult GetWardRoomValidSelect(string orgId,string bqCode)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _SysWardRoomDmnService.GetWardRoomListValid(orgId,bqCode);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.bfCode;
                treeModel.text = item.bfNo;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        #endregion

    }
}