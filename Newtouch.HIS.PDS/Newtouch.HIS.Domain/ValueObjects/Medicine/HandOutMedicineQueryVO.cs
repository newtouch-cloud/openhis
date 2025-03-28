using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 内部发药查询返回对象
    /// </summary>
    public class HandOutMedicineQueryVO
    {
        public int djlx { get; set; }
        public decimal Zje { get; set; }
        public decimal ckje { get; set; }
        public string dlcode { get; set; }
        public string jxmc { get; set; }
        public string Ckczy { get; set; }
        public string Rkczy { get; set; }
        public string sx { get; set; }
        public string Yplbmc { get; set; }
        public DateTime Cksj { get; set; }
        public string ckbm { get; set; }
        public string rkbm { get; set; }
        public string py { get; set; }
        public string Ypdm { get; set; }
        public string ypmc { get; set; }
        public string ypgg { get; set; }
        
        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }
        public decimal Pfj { get; set; }
        public decimal Lsj { get; set; }
        public string Dw { get; set; }
        public string Ph { get; set; }
        public string Yxq { get; set; }
        public int Sl { get; set; }
        public int kc { get; set; }
        public string Wg { get; set; }
        public string Pdh { get; set; }
        public decimal JJ { get; set; }
        public string fysl { get; set; }
    }
}
