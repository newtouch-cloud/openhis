using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO
{
    public class TemplateCtrlRequestDto
    {
        /// <summary>
        /// 模板权限关系
        /// </summary>
        public string gxId { get; set; }
        /// <summary>
        /// 岗位Id
        /// </summary>
        public string dutyCode { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string dutyName { get; set; }
        /// <summary>
        /// 权限控制编码（EnummbqxFp）
        /// </summary>
        public int ctrlLevel { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string zt { get; set; }

    }
}
