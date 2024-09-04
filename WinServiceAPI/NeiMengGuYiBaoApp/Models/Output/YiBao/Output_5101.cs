using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_5101 : OutputBase
    {
        public List<Output5101> feedetail { get; set; }
    }
    public class Output5101
    {
        /// <summary>
        /// 1|医院科室编码|字符型|20|Y|
        /// </summary>
        public string hosp_dept_codg { get; set; }

        /// <summary>
        /// 2|医院科室名称|字符型|100|Y|
        /// </summary>
        public string hosp_dept_name { get; set; }

        /// <summary>
        /// 3|开始时间|日期时间型|Y|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public DateTime begntime { get; set; }

        /// <summary>
        /// 4|结束时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public DateTime endtime { get; set; }

        /// <summary>
        /// 5|简介|字符型|500|
        /// </summary>
        public string itro { get; set; }

        /// <summary>
        /// 6|科室负责人姓名|字符型|50|Y|
        /// </summary>
        public string dept_resper_name { get; set; }

        /// <summary>
        /// 7|科室负责人电话|字符型|50|Y|
        /// </summary>
        public string dept_resper_tel { get; set; }

        /// <summary>
        /// 8|科室医疗服务范围|字符型|500|
        /// </summary>
        public string dept_med_serv_scp { get; set; }

        /// <summary>
        /// 9|科别|字符型|6|Y|Y|
        /// </summary>
        public string caty { get; set; }

        /// <summary>
        /// 10|科室成立日期|日期时间型|
        /// </summary>
        public DateTime dept_estbdat { get; set; }

        /// <summary>
        /// 11|批准床位数量|数值型|11|
        /// </summary>
        public int aprv_bed_cnt { get; set; }

        /// <summary>
        /// 12|医保认可床位数|数值型|11|
        /// </summary>
        public int hi_crtf_bed_cnt { get; set; }

        /// <summary>
        /// 13|统筹区编号|字符型|6|
        /// </summary>
        public string poolarea_no { get; set; }

        /// <summary>
        /// 14|医师人数|数值型|5|
        /// </summary>
        public int dr_psncnt { get; set; }

        /// <summary>
        /// 15|药师人数|数值型|5|
        /// </summary>
        public int phar_psncnt { get; set; }

        /// <summary>
        /// 16|护士人数|数值型|5|
        /// </summary>
        public int nurs_psncnt { get; set; }

        /// <summary>
        /// 17|技师人数|数值型|5|
        /// </summary>
        public int tecn_psncnt { get; set; }

        /// <summary>
        /// 18|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

    }
}
