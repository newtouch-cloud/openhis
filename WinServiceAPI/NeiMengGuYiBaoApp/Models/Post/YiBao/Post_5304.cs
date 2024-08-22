using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
  public  class Post_5304
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

        //        5304 转院信息查询
        //1  psn_no 人员编号  字符型  30  
        //2  begntime 开始时间  日期时间型 Yyyyy-MM-ddHH:mm:ss
        //3  endtime 结束时间  日期时间型 yyyy-MM-ddHH:mm:
        public string begntime { get; set; }

        public string endtime { get; set; }
        public string psn_no { get; set; }
    }
}
