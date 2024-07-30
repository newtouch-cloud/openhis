using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class GroupPackageDetailBO
    {
        /// <summary>
        /// 
        /// </summary>
        public GroupPackageEntity ztEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GroupPackageItemVO> ztxmList { get; set; }
    }
}
