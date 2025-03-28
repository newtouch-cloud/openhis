using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：住院患者信息
    /// </summary>
    [Table("zy_brjbxx")]
    public class ZybrjbxxEntity : IEntity<ZybrjbxxEntity>
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
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// blh
        /// </summary>
        /// <returns></returns>
        public string blh { get; set; }
        /// <summary>
        /// xm
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }
        /// <summary>
        /// py
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// wb
        /// </summary>
        /// <returns></returns>
        public string wb { get; set; }
        /// <summary>
        /// sfzh
        /// </summary>
        /// <returns></returns>
        public string sfzh { get; set; }
        /// <summary>
        /// sex
        /// </summary>
        /// <returns></returns>
        public string sex { get; set; }
        /// <summary>
        /// birth
        /// </summary>
        /// <returns></returns>
        public DateTime? birth { get; set; }
        /// <summary>
        /// zybz
        /// </summary>
        /// <returns></returns>
        public int zybz { get; set; }
        /// <summary>
        /// sfqj
        /// </summary>
        /// <returns></returns>
        public bool? sfqj { get; set; }
        /// <summary>
        /// DeptCode
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// WardCode
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// ysgh
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// BedCode
        /// </summary>
        /// <returns></returns>
        public string BedCode { get; set; }
        public string BedName { get; set; }
        /// <summary>
        /// ryrq
        /// </summary>
        /// <returns></returns>
        public DateTime ryrq { get; set; }
        /// <summary>
        /// rqrq
        /// </summary>
        /// <returns></returns>
        public DateTime? rqrq { get; set; }
        /// <summary>
        /// cqrq
        /// </summary>
        /// <returns></returns>
        public DateTime? cqrq { get; set; }
        /// <summary>
        /// wzjb
        /// </summary>
        /// <returns></returns>
        public string wzjb { get; set; }
        /// <summary>
        /// hljb
        /// </summary>
        /// <returns></returns>
        public string hljb { get; set; }
        /// <summary>
        /// ryfs
        /// </summary>
        /// <returns></returns>
        public string ryfs { get; set; }
        /// <summary>
        /// cyfs
        /// </summary>
        /// <returns></returns>
        public string cyfs { get; set; }
        /// <summary>
        /// gdxmzxrq
        /// </summary>
        /// <returns></returns>
        public DateTime? gdxmzxrq { get; set; }
        /// <summary>
        /// brxzdm
        /// </summary>
        /// <returns></returns>
        public string brxzdm { get; set; }
        /// <summary>
        /// brxzmc
        /// </summary>
        /// <returns></returns>
        public string brxzmc { get; set; }
        /// <summary>
        /// cardno
        /// </summary>
        /// <returns></returns>
        public string cardno { get; set; }
        /// <summary>
        /// cardtype
        /// </summary>
        /// <returns></returns>
        public string cardtype { get; set; }
        /// <summary>
        /// lxr
        /// </summary>
        /// <returns></returns>
        public string lxr { get; set; }
        /// <summary>
        /// lxrgx
        /// </summary>
        /// <returns></returns>
        public string lxrgx { get; set; }
        /// <summary>
        /// lxrdh
        /// </summary>
        /// <returns></returns>
        public string lxrdh { get; set; }
        /// <summary>
        /// zddm
        /// </summary>
        /// <returns></returns>
        public string zddm { get; set; }
        /// <summary>
        /// zdmc
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// cyzddm
        /// </summary>
        /// <returns></returns>
        public string cyzddm { get; set; }
        /// <summary>
        /// cyzdmc
        /// </summary>
        /// <returns></returns>
        public string cyzdmc { get; set; }
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
        /// <summary>
        /// 文档状态
        /// </summary>
        public string RecordStu { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? CommitTime { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Commitor { get; set; }
        /// <summary>
        /// 病房
        /// </summary>
        public string bfNo { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string mz { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string gj { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string zy { get; set; }

        /// <summary>
        /// 年龄显示
        /// </summary>
        public string nlshow { get; set; }
    }
}