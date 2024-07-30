using CQYiBaoInterface.Models.ShangHai.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SH01Input: InputBase
    {
        /// <summary>
        /// 凭证类别
        /// </summary>
        public string cardtype { get; set; }

        /// <summary>
        /// 凭证码
        /// </summary>
        public string carddata { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptid { get; set; }

        /// <summary>
        /// 诊疗项目代码
        /// </summary>
        public string zlxmdm { get; set; }

        /// <summary>
        /// 特殊人员标识
        /// </summary>
        public string personspectag { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>

        public string yllb { get; set; }

        /// <summary>
        /// 大病项目代码
        /// </summary>
        public string dbtype { get; set; }

        /// <summary>
        /// 病人类型
        /// </summary>
        public string persontype { get; set; }
        /// <summary>
        /// 工伤认定号
        /// </summary>

        public string gsrdh { get; set; }

        /// <summary>
        /// 交易费用总额
        /// </summary>
        public decimal totalexpense { get; set; }

        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwfyze { get; set; }
        /// <summary>
        /// 诊疗费
        /// </summary>

        public decimal zhenlf { get; set; }
        /// <summary>
        /// 门（急）诊诊疗费自费
        /// </summary>

        public decimal ghf { get; set; }
        /// <summary>
        /// 非医保结算范围费用总额
        /// </summary>

        public decimal fybjsfwfyze { get; set; }
        /// <summary>
        /// 享受社区减免标志
        /// </summary>

        public string jmbz { get; set; }
        /// <summary>
        /// 线上业务类型1：一般线上交易2：互联网医院诊疗 线上交易必填
        /// </summary>
        public string xsywlx { get; set; }
    }
}
