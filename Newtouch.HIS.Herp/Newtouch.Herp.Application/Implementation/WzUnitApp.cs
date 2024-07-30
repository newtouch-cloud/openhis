using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 物资单位操作
    /// </summary>
    public class WzUnitApp : AppBase, IWzUnitApp
    {
        private readonly IWzUnitRepo _wzUnitRepo;

        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzUnitEntity.Create(true);
                return _wzUnitRepo.Insert(wzUnitEntity);
            }
            var dbUnit = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            if (dbUnit == null) return 0;
            dbUnit.name = wzUnitEntity.name;
            dbUnit.remark = wzUnitEntity.remark;
            dbUnit.zt = wzUnitEntity.zt;
            dbUnit.Modify();
            return _wzUnitRepo.Update(dbUnit);
        }
    }
}
