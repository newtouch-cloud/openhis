using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.System.Controllers
{
    /// <summary>
    /// 消息队列
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MsgQueueController : ControllerBase
    {
        private readonly IMsgQueueDmnService _msgQueueDmn;

        public MsgQueueController(IMsgQueueDmnService msgQueueDmn)
        {
            _msgQueueDmn = msgQueueDmn;
        }
        /// <summary>
        /// 新增消息组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NewNoticeGroup")]
        public async Task<BusResult<string>> NewNoticeGroupAsync(Request<MsgNoticeGroupBasicVO> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.AppId = request.AppId;
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _msgQueueDmn.AddNoticeGroup(request.Data);
            if (string.IsNullOrWhiteSpace(data))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "未知原因导致添加失败，请联系管理员" };
            }
            return new BusResult<string> { code = ResponseResultCode.SUCCESS, msg = "添加成功", Data = data };

        }
        /// <summary>
        /// 查询消息组配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNoticeGroup")]
        public async Task<BusResult<List<MsgNoticeGroupVO>>> GetNoticeGroupAsync(Request<MsgNoticeGroupBasicVO> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MsgNoticeGroupVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _msgQueueDmn.NoticeGroupQuery(request.Data);
            if (data.Count == 0)
            {
                return new BusResult<List<MsgNoticeGroupVO>> { code = ResponseResultCode.FAIL, msg = "未找到可用数据" };
            }
            return new BusResult<List<MsgNoticeGroupVO>> { code = ResponseResultCode.SUCCESS, Data = data };

        }

        /// <summary>
        /// 新增通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NewNotice")]
        public async Task<BusResult<List<string>>> NewNoticeAsync(Request<MsgNoticeQueueBasicVO> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<string>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.AppId = request.AppId;
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _msgQueueDmn.AddNotice(request.Data);
            if (data.Count == 0)
            {
                return new BusResult<List<string>> { code = ResponseResultCode.FAIL, msg = "未知原因导致添加失败，请联系管理员" };
            }
            return new BusResult<List<string>> { code = ResponseResultCode.SUCCESS, msg = "添加成功", Data = data };

        }
        /// <summary>
        /// 查询消息详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNoticeInfo")]
        public async Task<BusResult<List<MsgNoticeQueueVO>>> GetNoticeInfoAsync(Request<MsgNoticeQueueBasicVO> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _msgQueueDmn.NoticeInfoQuery(request.Data);
            if (data.Count == 0)
            {
                return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.FAIL, msg = "未找到可用数据" };
            }
            return new BusResult<List<MsgNoticeQueueVO>> { code = ResponseResultCode.SUCCESS, Data = data };

        }
        /// <summary>
        /// 质控消息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NoticeQuery")]
        public async Task<BusResult<List<MsgNoticeQueueQueryRspoVO>>> NoticeQueryAsync(Request<MsgNoticeQueueQueryVO> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MsgNoticeQueueQueryRspoVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _msgQueueDmn.NoticeQuery(request.Data, request.AppId, DBEnum.BaseDb);
            if (data.Count == 0)
            {
                return new BusResult<List<MsgNoticeQueueQueryRspoVO>> { code = ResponseResultCode.FAIL, msg = "未找到可用数据" };
            }
            return new BusResult<List<MsgNoticeQueueQueryRspoVO>> { code = ResponseResultCode.SUCCESS, Data = data };

        }

       
    }
}
