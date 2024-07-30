using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 物资收费项目对照
    /// </summary>
    public class RelProductAndsfxmApp : AppBase, IRelProductAndsfxmApp
    {
        private readonly IRelProductAndsfxmRepo _relProductAndsfxmRepo;

        /// <summary>
        /// 提交物资收费项目对照
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string SubmitRelProductAndsfxm(RelProductAndsfxmEntity entity)
        {
            if (entity == null) return "提交内容不能为空";
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                //新增
                var oldEntitis = _relProductAndsfxmRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.sfdlCode == entity.sfdlCode && p.productId == entity.productId);
                if (oldEntitis != null && oldEntitis.Any()) return "已存在相同关联关系，不能重复提交";
                entity.Create(true);
                return _relProductAndsfxmRepo.Insert(entity) > 0 ? "" : "新增失败";
            }
            entity.Modify();
            return _relProductAndsfxmRepo.Update(entity) > 0 ? "" : "更新失败";
        }
    }
}
