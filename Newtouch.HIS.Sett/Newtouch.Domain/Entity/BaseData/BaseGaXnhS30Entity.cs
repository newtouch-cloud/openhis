using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.BaseData
{
    [Table("Gaxnh_S30")]
    public class BaseGaXnhS30Entity : IEntity<BaseGaXnhS30Entity>
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 疾病中文名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 项目类别
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string dosageForm { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 使用级别
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// 补偿比例
        /// </summary>
        public string scale { get; set; }
        /// <summary>
        /// 医保范围1保内 0保外
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 是否基药
        /// </summary>
        public string isBase { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// 价格（省）
        /// </summary>
        public string price5 { get; set; }
        /// <summary>
        /// 价格（市）
        /// </summary>
        public string price4 { get; set; }
        /// <summary>
        /// 价格（县）
        /// </summary>
        public string price3 { get; set; }
        /// <summary>
        /// 价格（乡）
        /// </summary>
        public string price2 { get; set; }
        /// <summary>
        /// 价格（村）
        /// </summary>
        public string price1 { get; set; }
        /// <summary>
        /// 限价（省）
        /// </summary>
        public string limitPrice5 { get; set; }
        /// <summary>
        /// 限价（市）
        /// </summary>
        public string limitPrice4 { get; set; }
        /// <summary>
        /// 限价（县）
        /// </summary>
        public string limitPrice3 { get; set; }
        /// <summary>
        /// 限价（乡）
        /// </summary>
        public string limitPrice2 { get; set; }
        /// <summary>
        /// 限价（村）
        /// </summary>
        public string limitPrice1 { get; set; }
        /// <summary>
        /// 目录级别
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// 状态0未审核1已审核2未通过
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string datetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

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
    }
}
