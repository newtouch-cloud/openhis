using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 内部发药申请
    /// </summary>
    public class ApplyController : ControllerBase
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitApply(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.rkbm = Constants.CurrentYfbm.yfbmCode;
            djInfoDTO.djlx = (int)EnumSldlx.neibushenlingdan;
            var result = new ApplyProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}