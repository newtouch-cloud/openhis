using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 中医馆_草药推送_记录
    /// </summary>
    [Table("cmm_his_02_record")]
    public class CmmHis02RecordEntity : IEntity<CmmHis02RecordEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 药品编号
        /// 必填
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// 药品名称
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 药品编码
        /// 必填
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 返回结果状态码  1： 成功； 0：失败
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// 返回结果描述  成功；失败(补充失败原因描述)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}