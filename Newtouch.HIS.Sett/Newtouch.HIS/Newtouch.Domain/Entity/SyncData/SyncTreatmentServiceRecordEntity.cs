using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 同步治疗记录
    /// </summary>
    [Table("TB_Sync_TreatmentServiceRecord")]
    public class SyncTreatmentServiceRecordEntity : IEntity<SyncTreatmentServiceRecordEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string outerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string serviceCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string serviceDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string collectionMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? units { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? durationPerUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? minutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string siteId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string siteName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string admsNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string therapistId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string therapistName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string disciplineTrack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 就是治疗日志（服务日期），不会变更
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public DateTime DownloadTime { get; set; }

        /// <summary>
        /// 1 未确认 2 已确认 3 作废
        /// </summary>
        public int? clzt { get; set; }

        /// <summary>
        /// 是否自动确认
        /// </summary>
        public bool? zdqrbz { get; set; }

        /// <summary>
        /// 问题记录标志。比如serviceCode我们的基础数据里没有
        /// </summary>
        public bool? wtjlbz { get; set; }

        /// <summary>
        /// 备注。同步数据被搁置原因、不能被确认的原因
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 处理人（UserCode）
        /// </summary>
        public string clr { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? clsj { get; set; }

        /// <summary>
        /// 重新处理标志
        /// </summary>
        public bool? cxclbz { get; set; }

        /// <summary>
        /// 最后处理人（UserCode）
        /// </summary>
        public string zhclr { get; set; }

        /// <summary>
        /// 最后处理时间
        /// </summary>
        public DateTime? zhclsj { get; set; }

        /// <summary>
        /// 计费表Id
        /// </summary>
        public int? jfbId { get; set; }

        public string zt { get; set; }

        public DateTime? serviceDate { get; set; }

    }
}
