using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SN01MZ:Post_Base
    {
        /// <summary>
        /// 0挂号  1处方 3 处方退费在传
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 处方内码
        /// </summary>
        public string cfnm { get; set; }
        /// <summary>
        /// 挂号项目
        /// </summary>
        public string ghxm { get; set; }
        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// ==磁卡费
        /// </summary>
        public string ckf { get; set; }
        /// <summary>
        /// 工本费
        /// </summary>
        public string gbf { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }
        /// <summary>
        /// 登记号
        /// </summary>
        public string djh { get; set; }
        /// <summary>
        /// 本次费用明细包的医疗费用总额
        /// </summary>
        public decimal bcmxylfyze { get; set; }

        /// <summary>
        /// 明细账单号  
        /// </summary>
        public string mxzdh { get; set; }
        /// <summary>
        /// 结算类型标志
        /// </summary>
        public string jslxbz { get; set; }
        /// <summary>
        /// @jsnm varchar(50) --结算内码
        /// </summary>
        public string jsnm { get; set; }

        public string ksbm { get; set; }

        public string ysbm { get; set; }

        public string dise_codg { get; set; }

        public decimal ybzje { get; set; }
        public decimal zfzje { get; set; }
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 门诊半退结算 (退结算明细内码和数量)
        /// </summary>
        public IList<tjsxmDict> tjsxmDict { get;set; }
    }
    public class tjsxmDict
    {
        public string tjsmxnm { get; set; }
        public decimal tsl { get; set; }
    }
}
