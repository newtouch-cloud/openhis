using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 药品
    /// </summary>
    public interface ISysMedicineDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineEntity"></param>
        /// <param name="medicinePropEntity"></param>
        /// <param name="ypId"></param>
        void SubmitMedicine(SysMedicineVO model, int? ypId);

        /// <summary>
        /// 修改医保同步时间
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        bool YibaoUpload(int ypId, out string error);

        /// <summary>
        /// 绑定药品list集合
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode, string keyword = null);


		
		/// <summary>
		/// 修改信息时,绑定修改值
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		SysMedicineVO GetFormJson(int keyValue);

        /// <summary>
        /// 获取系统人员信息
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineVO SelectMedicineInfo(string ypCode, string organizeId);

		/// <summary>
		/// 获取药品系数
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		string GetTradePricePlus(string name);

		/// <summary>
		/// 获取当前组织下的系统药品信息
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysMedicineVO> GetPaginationListdm(string organizeId, Pagination pagination, string type, string keyword = null);
        IList<SysMedicineMldzxxVO> GetPaginationListMldzxx(string organizeId, Pagination pagination, string type, string keyword = null);
        /// <summary>
        /// 查询医保药品信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        IList<G_yb_ypxxVO> GetYpypxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		/// <summary>
		/// 保存医保对码信息
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		int SaveYpYb(G_yb_ypxxVO ybxx, int? ypid, string organizeId);


		/// <summary>
		/// 系统材料信息
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysMedicineVO> GetclxxList(string organizeId, Pagination pagination, string type, string keyword = null);

		/// <summary>
		/// 查询医保材料信息 根据系统材料信息
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		IList<G_yb_clxxVO> GetYbclxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		/// <summary>
		/// 保存材料对码
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		int SaveYpcl(G_yb_clxxVO ybxx, int? ypid, string organizeId);



		/// <summary>
		/// 系统项目信息
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysChargeItemEntity> GetxmxxList(string organizeId, Pagination pagination, string type, string keyword = null);


		/// <summary>
		/// 查询医保项目信息 根据系统项目信息
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		IList<G_yb_xmxxVO> GetYbxmxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		int SaveYpxm(G_yb_xmxxVO ybxx, int? ypid, string organizeId);

        /// <summary>
        /// 药品用法
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUsageEntity> GetUsageFloat(string keyword);

    }
}
