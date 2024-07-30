using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// 住院费用分析DTO
    /// </summary>
    public class ZYFYFXDTO
    {
        /// <summary>
        /// 住院总费用
        /// </summary>
        public decimal? zfy { get; set; }
        /// <summary>
        /// 住院总费用-环比
        /// </summary>
        public string zfy_hb { get; set; }
        /// <summary>
        ///  住院总费用-同比
        /// </summary>
        public string zfy_tb { get; set; }
        /// <summary>
        /// 药品费
        /// </summary>
        public decimal? ypf { get; set; }
        /// <summary>
        /// 耗材费
        /// </summary>
        public decimal? hcf { get; set; }
        /// <summary>
        /// 诊疗费
        /// </summary>
        public decimal? zlf { get; set; }
        /// <summary>
        /// 出院人数
        /// </summary>
        public int? cyrs { get; set; }
        /// <summary>
        /// 出院人数-同比
        /// </summary>
        public string cyrs_hb { get; set; }
        /// <summary>
        /// 出院人数-环比
        /// </summary>
        public string cyrs_tb { get; set; }
        /// <summary>
        /// 均次住院费用
        /// </summary>
        public decimal? jczyfy { get; set; }
        /// <summary>
        /// 诊疗费-环比
        /// </summary>
        public string jczyfy_hb { get; set; }
        /// <summary>
        /// 诊疗费-同比
        /// </summary>
        public string jczyfy_tb { get; set; }
    }
    public class ZYFYFX_KSFYFXDTO
    {
        /// <summary>
        /// 住院科室
        /// </summary>
        public string zyks { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksdm { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        public decimal? zfy { get; set; }
        /// <summary>
        /// 药品费
        /// </summary>
        public decimal? ypf { get; set; }
        /// <summary>
        /// 药品费-占比
        /// </summary>
        public string ypf_zb { get; set; }
        /// <summary>
        /// 耗材费
        /// </summary>
        public decimal? hcf { get; set; }
        /// <summary>
        /// 耗材费-占比
        /// </summary>
        public string hcf_zb { get; set; }
        /// <summary>
        /// 检验检查费
        /// </summary>
        public decimal? jyjcf { get; set; }
        /// <decimal>
        /// 资料费
        /// </summary>
        public decimal? zlf { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public decimal? qt { get; set; }
    }
    /// <summary>
    /// 出院患者费用分析
    /// </summary>
    public class ZYFYFX_CYHZFYFXDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// 次均费用分类
        /// </summary>
        public string cjfyfl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 占比
        /// </summary>
        public string zb { get; set; }
    }
    public class CYHZFYFTJTDTO
    {
        public string  nf{ get; set; }
        public decimal? 次均总费用 { get; set; }
        public decimal? 次均药费 { get; set; }
        public decimal? 次均耗材费 { get; set; }
        public decimal? 次均检验检查费 { get; set; }
        public decimal? 次均治疗费 { get; set; }
        public decimal? 次均其他费 { get; set; }
    }
}
