using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_9101
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

        /// <summary>
        /// 3   医药机构编号  字符型  30  
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 需要上传的数据
        /// </summary>
        public DataTable dtIn { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName { get; set; }
    }
}
