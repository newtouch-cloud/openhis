using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
   public class Post_5203
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
        ///  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }

        public string psn_no { get; set; }
        public string setl_id { get; set; }
        public string mdtrt_id { get; set; }

        //1  psn_no 人员编号  字符型  30  Y 
        //2  setl_id 结算 ID 字符型  30  Y 
        //3  mdtrt_id 就诊 ID 字符型  30  
    }
}
