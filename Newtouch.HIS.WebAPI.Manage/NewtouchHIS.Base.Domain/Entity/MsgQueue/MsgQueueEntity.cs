using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///消息通知基础类
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("dic_msg_queue", "MsgQueueEntity")]
    public partial class MsgQueueEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppId不可为空")]
        [StringLength(50, ErrorMessage = "AppId长度限制为50")]
        public string AppId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "msgtypecode不可为空")]
        public int msgtypecode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "msgcontent长度限制为500")]
        public string msgcontent { get; set; }

        /// <summary>
        /// Desc:消息内容需要传递参数的格式化处理
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "msgformat长度限制为100")]
        public string msgformat { get; set; }

        /// <summary>
        /// Desc:通知状态 MsgNoticeEnum 0 未发送 ，1 已发送, 2 已读 
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? msgstu { get; set; }

        /// <summary>
        /// Desc:门诊：门诊号  住院：住院号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "patno长度限制为100")]
        public string patno { get; set; }

        /// <summary>
        /// Desc:门诊：处方号等  住院：医嘱号等
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ywlsh长度限制为100")]
        public string ywlsh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ks长度限制为50")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "bq长度限制为50")]
        public string bq { get; set; }

        /// <summary>
        /// Desc:,分割的操作员号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1000, ErrorMessage = "czys长度限制为1000")]
        public string czys { get; set; }

        /// <summary>
        /// Desc:,分割的岗位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "dutylimit长度限制为200")]
        public string dutylimit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode长度限制为50")]
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt不可为空")]
        [StringLength(1, ErrorMessage = "zt长度限制为1")]
        public string zt { get; set; }
    }
}
