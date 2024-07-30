using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
	public class OrdersDrugsVO
	{
		public Int64 id { get; set; }
		public string zyh { get; set; }
		public string patientName { get; set; }
		public string ypmc { get; set; }
		public string cw { get; set; }
		public string slStr { get; set; }
		public string jxmc { get; set; }
		public string ypgg { get; set; }

		public string pcmc { get; set; }
		public string zh { get; set; }
		public string ylStr { get; set; }
		public string yznr { get; set; }
		public string yzxzmc { get; set; }
		public string ycmc { get; set; }
		public string zlff { get; set; }
		public string CreatorCode { get; set; }
		public DateTime CreateTime { get; set; }
	}
}
