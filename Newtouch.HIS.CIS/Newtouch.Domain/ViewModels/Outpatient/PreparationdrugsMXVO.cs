using Newtouch.Domain.Entity;
using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// 备药
    /// </summary>
    public class PreparationdrugsMXVO
    {
        public string Id { get; set; }
        public string byId { get; set; }

        public string OrganizeId { get; set; }
        public string ypdm { get; set; }
        public string sl { get; set; }
        public string dw { get; set; }
        public string zhyz { get; set; }
        public string pfj { get; set; }
        public string lsj { get; set; }
        public string zje { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string gg { get; set; }
        public string yplb { get; set; }
        public string yxq { get; set; }
        public string sccj { get; set; }
        public string ypmc { get; set; }
        public string pc { get; set; }
        public string ph { get; set; }
        public string operate { get; set; }
    }
}
