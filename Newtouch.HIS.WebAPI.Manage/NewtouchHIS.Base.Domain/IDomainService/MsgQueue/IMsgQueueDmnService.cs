using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface IMsgQueueDmnService : IScopedDependency
    {
        #region 消息组管理
        /// <summary>
        /// 添加消息组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// 更新消息组配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ModifyNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// 消息组配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeGroupVO>> NoticeGroupQuery(MsgNoticeGroupBasicVO request);
        #endregion

        #region 消息队列
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> AddNotice(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// 更新消息状态
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string queueId, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// 批量更新消息状态
        /// </summary>
        /// <param name="queueIds"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <param name="procSuccess"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string[] queueIds, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// 更新消息基本信息
        /// </summary>
        /// <param name="ety"></param>
        /// <returns></returns>
        Task<bool> NoticeQueueInfoModify(MsgNoticeQueueVO ety);
        #endregion

        #region Query
        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueVO>> NoticeInfoQuery(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// 质控消息列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueModelVO<MrqcMsgNoticeModel>>> MrqcNoticeQuery(MsgNoticeQueueQueryVO request);
        /// <summary>
        /// 获取消息通知列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueQueryRspoVO>> NoticeQuery(MsgNoticeQueueQueryVO request, string AppId, DBEnum db = DBEnum.BaseDb);

        /// <summary>
        /// 消息队列查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<MsgNoticeQueueQueryRspoVO>>> NoticeQueryPage(OLPagination<MsgNoticeQueueQueryVO> request, string AppId);
        #endregion
    }
}
