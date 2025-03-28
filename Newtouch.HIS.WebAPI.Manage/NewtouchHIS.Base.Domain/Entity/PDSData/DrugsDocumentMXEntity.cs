using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.PDSData
{
    [Tenant(DBEnum.PdsDb)]
    [SugarTable("xt_yp_crkmx", "出入库单据明细")]
    public partial class DrugsDocumentMXEntity : ISysEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String crkmxId { get; set; }

        /// <summary>
        /// 来自表 XT_YP_CRKDJK的主键
        /// </summary>
        public System.String crkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String sldmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Fph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Kprq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Dprq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Lsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Ykpfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Yklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Sl { get; set; }

        /// <summary>
        /// 默认值1
        /// </summary>
        public System.Int32 Rkzhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Rkbmkc { get; set; }

        /// <summary>
        /// 默认值1
        /// </summary>
        public System.Int32 Ckzhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Ckbmkc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Wg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? zbbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String jkzcz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String hgzm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ysjg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Thyy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Cljg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? scrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? kl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? jj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String pc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String LastModifierCode { get; set; }

        /// <summary>
        /// 出库单位（单位名称）
        /// </summary>
        public System.String ckdw { get; set; }

        /// <summary>
        /// 入库单位（单位名称）
        /// </summary>
        public System.String rkdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? pfjze { get; set; }

        public System.String? isyfy { get; set; }
        
    }    
}
