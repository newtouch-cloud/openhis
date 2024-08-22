using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// 已结算处方明细查询接口返回报文
    /// </summary>
    public class OutpatientSettledRpQueryResponseDTO
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 结算明细内码
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 用量+用量单位
        /// </summary>
        public string ylstr { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 数量+数量单位
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 剂量+剂量单位
        /// </summary>
        public string jlstr { get; set; }
        public string yfcode { get; set; }
        public string ycCode { get; set; }
        public int? zxcs { get; set; }
    }
}
