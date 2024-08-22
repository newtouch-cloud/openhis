using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
  public  class Post_5102
    {
        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// 执业人员分类 字符型 6 Y Y 参照医务人员类别 (medins_psn_type)
        /// </summary>
        public string prac_psn_type { get; set; }

        /// <summary>
        /// 人员证件类型 字符型 6 Y 
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码 字符型 50
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 执业人员姓名 字符型 50
        /// </summary>
        public string prac_psn_name { get; set; }

        /// <summary>
        /// 执业人员代码 字符型 20  参照标准编码：医保医师编码、医保护士编码、医保药师编码
        /// </summary>
        public string prac_psn_code { get; set; }
    }
}
