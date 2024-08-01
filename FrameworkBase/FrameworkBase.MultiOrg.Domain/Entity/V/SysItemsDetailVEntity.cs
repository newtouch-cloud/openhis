using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 字典项
    /// </summary>
    [Table("V_S_Sys_ItemsDetail")]
    public class SysItemsDetailVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
		
		/// <summary>
        /// 顶级组织机构
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 关联字典分类Id
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

    }
}
