using System;

namespace Newtouch.HIS.Domain.Entity
{
    [Obsolete("please use the view")]
    public class ItemsEntity : IEntity<ItemsEntity>
    {
        public string Id { get; set; }
        public string F_ParentId { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public bool? F_IsTree { get; set; }
        public int? F_Layers { get; set; }
        public string F_Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public int? px { get; set; }
    }
}
