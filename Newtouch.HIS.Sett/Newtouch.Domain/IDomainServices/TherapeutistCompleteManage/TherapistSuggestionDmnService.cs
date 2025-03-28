using Newtouch.Common.Model;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ITherapistSuggestionDmnService
    {
        void SaveData(IList<TherapistSuggestionEntity> itemList, IList<string> delIds, string orgId);
        List<MzZyPatInfoDto> GetMzZyPatInfo(string mzzyh, string orgId, string brlx);
        List<TherapistAdviceDto> GetZLJYList(string mzzyh, string orgId, string brlx);

        /// <summary>
        /// 获取治疗建议列表（API用）
        /// </summary>
        /// <param name="mzzyh"></param>
        /// <param name="orgId"></param>
        /// <param name="brlx"></param>
        /// <param name="startTime">开立起始时间</param>
        /// <returns></returns>
        IList<TherapistSuggestionVO> GetTherapistSuggestionList(string mzzyh, string orgId, string brlx, DateTime startTime);

        /// <summary>
        /// 更新建议的转换状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(string orgId, IList<SuggestionCvStatuUpdateDTO> cvList);
    }
}
