using Newtouch.Domain.Entity;
using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// 备药
    /// </summary>
    public class PreparationdrugsVO 
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }
        public string yfbm { get; set; }
        public string bqbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string shzt { get; set; }
        public string tjsj { get; set; }
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
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        public string yfmc { get; set; }
        public string ksmc { get; set; }
        public string bqmc { get; set; }
    }
}
