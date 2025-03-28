using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 物资类别操作
    /// </summary>
    public class ProductTypeApp : AppBase, IProductTypeApp
    {
        private readonly IWzTypeRepo _wzTypeRepo;

        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzTypeEntity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(WzTypeEntity wzTypeEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzTypeEntity.Create(true);
                return _wzTypeRepo.Insert(wzTypeEntity);
            }
            var dbType = _wzTypeRepo.FindEntity(p => p.Id == keyWord);
            if (dbType == null) return 0;
            dbType.parentId = wzTypeEntity.parentId;
            dbType.name = wzTypeEntity.name;
            dbType.remark = wzTypeEntity.remark;
            dbType.px = wzTypeEntity.px;
            dbType.zt = wzTypeEntity.zt;
            dbType.Modify();
            return _wzTypeRepo.Update(dbType);
        }
    }
}
