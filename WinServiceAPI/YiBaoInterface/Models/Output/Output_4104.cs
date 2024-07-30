using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_4104 : OutputBase
    {
        public string fixmedins_code {get;set; }
        public string fixmedins_name{ get; set; }
        public string setl_ym { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public string qltctrl_rslt { get; set; }
        public string err_lv { get; set; }
        public string retn_flag { get; set; }
        public string qltctrl_ver { get; set; }
        public string psn_cert_type { get; set; }
        public string cert_no { get; set; }
        public string detailinfo { get; set; }
        public List<detailinfo> deraukubfi { get; set; }
    }

    public class detailinfo {
        public string exam_data_fld{get;set; }
        public string qltctrl_chkrslt{ get; set; }
        public string err_lv { get; set; }
        public string retn_flag { get; set; }
        public string init_val { get; set; }
    }
}
