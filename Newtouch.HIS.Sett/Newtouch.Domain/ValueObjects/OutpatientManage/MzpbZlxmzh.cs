using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 门诊排班治疗项目组合
    /// </summary>
    public class MzpbZlxmzh
    {

        public string OrganizeId { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 诊疗组合名称
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// 诊疗组合编码
        /// </summary>
        public string zhcode { get; set; }
        /// <summary>
        /// 明细项目排序
        /// </summary>
        public string ord { get; set; }
        /// <summary>
        /// 诊疗明细项目
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// 诊疗明细项目名称
        /// </summary>
        public string zlxmmc { get; set; }
        /// <summary>
        /// 诊疗明细项目单价
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 诊疗明细项目拼音
        /// </summary>
        public string zlxmpy { get; set; }
        /// <summary>
        /// 组合金额
        /// </summary>
        public decimal zhje { get; set; }
        /// <summary>
        /// 收费大类
        /// </summary>
        public string sfdl { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
