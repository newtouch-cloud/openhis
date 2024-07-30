using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
	public class Ybjk_DZ_output: SqlBase
	{
		/// <summary>
		/// 对帐日
		/// </summary>
		public string daycollate { get; set; }
		/// <summary>
		/// 对帐日中心流水号数量
		/// </summary>
		public int daycount { get; set; }
		/// <summary>
		/// 当年帐户支付总额
		/// </summary>
		public decimal totalcuraccpay { get; set; }
		/// <summary>
		/// 历年帐户支付总额
		/// </summary>
		public decimal totalhisaccpay { get; set; }
		/// <summary>
		/// 现金自负总额
		/// </summary>
		public decimal totalcashpay { get; set; }
		/// <summary>
		/// 统筹支付总额
		/// </summary>
		public decimal totaltcpay { get; set; }
		/// <summary>
		/// 附加支付总额
		/// </summary>
		public decimal totaldffjpay { get; set; }
		/// <summary>
		/// 分类自负总额
		/// </summary>
		public decimal totalflzf { get; set; }
		/// <summary>
		/// 非医保结算范围费用总额总额
		/// </summary>
		public decimal totalfybjsfw { get; set; }
		/// <summary>
		/// 返回对账结果
		/// </summary>
		public string resultcollate { get; set; }
		/// <summary>
		/// 返回对账日
		/// </summary>
		public string outdaycollate { get; set; }

		public DateTime? czrq { get; set; }

		public int zt { get; set; }

		public DateTime? zt_rq { get; set; }
        /// <summary>
        /// 对账状态 0对账失败 1对账成功
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 对账结果输出显示
        /// </summary>
        public string Result { get; set; }
    }
}
