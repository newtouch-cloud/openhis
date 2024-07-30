using System;

namespace Newtouch.Domain.ValueObjects
{
    public class GetSysBodyPartsVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string bwCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bwmc { get; set; }

        public string bwff { get; set; }

        public string sqlx { get; set; }

        public string bwflCode { get; set; }
        public string bwflmc { get; set; }
    }

    public class GetYxBodyPartsVO {
        public string jcbw { get; set; }
        public string bwflCode { get; set; }
        public string bwflmc { get; set; }
    }
    public class GetSysJcBodyPartsVo
    {
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bwmc { get; set; }
        public string jcff { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string bwflCode { get; set; }
        public string bwflmc { get; set; }
    }
}
