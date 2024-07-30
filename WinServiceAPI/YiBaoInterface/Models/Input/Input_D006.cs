using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_D006 : InputBase
    {
        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string fixmedinsCode { get; set; }//定点医疗机构编号 	字符型 	20 	 	Y 	 
        public string mdtrtId { get; set; }//医保就诊ID 	字符型 	30 	 	Y 	医保门诊挂号时返回 
        public string psnName { get; set; }//人员名称 	字符型 	50 	 	Y 	 
        public string psnCertType { get; set; }//人员证件类型 	字符型 	6 	Y 	Y 	 
        public string certno { get; set; }//证件号码 	字符型 	50 	 	Y 	 
    }
}
