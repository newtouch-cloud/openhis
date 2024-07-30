using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL.ShangHai
{
    public class Ybjk_SN01_Input:SqlBase
    {
        public string Id { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

        public string cardtype { get; set; }

        public string carddata { get; set; }
        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }
        /// <summary>
        /// 登记号
        /// </summary>
        public string djh { get; set; }
        /// <summary>
        /// 明细账单号:初始为空, 后续上传需要将返回的明细账单号填入
        /// </summary>
        public string mxzdh { get; set; }
        /// <summary>
        /// 本次费用明细包的医疗费用总额
        /// </summary>
        public decimal bcmxylfyze { get; set; }
        /// <summary>
        /// 结算类型标志
        /// </summary>

        public string jslxbz { get; set; }
        /// <summary>
        /// 上传返回的明细账单号
        /// </summary>
        public string fhmxzdh { get; set; }

    }


}
