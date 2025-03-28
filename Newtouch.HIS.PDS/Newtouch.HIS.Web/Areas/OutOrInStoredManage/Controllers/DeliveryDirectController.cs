using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 直接出库
    /// </summary>
    public class DeliveryDirectController : ControllerBase
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitDeliveryDirect(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.djlx = (int)EnumDanJuLX.zhijiefayao;
            djInfoDTO.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new DeliveryDirectProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}