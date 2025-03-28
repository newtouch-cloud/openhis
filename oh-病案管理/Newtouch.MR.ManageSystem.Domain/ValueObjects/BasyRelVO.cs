using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class BasyRelVO
    {
        /// <summary>
        /// 病案首页综合视图-关联代码
        /// </summary>
        public string R_YLFKFS { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        /// <returns></returns>
        public string R_BAH { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string R_XB { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        /// <returns></returns>
        public string R_GJ { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        public string R_MZ { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        /// <returns></returns>
        public string R_ZY { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        /// <returns></returns>
        public string R_HY { get; set; }
        /// <summary>
        /// 关系
        /// </summary>
        /// <returns></returns>
        public string R_GX { get; set; }
        /// <summary>
        /// 入院途径
        /// </summary>
        /// <returns></returns>
        public string R_RYTJ { get; set; }
        /// <summary>
        /// 入院科别
        /// </summary>
        /// <returns></returns>
        public string R_RYKB { get; set; }
        /// <summary>
        /// 入院病房
        /// </summary>
        /// <returns></returns>
        public string R_RYBF { get; set; }

        /// <summary>
        /// 转科科别
        /// </summary>
        /// <returns></returns>
        public string R_ZKKB { get; set; }
        /// <summary>
        /// 代码：科室代码
        /// </summary>
        /// <returns></returns>
        public string R_CYKB { get; set; }

        /// <summary>
        /// 出院病房
        /// </summary>
        /// <returns></returns>
        public string R_CYBF { get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        /// <returns></returns>
        public string R_BLZDDM { get; set; }
        /// <summary>
        /// 代码：病人来源代码
        /// </summary>
        /// <returns></returns>
        public string R_BRLY { get; set; }
        /// <summary>
        /// 药物过敏
        /// </summary>
        /// <returns></returns>
        public string R_YWGM { get; set; }
        /// <summary>
        /// 血型
        /// </summary>
        /// <returns></returns>
        public string R_XX { get; set; }

        /// <summary>
        /// 代码：Rh
        /// </summary>
        /// <returns></returns>
        public string R_RH { get; set; }
        /// <summary>
        /// 科主任
        /// </summary>
        /// <returns></returns>
        public string R_KZR { get; set; }

        /// <summary>
        /// 主任（副主任）医师
        /// </summary>
        /// <returns></returns>
        public string R_ZRYS { get; set; }

        /// <summary>
        /// 主治医师
        /// </summary>
        /// <returns></returns>
        public string R_ZZYS { get; set; }

        /// <summary>
        /// 住院医师
        /// </summary>
        /// <returns></returns>
        public string R_ZYYS { get; set; }

        /// <summary>
        /// 责任护士
        /// </summary>
        /// <returns></returns>
        public string R_ZRHS { get; set; }

        /// <summary>
        /// 进修医师
        /// </summary>
        /// <returns></returns>
        public string R_JXYS { get; set; }

        /// <summary>
        /// 实习医师
        /// </summary>
        /// <returns></returns>
        public string R_SXYS { get; set; }

        /// <summary>
        /// 编码员
        /// </summary>
        /// <returns></returns>
        public string R_BMY { get; set; }

        /// <summary>
        /// 代码：病案质量
        /// </summary>
        /// <returns></returns>
        public string R_BAZL { get; set; }

        /// <summary>
        /// 质控医师
        /// </summary>
        /// <returns></returns>
        public string R_ZKYS { get; set; }

        /// <summary>
        /// 质控护士
        /// </summary>
        /// <returns></returns>
        public string R_ZKHS { get; set; }
        /// <summary>
        /// 离院方式
        /// </summary>
        /// <returns></returns>
        public string R_LYFS { get; set; }
        /// <summary>
        /// 病情分型 1 病危 2 病重 3 疑难 4 抢救 5 一般
        /// </summary>
        public string R_BQFX { get; set; }
        /// <summary>
        /// 是否有出院31天内再住院计划手术情况
        /// </summary>
        /// <returns></returns>
        public string R_SFZZYJH { get; set; }
    }
}
