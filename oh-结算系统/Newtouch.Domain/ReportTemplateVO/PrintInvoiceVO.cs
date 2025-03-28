using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintInvoiceVO
    {
        /// <summary>
        /// 窗口 
        /// </summary>
        public string Xwindow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fph { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string ItemSubReport { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TotalStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SocialPay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExtraPay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayForClass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayForSelf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrivateExpense { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CurrentRemain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PastRemain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cashier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 支付方式 如：现金:xx元，pos：yy元
        /// </summary>
        public string PayFuc { get; set; }
        /// <summary>
        /// 分币误差
        /// </summary>
        public string XjWc { get; set; }
        /// <summary>
        /// 合计 如：应收xxx预收yyy找零zzz
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 科室 如：疼痛科
        /// </summary>
        public string Ks { get; set; }
        /// <summary>
        /// True为打印：此收据部分项目名称，内容进行了简化。详细信息请在触摸屏等正式公示途径中查询。
        /// </summary>
        public string IsFull { get; set; }

    }
}
