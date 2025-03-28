using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    public class tyCFMainInfo
    {
        public int cfmxId { get; set; }

        public string yp { get; set; }
        public string fph { get; set; }
        public string xm { get; set; }
        public short nl { get; set; }
        public string kh { get; set; }
        public string cfh { get; set; }
        public string brlx { get; set; }
        public DateTime sfsj { get; set; }
        public decimal zje { get; set; }
        public int cflx { get; set; }
        public string ybh { get; set; }
        public string xb { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public string pyy { get; set; }
    }

    public class fyCFMainInfoComparer : IEqualityComparer<tyCFMainInfo>
    {
        public bool Equals(tyCFMainInfo x, tyCFMainInfo other)
        {
            return x.brlx == other.brlx && x.fph == other.fph && x.xm == other.xm && x.nl == other.nl && x.kh == other.kh && x.cfh == other.cfh && x.sfsj == other.sfsj && x.zje == other.zje && x.cflx == other.cflx && x.ybh == other.ybh && x.xb == other.xb && x.ks == other.ks && x.ys == other.ys && x.pyy == other.pyy;
        }

        public int GetHashCode(tyCFMainInfo obj)
        {
            return base.GetHashCode();
            //throw new NotImplementedException();
        }
    }
}
