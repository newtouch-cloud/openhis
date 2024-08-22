using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao

{
    public class Post_2204
    {
        /// <summary>
        ///   chrg_bchno 门诊费用上传用到 其他地方不用 收费批次号  字符型  30  Y 同一收费批次号病种编号 必须一致
        /// </summary>
        //public string chrg_bchno { get; set; }

        ///<summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }


        /// <summary>
        ///  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 0挂号  1处方 3 处方退费在结
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 处方内码
        /// </summary>
        public string cfnm { get; set; }
        /// <summary>
        /// 挂号项目
        /// </summary>
        public string ghxm { get; set; }
        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// ==磁卡费
        /// </summary>
        public string ckf { get; set; }
        /// <summary>
        /// 工本费
        /// </summary>
        public string gbf { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 就诊id
        /// </summary>
        public string jzid { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string rybh { get; set; }

        /// <summary>
        /// @pch varchar(50) --收费批次号  
        /// </summary>
        public string pch { get; set; }


        /// <summary>
        /// @jsnm varchar(50) --结算内码
        /// </summary>
        public string jsnm { get; set; }

        public string ksbm { get; set; }

        public string ysbm { get; set; }

        public string dise_codg { get; set; }

        /// <summary>
        /// 门诊半退结算 (退结算明细内码和数量)
        /// </summary>
        public IList<tjsxmDict> tjsxmDict { get; set; }
    }
    public class tjsxmDict
    {
        public string tjsmxnm { get; set; }
        public decimal tsl { get; set; }
    }
}
