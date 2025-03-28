using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class LsyzPrintVo
    {
        public long csy { get; set; }
        public long yznrs { get; set; }
        public string rowflag { get; set; }
        public long ztpx { get; set; }
        public long ztnum { get; set; }
        public string yzh { get; set; }
        public string Id { get; set; }

        public int? zh { get; set; }
        public string WardCode { get; set; }
        public string wardname { get; set; }
        public string DeptCode { get; set; }
        public string bedCode { get; set; }

        public string bfNo { get; set; }
        public string ysgh { get; set; }
        public string pcCode { get; set; }
        public string yzpcmc { get; set; }
        public string zdm { get; set; }
        public string xmmc { get; set; }
        public int? yzzt { get; set; }
        public int? zbbz { get; set; }
        public int? yzlx { get; set; }

        public string zfysgh { get; set; }

        public string zfsj { get; set; }

        public string zfr { get; set; }
        public DateTime? shsj { get; set; }
        public string shr { get; set; }
        public string ypyfmc { get; set; }
        public string kssj { get; set; }
        public string zxsj { get; set; }
        public string zxr { get; set; }
        public string ztnr { get; set; }
        public string yznr { get; set; }
        public string ztmc { get; set; }
        public string ypyfdm { get; set; }
        public string zxksdm { get; set; }
        public string printyznr { get; set; }
        public string newypyfmc { get; set; }
        public string hzxm { get; set; }
        public DateTime CreateTime { get; set; }
        public string yzzqm { get; set; }
        public string zxUrl { get; set; }
        public string yzzUrl { get; set; }
        public string tzUrl { get; set; }
        public string shtzz { get; set; }

        public string ispcjg { get; set; }
    }
}
