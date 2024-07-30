using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SN01ZY: Post_Base
    {
        /// <summary>
        /// 医疗类别
        ///</summary>
        public string med_type { get; set; }

        /// <summary>
        ///  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string orgId { get; set; }



        /// <summary>
        /// 固定上传多少条记录
        /// </summary>
        public int uploadCount { get; set; }
        /// <summary>
        /// 费用截止时间
        /// </summary>
        public string jssj { get; set; }
        /// <summary>
        /// 上传费用计费编号
        /// </summary>
        public string jfbbh { get; set; }
        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }
    }
}
