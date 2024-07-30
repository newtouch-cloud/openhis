using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.Services;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using System.Text;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.WebAPI.Manage.Areas.CIS.Controllers
{
    /// <summary>
    /// 远程诊疗
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteTreatedController : ApiBaseController
    {
        private readonly IMsgQueueDmnService _msgQueue;
        private readonly IPatientInfoDmnService _patientInfoDmn;
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly IRemoteTreatedDmnService _remoteTreatedDmn;
        public RemoteTreatedController(IHttpClientHelper httpClient,
            IMsgQueueDmnService msgQueue, IPatientInfoDmnService patientInfoDmn, ISysStaffDmnService sysStaffDmn, IRemoteTreatedDmnService remoteTreatedDmn)
            : base(httpClient)
        {
            _msgQueue = msgQueue;
            _patientInfoDmn = patientInfoDmn;
            _sysStaffDmn = sysStaffDmn;
            _remoteTreatedDmn = remoteTreatedDmn;
        }
        /// <summary>
        /// 远程诊疗Step1：第三方申请HIS远程诊疗（仅自费）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoteTreatedApplyZfOnly")]
        public async Task<BusResult<TreatedApplyReponse>> RemoteTreatedApplyZfOnlyAsync(Request<TreatedApplyBaseVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyOrg) || string.IsNullOrWhiteSpace(request.Data.ApplyOrgName))
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "申请诊疗机构信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.xm) || string.IsNullOrWhiteSpace(request.Data.kh) || request.Data.patid == null)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "患者关键信息：患者标识、患者姓名、就诊卡不可为空" };
            }
            //校验患者信息、科室医生
            var patInfo = await _patientInfoDmn.GetPatientbyPatid(Convert.ToInt32(request.Data.patid), request.OrganizeId);
            var card = await _patientInfoDmn.GetPatientCard(Convert.ToInt32(request.Data.patid), request.OrganizeId, request.Data.kh);
            if (card.Count == 0)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "患者信息卡信息异常" };
            }
            //查找医生信息
            var doc = await _sysStaffDmn.GetStaffDeptByGh(request.Data.ysgh, request.OrganizeId);
            //暂不校验科室
            if (doc.Count == 0)
            {
                return new BusResult<TreatedApplyReponse> { code = ResponseResultCode.FAIL, msg = "未找到相关的医生信息" };
            }
            var applyInfo = request.Data.Adapt<TreatedApplyVO>();
            applyInfo.brxz = ((int)EnumBrxz.zf).ToString();
            applyInfo.xb = patInfo.xb;
            applyInfo.birth = patInfo.csny;
            applyInfo.OrganizeId = request.OrganizeId;
            applyInfo.AppId = request.AppId;
            var applyId = await _remoteTreatedDmn.AddTreatedApply(applyInfo, request.AppId);
            return new BusResult<TreatedApplyReponse>
            {
                code = ResponseResultCode.SUCCESS,
                msg = $"诊疗申请发送成功",
                Data = new TreatedApplyReponse
                {
                    ApplyId = applyId,
                    sqr = request.Data.sqr,
                    sqlsh = request.Data.sqlsh,
                    sqrgh = request.Data.sqrgh,
                    ApplyOrg = request.Data.ApplyOrg,
                    ApplyOrgName = request.Data.ApplyOrgName,
                }
            };
        }
        #region HIS 内部
        /// <summary>
        /// 远程诊疗Step1：HIS医生同意申请发起诊疗会议
        /// </summary>
        /// <param name="request"></param>
        /// <returns>会议号，诊疗用户Token、会议地址</returns>
        [HttpPost]
        [Route("DoctorMeetingApply")]
        public async Task<BusResult<MeettingResponse>> DoctorMeetingApplyAsync(Request<TreatedUserRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            //查询申请是否正常 
            var apply = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.applyId, request.OrganizeId);
            if (apply == null || (apply.sqzt != (int)EmunRemoteTreatedStu.dqr && apply.sqzt != (int)EmunRemoteTreatedStu.jzz))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "诊疗申请状态异常，请确认是否结束或撤销" };
            }
            if (DateTime.Compare(apply.sqsj ?? DateTime.Now, DateTime.Now.Date) < 0)
            {
                throw new FailedException($"申请诊疗时间:{apply.sqsj}已过期");
            }
            //双方均可发起会议
            if (!(apply.sqr == request.Data.usercode || apply.ysgh == request.Data.usercode))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "诊疗会议必须由诊疗医生或诊疗申请人发起" };
            }
            //该申请尚未生成会议号 生成聊天室房间号
            if (string.IsNullOrWhiteSpace(apply.mettingId) || request.Data.roomReset)
            {
                ChatRoom cr = new ChatRoom();
                apply.mettingId = cr.GetNewRoomId(request.OrganizeId).ToString();
            }
            var tokenReq = new MeettingTokenRequest
            {
                roomid = apply.mettingId,
                device = MediaTerminalType.Browser.ToString(),
                username = $"{request.Data.username}({request.Data.usercode})"
            };
            var tokenResp = await TreatedMeetingToken(tokenReq);
            if (apply.sqzt != (int)EmunRemoteTreatedStu.jzz || !string.IsNullOrWhiteSpace(request.Data.mzh))
            {
                //回写远程申请
                apply.sqzt = (int)EmunRemoteTreatedStu.jzz;
                apply.mzh = request.Data.mzh;
                var modifyApply = await _remoteTreatedDmn.ModifiedApplyStu(apply.Adapt<TreatedApplyEntity>(), request.AppId);
                if (!modifyApply)
                {
                    return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "会议号回写失败，请重新申请会议" };
                }
            }
            //通知云诊所 
            //申请Id  同意诊疗 ：True 会议号：roomid
            return new BusResult<MeettingResponse> { code = ResponseResultCode.SUCCESS, Data = tokenResp };
        }
        #endregion

        /// <summary>
        /// 远程诊疗Step3： 普通用户加入会议
        /// 返回值用于会议室页面验证
        /// To:第三方、HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserJoinMeetingApply")]
        public async Task<BusResult<MeettingResponse>> UserJoinMeetingApplyAsync(Request<TreatedUserRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.roomid))
            {
                return new BusResult<MeettingResponse> { code = ResponseResultCode.FAIL, msg = "会议Id不可为空" };
            }
            var tokenReq = new MeettingTokenRequest
            {
                roomid = request.Data.roomid,
                device = MediaTerminalType.Browser.ToString(),
                username = $"{request.Data.username}({request.Data.usercode})"
            };
            var tokenResp = await TreatedMeetingToken(tokenReq);

            return new BusResult<MeettingResponse> { code = ResponseResultCode.SUCCESS, Data = tokenResp };
        }

        /// <summary>
        /// 诊疗信息查询 by HIS诊疗申请Id
        /// To:第三方、HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyInfo")]
        public async Task<BusResult<TreatedApplyVO>> TreatedApplyInfoAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<TreatedApplyVO> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId) && string.IsNullOrWhiteSpace(request.Data.sqlsh))
            {
                return new BusResult<TreatedApplyVO> { code = ResponseResultCode.FAIL, msg = "his申请Id、第三方申请流水号不可全部为空" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId, request.Data.sqlsh);
            return new BusResult<TreatedApplyVO> { code = ResponseResultCode.SUCCESS, Data = response };
        }
        /// <summary>
        /// 拒绝诊疗申请 
        /// To:HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyRefused")]
        public async Task<BusResult<bool>> TreatedApplyRefusedAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "his申请Id、第三方申请流水号不可全部为空" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "未找到有效诊疗申请信息" };
            }
            if (response.sqzt == (int)EmunRemoteTreatedStu.yth)
            {
                return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "诊疗申请已处理驳回" };
            }
            if (response.sqzt != (int)EmunRemoteTreatedStu.dqr)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "申请状态已变更，无法驳回" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.yth;
            var cancel = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "诊疗申请驳回成功", Data = cancel };
        }
        //[HttpPost]
        //[Route("GotoMeeting")]
        //public async Task<HttpResponseMessage> GotoMeeting(Request<MeettingResponse> request)
        //{
        //    HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.Moved);
        //    resp.Headers.Location = new Uri(RemoteTreatedHelper.RemoteTreatedMeettingRoute);
        //    return resp;
        //}

        #region 外部系统 Only
        /// <summary>
        /// 撤销诊疗申请 
        /// To:第三方
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TreatedApplyCancel")]
        public async Task<BusResult<bool>> TreatedApplyCancelAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "his申请Id、第三方申请流水号不可全部为空" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "未找到有效诊疗申请信息" };
            }
            if (response.sqzt == (int)EmunRemoteTreatedStu.ycx)
            {
                return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "诊疗申请已撤销" };
            }
            if (response.sqzt != (int)EmunRemoteTreatedStu.dqr)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "申请状态已变更，无法撤销" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.ycx;
            var cancel = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "诊疗申请撤销成功", Data = cancel };
        }

        /// <summary>
        /// 发药通知 
        /// To:第三方
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrugDispensing")]
        public async Task<BusResult<bool>> DrugDispensingAsync(Request<TreatedApplyInfoRequest> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ApplyId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "his申请Id、第三方申请流水号不可全部为空" };
            }
            var response = await _remoteTreatedDmn.TreatedApplyInfo(request.Data.ApplyId, request.OrganizeId);
            if (response == null)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "未找到有效诊疗申请信息" };
            }
            response.sqzt = (int)EmunRemoteTreatedStu.yfy;
            var fy = await _remoteTreatedDmn.ModifiedApplyStu(response.Adapt<TreatedApplyEntity>(), request.AppId);
            //调用第三方 诊疗结果通知 接口
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "发药状态更新成功", Data = fy };
        }

        #endregion

        /// <summary>
        /// 诊疗用户身份信息解密
        /// To:第三方、HIS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserIdDecode")]
        public async Task<BusResult<string>> UserIdDecodeAsync(Request<string> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            string data = await Task.Run(() => RemoteTreatedHelper.GetRtcuser(request.Data));
            return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = data };
        }



        #region private
        /// <summary>
        /// 音视频服务接口封装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        private async Task<MeettingResponse> TreatedMeetingToken(MeettingTokenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.roomid) || string.IsNullOrWhiteSpace(request.username))
            {
                throw new FailedException("音视频房间号及用户名不可为空");
            }
            MeettingTokenRequest tokenReq = new MeettingTokenRequest
            {
                roomid = request.roomid,
                username = request.username,
                device = request.device ?? MediaTerminalType.Browser.ToString()
            };
            var tokenResp = await MediaTokenHttpPost(tokenReq);

            //var tokenResp = new MeettingTokenResponse
            //{
            //    username = request.username,
            //    rtcuserid = RemoteTreatedHelper.SetRtcuser(request.username),
            //    roomid = request.roomid,
            //    status = "Success"
            //};
            if (tokenResp.status != "Success" || !string.IsNullOrWhiteSpace(tokenResp.errorCode))
            {
                throw new FailedException("音视频会议接口返回异常：" + tokenResp.errorCode);
            }
            MeettingResponse resp = tokenResp.Adapt<MeettingResponse>();
            resp.organization = RemoteTreatedHelper.Organization;
            resp.device = tokenReq.device;
            resp.roompath = RemoteTreatedHelper.RemoteTreatedMeettingRoute;
            //自动解密
            resp.rtcuserid = await Task.Run(() => RemoteTreatedHelper.GetRtcuser(tokenResp.rtcuserid));
            return resp;
        }

        private async Task<MeettingTokenResponse> MediaTokenHttpPost(MeettingTokenRequest request)
        {
            var header = RemoteTreatedHelper.HeaderItems();
            return await _httpClient.PostAsync<MeettingTokenResponse>($"{RemoteTreatedHelper.OuterMediaTokenRoute}", request.ToJson(), header, Encoding.UTF8);
        }

        #endregion
    }
}
