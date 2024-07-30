using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewtouchHIS.HangFire.Core;
using NewtouchHIS.HangFire.UI.Models;
using NewtouchHIS.HangFire.UI.Models.MRQC;
using NewtouchHIS.HangFire.UI.Utilities;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.HangFire.UI.Controllers
{
    [Route("api/noticejob")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static AppAPIHostConfig _jobApiHost;
        private static string _jobServer;
        private static string _appId;
        public NoticeController(IConfiguration configuration)
        {
            _config = configuration;
            _jobApiHost = ConfigInitHelper.SysConfig.AppAPIHost;
            _jobServer = ConfigInitHelper.SysConfig.AppAPIHost.ScheduleJobHost;
            _appId = ConfigInitHelper.SysConfig.AppId;
        }
        /// <summary>
        /// 病历质控消息处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("MrqcNoticeBeginProc")]
        public async Task<string> MrqcNoticeBeginProcAsync()
        {
            string procIds = string.Empty;
            //循环任务 查找未处理消息
            var msgList = await ApiManageHttpHelper.PostAsync<List<MsgNoticeQueueBasicVO>>($"{ApiManageHelper.NoticeImmediatelyWaitProcApi}", (new Request<object>
            {
                AppId = _appId,
                OrganizeId = ConfigInitHelper.SysConfig.Top_OrganizeId,
                Timestamp = DateTime.Now,
            }).ToJson());
            if (msgList != null && msgList.Data != null && msgList.Data.Count > 0)
            {
                foreach (var msg in msgList.Data)
                {
                    MrqcNoticeDataDTO noticedata = new MrqcNoticeDataDTO();
                    if (!string.IsNullOrWhiteSpace(msg.ContentData))
                    {
                        noticedata = JsonConvert.DeserializeObject<MrqcNoticeDataDTO>(msg.ContentData);
                    }
                    var result = await JobHelper.AddBackgroundJobPost(new HttpJobRequest
                    {
                        serviceUrl = $"{_jobServer}/job",
                        JobName = $"[病历质控消息处理][From:{msg.SendFrom}][{msg.NoticeId}]",
                        ApiUrl = $"{_jobApiHost.SiteNoticeCenterHost}/api/notice/sendNoticeToUser",
                        QueueName = "Immediately",
                        DelayFromMinutes = 5,
                        PostData = new NoticeSendBase
                        {
                            orgid = msg.OrganizeId,
                            user = msg.Recipient,
                            message = $"[][{noticedata?.xm}:{noticedata?.ch}:{noticedata?.fknr}]",
                            msgid = msg.Id,
                            noticeid = msg.NoticeId,
                        },
                        SuccessCall = new Hangfire.HttpJob.Client.HttpCallbackJob
                        {
                            Method = "Post",
                            Url = ApiManageHelper.NoticeSendSuccessApi,
                            Data = new Request<object>
                            {
                                AppId = _appId,
                                OrganizeId = msg.OrganizeId,
                                Timestamp = DateTime.Now,
                                Data = new
                                {
                                    QueueId = msg.Id,
                                }
                            }
                        },
                        FailedCall = new Hangfire.HttpJob.Client.HttpCallbackJob
                        {
                            Method = "Post",
                            Url = ApiManageHelper.NoticeSendFailedApi,
                            Data = new Request<object>
                            {
                                AppId = _appId,
                                OrganizeId = msg.OrganizeId,
                                Timestamp = DateTime.Now,
                                Data = new
                                {
                                    QueueId = msg.Id,
                                }
                            }
                        }
                    });
                    if (result.IsSuccess)
                    {
                        procIds += result.JobId + ",";
                        var successCallRequest = new Request<object>
                        {
                            AppId = _appId,
                            OrganizeId = msg.OrganizeId,
                            Timestamp = DateTime.Now,
                            Data = new
                            {
                                QueueId = msg.Id,
                                JobId = result.JobId
                            }
                        };
                        await ApiManageHttpHelper.PostAsync<bool>($"{ApiManageHelper.NoticeProcSuccessApi}", successCallRequest.ToJson());
                    }
                    else
                    {
                        var failedCallRequest = new Request<object>
                        {
                            AppId = _appId,
                            OrganizeId = msg.OrganizeId,
                            Timestamp = DateTime.Now,
                            Data = new
                            {
                                QueueId = msg.Id,
                                ErrMessage = result.ErrMessage
                            }
                        };
                        await ApiManageHttpHelper.PostAsync<bool>($"{ApiManageHelper.NoticeProcFailedApi}", failedCallRequest.ToJson());
                    }
                }

            }
            return procIds;
        }
        /// <summary>
        /// 超时未读消息重发
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("MrqcNoticeUnReadProc")]
        public async Task<string> MrqcNoticeUnReadProcAsync()
        {
            string procIds = string.Empty;
            //循环任务 查找未处理消息
            var msgList = await ApiManageHttpHelper.PostAsync<List<MsgNoticeQueueBasicVO>>($"{ApiManageHelper.NoticeUnReadProcApi}", (new Request<object>
            {
                AppId = _appId,
                OrganizeId = ConfigInitHelper.SysConfig.Top_OrganizeId,
                Timestamp = DateTime.Now,
            }).ToJson());
            if (msgList != null && msgList.Data != null && msgList.Data.Count > 0)
            {
                foreach (var msg in msgList.Data)
                {
                    MrqcNoticeDataDTO noticedata = new MrqcNoticeDataDTO();
                    if (!string.IsNullOrWhiteSpace(msg.ContentData))
                    {
                        noticedata = JsonConvert.DeserializeObject<MrqcNoticeDataDTO>(msg.ContentData);
                    }
                    var result = await JobHelper.AddBackgroundJobPost(new HttpJobRequest
                    {
                        serviceUrl = $"{_jobServer}/job",
                        JobName = $"[病历质控未读重发][From:{msg.SendFrom}][{msg.NoticeId}]",
                        ApiUrl = $"{_jobApiHost.SiteNoticeCenterHost}/api/notice/sendNoticeToUser",
                        QueueName = "Immediately",
                        DelayFromMinutes = 5,
                        PostData = new NoticeSendBase
                        {
                            orgid = msg.OrganizeId,
                            user = msg.Recipient,
                            message = $"[病历质控][{noticedata?.xm}:{noticedata?.ch}:{noticedata?.fknr}]",
                            msgid = msg.Id,
                            noticeid = msg.NoticeId,
                        }

                    });
                }
            }
            return procIds;
        }
    }
}
