using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Infrastructure;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：入院登记-诊断
    /// </summary>
    public class YBInpPatRegInfoDiagRepo : RepositoryBase<YBInpPatRegInfoDiagEntity>, IYBInpPatRegInfoDiagRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public YBInpPatRegInfoDiagRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }
        public void Submit(YBInpPatRegInfoDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.BKF263))
                {
                    ety.Modify(ety.Id);
                    this.Update(ety);
                }
                else
                {
                    ety.BKF263 = EFDBBaseFuncHelper.Instance.MedicalRecordLSH(ety.OrganizeId);
                    ety.Create(true);
                    this.Insert(ety);
                }
            }
        }
    }
}