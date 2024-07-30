using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Apply
{
    public class JyjcExecVo
    {
        public string Id { get; set; }
        public string hzlx { get; set; }
        public string mzzyh { get; set; }
        public string hzxm { get; set; }
        public string zjh { get; set; }
        public int? cflx { get; set; }
        public string cflxstr { get; set; }
        public string sqdh { get; set; }
        public string sqdlx { get; set; }
        public DateTime kdrq { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string gg { get; set; }
        public string dw { get; set; }
        public int? sl { get; set; }
        public decimal? dj { get; set; }
        public decimal? je { get; set; }
        public string cfh { get; set; }
        public string kdys { get; set; }
        public string kdysmc { get; set; }
        public string kdks { get; set; }
        public string kdksmc { get; set; }
        public string zxks { get; set; }
        public string zxksmc { get; set; }
        public string jzr { get; set; }
        public string zxr { get; set; }
        public DateTime? zxrq { get; set; }
        public string fylx { get; set; }
        public string shr { get; set; }
    }

    public class jyjcExecReq
    {
        public string mzzyh { get; set; }
        public string hzlx { get; set; }
        public string xm { get; set; }
        public string fylx { get; set; }
        public string sqdlx { get; set; }
        public string sqdh { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
        public string zxks { get; set; }
        public string kdks { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string kdys { get; set; }
        public DateTime kdrq { get; set; }
        public string shr { get; set; }
        public string dw { get; set; }
        public string gg { get; set; }
    }
}
