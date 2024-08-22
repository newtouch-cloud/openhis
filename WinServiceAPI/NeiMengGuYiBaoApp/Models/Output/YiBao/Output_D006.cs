using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_D006 : OutputBase
    {
        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string pharName { get; set; }//医保药师姓名 	字符型 	3 	 	Y 	 
        public string pharCode { get; set; }//医保药师代码 	字符型 	20 	 	Y 	 
        public string rxChkStasCodg { get; set; }//处方审核状态代码 	字符型 	3 	Y 	Y 	参考（rx_chk_stas_codg） 
        public string rxChkOpnn { get; set; }//处方审核意见 	文本型 	 	 	Y 	 
        public string rxChkTime { get; set; }//处方审核时间 	日期型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string rxStasCodg { get; set; }//医保处方状态编码 	字符型 	3 	Y 	Y 	参考（rx_stas_codg） 
        public string rxStasName { get; set; }//医保处方状态名称 	字符型 	20 	 	Y 	 
        public string rxChkStasName { get; set; }//处方审核状态名称 	字符型 	10 	 	Y 	 
    }
}
