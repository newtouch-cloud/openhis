using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class CostOrderResp
    {
        public string OrderNo { get; set; }
        public string Mzh { get; set; }
        public string CostName { get; set; }
        public DateTime DiagDay { get; set; }
        public decimal TotalAmount { get; set; }
        public string Dept { get; set; }
        public string Doctor { get; set; }
    }
    public class OrderInfo
    {
        public string CardNo { get; set; }
        public string Mzh { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string nlshow { get; set; }
        public string ClinicDoctor { get; set; }
        public string ClinicDateTime { get; set; }
        public decimal? TotalAmount { get; set; }
        public string OrderNo { get; set; }
        public string Fph { get; set; }

    }
    public class CostOrderDetailResp: OrderInfo
    {
        public List<CostOrderDetail> OrderDetailData { get; set; }
        public List<OutPatientCost> CostTypeData { get; set; }
    }
    public class OutPatientCost
    {
        public string Cfh { get; set; }
        public string CfTypeName { get; set; }
        public decimal Amount { get; set; }
    }
    public class CostOrderDetail
    {
        public string Cfh { get; set; }
        public string Doctor { get; set; }
        public string Dept { get; set; }
        public decimal Price { get; set; }
        public decimal Num { get; set; }
        public decimal Amount { get; set; }
        public string CfType { get; set; }
        public string XmCode { get; set; }
        public string Xmmc { get; set; }
        public string dw { get; set; }
        public string Gg { get; set; }
        public string Flmc { get; set; }
        public string ReMark { get; set; }
    }
}
