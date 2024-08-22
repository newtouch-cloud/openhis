using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    class GuiAnMzjsDto
    {
    }
    /// <summary>
    /// 本地查询所用
    /// </summary>
    public class GuiAnMzjsPatInfoDto
    {
        public string prm_akc190 { get; set; }
        public string prm_aac001 { get; set; }
        public string prm_ykc173 { get; set; }
        public string prm_hisfyze { get; set; }
        public string prm_aka130 { get; set; }
        public string prm_yka110 { get; set; }
        public string prm_aae013 { get; set; }
        public string prm_aae011 { get; set; }
        public string prm_ykc141 { get; set; }
        public string prm_ykb065 { get; set; }
    }

    /// <summary>
    /// 医保结算上传
    /// </summary>
    public class InPtmzjs
    {
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// 门诊诊断信息
        /// </summary>
        public string prm_ykc173 { get; set; }
        /// <summary>
        /// HIS 费用总额
        /// </summary>
        public string prm_hisfyze { get; set; }
        /// <summary>
        /// 支付类别
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string prm_yka110 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// 经办人编码
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// 执行社会保险办法
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// 结算明细
        /// </summary>
        public List<FeejsMx> row { get; set; }
    }

    /// <summary>
    /// 费用结算明细
    /// </summary>
    public class FeejsMx
    {

        /// <summary>
        /// 记账流水号
        /// </summary>
        public string yka105 { get; set; }
        /// <summary>
        /// YKD125
        /// </summary>
        public string ykd125 { get; set; }
        /// <summary>
        /// YKD126
        /// </summary>
        public string ykd126 { get; set; }
        /// <summary>
        /// 医保通用项目编码_YKA002
        /// </summary>
        public string yka002 { get; set; }
        /// <summary>
        /// 医保通用项目名称_YKA003
        /// </summary>
        public string yka003 { get; set; }
        /// <summary>
        /// 数量_AKC226
        /// </summary>
        public decimal akc226 { get; set; }
        /// <summary>
        /// 实际价格_AKC225
        /// </summary>
        public decimal akc225 { get; set; }
        /// <summary>
        /// 明细项目费用总额_YKA315
        /// </summary>
        public decimal yka315 { get; set; }
        /// <summary>
        /// 开单科室编码_YKA097
        /// </summary>
        public string yka097 { get; set; }
        /// <summary>
        /// 开单科室名称_YKA098
        /// </summary>
        public string yka098 { get; set; }
        /// <summary>
        /// 开单医生公民身份号码
        /// </summary>
        public string ykd102 { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        public string yka099 { get; set; }
        /// <summary>
        /// 受单科室编码_YKA100
        /// </summary>
        public string yka100 { get; set; }
        /// <summary>
        /// 受单科室名称_YKA101
        /// </summary>
        public string yka101 { get; set; }
        /// <summary>
        /// 受单医生公民身份号码
        /// </summary>
        public string ykd106 { get; set; }
        /// <summary>
        /// 受单医生姓名
        /// </summary>
        public string yka102 { get; set; }
        /// <summary>
        /// 明细发生时间_YKE123
        /// </summary>
        public string yke123 { get; set; }
        /// <summary>
        /// 经办人姓名_YKC141
        /// </summary>
        public string ykc141 { get; set; }
        /// <summary>
        /// 经办时间_AAE036
        /// </summary>
        public string aae036 { get; set; }
        /// <summary>
        /// 备注_AAE013
        /// </summary>
        public string aae013 { get; set; }
        /// <summary>
        /// 中药使用方式
        /// </summary>
        public string yke201 { get; set; }
        /// <summary>
        /// 最小计价单位
        /// </summary>
        public string yka295 { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string aka074 { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string aka070 { get; set; }
        /// <summary>
        /// 剂型名称
        /// </summary>
        public string yae374 { get; set; }
        /// <summary>
        /// 是否医院制剂
        /// </summary>
        public string yke009 { get; set; }
        /// <summary>
        /// 医院审批标志
        /// </summary>
        public string yke186 { get; set; }
    }

    /// <summary>
    /// 返回前端数据整合：医保+自立
    /// </summary>
    public class guianMainAllOfMzjs
    {
        /// <summary>
        /// 退款总金额
        /// </summary>
        public decimal? tkzje { get; set; }
        /// <summary>
        /// 再次结算医保总金额
        /// </summary>
        public decimal? ybzje { get; set; }
        /// <summary>
        /// 再次结算自立总金额
        /// </summary>
        public decimal? zlzje { get; set; }

        /// <summary>
        /// 自立项目列表
        /// </summary>
        public List<FeejsMx> guianZLMainOfMzjs;

        /// <summary>
        /// 医保项目列表
        /// </summary>
        public InPtmzjs guianYBMainOfMzjs;
    }
}
