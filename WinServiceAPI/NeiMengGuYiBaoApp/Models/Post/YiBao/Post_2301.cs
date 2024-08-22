using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_2301
    {
        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        

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


    }

    public class Post_2301V2:Post_2301
    {
        /// <summary>
        /// 费用截止时间
        /// </summary>
        public string jssj { get; set; }
        /// <summary>
        /// 上传费用计费编号
        /// </summary>
        public string jfbbh { get; set; }
    }
}
