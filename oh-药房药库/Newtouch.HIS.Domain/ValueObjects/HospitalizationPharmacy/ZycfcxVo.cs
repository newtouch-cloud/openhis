using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy
{
    public class ZycfcxVo
    {
        public string bq { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
        public string keyword { get; set; }
        public string organizeId { get; set; }
        public string yzh { get; set; }
        public DateTime? zxrq { get; set; }
        /// <summary>
        /// 1:临时 2:长期
        /// </summary>
        public string yzxz { get; set; }
    }

    public class ZycfcxList
    {
        public string zyh { get; set; }

        public string hzxm { get; set; }
        
        public string WardCode { get; set; }
        public string bqmc { get; set; }
        public string DeptCode { get; set; }
        public string cfh { get; set; }
        public string yzxz { get; set; }
        public string yzxzmc { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        public decimal? je { get; set; }
        public int? yzlx { get; set; }
        public int? yzlxstr { get; set; }
        public DateTime? kssj { get; set; }
        public string fybz { get; set; }
        public long? lyxh { get; set; }
    }

    public class ZycfcxDetailList: ZycfcxList
    {
        public string Id { get; set; }
        public int? zh { get; set; }
        public string pcCode { get; set; }
        public string xmdm { get; set; }
        public string xmmc { get; set; }
        public int? yzzt { get; set; }
        public string dw { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal? ypjl { get; set; }
        public string ypgg { get; set; }
        public string yznr { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
    }
}
