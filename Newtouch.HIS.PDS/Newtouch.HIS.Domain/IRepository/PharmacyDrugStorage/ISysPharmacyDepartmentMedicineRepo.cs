using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_bmypxx
    /// </summary>
    public interface ISysPharmacyDepartmentMedicineRepo : IRepositoryBase<SysPharmacyDepartmentMedicineEntity>
    {
        /// <summary>
        /// 获取药品库位
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetYpkw(string ypCode, string yfbmCode, string orgId);

        /// <summary>
        /// 入库部门是否有权限使用该药
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="rkbm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        bool CheckRkbmOwnMedicine(string yp, string rkbm, string organizeId);

        /// <summary>
        /// 根据药编码和药房部门编码获取本部门药品信息
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        IList<SysPharmacyDepartmentMedicineEntity> SelectData(string ypCode, string organizeId, string yfbmCode);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="bmypId"></param>
        /// <param name="zt"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        int UpdateZt(string bmypId, string zt, string lastModifierCode, DateTime lastModifyTime);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        int UpdateZt(string ypCode, string zt, string organizeId, string lastModifierCode, DateTime lastModifyTime);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteItem(string ypCode, string organizeId);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="bmypId"></param>
        /// <returns></returns>
        int DeleteItem(string bmypId);
    }
}
