using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页手术记录表
    /// </summary>
    [Table("mr_basy_ss")]
    public class MrbasyssEntity : IEntity<MrbasyssEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        /// <summary>
        /// 手术及操作编码
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// 手术及操作日期
        /// </summary>
        /// <returns></returns>
        public DateTime SSJCZRQ { get; set; }

        /// <summary>
        /// 手术级别
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// 手术及操作名称
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// 术者
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// I助
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// II助
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// 切口等级
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// 切口愈合类别
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }
        /// <summary>
        /// 切口愈合描述
        /// </summary>
        public string QKYHDJ { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// 麻醉医师
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }
        public string OrganizeId { get; set; }

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

    }
}