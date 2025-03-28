using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public class MzBespeakRegisterVO
    {
        /// <summary>
        /// 选中科室或专家已预约挂号总数
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
        
        /// <summary>
        /// 选中科室或专家已号总数
        /// </summary>
        public int alreadyRegisterCount { get; set; }

        /// <summary>
        /// 当前操作员
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 全天最大预约数  每个预约时段的最大预约数总和
        /// </summary>
        public int? allBespeakMaxCount { get; set; }

        /// <summary>
        /// 当前患者挂号信息
        /// </summary>
        public BespeakRegisterParamDTO registerInfo { get; set; }

        /// <summary>
        /// 选中科室或专家指定预约时间挂号排班 
        /// </summary>
        public List<MzBespeakRegisterSchedulingQueryResponseDTO> bespeakRegSchedulingDetail { get; set; }

        /// <summary>
        /// 当前患者预约挂号信息  不分科室、专家、日期
        /// </summary>
        public List<MzBespeakRegisterQueryResponseDTO> bespeakRegDetail { get; set; }
    }
}
