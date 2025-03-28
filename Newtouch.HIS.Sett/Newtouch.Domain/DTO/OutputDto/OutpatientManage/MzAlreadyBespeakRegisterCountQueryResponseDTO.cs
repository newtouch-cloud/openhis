using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// 科室或专家已挂号总数查询返回报文
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryResponseDTO
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 已预约挂号总数
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
