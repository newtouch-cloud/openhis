using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 10:04
    /// 描 述：手术申请记录
    /// </summary>
    [Table("OR_ApplyInfo")]
    public class ORApplyInfoEntity : IEntity<ORApplyInfoEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 申请编码
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string xb { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        public string csrq { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        /// <returns></returns>
        public string sfz { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// 拟手术名称
        /// </summary>
        /// <returns></returns>
        public string ssmcn { get; set; }

        /// <summary>
        /// 申请手术代码
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// 手术时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sssj { get; set; }

        /// <summary>
        /// 主刀医生编号
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }

        /// <summary>
        /// 主刀医生姓名
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }
		/// <summary>
		/// 麻醉医师
		/// </summary>
		public string mzys { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }
        /// <summary>
        /// 手术部位
        /// </summary>
        public string ssbw { get; set; }
        /// <summary>
        /// 是否隔离
        /// </summary>
        public string isgl { get; set; }
        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }   
        
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        //public string nl { get; set; }
    }
}