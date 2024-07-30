using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class ZymeddocsrelationVO
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// blId
        /// </summary>
        /// <returns></returns>
        public string blId { get; set; }
        /// <summary>
        /// 病历类型Id
        /// </summary>
        /// <returns></returns>
        public string mbId { get; set; }
        /// <summary>
        /// 病历名称
        /// </summary>
        /// <returns></returns>
        public string blmc { get; set; }
        /// <summary>
        /// 病历标题
        /// </summary>
        /// <returns></returns>
        public string blbt { get; set; }
        /// <summary>
        /// 是否已签名 0 未签 1 已签
        /// </summary>
        /// <returns></returns>
        public int? blzt { get; set; }
        /// <summary>
        /// ysgh
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// ysxm
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }
        /// <summary>
        /// blrq
        /// </summary>
        /// <returns></returns>
        public DateTime? blrq { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// 是否父节点 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public int? IsParent { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        public string bllx { get; set; }
    }
}
