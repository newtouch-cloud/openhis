using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("YBJKDATA")]
    public class QhdPrescriptionDTO
    {
        public REQUESTDATA REQUESTDATA { get; set; }
    }

    public class REQUESTDATA
    {
        /// <summary>
        /// 定点医疗机构编码
        /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// 定点医疗机构名称
        /// </summary>
        public string AKB021 { get; set; }
        /// <summary>
        /// 交易代码
        /// </summary>
        public string MSGNO { get; set; }
        /// <summary>
        /// 发送方交易流水号
        /// </summary>
        public string MSGID { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string GRANTID { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OPERID { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OPERNAME { get; set; }
        public KC21XML KC21XML { get; set; }
        public List<KC22ROW> KC22XML { get; set; }
        public List<KC33ROW> KC33XML { get; set; }
    }
    public class KC21XML
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string AKA130 { get; set; }
        /// <summary>
        /// 门诊日期YYYYMMDDHH24MISS
        /// </summary>
        public string AKC192 { get; set; }
        /// <summary>
        /// 入院诊断疾病编码/门诊诊断疾病编码
        /// </summary>
        public string AKC193 { get; set; }
        /// <summary>
        /// 入院诊断疾病名称/门诊诊断疾病名称
        /// </summary>
        public string ZKC274 { get; set; }
        /// <summary>
        /// 出院日期 YYYYMMDDHH24MISS
        /// </summary>
        public string AKC194 { get; set; }
        /// <summary>
        /// 出院原因
        /// </summary>
        public string AKC195 { get; set; }
        /// <summary>
        /// 出院诊断疾病编码
        /// </summary>
        public string AKC196 { get; set; }
        /// <summary>
        /// 出院诊断疾病名称
        /// </summary>
        public string ZKC275 { get; set; }
        /// <summary>
        /// 重症精神病标志
        /// </summary>
        public string ZKC285 { get; set; }
        /// <summary>
        /// 医院内部挂号单号
        /// </summary>
        public string ZKC286 { get; set; }
        /// <summary>
        /// 科室编号
        /// </summary>
        public string BKF040 { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ZKC272 { get; set; }
        /// <summary>
        /// 主治医师编码
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// 主治医师姓名
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// 医保编号
        /// </summary>
        public string AAC001 { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CKC502 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string AAC003 { get; set; }
        /// <summary>
        /// 公民身份号码
        /// </summary>
        public string AAE135 { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string AAC004 { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public decimal BAE450 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// 经办时间YYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC22ROW
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// 处方日期 YYYYMMDDHH24MISS
        /// </summary>
        public string AKC221 { get; set; }
        /// <summary>
        /// 医保药品/项目编码
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// 医保药品/项目名称
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// 医院收费项目编码
        /// </summary>
        public string AKC515 { get; set; }
        /// <summary>
        /// 医院收费项目名称
        /// </summary>
        public string AKC223 { get; set; }
        /// <summary>
        /// 项目类别1:药品 2：诊疗项目 3：服务设施
        /// </summary>
        public string AKC224 { get; set; }
        /// <summary>
        /// 费用类别
        /// </summary>
        public string AKA063 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal AKC225 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal AKC226 { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal AKC227 { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string AKA070 { get; set; }
        /// <summary>
        /// 药品剂量单位
        /// </summary>
        public string CKC132 { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string AKA074 { get; set; }
        /// <summary>
        /// 每次用量
        /// </summary>
        public decimal? AKA071 { get; set; }
        /// <summary>
        /// 药品每日最大剂量
        /// </summary>
        public decimal? AKA075 { get; set; }
        /// <summary>
        /// 使用频次
        /// </summary>
        public string AKA072 { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        public string AKA073 { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        public string ZKA101 { get; set; }
        /// <summary>
        /// 当前单位标记
        /// </summary>
        public string AKA067 { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        public decimal AKC229 { get; set; }
        /// <summary>
        /// 草药单复方标志
        /// </summary>
        public string BKC127 { get; set; }
        /// <summary>
        /// 处方医师编码
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// 处方医师姓名
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string AAE011 { get; set; }
        /// <summary>
        /// 经办时间 YYYYMMDDHH24MISS
        /// </summary>
        public string AAE036 { get; set; }
    }

    public class KC33ROW
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 诊断顺序
        /// </summary>
        public int BKE150 { get; set; }
        /// <summary>
        /// 是否主诊断
        /// </summary>
        public string CKC305 { get; set; }
        /// <summary>
        /// 诊断日期YYYYMMDDHH24MISS
        /// </summary>
        public string CKC304 { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string CKC302 { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string CKC303 { get; set; }
        /// <summary>
        /// 诊断医师编码
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// 诊断医师姓名
        /// </summary>
        public string ZKC271 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string AAE013 { get; set; }
    }
}
