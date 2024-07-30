using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 物资类别操作
    /// </summary>
    public class ProductTypeApp : AppBase, IProductTypeApp
    {
        private readonly IWzTypeRepo _wzTypeRepo;

        public ProductTypeApp(IWzTypeRepo wzTypeRepo)
        {
            _wzTypeRepo = wzTypeRepo;
        }

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
