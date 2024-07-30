using CQYiBaoInterface.Models.ShangHai.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
    public class SM01OutPut: OutputBase
    {
        public string cardtype { get; set; }

        public string cardid { get; set; }

        public string personname { get; set; }

        public string accountattr { get; set; }

        public decimal curaccountamt { get; set; }

        public decimal hisaccountamt { get; set; }

        public decimal totalmzzfdpay { get; set; }

        public decimal qfxxpay { get; set; }

        public decimal rationpay { get; set; }

        public decimal beinqf { get; set; }

        public decimal tcfdx { get; set; }

        public decimal qfxsfdxxfylj { get; set; }

        public string jlch { get; set; }

        public string ybsqjmbz { get; set; }

        public string xb { get; set; }
    }

    public class S000_SM01OutPut: SHOutputReturn
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public S000_SE01_SM01OutPut sm01 { get; set; }
        /// <summary>
        /// 账户信息
        /// </summary>
        //public SM01OutPut sm01 { get; set; }
        /// <summary>
        /// 保障卡
        /// </summary>
        //public S000Output s000 { get; set; }
        /// <summary>
        /// 电子凭证
        /// </summary>
        //public SE01Output se01 { get; set; }
    }

    public class S000_SE01_SM01OutPut: SM01OutPut
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm {get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string lxdh { get; set; }
        /// <summary>
        /// 通信地址
        /// </summary>
        public string txdz { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string yzbm { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string xzqh { get; set; }


        //电子凭证

        /// <summary>
        /// 证件号
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string idtype { get; set; }
        /// <summary>
        /// 电子凭证
        /// </summary>
        public string ecToken { get; set; }
       
    }

}
