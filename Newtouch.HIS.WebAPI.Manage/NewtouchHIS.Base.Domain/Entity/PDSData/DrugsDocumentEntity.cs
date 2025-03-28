using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.PDSData
{
    [Tenant(DBEnum.PdsDb)]
    [SugarTable("xt_yp_crkdj", "出入库单据")]
    public partial class DrugsDocumentEntity : ISysEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public System.String crkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OrganizeId { get; set; }

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
    }    
}
