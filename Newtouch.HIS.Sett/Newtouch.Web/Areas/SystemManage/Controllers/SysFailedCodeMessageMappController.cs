using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-09-30 14:54
    /// 描 述：错误提示配置
    /// </summary>
    public class SysFailedCodeMessageMappController : ControllerBase
    {
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _sysFailedCodeMessageMappRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysFailedCodeMessageMappRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue)
        {
            _sysFailedCodeMessageMappRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _sysFailedCodeMessageMappRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}