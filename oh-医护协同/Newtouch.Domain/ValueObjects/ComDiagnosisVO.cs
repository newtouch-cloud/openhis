using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class ComDiagnosisVO
    {
        public string zdmc { get; set; }
        public string zdCode { get; set; }
        //public string py { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 常用诊断名称
        /// </summary>
        public string cyzdmc { get; set; }
        /// <summary>
        /// 常用诊断编码
        /// </summary>
        public string cyzdbm { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 常用诊断类型(1 西医 2 中医)
        /// </summary>
        public string cyzdtype { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 状态(0 无效 1 有效)
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 个人还是科室
        /// </summary>
        public string isgr { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public System.Int64? px { get; set; }

    }
}
