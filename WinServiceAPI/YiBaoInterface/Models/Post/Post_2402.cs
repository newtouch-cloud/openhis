using CQYiBaoInterface.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_2402
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
        ///  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }

        //med_type 医疗类别  字符型
        /// <summary>
        /// med_type 医疗类别  字符型
        /// </summary>
        public string med_type { get; set; }


        /// <summary>
        /// //mdtrt_id  就诊 ID  字符型   入院登记修改的时候传入
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }


        /// <summary>
        /// 就诊类型 02 身份证 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// </summary>
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        ///  @mdtrt_cert_no varchar(50), --就诊凭证编号  
        /// </summary>
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        ///  @orgId varchar(50),--组织机构  
        /// </summary>
        public string orgId { get; set; }

    
        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; }
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; }

    }
}
