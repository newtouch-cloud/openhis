using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院床位使用记录库
    /// </summary>
    [Table("zy_cwsyjlk")]
    public class InpatientBedUseRecordEntity : IEntity<InpatientBedUseRecordEntity>
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
        /// blh
        /// </summary>
        /// <returns></returns>
        public string blh { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// BedCode
        /// </summary>
        /// <returns></returns>
        public string BedCode { get; set; }
        /// <summary>
        /// BedNo
        /// </summary>
        /// <returns></returns>
        public string BedNo { get; set; }
        /// <summary>
        /// WardCode
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// WardName
        /// </summary>
        /// <returns></returns>
        public string WardName { get; set; }
        /// <summary>
        /// RoomCode
        /// </summary>
        /// <returns></returns>
        public string RoomCode { get; set; }
        /// <summary>
        /// RoomName
        /// </summary>
        /// <returns></returns>
        public string RoomName { get; set; }
        /// <summary>
        /// DeptCode
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// DeptName
        /// </summary>
        /// <returns></returns>
        public string DeptName { get; set; }
        /// <summary>
        /// zyysdm
        /// </summary>
        /// <returns></returns>
        public string zyysdm { get; set; }
        /// <summary>
        /// zzysdm
        /// </summary>
        /// <returns></returns>
        public string zzysdm { get; set; }
        /// <summary>
        /// zrysdm
        /// </summary>
        /// <returns></returns>
        public string zrysdm { get; set; }
        /// <summary>
        /// TransWardCode
        /// </summary>
        /// <returns></returns>
        public string TransWardCode { get; set; }
        /// <summary>
        /// TransDeptCode
        /// </summary>
        /// <returns></returns>
        public string TransDeptCode { get; set; }
        /// <summary>
        /// TransBedCode
        /// </summary>
        /// <returns></returns>
        public string TransBedCode { get; set; }
        /// <summary>
        /// (0当前1转床2转区3出区)
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// OccBeginDate
        /// </summary>
        /// <returns></returns>
        public DateTime? OccBeginDate { get; set; }
        /// <summary>
        /// OccEndDate
        /// </summary>
        /// <returns></returns>
        public DateTime? OccEndDate { get; set; }
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