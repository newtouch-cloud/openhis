using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage
{
    public class YibaoDataVo
    {
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 结算ID
        /// </summary>
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public DateTime? setl_time { get; set; }
        public string zymzh { get; set; }
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// 交易入参报文ID 冲正交易使用
        /// </summary>
        public string medins_setl_id { get; set; }
        public string cbdbm { get; set; }
        /// <summary>
        /// 交易编码
        /// </summary>
        public string infno { get; set; }
        /// <summary>
        /// 交易错误信息
        /// </summary>
        public string errormsg { get; set; }
        /// <summary>
        /// 单边帐
        /// </summary>
        public string dbz { get; set; }
        public int? jsnm { get; set; }

        public decimal? xjzf { get; set; }
    }
}
