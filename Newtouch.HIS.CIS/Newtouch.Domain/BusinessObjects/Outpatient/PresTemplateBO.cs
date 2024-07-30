using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class PresTemplateBO
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        public int mblx { get; set; }

        /// <summary>
        /// 模板明细
        /// </summary>
        public List<PresTemplateDetailVO> mbmxList { get; set; }
    }
    public class PresTemplateTree
    {
        public string mbmc { get; set; }
        public int cflx { get; set; }
        public string mbId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
