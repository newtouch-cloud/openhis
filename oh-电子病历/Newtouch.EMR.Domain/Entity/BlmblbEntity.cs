using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-05 11:34
    /// 描 述：病历模板列表
    /// </summary>
    [Table("bl_mblb")]
    public class BlmblbEntity : IEntity<BlmblbEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 通用 1个人 2科室
        /// </summary>
        /// <returns></returns>
        public int? mbqx { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string mbbm { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        /// <returns></returns>
        public string mbmc { get; set; }
        /// <summary>
        /// 病历类型ID
        /// </summary>
        /// <returns></returns>
        public string bllxId { get; set; }
        /// <summary>
        /// 病历类型
        /// </summary>
        /// <returns></returns>
        public string bllxmc { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        /// <returns></returns>
        public string ksbm { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>
        /// <returns></returns>
        public string mblj { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// 是否为空模板0 否 1 是
        /// </summary>
        /// <returns></returns>
        public int? Isempty { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
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

        public string bllx { get; set; }
        /// <summary>
        /// 是否上传医保
        /// </summary>
        public string IsYB { get; set; }
        /// <summary>
        /// 关联医保方法
        /// </summary>
        public string Ybbm { get; set; }
        /// <summary>
        /// 门诊模板标识 1 门诊 
        /// </summary>
        public string mzbz { get; set; }
        /// <summary>
        /// 是否启用数据源加载
        /// </summary>
        public string EnableDataLoad { get; set; }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 模板加载方式
        /// </summary>
        public int? LoadWay { get; set; }
    }



}