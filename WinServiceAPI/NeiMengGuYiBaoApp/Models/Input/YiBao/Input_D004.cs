using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_D004 : InputBase
    {

        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string fixmedinsCode { get; set; }//	定点医疗机构编号 	字符型 	20 	 	Y 	 
        public string drCode { get; set; }//	撤销医师的医保医师代码 	字符型 	20 	 	Y 	 
        public string undoDrName { get; set; }//	撤销医师姓名 	字符型 	50 	 	Y 	 
        public string undoDrCertType { get; set; }//	撤销医师证件类型 	字符型 	6 	Y 	Y 	参照人员证件(psn_cert_type) 
        public string undoDrCertno { get; set; }// 	撤销医师证件号码 	字符型 	50 	 	Y 	 
        public string undoRea { get; set; }//撤销原因描述 	字符型 	200 	 	Y 	 
        public string undoTime { get; set; }// 	撤销时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
    }

}
