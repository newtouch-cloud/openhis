using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SI11Input:InputBase
    {
        public string cardtype { get; set; }
        public string carddata { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptid { get; set; }

        /// <summary>
        /// 特殊人员标识
        /// </summary>
        public string personspectag { get; set; }

        /// <summary>
        /// 医疗类别
        /// </summary>
        public string yllb { get; set; }

        /// <summary>
        /// 病人类型
        /// </summary>
        public string persontype { get; set; }

        /// <summary>
        /// 工伤认定号
        /// </summary>
        public string gsrdh { get; set; }

        /// <summary>
        /// 大病项目代码
        /// </summary>
        public string dbtype { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        public List<Zdnos> zdnos { get; set; }

        /// <summary>
        /// 家床结算开始日期
        /// </summary>
        public string jsksrq { get; set; }

        /// <summary>
        /// 家床结算结束日期
        /// </summary>
        public string jsjsrq { get; set; }
        /// <summary>
        /// 家床就诊次数
        /// </summary>
        public int jzcs { get; set; }

        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }

        /// <summary>
        /// 线上业务类型
        /// </summary>
        public string xsywlx { get; set; }
        /// <summary>
        /// 明细账单号
        /// </summary>
        public List<Mxzdhs> mxzdhs { get; set; }
    }
    /// <summary>
    /// 诊断
    /// </summary>
    public class Zdnos
    {
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string zdno { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc { get; set; }
    }
    /// <summary>
    /// 明细账单
    /// </summary>
    public class Mxzdhs
    {
        /// <summary>
        /// 明细账单号
        /// </summary>
        public string mxzdh { get; set; }
        /// <summary>
        /// 交易费用总额
        /// </summary>
        public decimal totalexpense { get; set; }
        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwfyze { get; set; }
        /// <summary>
        /// 非医保结算范围费用总额
        /// </summary>
        public decimal fybjsfwfyze { get; set; }

        public decimal? zfbl { get; set; }
        public decimal? zfje { get; set; }
        public decimal? fyxj { get; set; }
    }
}
