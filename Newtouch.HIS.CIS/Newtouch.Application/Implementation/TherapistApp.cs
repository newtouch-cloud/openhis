using System.Collections.Generic;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Common.Web;
using Newtouch.Infrastructure;
using System;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// 治疗相关
    /// </summary>
    public class TherapistApp : AppBase, ITherapistApp
    {
        /// <summary>
        /// 拉取待就诊治疗建议列表
        /// </summary>
        /// <param name="brlx">病人类型--1门诊 2住院</param>
        /// <param name="mzzyh">门诊住院号</param>
        /// <returns></returns>
        public IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh,int dayNum)
        {
            if (string.IsNullOrWhiteSpace(brlx) || string.IsNullOrWhiteSpace(mzzyh))
            {
                return null;
            }
            DateTime starttime = DateTime.Now.AddDays(-dayNum);
            var reqObj = new
            {
                OrganizeId = this.OrganizeId,
                brlx = brlx,
                mzzyh = mzzyh,
                startTime= starttime
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<IList<WaitCvTherapistSuggestionDTO>>>(
                "/api/Therapist/TherapistSuggestionQuery", reqObj, autoAppendToken: false);
            return apiResp.data;
        }

        /// <summary>
        /// 更新建议的转换状态
        /// </summary>
        /// <param name="cvList"></param>
        public void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList)
        {
            if (cvList == null || cvList.Count == 0)
            {
                return;
            }
            var reqObj = new
            {
                OrganizeId = this.OrganizeId,
                CvList = cvList,
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "/api/Therapist/UpdateSuggestionCvStatus", reqObj, autoAppendToken: false);
            return;
        }

    }
}
