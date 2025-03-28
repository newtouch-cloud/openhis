using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院医嘱套餐
    /// </summary>
    [Table("zy_yztc")]
    public class InpatientOrderPackageEntity : IEntity<InpatientOrderPackageEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// tcmc
        /// </summary>
        /// <returns></returns>
        public string tcmc { get; set; }
        /// <summary>
        /// 医嘱类型： 0：药品医嘱，1：抗生素医嘱，2：检查医嘱，3：检验医嘱，4：指示医嘱，5：普通医嘱 ， 6 停止医嘱， 7  出院带药， 8 膳食医嘱， 9 手术医嘱   10  中草药 
        /// </summary>
        /// <returns></returns>
        public int tclx { get; set; }
        /// <summary>
        /// 0 个人 1 科室 2 全院
        /// </summary>
        /// <returns></returns>
        public int tcfw { get; set; }
        /// <summary>
        /// 病区代码
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// 医生代码
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}