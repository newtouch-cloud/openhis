using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// 科室发药查询
    /// </summary>
    public class DepartmentDrugsVO
    { /// <summary>
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
        public System.String Ypmc { get; set; }

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
        public System.Int32 djlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Pdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Rkbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Ckbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Rksj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Cksj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Rkczy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Ckczy { get; set; }

        /// <summary>
        /// 来自 xt_ypcrkfs表的方式代码
        /// </summary>
        public System.String Crkfsdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Czsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Sqsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Shczy { get; set; }

        /// <summary>
        /// 0:未审核 1:已审核 
        /// </summary>
        public System.String shzt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String crkmxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String zstbzt { get; set; }
        public System.String ckdw { get; set; }
        public System.String rkdw { get; set; }

    }
}
