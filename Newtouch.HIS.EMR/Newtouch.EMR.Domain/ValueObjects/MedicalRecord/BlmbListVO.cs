using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class BlmbListVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 通用 1个人 2科室
        /// </summary>
        /// <returns></returns>
        public int? mbqx { get; set; }
        public string parentId { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string mbbm { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        /// <returns></returns>
        public string mbmc { get; set; }
        /// <summary>
        /// 病历类型ID
        /// </summary>
        /// <returns></returns>
        public string bllxId { get; set; }
        /// <summary>
        /// 病历类型
        /// </summary>
        /// <returns></returns>
        public string bllxmc { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        /// <returns></returns>
        public string ksbm { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>
        /// <returns></returns>
        public string mblj { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// 是否为空模板0 否 1 是
        /// </summary>
        /// <returns></returns>
        public int? Isempty { get; set; }
        /// <summary>
        /// 是否上传医保
        /// </summary>
        public string IsYB { get; set; }
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
        /// <summary>
        /// 所属病历大类
        /// </summary>
        public string bldl { get; set; }
        /// <summary>
        /// 所属病历大类名称
        /// </summary>
        public string bldlmc { get; set; }
        public string Ybbm { get; set; }
        public string mzbz { get; set; }
        /// <summary>
        /// 模板加载方式
        /// </summary>
        public int? LoadWay { get; set; }
        public string Turl { get; set; }
    }
}
