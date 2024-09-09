using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4506 : InputBase
    {
        public List<data4506> data { get; set; }
    }

    public class data4506
    {
        /// <summary>
        /// 就医流水号
        /// </summary>
        public string mdtrt_sn { get; set; }
        /// <summary>
        /// 就诊 ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 门诊/住院标志
        /// </summary>
        public string otp_ipt_flag { get; set; }
        /// <summary>
        /// 检查/检验标志
        /// </summary>
        public string exam_test_flag { get; set; }
        /// <summary>
        /// 申请号
        /// </summary>
        public string appy_no { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 文件格式类型
        /// </summary>
        public string file_formate { get; set; }
        /// <summary>
        /// 检查/检验报告base64编码格式
        /// </summary>
        public string exam_test_rpot { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; }
    }
}
