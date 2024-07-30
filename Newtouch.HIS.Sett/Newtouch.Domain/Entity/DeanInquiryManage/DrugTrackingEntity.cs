using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
   public class DrugTrackingEntity
    {
        public string ypmc { get; set; }
        public string ypdm { get; set; }
        public string ypgg { get; set; }
        public string zxdw { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal je { get; set; }
        [DecimalPrecision(11, 2)]
        public decimal sl { get; set; }


    }

    public class Profitandlossranking
    {
        public string syyy { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal pfjze { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal lsjze { get; set; }

        public int sl { get; set; }

    }

    public class DoctorBillingRanking
    {
        public string Name { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal zje { get; set; }
        public int sl { get; set; }


    } 
}
