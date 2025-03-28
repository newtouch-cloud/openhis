using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    [Table("xt_accessregistor")]
    public class AccessAuthEntity:IEntity<AccessAuthEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 注册编码
        /// </summary>
        public string RegCode { get; set; }
        /// <summary>
        /// 注册名称
        /// </summary>
        public string RegName { get; set; }
        public string py { get; set; }

        /// <summary>
        /// 权限等级 AuthorizedLevEnum
        /// </summary>
        public int? AuthorizedLev { get; set; }
        /// <summary>
        /// 授权期限 AuthorizedPeriod
        /// </summary>
        public int? AuthorizedPeriod { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string accesskey { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }

    }
}
