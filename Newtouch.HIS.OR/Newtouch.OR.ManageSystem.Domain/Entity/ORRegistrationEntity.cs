using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术登记
    /// </summary>
    [Table("OR_Registration")]
    public class ORRegistrationEntity : IEntity<ORRegistrationEntity>
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
        /// 排班ID
        /// </summary>
        /// <returns></returns>
        public string ArrangeId { get; set; }

        /// <summary>
        /// 申请编码
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// 手术登记号
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// 申请状态 
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        /// <returns></returns>
        public string ryzd { get; set; }

        /// <summary>
        /// 入院诊断名称
        /// </summary>
        /// <returns></returns>
        public string ryzdmc { get; set; }

        /// <summary>
        /// 术后诊断
        /// </summary>
        /// <returns></returns>
        public string sszd { get; set; }

        /// <summary>
        /// 术后诊断名称
        /// </summary>
        /// <returns></returns>
        public string sszdmc { get; set; }

        /// <summary>
        /// 病情
        /// </summary>
        /// <returns></returns>
        public string shbq { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// 手术代码
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// 手术安排时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ssapsj { get; set; }

        /// <summary>
        /// 手术申请时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sssqsj { get; set; }

        /// <summary>
        /// 手术开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sskssj { get; set; }

        /// <summary>
        /// 手术结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ssjssj { get; set; }

        ///// <summary>
        ///// 主刀医生编号
        ///// </summary>
        ///// <returns></returns>
        //public string ysgh { get; set; }

        ///// <summary>
        ///// 主刀医生姓名
        ///// </summary>
        ///// <returns></returns>
        //public string ysxm { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }

        /// <summary>
        /// 手术室
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }

        /// <summary>
        /// 台次
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }

        ///// <summary>
        ///// 助理医生
        ///// </summary>
        ///// <returns></returns>
        //public string zlys1 { get; set; }

        ///// <summary>
        ///// 助理医生姓名
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm1 { get; set; }

        ///// <summary>
        ///// zlys2
        ///// </summary>
        ///// <returns></returns>
        //public string zlys2 { get; set; }

        ///// <summary>
        ///// zlysxm2
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm2 { get; set; }

        ///// <summary>
        ///// zlys3
        ///// </summary>
        ///// <returns></returns>
        //public string zlys3 { get; set; }

        ///// <summary>
        ///// zlysxm3
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm3 { get; set; }

        ///// <summary>
        ///// zlys4
        ///// </summary>
        ///// <returns></returns>
        //public string zlys4 { get; set; }

        ///// <summary>
        ///// zlysxm4
        ///// </summary>
        ///// <returns></returns>
        //public string zlysxm4 { get; set; }

        /// <summary>
        /// 切口等级
        /// </summary>
        /// <returns></returns>
        public string qkdj { get; set; }

        /// <summary>
        /// 是否隔离 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string isgl { get; set; }

        /// <summary>
        /// 是否有菌 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string isjun { get; set; }

        /// <summary>
        /// 输血量
        /// </summary>
        /// <returns></returns>
        public decimal? shuxl { get; set; }

        /// <summary>
        /// 失血量
        /// </summary>
        /// <returns></returns>
        public decimal? shixl { get; set; }

        /// <summary>
        /// 总入量
        /// </summary>
        /// <returns></returns>
        public decimal? zrxl { get; set; }

        /// <summary>
        /// 总出量
        /// </summary>
        /// <returns></returns>
        public decimal? zcxl { get; set; }

        ///// <summary>
        ///// 巡回护士
        ///// </summary>
        ///// <returns></returns>
        //public string xhhs { get; set; }

        ///// <summary>
        ///// 巡回护士姓名
        ///// </summary>
        ///// <returns></returns>
        //public string xhhsxm { get; set; }

        ///// <summary>
        ///// 洗手护士
        ///// </summary>
        ///// <returns></returns>
        //public string xshs { get; set; }

        ///// <summary>
        ///// 洗手护士姓名
        ///// </summary>
        ///// <returns></returns>
        //public string xshsxm { get; set; }

        /// <summary>
        /// 手术部位
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }

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
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }

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

    }
}