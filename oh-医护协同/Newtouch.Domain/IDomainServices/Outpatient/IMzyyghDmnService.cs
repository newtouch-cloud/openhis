using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.ViewModels.Outpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 门诊预约挂号
    /// </summary>
    public interface IMzyyghDmnService
    {
        /// <summary>
        /// 根据病历号查询门诊预约挂号信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        IList<MzyyghVO> SelectMzyyghDetail(Pagination pagination, string blh, string organizeId, DateTime regDate);

        /// <summary>
        /// 门诊预约查询
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh">证件号</param>
        /// <param name="organizeId"></param>
        List<MzBespeakRegisterQueryResponseDTO> MzBespeakRegisterQuery(string blh, string kh, string zjh, string organizeId);

        /// <summary>
        /// 获取当前科室或专家已预约挂号总数
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzAlreadyBespeakRegisterCountQueryResponseDTO SelectAlreadyBespeakRegisterCount(string ksCode, string ysgh, DateTime regDate, string regTime, string organizeId);

        /// <summary>
        /// 查询已预约挂号
        /// </summary>
        /// <param name="mzlx">门诊住院标志 1：普通门诊  2：急诊   3：专家门诊</param>
        /// <param name="departmentCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzyyghDetailVo> SelectMzyyghDetail(string mzlx, string departmentCode, string ysgh, DateTime beginTime, DateTime endTime, string organizeId);
    }
}
