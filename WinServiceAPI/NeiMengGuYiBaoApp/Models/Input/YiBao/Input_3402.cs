
namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
   public class Input_3402:InputBase
    {
        public Deptinfo3402 deptinfo { get; set; }
    }

    public class Deptinfo3402
    {
        /// <summary>
        /// 1|hosp_dept_codg|医院科室编码|字符型|20
        /// </summary>
        public string hosp_dept_codg { get; set; }

        /// <summary>
        /// 2|hosp_dept_name|医院科室名称|字符型|100
        /// </summary>
        public string hosp_dept_name { get; set; }

        /// <summary>
        /// 3|begntime|开始时间|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string begntime { get; set; }

        /// <summary>
        /// 4|endtime|结束时间|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 5|itro|简介|字符型|500
        /// </summary>
        public string itro { get; set; }

        /// <summary>
        /// 6|dept_resper_name|科室负责人姓名|字符型|50
        /// </summary>
        public string dept_resper_name { get; set; }

        /// <summary>
        /// 7|dept_resper_tel|科室负责人电话|字符型|50
        /// </summary>
        public string dept_resper_tel { get; set; }

        /// <summary>
        /// 8|dept_med_serv_scp|科室医疗服务范围|字符型|500
        /// </summary>
        public string dept_med_serv_scp { get; set; }

        /// <summary>
        /// 9|caty|科别|字符型|6
        /// </summary>
        public string caty { get; set; }

        /// <summary>
        /// 10|dept_estbdat|科室成立日期|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string dept_estbdat { get; set; }

        /// <summary>
        /// 11|aprv_bed_cnt|批准床位数量|数值型|11
        /// </summary>
        public int aprv_bed_cnt { get; set; }

        /// <summary>
        /// 12|hi_crtf_bed_cnt|医保认可床位数|数值型|11
        /// </summary>
        public int hi_crtf_bed_cnt { get; set; }

        /// <summary>
        /// 13|poolarea_no|统筹区编号|字符型|6
        /// </summary>
        public string poolarea_no { get; set; }

        /// <summary>
        /// 14|dr_psncnt|医师人数|数值型|5
        /// </summary>
        public int dr_psncnt { get; set; }

        /// <summary>
        /// 15|phar_psncnt|药师人数|数值型|5
        /// </summary>
        public int phar_psncnt { get; set; }

        /// <summary>
        /// 16|nurs_psncnt|护士人数|数值型|5
        /// </summary>
        public int nurs_psncnt { get; set; }

        /// <summary>
        /// 17|tecn_psncnt|技师人数|数值型|5
        /// </summary>
        public int tecn_psncnt { get; set; }

        /// <summary>
        /// 18|memo|备注|字符型|500
        /// </summary>
        public string memo { get; set; }
    }

}
