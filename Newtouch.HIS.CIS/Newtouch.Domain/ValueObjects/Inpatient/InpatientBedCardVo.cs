using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
	public class InpatientBedCardVo
	{
		public string organizeId { get; set; }
		public string zt { get; set; }
		public string xm { get; set; }
		public string sex { get; set; }
		public string nl { get; set; }
		public DateTime rqrq { get; set; }
		public string blh { get; set; }
		public string zgys { get; set; }
		public string zyh { get; set; }
		public string xh { get; set; }
		public string jb { get; set; }
		public string zrhs { get; set; }
		public string zrzz { get; set; }
		public string gms { get; set; }
		public string fbsx { get; set; }
		public string sjys { get; set; }
		public string hsz { get; set; }
		public string kzr { get; set; }
		public string zgysname { get; set; }
		public string zrhsname { get; set; }
		public string zrzzname { get; set; }
		public string sjysname { get; set; }
		public string hszname { get; set; }
		public string kzrname { get; set; }
	}

    public class InpatientVo
    {
        public string zyh { get; set; }
        public string ysgh { get; set; }
        public string ysName { get; set; }
        public DateTime? rqrq { get; set; }
        public string wzjb { get; set; }
    }
    public class InpatiContinuePrintVo
    {
        public DateTime? xdsj { get; set; }
        public string ys { get; set; }
        public string yznr { get; set; }
        public DateTime? zxsj { get; set; }
        public string hs { get; set; }
        public int? xh { get; set; }
        public int? pageCnt { get; set; }
    }
    public class InpatiContinuePrintPageVo
    {
        public int maxpage { get; set; }

        public int minpage { get; set; }
    }


}
