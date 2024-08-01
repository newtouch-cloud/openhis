using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:04
    /// 描 述：字典项
    /// </summary>
    [AutoResolveIgnore]
    public class SysItemsDataController : BaseController
    {
        private readonly ISysItemsDataRepo _sysItemsDataRepo;
        private readonly IItemDmnService _itemDmnService;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysItemsDataRepo"></param>
        /// <param name="itemDmnService"></param>
        public SysItemsDataController(ISysItemsDataRepo sysItemsDataRepo
            , IItemDmnService itemDmnService)
        {
            this._sysItemsDataRepo = sysItemsDataRepo;
            this._itemDmnService = itemDmnService;
        }

        /// <summary>
        /// 下拉 by Code 获取字典项
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string code)
        {
            var data = _itemDmnService.GetValidListByItemCode(code);
            List<object> list = new List<object>();
            foreach (SysItemsDataEntity item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var list = _sysItemsDataRepo.GetList(itemId, keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysItemsDataRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysItemsDataEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysItemsDataRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}