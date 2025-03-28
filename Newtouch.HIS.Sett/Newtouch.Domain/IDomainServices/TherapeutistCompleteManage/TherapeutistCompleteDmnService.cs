using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ITherapeutistCompleteDmnService
    {
        #region 治疗师工作列表
        IList<TherapeutistListGridVO> GetTherapeutistListWorkedList(Pagination pagination, string BeginDate, string EndDate, string hzxm, string zls, string dl, string orgId);
        #endregion

        #region 治疗师工作时间
        /// <summary>
        /// 治疗师工作时间安排列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="ysgh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TherapeutistWorkPlanGridVO> GetTherapeutistWorkPlanList(Pagination pagination, int? year, int? month, string ysgh, string orgId);

        /// <summary>
        /// 获取所有治疗师和默认天数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<AddStaffPlanVO> GetAllRehabDoctor(string orgId);

        /// <summary>
        /// 新增排班时，提交到数据库
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        void SubmitPlan(Dictionary<string, List<AddStaffPlanVO>> vo, string orgId);

        /// <summary>
        /// 查看单个治疗师单月排班信息
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        EditPlanVO GetFormJson(string keyvalue, string orgId);

        /// <summary>
        /// 编辑单个治疗师单个时间情况
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyvalue"></param>
        void EditRehabDoctorRange(DoctorWorkingDaysPlanEntity entity, string keyvalue, string orgId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyvalue"></param>
        void DelArrange(string keyvalue);
        #endregion

        #region 时长调整

        /// <summary>
        /// 时长调整时，统计治疗师时长
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<AdjustStaffHourVO> GetStaffWorkedHour(string orgId, string gh, string year, string month);

        /// <summary>
        /// 得到治疗师每月时长总和
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<AdjustStaffHourVO> GetStaffEachMonthHour(Pagination pagination, string orgId, string gh, string year,
            string month);

        void UpdateTime(Dictionary<string, List<submitTimeVO>> vo);

        #endregion

        #region 治疗师工作统计
        IList<StaffReportVO> GetStaffReport(Pagination pagination, string orgId, string ysgh, string year, string month);
        #endregion

        #region 治疗师工作效率柱状图
        SCNumBO GetVisitSC(string orgId);

        /// <summary>
        /// 获取治疗师工作效率详细
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IList<AdjustStaffHourVO> GetVisitDetailSC(Pagination pagination,string orgId, string type, string time);
        #endregion

    }
}
