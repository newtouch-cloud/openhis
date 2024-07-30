using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
    public interface IOutBookScheduleRepo : IRepositoryBase<OutBookScheduleEntity>
    {
        IList<OutBookScheduleVO> GetPagintionList(Pagination pagination, string orgId, DateTime time);
        IList<OutBookScheduleVO> GetPagintionListTime(string orgId, string kssj, string jssj, string ys, string czztcx, string ScheduId, string ks, string lx, string isbook);
        /// <summary>
        /// 排班分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="ys"></param>
        /// <param name="czztcx"></param>
        /// <param name="ScheduId"></param>
        /// <param name="ks"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        IList<OutBookScheduleVO> GetPagintionListTime(Pagination pagination, string orgId, string kssj, string jssj, string ys, string czztcx, string ScheduId, string ks, string lx, string isbook, out Pagination paging);
        int ExecSchedule();
        /// <summary>
        /// 项目组合排班
        /// </summary>
        /// <returns></returns>
        int ExecSchedulebyGroup();
    }
}
