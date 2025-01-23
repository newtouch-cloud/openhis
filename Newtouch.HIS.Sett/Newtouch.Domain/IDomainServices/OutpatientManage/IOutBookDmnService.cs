using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.Entity.OutpatientManage;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface IOutBookDmnService
    {
        IList<OutBookVO> GetPagintionList(Pagination pagination, string orgId, string keyword);
        IList<OutPatientStaffEntity> GetStaffList(string orgId);
        void SaveDatapb(List<OutBookScheduleEntity> pbList, string orgId);
        void SaveDatatzcz(Decimal ScheduId, string czzt, string orgId,string tzyy);
        string getStaffName(string gh);
        OutBookVO getArrangeInfo(int ghpbId,string orgId);
        OutBookDateVO getDateInfo(string organizeId, int ghpbId);
        IList<OutBookDateVO>  getDateTimeInfo(string organizeId, int ghpbId,string timeslot);
        int UpdateArrange(OutBookArrangeVO entity, int ghpbId, string orgId, string User, DateTime Time);
        int InsertArrange(OutBookArrangeVO entity, string orgId, string User, DateTime Time,int ghpbIdNew);
        int DeleteArrange(int ghpbId,string orgId);
        int InsertghpbTime(string begintime,string endtime, string orgId, string User, DateTime Time,int id);
        int getghpbId();
        IList<string> getStaffListByKs(string ks, string orgId);
        IList<string> getDateInfosjd(string orgId);
        IList<string> getDateInfosjdcount(int ghpbId,string orgId);
        /// <summary>
        /// 获取排班门诊诊疗项目组合
        /// </summary>
        /// <param name="zhcode"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MzpbZlxmzh> GetMzpbZlxmzh(string zhcode, string keyword, string orgId);
        /// <summary>
        /// 获取排班门诊诊疗项目组合明细
        /// </summary>
        /// <param name="zhcode"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MzpbZlxmzh> GetMzpbZlxmzhDetail(string zhcode, string keyword, string orgId);


        string PatBookGh(string cardNo, int ScheduId, string brxz, string Doctor, DateTime OutDate, string orgId);

        int CancalBook(string BookId);
    }
}
