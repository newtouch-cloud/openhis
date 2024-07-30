using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 患者过敏信息
    /// </summary>
    public class PatientGmxxDetailVO
    {
        /// <summary>
        /// 过敏信息表主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime csrq { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 过敏项目
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 过敏项目名称
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 过敏类型
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// 过敏类型名称
        /// </summary>
        public string gmlxmc { get; set; }

        /// <summary>
        /// 过敏药品
        /// </summary>
        public string gmyp { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
