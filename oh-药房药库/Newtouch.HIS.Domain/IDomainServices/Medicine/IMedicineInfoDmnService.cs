using Newtouch.Core.Common;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IMedicineInfoDmnService
    {
        /// <summary>
        /// 本部门药品信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MedicineInfoVO> GetMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// 本部门药品信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// 同步本部门药品信息
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="operateType"></param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        string FreshList(string[] yp, int operateType, string orgId, string yfbmCode);

        /// <summary>
        /// 控制
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="type">1 控制 2 正常</param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int ControlMedicine(string ypCode, string type, string orgId, string yfbmCode);

        /// <summary>
        /// 内部发药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        IList<HandOutMedicineQueryVO> GetHandOutMedicineInfoList(Pagination pagination, QueryMedicineInfoReqVO vo);

        /// <summary>
        /// 本部门同步药品候选信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetTbMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// 获取本部门过期药品信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<MedicineInfoVO> GetExpiredDrugsData(Pagination pagination, MedicineInfoParam param, string orgId);

        /// <summary>
        /// 获取药房人员列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);
    }
}
