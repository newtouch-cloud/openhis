using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.OrderCenter
{
    public class OrderCreateRequest : RequestBase
    {

        /// <summary>
        /// 订单类型 EnumOrderType
        /// </summary>
        [Required(ErrorMessage ="请传入订单类型")]
        public int? OrderType { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Required(ErrorMessage ="就诊卡号不可为空")]
        public string CardNo { get; set; }
        /// <summary>
        /// 本次就诊标识  门诊：mzh；住院：zyh
        /// </summary>
        [Required(ErrorMessage ="门诊订单请传入门诊号，住院订单请传入住院号")]
        public string VisitNo { get; set; }
        public int patId { get; set; }
        public string dzId { get; set; }
    }
}
