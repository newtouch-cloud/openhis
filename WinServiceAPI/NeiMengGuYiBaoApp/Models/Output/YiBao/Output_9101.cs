using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_9101 : OutputBase
    {
        //filename":"scmxdz","file_qury_no"
        /// <summary>
        ///  1  file_qury_no 文件查询号  字符型  30  Y 
        /// </summary>
        public string file_qury_no { get; set; }
        /// <summary>
        /// 2  filename 文件名  字符型  200  Y
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 3  fixmedins_code 医药机构编号  字符型  30  Y
        /// </summary>
        //public string fixmedins_code { get; set; }
        /// <summary>
        /// 4  dld_endtime 截止时间  字符型  30 
        /// </summary>
        public string dld_endtime { get; set; }

    }
    public class Output_9101_json
    {
        /// <summary>
        ///  1  file_qury_no 文件查询号  字符型  30  Y 
        /// </summary>
        public string file_qury_no { get; set; }
        /// <summary>
        /// 2  filename 文件名  字符型  200  Y
        /// </summary>
        public string filename { get; set; }
    }
}