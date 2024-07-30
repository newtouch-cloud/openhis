using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public class WzCrkfsApp : AppBase, IWzCrkfsApp
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;

        public WzCrkfsApp(IWzCrkfsRepo wzCrkfs)
        {
            _wzCrkfsRepo = wzCrkfs;
        }

        /// <summary>
        /// submit crkfs maintenance form
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                wzCrkfsEntity.Create(true);
                return _wzCrkfsRepo.Insert(wzCrkfsEntity);
            }
            var dbCrkfs = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            if (dbCrkfs == null) return 0;
            dbCrkfs.crkbz = wzCrkfsEntity.crkbz;
            dbCrkfs.crkfsmc = wzCrkfsEntity.crkfsmc;
            dbCrkfs.zt = wzCrkfsEntity.zt;
            dbCrkfs.Modify();
            return _wzCrkfsRepo.Update(dbCrkfs);
        }
    }
}
