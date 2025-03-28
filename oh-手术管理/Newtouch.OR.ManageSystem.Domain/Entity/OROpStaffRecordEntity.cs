using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术人员记录
    /// </summary>
    [Table("OR_OpStaffRecord")]
    public class OROpStaffRecordEntity : IEntity<OROpStaffRecordEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 手术登记号
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// 人员类别 1 主刀医生 2助理医生 3巡回护士 4洗手护士 
        /// </summary>
        /// <returns></returns>
        public string rylb { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        /// <returns></returns>
        public string rygh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string ryxm { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        /// <returns></returns>
        public int px { get; set; }

        /// <summary>
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}