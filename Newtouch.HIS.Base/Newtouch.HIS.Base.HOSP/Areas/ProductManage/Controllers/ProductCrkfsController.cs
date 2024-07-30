using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public class ProductCrkfsController : ControllerBase
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;
        private readonly IWzCrkfsApp _wzCrkfsApp;

        public ProductCrkfsController(IWzCrkfsRepo wzCrkfsRepo, IWzCrkfsApp wzCrkfsApp)
        {
            _wzCrkfsRepo = wzCrkfsRepo;
            _wzCrkfsApp = wzCrkfsApp;
        }

        /// <summary>
        /// 获取出入库方式列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetCrkfsGridJson(Pagination pagination, string keyWord = "")
        {
            pagination.sidx = "crkfsmc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _wzCrkfsRepo.FindList(p => "" == keyWord || p.crkfsmc.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            return _wzCrkfsRepo.DeleteCrkfsById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// 获取出入库方式信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// 物资单位维护 表单提交
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            wzCrkfsEntity.zt = wzCrkfsEntity.zt == "true" ? "1" : "0";
            return _wzCrkfsApp.SubmitForm(wzCrkfsEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }
    }
}