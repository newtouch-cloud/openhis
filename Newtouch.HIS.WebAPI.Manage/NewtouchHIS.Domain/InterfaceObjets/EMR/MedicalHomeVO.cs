using NewtouchHIS.Domain.Entity.EMR;

namespace NewtouchHIS.Domain.InterfaceObjets
{
    /// <summary>
    /// 患者病案首页
    /// </summary>
    public class MedicalHomeVO: MedicalHomeBaseVO
    {
        /// <summary>
        /// 诊断列表
        /// </summary>
        public List<MedicalHomeDiagEntity> DiagList { get; set; }
        /// <summary>
        /// 手术列表
        /// </summary>
        public List<MedicalHomeOperationEntity> OperationList { get; set; }
    }

    public class MedicalHomeBaseVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string YLFKFS { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string ZYH { get; set; }

        public string BAH { get; set; }
        public string PATID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string XM { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        /// <returns></returns>
        public string SFZH { get; set; }
        public string XB { get; set; }
        public string XB_Rel { get; set; }
        public DateTime CSRQ { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        public decimal NL { get; set; }
        /// <summary>
        /// 新生儿年龄
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
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string DH { get; set; }
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
        /// 是否病危和病重
        /// </summary>
        /// <returns></returns>
        public string BWHBZ { get; set; }        
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
        /// 主诊医师
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

        /// <summary>
        /// 主治医师
        /// </summary>
        /// <returns></returns>
        public string ZZYS1 { get; set; }


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
        /// 总费用
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// 自付金额
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }
    }
}
