using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class CqyzPrintVo
    {
        public string rowflag { get; set; }
        public string Id { get; set; }
        public long zhNum { get; set; }
        public int? zh { get; set; }
        public string zyh { get; set; }
        public string WardCode { get; set; }
        public string DeptCode { get; set; }
        public string deptname { get; set; }
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
        public string tzysgh { get; set; }
        public string tzsj { get; set; }
        public string tzr { get; set; }
        public string shsj { get; set; }
        public string shr { get; set; }
        public string kssj { get; set; }
        public string zxsj { get; set; }
        public string zxr { get; set; }
        public string dcr { get; set; }
        public string ztnr { get; set; }
        public string yznr { get; set; }
        public string ypyfdm { get; set; }
        public string ypyfmc { get; set; }
        public string zxksdm { get; set; }
        public string printyznr { get; set; }
        public string hzxm { get; set; }
        public DateTime? CreateTime { get; set; }
        public string yzzUrl { get; set; }
        public string zxUrl { get; set; }
        public string tzUrl { get; set; }
        public string yzzqm { get; set; }
        public string shtzz { get; set; }
    }

    public class PatidInfoVo
    {
        public string zyh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string BedName { get; set; }
        public string DeptName { get; set; }
        public string age { get; set; }
        public string sex { get; set; }
        public string zd { get; set; }
        public string yymc { get; set; }
    }
}
