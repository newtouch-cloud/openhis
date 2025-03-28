using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 库存盘点 xt_yp_pdxx
    /// </summary>
    public class SysMedicineInventoryRepo : RepositoryBase<SysMedicineInventoryEntity>, ISysMedicineInventoryRepo
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineInventoryRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 判断是否有未完成的盘点 （开始）
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventory()
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().Count(a => a.yfbmCode == Constants.CurrentYfbm.yfbmCode && a.Jssj == null && a.OrganizeId == organizeId);
        }

        /// <summary>
        /// 判断是否有未完成的盘点 （结束）
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(string pdId)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().Count(a => a.yfbmCode == Constants.CurrentYfbm.yfbmCode && a.Jssj == null && a.pdId == pdId && a.OrganizeId == organizeId);
        }

        /// <summary>
        /// 根据盘点ID获取盘点信息
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public SysMedicineInventoryEntity SelectPdInfByPdId(string pdId, string organizeId)
        {
            return IQueryable().FirstOrDefault(p => p.yfbmCode == Constants.CurrentYfbm.yfbmCode && p.pdId == pdId && p.OrganizeId == organizeId);
        }
    }
}
