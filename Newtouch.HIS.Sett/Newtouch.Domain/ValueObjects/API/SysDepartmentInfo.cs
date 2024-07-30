using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDepartmentInfo
    {
        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 可是编号
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public byte mzzybz { get; set; }

    }
}
