using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.Infrastructure;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage.Controllers
{
    /// <summary>
    /// 出库至科室
    /// </summary>
    public class DeliveryToDepartmentController : ControllerBase
    {

        private readonly IHandOutMedicineDmnService _handOutMedicineDmnService;

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="djInfoDTO"></param>
        /// <returns></returns>
        public ActionResult SubmitDeliveryToDepartment(DjInfoDTO djInfoDTO)
        {
            djInfoDTO.djlx = (int)EnumDanJuLX.keshifayao;
            djInfoDTO.ckbm = Constants.CurrentYfbm.yfbmCode;
            var result = new DeliveryToDepartmentProcess(djInfoDTO).Process();
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
    }
}