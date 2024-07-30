using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO.InputDto
{
    public class SysChargeTemplateDto
    {
        public string sfmbbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmbmc { get; set; }

        /// <summary>
        /// 0  全部  1 门诊 2 住院
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 0 全院模版   1 科室模版   2 个人模版   
        /// </summary>
        public byte kffw { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
