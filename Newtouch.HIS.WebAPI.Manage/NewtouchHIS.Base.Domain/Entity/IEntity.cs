using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// 通用字段基类
    /// (关联机构信息)
    /// </summary>
    public class IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode长度限制为50")]
        public string? LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt不可为空")]
        [StringLength(1, ErrorMessage = "zt长度限制为1")]
        public string zt { get; set; }


        public void NewEntity(string organizeId, string user)
        {
            this.zt = "1";
            this.CreateTime = DateTime.Now;
            this.OrganizeId = organizeId;
            this.CreatorCode = user;
        }
        public void ModifiedEntity(string organizeId, string user, bool isDel = false)
        {
            if (isDel)
            {
                this.zt = "0";
            }
            this.LastModifyTime = DateTime.Now;
            this.OrganizeId = organizeId;
            this.LastModifierCode = user;
        }
    }
}
