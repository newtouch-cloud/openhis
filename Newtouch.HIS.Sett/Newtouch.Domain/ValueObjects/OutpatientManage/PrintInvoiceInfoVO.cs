
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class PrintInvoiceInfoVO
    {
        /// <summary>
        /// 是否挂号
        /// </summary>
        public bool IsGh;
        public PrintInvoiceInfoVO(bool isGh = false)
        {
            this.IsGh = isGh;
        }
        /// <summary>
        /// 窗口 
        /// </summary>
        public List<string> Xwindow { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 医疗机构类型
        /// </summary>
        public string DepartmentType { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 医保类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 社会保障号码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 项目 金额
        /// </summary>
        public List<KeyValuePair<string, string>> ItemPrice = new List<KeyValuePair<string, string>>();
        /// <summary>
        /// 项目编码 名称 规格 数量 单价 金额（元）
        /// </summary>
        public List<Dictionary<string, string>> ItemDetails = new List<Dictionary<string, string>>();
        /// <summary>
        /// 合计 如：应收xxx预收yyy找零zzz
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 挂号明细
        /// 患者姓名:Name 病人内码:ID 普通或者专家:YS 科室:KS 科室流水号:KsNo 日期:dateTime 医生名称:YsName
        /// </summary>
        public Dictionary<string, string> GhItemDetails = new Dictionary<string, string>(){
            {"Name" ,string.Empty},
            {"ID",string.Empty},
            {"YS",string.Empty},
            {"KS",string.Empty},
            {"KsNo",string.Empty},
            {"dateTime",string.Empty},
            {"YsName",string.Empty}
        };
        /// <summary>
        /// 科室 如：疼痛科
        /// </summary>
        public string Ks { get; set; }
        /// <summary>
        /// 合计（大写）
        /// </summary>
        public decimal TotalStr { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string Cash { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 医保统筹支付
        /// </summary>
        public string SocialPay { get; set; }
        /// <summary>
        /// 附加支付
        /// </summary>
        public string ExtraPay { get; set; }
        /// <summary>
        /// 分类自负
        /// </summary>
        public string PayForClass { get; set; }
        /// <summary>
        /// 自负
        /// </summary>
        public string PayForSelf { get; set; }
        /// <summary>
        /// 自费
        /// </summary>
        public string PrivateExpense { get; set; }
        /// <summary>
        /// 当年账户余额
        /// </summary>
        public string CurrentRemain { get; set; }
        /// <summary>
        /// 历年账户余额
        /// </summary>
        public string PastRemain { get; set; }
        /// <summary>
        /// 支付方式 如：现金:xx元，pos：yy元
        /// </summary>
        public string PayFuc { get; set; }
        /// <summary>
        /// 分币误差
        /// </summary>
        public string XjWc { get; set; }
        /// <summary>
        /// 收款单位
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 收款员
        /// </summary>
        public string Cashier { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date;
        /// <summary>
        /// True为打印：此收据部分项目名称，内容进行了简化。详细信息请在触摸屏等正式公示途径中查询。
        /// </summary>
        public bool IsFull { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 发票模板路径
        /// </summary>
        public string FpdymbPath { get; set; }
        /// <summary>
        /// 挂号发票模板路径
        /// </summary>
        public string GhFpdymbPath { get; set; }
        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsBd { get; set; }

    }
}
