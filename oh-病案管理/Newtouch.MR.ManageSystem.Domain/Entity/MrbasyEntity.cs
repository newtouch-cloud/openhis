using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页
    /// </summary>
    [Table("mr_basy")]
    public class MrbasyEntity : IEntity<MrbasyEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 医疗付款方式
        /// </summary>
        /// <returns></returns>
        public string YLFKFS { get; set; }

        /// <summary>
        /// 健康卡号
        /// </summary>
        /// <returns></returns>
        public string JKKH { get; set; }

        /// <summary>
        /// 住院次数
        /// </summary>
        /// <returns></returns>
        public string ZYCS { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// PATID
        /// </summary>
        /// <returns></returns>
        public string PATID { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string XB { get; set; }

        /// <summary>
        /// 格式为：YYYYMMDD，例如：20131125
        /// </summary>
        /// <returns></returns>
        public DateTime CSRQ { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        public decimal NL { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        /// <returns></returns>
        public string GJ { get; set; }

        /// <summary>
        /// 11
        /// </summary>
        /// <returns></returns>
        public decimal? BZYZSNL { get; set; }

        /// <summary>
        /// 新生儿出生体重(克)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// 新生儿入院体重(克）
        /// </summary>
        /// <returns></returns>
        public decimal? XSERYTZ { get; set; }

        /// <summary>
        /// 出生地
        /// </summary>
        /// <returns></returns>
        public string CSD { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        /// <returns></returns>
        public string GG { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        /// <returns></returns>
        public string SFZH { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        /// <returns></returns>
        public string ZY { get; set; }

        /// <summary>
        /// 婚姻
        /// </summary>
        /// <returns></returns>
        public string HY { get; set; }

        /// <summary>
        /// 现住址
        /// </summary>
        /// <returns></returns>
        public string XZZ { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string DH { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        /// <returns></returns>
        public string XZZYB { get; set; }

        /// <summary>
        /// 户口地址
        /// </summary>
        /// <returns></returns>
        public string HKDZ { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        /// <returns></returns>
        public string HKDYB { get; set; }

        /// <summary>
        /// 工作单位及地址
        /// </summary>
        /// <returns></returns>
        public string GZDWJDZ { get; set; }

        /// <summary>
        /// 单位电话
        /// </summary>
        /// <returns></returns>
        public string DWDH { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        /// <returns></returns>
        public string DWYB { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        /// <returns></returns>
        public string LXRXM { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        /// <returns></returns>
        public string GX { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string LXRDZ { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string LXRDH { get; set; }

        /// <summary>
        /// 入院途径
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        /// <returns></returns>
        public DateTime RYSJ { get; set; }

        /// <summary>
        /// 时
        /// </summary>
        /// <returns></returns>
        public decimal? RYSJS { get; set; }

        /// <summary>
        /// 入院科别
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }

        /// <summary>
        /// 入院病房
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// 转科科别
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// 格式为：YYYYMMDD，例如：20131125
        /// </summary>
        /// <returns></returns>
        public DateTime CYSJ { get; set; }

        /// <summary>
        /// 时
        /// </summary>
        /// <returns></returns>
        public decimal? CYSJS { get; set; }

        /// <summary>
        /// 代码：科室代码
        /// </summary>
        /// <returns></returns>
        public string CYKB { get; set; }

        /// <summary>
        /// 出院病房
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }

        /// <summary>
        /// 实际住院(天)
        /// </summary>
        /// <returns></returns>
        public string SJZYTS { get; set; }

        /// <summary>
        /// 门(急)诊诊断
        /// </summary>
        /// <returns></returns>
        public string MZZD { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        /// <returns></returns>
        public string MZZDDM { get; set; }

        /// <summary>
        /// 实施临床路径
        /// </summary>
        /// <returns></returns>
        public string SSLCLJ { get; set; }

        /// <summary>
        /// 抢救次数
        /// </summary>
        /// <returns></returns>
        public decimal? QJCS { get; set; }

        /// <summary>
        /// 抢救成功次数
        /// </summary>
        /// <returns></returns>
        public decimal? QJCGCS { get; set; }

        /// <summary>
        /// 确诊日期
        /// </summary>
        /// <returns></returns>
        public DateTime? QZRQ { get; set; }

        /// <summary>
        /// 是否择期手术
        /// </summary>
        /// <returns></returns>
        public string ZQSS { get; set; }

        /// <summary>
        /// 代码：病人来源代码
        /// </summary>
        /// <returns></returns>
        public string BRLY { get; set; }

        /// <summary>
        /// 中毒的外部原因
        /// </summary>
        /// <returns></returns>
        public string WBYY { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        /// <returns></returns>
        public string H23 { get; set; }

        /// <summary>
        /// 病理诊断出
        /// </summary>
        /// <returns></returns>
        public string BLZD { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }

        /// <summary>
        /// 病理号
        /// </summary>
        /// <returns></returns>
        public string BLH { get; set; }

        /// <summary>
        /// 药物过敏
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }

        /// <summary>
        /// 过敏药物疾病
        /// </summary>
        /// <returns></returns>
        public string GMYW { get; set; }

        /// <summary>
        /// 死亡患者尸检
        /// </summary>
        /// <returns></returns>
        public string SWHZSJ { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        /// <returns></returns>
        public string XX { get; set; }

        /// <summary>
        /// 代码：Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }

        /// <summary>
        /// 科主任
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// 主任（副主任）医师
        /// </summary>
        /// <returns></returns>
        public string ZRYS { get; set; }

        /// <summary>
        /// 主治医师
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

        /// <summary>
        /// 住院医师
        /// </summary>
        /// <returns></returns>
        public string ZYYS { get; set; }

        /// <summary>
        /// 责任护士
        /// </summary>
        /// <returns></returns>
        public string ZRHS { get; set; }

        /// <summary>
        /// 进修医师
        /// </summary>
        /// <returns></returns>
        public string JXYS { get; set; }

        /// <summary>
        /// 实习医师
        /// </summary>
        /// <returns></returns>
        public string SXYS { get; set; }

        /// <summary>
        /// 编码员
        /// </summary>
        /// <returns></returns>
        public string BMY { get; set; }

        /// <summary>
        /// 代码：病案质量
        /// </summary>
        /// <returns></returns>
        public string BAZL { get; set; }

        /// <summary>
        /// 质控医师
        /// </summary>
        /// <returns></returns>
        public string ZKYS { get; set; }

        /// <summary>
        /// 质控护士
        /// </summary>
        /// <returns></returns>
        public string ZKHS { get; set; }

        /// <summary>
        /// 质控日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ZKRQ { get; set; }

        /// <summary>
        /// 离院方式
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// 医嘱转院，拟接收医疗机构名称
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// 医嘱转社区卫生服务机构/乡镇卫生院，拟接收医疗机构名称
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// 是否有出院31天内再住院计划手术情况
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// 目的
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：天
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：小时
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院前时间：分
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：天
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：小时
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// 颅脑损伤患者昏迷入院后时间：分
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_F { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 临床路径管理 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string LCLJGL { get; set; }

        /// <summary>
        /// 病种管理 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string LCBZGL { get; set; }

        /// <summary>
        /// 病种管理分类
        /// </summary>
        /// <returns></returns>
        public string BZGLFL { get; set; }

        /// <summary>
        /// 病情分型 1 病危 2 病重 3 疑难 4 抢救 5 一般
        /// </summary>
        /// <returns></returns>
        public string BQFX { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// CSD_SN
        /// </summary>
        /// <returns></returns>
        public string CSD_SN { get; set; }

        /// <summary>
        /// CSD_SI
        /// </summary>
        /// <returns></returns>
        public string CSD_SI { get; set; }

        /// <summary>
        /// CSD_QX
        /// </summary>
        /// <returns></returns>
        public string CSD_QX { get; set; }

        /// <summary>
        /// CSD_JD
        /// </summary>
        /// <returns></returns>
        public string CSD_JD { get; set; }

        /// <summary>
        /// XZZ_SN
        /// </summary>
        /// <returns></returns>
        public string XZZ_SN { get; set; }

        /// <summary>
        /// XZZ_SI
        /// </summary>
        /// <returns></returns>
        public string XZZ_SI { get; set; }

        /// <summary>
        /// XZZ_QX
        /// </summary>
        /// <returns></returns>
        public string XZZ_QX { get; set; }

        /// <summary>
        /// XZZ_JD
        /// </summary>
        /// <returns></returns>
        public string XZZ_JD { get; set; }

        /// <summary>
        /// HKDZ_SN
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SN { get; set; }

        /// <summary>
        /// HKDZ_SI
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SI { get; set; }

        /// <summary>
        /// HKDZ_QX
        /// </summary>
        /// <returns></returns>
        public string HKDZ_QX { get; set; }

        /// <summary>
        /// HKDZ_JD
        /// </summary>
        /// <returns></returns>
        public string HKDZ_JD { get; set; }

        /// <summary>
        /// LXRDZ_SN
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SN { get; set; }

        /// <summary>
        /// LXRDZ_SI
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SI { get; set; }

        /// <summary>
        /// LXRDZ_QX
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_QX { get; set; }

        /// <summary>
        /// LXRDZ_JD
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_JD { get; set; }

        /// <summary>
        /// RYCH
        /// </summary>
        /// <returns></returns>
        public string RYCH { get; set; }

        /// <summary>
        /// CYCH
        /// </summary>
        /// <returns></returns>
        public string CYCH { get; set; }

        /// <summary>
        /// 病案状态 1 待录入 2 录入中 3 已归档
        /// </summary>
        /// <returns></returns>
        public string bazt { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// 自付金额
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }

        /// <summary>
        /// 综合医疗服务类(1)一般医疗服务费
        /// </summary>
        /// <returns></returns>
        public decimal? YLFUF { get; set; }

        /// <summary>
        /// 中医辨证论治费
        /// </summary>
        /// <returns></returns>
        public decimal? BZLZF { get; set; }

        /// <summary>
        /// 中医辨证论治会诊费
        /// </summary>
        /// <returns></returns>
        public decimal? ZYBLZHZF { get; set; }

        /// <summary>
        /// 一般治疗操作费
        /// </summary>
        /// <returns></returns>
        public decimal? ZLCZF { get; set; }

        /// <summary>
        /// 护理费
        /// </summary>
        /// <returns></returns>
        public decimal? HLF { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        /// <returns></returns>
        public decimal? QTFY { get; set; }

        /// <summary>
        /// 诊断类(5)病理诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? BLZDF { get; set; }

        /// <summary>
        /// 实验室诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? SYSZDF { get; set; }

        /// <summary>
        /// 影像学诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? YXXZDF { get; set; }

        /// <summary>
        /// 临床诊断项目费
        /// </summary>
        /// <returns></returns>
        public decimal? LCZDXMF { get; set; }

        /// <summary>
        /// 治疗类(9)非手术治疗项目费
        /// </summary>
        /// <returns></returns>
        public decimal? FSSZLXMF { get; set; }

        /// <summary>
        /// 临床物理治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? WLZLF { get; set; }

        /// <summary>
        /// 手术治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? SSZLF { get; set; }

        /// <summary>
        /// 麻醉费
        /// </summary>
        /// <returns></returns>
        public decimal? MAF { get; set; }

        /// <summary>
        /// 手术费
        /// </summary>
        /// <returns></returns>
        public decimal? SSF { get; set; }

        /// <summary>
        /// 康复类(11)康复费
        /// </summary>
        /// <returns></returns>
        public decimal? KFF { get; set; }

        /// <summary>
        /// 中医类(中医和名族医医疗服务)（12）中医诊断
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZLF { get; set; }

        /// <summary>
        /// 中医治疗
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZL { get; set; }

        /// <summary>
        /// 中医外治
        /// </summary>
        /// <returns></returns>
        public decimal? ZYWZ { get; set; }

        /// <summary>
        /// 中医骨伤
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGS { get; set; }

        /// <summary>
        /// 针刺与灸法
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYJF { get; set; }

        /// <summary>
        /// 中医推拿治疗
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTNZL { get; set; }

        /// <summary>
        /// 中医肛肠治疗
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGCZL { get; set; }

        /// <summary>
        /// 中医特殊治疗
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSZL { get; set; }

        /// <summary>
        /// 中医其他
        /// </summary>
        /// <returns></returns>
        public decimal? ZYQT { get; set; }

        /// <summary>
        /// 中医特殊调配加工
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSTPJG { get; set; }

        /// <summary>
        /// 辨证施膳
        /// </summary>
        /// <returns></returns>
        public decimal? BZSS { get; set; }

        /// <summary>
        /// 西药类(15)西药费
        /// </summary>
        /// <returns></returns>
        public decimal? XYF { get; set; }

        /// <summary>
        /// 抗菌药物费
        /// </summary>
        /// <returns></returns>
        public decimal? KJYWF { get; set; }

        /// <summary>
        /// 中药类(16)中成药费
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF { get; set; }

        /// <summary>
        /// 医疗机构中药制剂费
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZJF { get; set; }

        /// <summary>
        /// 中草药费
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF1 { get; set; }

        /// <summary>
        /// 血液和血液制品类(18)血费
        /// </summary>
        /// <returns></returns>
        public decimal? XF { get; set; }

        /// <summary>
        /// 白蛋白类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? BDBLZPF { get; set; }

        /// <summary>
        /// 球蛋白类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? QDBLZPF { get; set; }

        /// <summary>
        /// 凝血因子类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? NXYZLZPF { get; set; }

        /// <summary>
        /// 细胞因子类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? XBYZLZPF { get; set; }

        /// <summary>
        /// 耗材类(23)检查用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? HCYYCLF { get; set; }

        /// <summary>
        /// 治疗用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? YYCLF { get; set; }

        /// <summary>
        /// 手术用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? YCXYYCLF { get; set; }

        /// <summary>
        /// 其他类(26)其他费
        /// </summary>
        /// <returns></returns>
        public decimal? QTF { get; set; }

    }
}