using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 创 建：李鑫
    /// 日 期：2019-07-19 14:00
    /// 描 述：住院结算-医保费用-贵安
    /// </summary>
    [Table("zy_js_gaxnhjyfy_details")]
    public class HospSettlementGAXNHMXFeeEntity : IEntity<HospSettlementGAXNHMXFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public int Id { get; set; }
        public string inpId { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联zy_js jsnm
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 计算名称
        /// </summary>
        public string calcName { get; set; }
        /// <summary>
        /// 计算说明
        /// </summary>
        public string calcMemo { get; set; }
        /// <summary>
        /// 本次计算
        /// </summary>
        public string calcBefore { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string calcAfter { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
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

    }
}