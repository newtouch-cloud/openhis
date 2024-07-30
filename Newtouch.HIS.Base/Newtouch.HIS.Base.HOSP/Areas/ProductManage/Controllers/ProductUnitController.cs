using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资单位
    /// </summary>
    public class ProductUnitController : ControllerBase
    {
        private readonly IWzUnitRepo _wzUnitRepo;
        private readonly IWzUnitApp _wzUnitApp;

        public ProductUnitController(IWzUnitRepo wzUnitRepo, IWzUnitApp wzUnitApp)
        {
            _wzUnitRepo = wzUnitRepo;
            _wzUnitApp = wzUnitApp;
        }

        /// <summary>
        /// 获取物资单位列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetUnitGridJson(Pagination pagination, string keyWord = "")
        {
            pagination.sidx = "name";
            pagination.sord = "asc";
            var data = new
            {
                rows = _wzUnitRepo.FindList(p => "" == keyWord || p.name.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// get enable product unit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProductUnitSelectJson()
        {
            var list = _wzUnitRepo.IQueryable(p => p.zt == "1").ToList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            return _wzUnitRepo.DeleteUnitById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// get unit information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// 物资单位维护 表单提交
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            wzUnitEntity.zt = wzUnitEntity.zt == "true" ? "1" : "0";
            return _wzUnitApp.SubmitForm(wzUnitEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }
    }
}