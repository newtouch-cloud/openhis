using Newtouch.Domain.DTO;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 治疗相关
    /// </summary>
    public interface ITherapistApp
    {
        /// <summary>
        /// 拉取待就诊治疗建议列表
        /// </summary>
        /// <param name="brlx">病人类型--1门诊 2住院</param>
        /// <param name="mzzyh">门诊住院号</param>
        /// <returns></returns>
        IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh, int dayNum);

        /// <summary>
        /// 更新建议的转换状态
        /// </summary>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList);

    }
}
