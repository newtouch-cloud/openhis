using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class FinanceReceiptVO
    {
        /// <summary>
        /// 财务收据ID
        /// </summary>
        public string cwsjId { get; set; }

        /// <summary>
        /// 人员
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// 起始收据号
        /// </summary>
        public long qssjh { get; set; }

        /// <summary>
        /// 当前收据号
        /// </summary>
        public long dqsjh { get; set; }

        /// <summary>
        /// 结束收据号
        /// </summary>
        public long? jssjh { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 0:停用; 1:启用
        /// </summary>
        public string zt { get; set; }
    }

    public class FinanceInvoiceVO
    {
        public string fpdm { get; set; }
        public string szm { get; set; }
        public string lyry { get; set; }
        public long dqfph { get; set; }
        public long qsfph { get; set; }
        public long jsfph { get; set; }

        /// <summary>
        /// 0:停用; 1:启用
        /// </summary>
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string ry { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public int? is_del { get; set; }
    }

    /// <summary>
    /// 发票详情
    /// </summary>
    public class InvoiceDetailVO
    {
        /// <summary>
        /// 就诊类型 门诊 | 住院
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string jzh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime sfrq { get; set; }
    }
}
