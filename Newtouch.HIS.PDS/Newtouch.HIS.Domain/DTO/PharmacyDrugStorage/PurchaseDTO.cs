using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class PurchaseDTO 
    {
        public string cgId { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// 配送点编码
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// 医院计划单号
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// 最晚到货日期
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int ddzt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public List<cgmx> cgmx;
    }

    public class cgmx
    {
        public string OrganizeId { get; set; }
        public string bzsm { get; set; }
        public decimal cgdj { get; set; }
        public string cgjldw { get; set; }
        public decimal cgsl { get; set; }
        public string cglx { get; set; }
        public string czlx { get; set; }
        public string dcpsbs { get; set; }
        public string ggbz { get; set; }
        public string splx { get; set; }
        public string sxh { get; set; }
        public string yqbm { get; set; }
        public string zxspbm { get; set; }
    }
}
