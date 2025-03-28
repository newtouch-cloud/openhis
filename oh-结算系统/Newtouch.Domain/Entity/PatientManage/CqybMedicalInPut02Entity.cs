using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    /// <summary>
    /// 重庆医保_就诊登记02入参落地
    /// </summary>
    [Table("cqyb_OutPutInPar02")]
    public class CqybMedicalInPut02Entity : IEntity<CqybMedicalInPut02Entity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// 交易类型 1:门诊 2：住院
        /// </summary>
        public string jytype { get; set; }
        /// <summary>
        /// 门诊住院号
        /// </summary>
        public string zymzh { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string yllb { get; set; }
       
        /// <summary>
        /// 社会保障识别号
        /// </summary>
        public string shbzsbh { get; set; }
        /// <summary>
        /// 入院科室编码
        /// </summary>
        public string ryksbm { get; set; }
        /// <summary>
        /// 入院医师编码
        /// </summary>
        public string ryysbm { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public string ryrq { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string jbr { get; set; }
        /// <summary>
        /// 并发症
        /// </summary>
        public string bfz { get; set; }
        /// <summary>
        /// 急诊转住院发生时间
        /// </summary>
        public DateTime? jzzzysj { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string bah { get; set; }
        /// <summary>
        /// 生育证号码
        /// </summary>
        public string syzhm { get; set; }
        /// <summary>
        /// 新生儿出生日期
        /// </summary>
        public DateTime? xsecsrq { get; set; }
        /// <summary>
        /// 居民特殊就诊标记
        /// </summary>
        public string jmtsjzbj { get; set; }
        /// <summary>
        /// 险种类别
        /// </summary>
        public string xzlb { get; set; }
        /// <summary>
        /// 工伤个人编号
        /// </summary>
        public string gsgrbh { get; set; }
        /// <summary>
        /// 工伤单位编号
        /// </summary>
        public string gsdwbh { get; set; }
        /// <summary>
        /// 主诉
        /// </summary>
        public string zs { get; set; }
        /// <summary>
        /// 症状描述
        /// </summary>
        public string zzms { get; set; }
        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int px { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 认证方式 01：社保 02：电子凭证
        /// </summary>

        public string rzfs { get; set; }
        /// <summary>
        /// 入院国家医师代码
        /// </summary>
        public string gjysbm { get; set; }
    }
}
