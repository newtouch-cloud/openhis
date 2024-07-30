using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 外部入库
    /// </summary>
    public class InStorageController : ControllerBase
    {
        private readonly ISysMedicineCrkfsDmnService _iSysMedicineCrkfsDmnService;

        /// <summary>
        /// 提交外部入库
        /// </summary>
        /// <param name="djInfoDto"></param>
        /// <returns></returns>
        public ActionResult SubmitInStorage(DjInfoDTO djInfoDto)
        {
            djInfoDto.djlx = (int)EnumDanJuLX.yaopinruku;
            djInfoDto.rkbm = Constants.CurrentYfbm.yfbmCode;
            var result = new InStorageProcess(djInfoDto).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }

        /// <summary>
        /// 初始化单据号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InitDjh(string djmc)
        {
            return Success(null, EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo(djmc, Constants.CurrentYfbm.yfbmCode, OrganizeId));
        }

        /// <summary>
        /// 获取出入库方式
        /// </summary>
        /// <param name="crkbz">0:入库  1：出库</param>
        /// <returns></returns>
        public ActionResult GetCrkfs(string crkbz)
        {
            var result = _iSysMedicineCrkfsDmnService.GetList(crkbz);
            return Content(result.ToJson());
        }
    }
}