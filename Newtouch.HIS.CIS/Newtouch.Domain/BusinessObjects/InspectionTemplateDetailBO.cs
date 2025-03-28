using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class InspectionTemplateDetailBO
    {
        /// <summary>
        /// 模板
        /// </summary>
        public InspectionTemplateVO mbEntity { get; set; }

        /// <summary>
        /// 模板组套
        /// </summary>
        public List<GroupPackageEntity> mbztList { get; set; }
    }
}
