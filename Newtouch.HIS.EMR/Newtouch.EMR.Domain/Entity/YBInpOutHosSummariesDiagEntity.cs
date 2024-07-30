using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：出院小结-诊断
    /// </summary>
    [Table("YB_Inp_OutHosSummaries_Diag")]
    public class YBInpOutHosSummariesDiagEntity : IEntity<YBInpOutHosSummariesDiagEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 就诊流水号
        /// </summary>
        /// <returns></returns>
        public string AKC190 { get; set; }

        /// <summary>
        /// 居民健康卡号
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        public string BKC191 { get; set; }

        /// <summary>
        /// 诊断分类1 入院诊断；2 出院诊断
        /// </summary>
        /// <returns></returns>
        public string BKF417 { get; set; }

        /// <summary>
        /// 主从诊断标识1 主要诊断；2 其他诊断；3 病理诊断
        /// </summary>
        /// <returns></returns>
        public string BKF718 { get; set; }

        /// <summary>
        /// 诊断顺位
        /// </summary>
        /// <returns></returns>
        public string BKF512 { get; set; }

        /// <summary>
        /// 西医诊断编码
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// 西医诊断名称
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// 中医病名代码
        /// </summary>
        /// <returns></returns>
        public string BKF460 { get; set; }

        /// <summary>
        /// 中医病名
        /// </summary>
        /// <returns></returns>
        public string BKF459 { get; set; }

        /// <summary>
        /// 中医证候代码
        /// </summary>
        /// <returns></returns>
        public string BKF475 { get; set; }

        /// <summary>
        /// 中医证候
        /// </summary>
        /// <returns></returns>
        public string BKF474 { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// 出院小结主Id
        /// </summary>
        public string OutId { get; set; }

    }
}