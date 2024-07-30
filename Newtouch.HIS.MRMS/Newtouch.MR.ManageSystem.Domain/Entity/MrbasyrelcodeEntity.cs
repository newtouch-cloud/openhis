using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页基础信息字典关联项
    [Table("mr_basy_rel_code")]
    public class MrbasyrelcodeEntity:IEntity<MrbasyrelcodeEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string SYId { get; set; }
        /// <summary>
        /// 医疗付款方式
        /// </summary>
        /// <returns></returns>
        public string YLFKFS { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string XB { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        /// <returns></returns>
        public string GJ { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        public string MZ { get; set; }
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
        /// 关系
        /// </summary>
        /// <returns></returns>
        public string GX { get; set; }
        /// <summary>
        /// 入院途径
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }
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
        /// 疾病编码
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }
        /// <summary>
        /// 代码：病人来源代码
        /// </summary>
        /// <returns></returns>
        public string BRLY { get; set; }
        /// <summary>
        /// 药物过敏
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }
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
        /// 离院方式
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }
        /// <summary>
        /// 病情分型 1 病危 2 病重 3 疑难 4 抢救 5 一般
        /// </summary>
        public string BQFX { get; set; }
        /// <summary>
        /// 是否有出院31天内再住院计划手术情况
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }
    }
}
