using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeiMengGuYiBaoApp.Models.Input.YiBao;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_9002
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
        public string biz_appy_type { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string sign_no { get; set; }
    }
}
