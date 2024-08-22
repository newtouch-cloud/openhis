using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
   public class diseinfo
    {
        /// <summary>
        /// 1|人员编号|字符型|30|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string psn_no { get; set; }

        /// <summary>
        /// 2|诊断类别|字符型|3| Y| Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string diag_type { get; set; }

        /// <summary>
        /// 3|主诊断标志|字符型|3| Y| Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string maindiag_flag { get; set; }

        /// <summary>
        /// 4|诊断排序号|数值型|2|  | Y|  
        /// </summary> 
        public string diag_srt_no { get; set; }

        /// <summary>
        /// 5|诊断代码|字符型|20|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string diag_code { get; set; }

        /// <summary>
        /// 6|诊断名称|字符型|100|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string diag_name { get; set; }

        /// <summary>
        /// 7|入院病情|字符型|500|  |  |  入院诊断需要上传
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string adm_cond { get; set; }

        /// <summary>
        /// 8|诊断科室|字符型|50|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string diag_dept { get; set; }

        /// <summary>
        /// 9|诊断医生编码|字符型|30|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string dise_dor_no { get; set; }

        /// <summary>
        /// 10|诊断医生姓名|字符型|50|  | Y|  
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string dise_dor_name { get; set; }

        /// <summary>
        /// 11|诊断时间|日期时间型|  |  | Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string diag_time { get; set; }

        //
        /// <summary>
        ///  1  mdtrt_id 就诊 ID 字符型 出院诊断需要上传
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string mdtrt_id { get; set; }


        /// <summary>
        /// /vali_flag  有效标志  字符型  3  Y Y  门诊诊断需要上传
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public string vali_flag { get; set; }
    }
}
