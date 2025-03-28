using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class BaseBllxVO
    {
        public string Id { get; set; }
        public string bllx { get; set; }
        public string bllxmc { get; set; }
        public string RelDutys { get; set; }
        public string RelDutysDesc { get; set; }
        /// <summary>
        /// 病历类型简码
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// 关联数据表 为空时默认
        /// </summary>
        public string relTB { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        /// <summary>
        /// 目录层级 初始层级1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// 目录层级名称 默认一级时间 以※分隔
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// 门诊模板标识 1 门诊 
        /// </summary>
        public string mzbz { get; set; }
    }
}
