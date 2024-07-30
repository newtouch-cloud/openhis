using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_3201
    {


        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        ///  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        ///  4  stmt_begndate 对账开始日期  日期型 Y
        /// </summary>
        public DateTime stmt_begndate { get; set; }
        /// <summary>
        /// /5  stmt_enddate 对账结束日期  日期型
        /// </summary>
        public DateTime stmt_enddate { get; set; }
        /// <summary>
        /// //1 insutype 险种  字符型  6  Y Y 
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        ///  //2  clr_type 清算类别  字符型  6  Y Y 
        /// </summary>
        public string clr_type { get; set; }
        /// <summary>
        /// //3  setl_optins 结算经办机构
        /// </summary>
        public string setl_optins { get; set; }

		public decimal? medfee_sumamt { get; set; }

		public decimal? fund_pay_sumamt { get; set; }
		public decimal? acct_pay { get; set; }
		public int fixmedins_setl_cnt { get; set; }
		public string refd_setl_flag { get; set; }

		/// <summary>
		/// 上传文件名称 对账明细的时候需要上传 给值 
		/// </summary>
		public string fileName { get; set; }


    }
}
