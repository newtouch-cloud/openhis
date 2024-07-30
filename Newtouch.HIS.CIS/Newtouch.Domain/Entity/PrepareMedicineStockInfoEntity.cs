using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    [Table("xt_ksby_kcxx")]
    public class PrepareMedicineStockInfoEntity : IEntity<PrepareMedicineStockInfoEntity>
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string kcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ypkw { get; set; }

        /// <summary>
        /// 0：不控制1：控制
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// 已配药未发（领）药的数量
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0：启用1：停用
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// 来自 表 XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// 进价  药库单位
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// 对应药房药房的拆零数
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 产地目录
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pc { get; set; }

        /// <summary>
        /// 是否已锁  0：未锁 ；    1/null：已锁
        /// </summary>
        public string locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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


        public string Updating { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
    }
}
