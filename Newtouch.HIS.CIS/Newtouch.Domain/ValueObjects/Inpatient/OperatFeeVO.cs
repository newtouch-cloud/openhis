using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 手术费用补录
    /// </summary>
    public class OperatFeeVO
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
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
        /// yzxh
        /// </summary>
        /// <returns></returns>
        public string yzxh { get; set; }
        /// <summary>
        /// zxrq
        /// </summary>
        /// <returns></returns>
        public DateTime? zxrq { get; set; }
        /// <summary>
        /// qqrq
        /// </summary>
        /// <returns></returns>
        public DateTime? qqrq { get; set; }
        /// <summary>
        /// xmdm
        /// </summary>
        /// <returns></returns>
        public string xmdm { get; set; }
        /// <summary>
        /// xmmc
        /// </summary>
        /// <returns></returns>
        public string xmmc { get; set; }
        /// <summary>
        /// dxmdm
        /// </summary>
        /// <returns></returns>
        public string dxmdm { get; set; }
        /// <summary>
        /// gg
        /// </summary>
        /// <returns></returns>
        public string gg { get; set; }
        /// <summary>
        /// dw
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// dwxs
        /// </summary>
        /// <returns></returns>
        public decimal? dwxs { get; set; }
        /// <summary>
        /// ykxs
        /// </summary>
        /// <returns></returns>
        public decimal? ykxs { get; set; }
        /// <summary>
        /// sl
        /// </summary>
        /// <returns></returns>
        public decimal? sl { get; set; }
        /// <summary>
        /// dj
        /// </summary>
        /// <returns></returns>
        public decimal? dj { get; set; }
        /// <summary>
        /// zfdj
        /// </summary>
        /// <returns></returns>
        public decimal? zfdj { get; set; }
        /// <summary>
        /// yhdj
        /// </summary>
        /// <returns></returns>
        public decimal? yhdj { get; set; }
        /// <summary>
        /// zje
        /// </summary>
        /// <returns></returns>
        public decimal? zje { get; set; }
        /// <summary>
        /// zfje
        /// </summary>
        /// <returns></returns>
        public decimal? zfje { get; set; }
        /// <summary>
        /// yhje
        /// </summary>
        /// <returns></returns>
        public decimal? yhje { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? yzxz { get; set; }
        /// <summary>
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
        /// <summary>
        /// flzfje
        /// </summary>
        /// <returns></returns>
        public decimal? flzfje { get; set; }
        /// <summary>
        /// ybshbz
        /// </summary>
        /// <returns></returns>
        public int? ybshbz { get; set; }
        /// <summary>
        /// ybdm
        /// </summary>
        /// <returns></returns>
        public string ybdm { get; set; }
        /// <summary>
        /// jzks
        /// </summary>
        /// <returns></returns>
        public string jzks { get; set; }
        /// <summary>
        /// 1固定0不固定
        /// </summary>
        /// <returns></returns>
        public int? gdzxbz { get; set; }
        /// <summary>
        /// -1收费项目 0药品
        /// </summary>
        /// <returns></returns>
        public int? yzlb { get; set; }
        /// <summary>
        /// WardCode
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// DeptCode
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// fjdm
        /// </summary>
        /// <returns></returns>
        public string fjdm { get; set; }
        /// <summary>
        /// cwdm
        /// </summary>
        /// <returns></returns>
        public string cwdm { get; set; }
        /// <summary>
        /// czyh
        /// </summary>
        /// <returns></returns>
        public string czyh { get; set; }
        /// <summary>
        /// ysgh
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// ysksdm
        /// </summary>
        /// <returns></returns>
        public string ysksdm { get; set; }
        /// <summary>
        /// qrksdm
        /// </summary>
        /// <returns></returns>
        public string qrksdm { get; set; }
        /// <summary>
        /// zxksdm
        /// </summary>
        /// <returns></returns>
        public string zxksdm { get; set; }
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
        /// zxzt
        /// </summary>
        /// <returns></returns>
        public string zxzt { get; set; }

    }
}