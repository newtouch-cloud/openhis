using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
    public interface IOutBookScheduleRepo : IRepositoryBase<OutBookScheduleEntity>
    {
        IList<OutBookScheduleVO> GetPagintionList(Pagination pagination, string orgId, DateTime time);
        IList<OutBookScheduleVO> GetPagintionListTime(string orgId,string kssj,string jssj,string ys,string czztcx, string ScheduId, string ks, string lx);
        int ExecSchedule();
        /// <summary>
        /// 项目组合排班
        /// </summary>
        /// <returns></returns>
        int ExecSchedulebyGroup();
    }
}
