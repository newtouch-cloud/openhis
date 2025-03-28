using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 药房
    /// </summary>
    public interface IPharmacyDmnService
    {
        /// <summary>
        /// 查询药房窗口信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfckCode"></param>
        /// <param name="yfckmc"></param>
        /// <param name="topOrganizeId"></param>
        /// <returns></returns>
        IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<PharmacyDepartmentOpenMedicineRepoVO> OpenMedicineIndex(string organizeId, string keyword = null);

        /// <summary>
        /// 查看药房部门药品信息(大类)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        PharmacyDepartmentOpenMedicineRepoVO SelectDepartmentMedicine(string dlCode, string yfbmCode,string organizeId);

        /// <summary>
        /// 查看药房部门药品信息(大类)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId);

    }
}
