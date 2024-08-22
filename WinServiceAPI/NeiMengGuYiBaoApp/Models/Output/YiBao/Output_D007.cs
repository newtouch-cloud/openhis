using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_D007 : OutputBase
    {
        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string setlTime { get; set; }//医保结算时间 	日期型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string rxStasCodg { get; set; }//医保处方状态编码 	字符型 	3 	Y 	Y 	参考（rx_stas_codg） 
        public string rxStasName { get; set; }//医保处方状态名称 	字符型 	20 	 	Y 	 
        public string rxUsedStasCodg { get; set; }//处方使用状态编号 	字符型 	3 	Y 	Y 	参考（rx_used_stas_codg） 
        public string rxUsedStasName { get; set; }//处方使用状态名称 	字符型 	20 	 	Y 	 
        public List<seltdelts> seltdelts { get; set; }//

    }
    public class seltdelts
    {
        public string medListCodg { get; set; }//医疗目录编码 	字符型 	20 	 	Y 
        public string drugGenname { get; set; }//通用名 	字符型 	50 	 	Y 
        public string drugProdname { get; set; }//药品商品名 	字符型 	50 	 	Y 
        public string drugDosform { get; set; }//药品剂型 	字符型 	20 	 	Y 
        public string drugSpec { get; set; }//药品规格 	数值型 	（16，3） 	 	Y 
        public string cnt { get; set; }//数量 	数值型 	（16，3） 	 	Y 
        public string aprvno { get; set; }//批准文号 	字符型 	20 	 	 
        public string bchno { get; set; }//批次号 	字符型 	20 	 	 
        public string manuLotnum { get; set; }//生产批号 	字符型 	20 	 	 
        public string prdrName { get; set; }//生厂厂家 	字符型 	50 	 	Y 
        public string takeDrugFlag { get; set; }//取药标志位 	字符型 	3 	Y 	Y 
    }
}
