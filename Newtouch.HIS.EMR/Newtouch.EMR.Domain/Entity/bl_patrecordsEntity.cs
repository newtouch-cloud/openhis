using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_patrecords")]
    public  class bl_patrecordsEntity : IEntity<bl_patrecordsEntity>
    {
        public string Id { get; set; }
        public string bllx { get; set; }
        public string dzbl_id { get; set; }
        public string zyh { get; set; }
        public string mzh { get; set; }
        public string ybdj_id { get; set; }
        public Nullable<int> yebh { get; set; }
        public Nullable<int> djxh { get; set; }
        public decimal pzhm_bl { get; set; }
        public string mbbh { get; set; }
        public string blxtml { get; set; }
        public string blxtmc_yj { get; set; }
        public string blxtmc_hj { get; set; }
        public string blxtmc_xml { get; set; }
        public string ksmc { get; set; }
        public string ksdm { get; set; }
        public string zgysmc { get; set; }
        public string czydm_zgys { get; set; }
        public string zzysmc { get; set; }
        public string czydm_zzys { get; set; }
        public Nullable<System.DateTime> blrq { get; set; }
        public Nullable<System.DateTime> blxtrq { get; set; }
        public string shrmc { get; set; }
        public string czydm_shr { get; set; }
        public Nullable<System.DateTime> shrq { get; set; }
        public string kzrmc { get; set; }
        public string czydm_kzr { get; set; }
        public Nullable<System.DateTime> shrq_kzr { get; set; }
        public Nullable<System.DateTime> zfrq { get; set; }
        public string zfrmc { get; set; }
        public string czydm_zfr { get; set; }
        public string czrmc { get; set; }
        public string czydm_czr { get; set; }
        public string sxysmc { get; set; }
        public string czydm_sxys { get; set; }
        public string sxysxm { get; set; }
        public string czyxm_sxys { get; set; }
        public Nullable<int> blbxzt { get; set; }
        public Nullable<bool> blbczt { get; set; }
        public Nullable<System.DateTime> qmrq { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
        public Nullable<int> IsLock { get; set; }
    }
}
