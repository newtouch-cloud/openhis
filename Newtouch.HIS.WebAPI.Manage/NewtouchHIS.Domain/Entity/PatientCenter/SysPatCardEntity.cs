using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    ///<summary>
    /// 就诊卡
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_card", "SysPatCardEntity")]
    public class SysPatCardEntity : IEntity
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string CardId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "CardNo长度限制为500")]
        public string CardNo { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CardType不可为空")]
        [StringLength(50, ErrorMessage = "CardType长度限制为50")]
        public string CardType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CardTypeName不可为空")]
        [StringLength(50, ErrorMessage = "CardTypeName长度限制为50")]
        public string CardTypeName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cbdbm长度限制为20")]
        public string cbdbm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "cblb长度限制为50")]
        public string cblb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "grbh长度限制为200")]
        public string grbh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xzlx长度限制为20")]
        public string xzlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "patid不可为空")]
        public int patid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "hzxm不可为空")]
        [StringLength(50, ErrorMessage = "hzxm长度限制为50")]
        public string hzxm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(5, ErrorMessage = "brxz长度限制为5")]
        public string brxz { get; set; }
    }

    public class SysPatCardIndexVO
    {
        public int patid { get; set; }
        public string hzxm { get; set; }
        public string CardNo { get; set; }
        public string CardTypeName { get; set; }
        public string brxz { get; set; }
    }
}
