
namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
   public class Input_3403:InputBase
    {
        //public List<invinfo3501> invinfo { get; set; }
        public Data3403 data { get; set; }
    }

    public class Data3403
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
        /// 3|begntime|开始时间|日期型|yyyy-MM-dd
        /// </summary>
        public string begntime { get; set; }
    }

}
