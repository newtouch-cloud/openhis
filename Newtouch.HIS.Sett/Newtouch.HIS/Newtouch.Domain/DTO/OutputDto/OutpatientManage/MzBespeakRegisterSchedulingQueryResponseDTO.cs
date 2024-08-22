using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// 门诊预约挂号排班返回报文
    /// </summary>
    public class MzBespeakRegisterSchedulingQueryResponseDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 门诊类型  1：普通门诊    2：急症     3：专家门诊
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 时段
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 专家工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 专家名称
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// 最大预约数
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }
    }
}
