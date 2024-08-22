using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 医保日间手术治疗目录表
    /// </summary>
    public class G_yb_daysrg_trt_list_bVO
    {
        /// <summary>
        /// 日间手术治疗目录ID
        /// </summary>
        public string DAYSRG_TRT_LIST_ID { get; set; }
        /// <summary>
        /// 日间手术病种目录代码
        /// </summary>
        public string DAYSRG_DISE_LIST_CODE { get; set; }
        /// <summary>
        /// 日间手术病种名称
        /// </summary>
        public string DAYSRG_DISE_NAME { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string VALI_FLAG { get; set; }
        /// <summary>
        /// 数据唯一记录号
        /// </summary>
        public string RID { get; set; }

        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CRTE_TIME { get; set; }
        /// <summary>
        ///  数据更新时间
        /// </summary>
        public DateTime UPDT_TIME { get; set; }
        ///// <summary>
        ///// 创建人ID
        ///// </summary>
        //public string CRTER_ID { get; set; }
        ///// <summary>
        ///// 创建人姓名
        ///// </summary>
        //public string CRTER_NAME { get; set; }
        ///// <summary>
        /////   创建机构编号
        ///// </summary>
        //public string CRTE_OPTINS_NO { get; set; }
        ///// <summary>
        ///// 经办人ID
        ///// </summary>
        //public string OPTER_ID { get; set; }
        ///// <summary>
        /////  经办人姓名
        ///// </summary>
        //public string OPTER_NAME { get; set; }
        ///// <summary>
        /////  经办时间
        ///// </summary>
        //public DateTime OPT_TIME { get; set; }
        ///// <summary>
        /////  经办机构编号
        ///// </summary>
        //public string OPTINS_NO { get; set; }
        /// <summary>
        ///  版本号
        /// </summary>
        public string VER { get; set; }
        /// <summary>
        ///  病种内涵
        /// </summary>
        public string DISE_CONT { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string MEMO { get; set; }
        /// <summary>
        ///  版本名称
        /// </summary>
        public string VER_NAME { get; set; }
        /// <summary>
        ///  诊疗指南页码
        /// </summary>
        public string TRT_GUIDE_PAGEN { get; set; }
        /// <summary>
        /// 诊疗指南电子档案
        /// </summary>
        public string TRT_GUIDE_ELECACS { get; set; }
        /// <summary>
        ///  限定手术操作代码
        /// </summary>
        public string QUA_OPRN_OPRT_CODE { get; set; }
        /// <summary>
        ///  限定手术操作名称
        /// </summary>
        public string QUA_OPRN_OPRT_NAME { get; set; }
        ///// <summary>
        /////  下发标志
        ///// </summary>
        //public string ISU_FLAG { get; set; }
        ///// <summary>
        /////  传输数据ID
        ///// </summary>
        //public string TRAM_DATA_ID { get; set; }
        ///// <summary>
        /////  生效时间
        ///// </summary>
        //public DateTime? EFFT_TIME { get; set; }
        ///// <summary>
        /////  失效时间
        ///// </summary>
        //public DateTime? INVD_TIME { get; set; }
    }
}
