using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class HisPatinfoVO
    {
        /// <summary>
        /// 分页
        /// </summary>
        public Pagination pagination { get; set; }
        /// <summary>
        /// 同步his患者信息列表
        /// </summary>
        public List<HisPatinfoDto> List { get; set; }
    }
}
