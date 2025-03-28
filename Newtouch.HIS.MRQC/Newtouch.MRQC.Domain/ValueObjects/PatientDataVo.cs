using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects
{
    public class PatientDataVo
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string BedCode { get; set; }
        public string BedName { get; set; }
        public string Xm { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public DateTime? Ryrq { get; set; }
        public DateTime? Cyrq { get; set; }
        public string HealthCardNo { get; set; }
        public string Zyh { get; set; }
        public string Fylb { get; set; }
        public decimal? TotalFee { get; set; }
        public string RyysGh { get; set; }
        public string ZYYS { get; set; }
        public string ZZYS1 { get; set; }
        public string ZRYS { get; set; }
        public string Sfzh { get; set; }
        public DateTime? Csrq { get; set; }
        public string Csd { get; set; }
        public string Lxrxm { get; set; }
        public string Lxrdh { get; set; }
        public string Lxrdz { get; set; }
        public string Gzdwjdz { get; set; }
        public string Bazt { get; set; }
        public string Ylzh { get; set; }
        public string Sbkh { get; set; }
    }

    public class DiagDataVo
    {
        public string zdys { get; set; }
        public string zdbm { get; set; }
        public string zdmc { get; set; }
        public string zdrq { get; set; }
        public string zdbz { get; set; }
    }
}
