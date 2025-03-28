using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintInvoiceRegisterVO
    {
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

        public string KsNo { get; set; }



        #region 挂号显示内容
        /// <summary>
        /// 
        /// </summary>
        public string P_Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_GH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_KS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_BKS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string P_YsName { get; set; }
        #endregion

    }
}
