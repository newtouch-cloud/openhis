using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class bafeeRelVO : IEntity<bafeeRelEntity>
    {
        /// <summary>
        /// 大类名称
        /// </summary>
        public string name { get; set; }
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string sfxm { get; set; }
        public string sfxmmc { get; set; }
        public string feetypecode { get; set; }
        public string zt { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
