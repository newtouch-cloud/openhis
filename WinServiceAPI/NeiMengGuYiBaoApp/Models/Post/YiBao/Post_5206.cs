using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
 public   class Post_5206
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

        //        5206 人员累计信息查询
        //1  psn_no 人员编号  字符型  30  Y 
        //2  cum_ym 累计年月  字符型  6 
        public string psn_no { get; set; }
        public string cum_ym { get; set; }
    }
}
