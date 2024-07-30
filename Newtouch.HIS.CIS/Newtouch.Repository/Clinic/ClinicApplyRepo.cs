using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Clinic;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Clinic
{
    public class ClinicApplyRepo : RepositoryBase<ClinicApplyInfoEntity>, IClinicApplyRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ClinicApplyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 更新申请状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sqzt"></param>
        public void Updatesqzt(string Id, int sqzt,string orgId)
        {
            //var dbEntity = this.FindEntity(Id);

            var dbEntity = this.IQueryable().Where(p => p.Id == Id && p.zt == "1" && p.OrganizeId== orgId).FirstOrDefault();
            if (dbEntity != null)
            {
                //properties
                dbEntity.sqzt = sqzt;
                dbEntity.Modify(Id);
                this.Update(dbEntity);
            }

        }


        public ClinicApplyInfoEntity GetYczl(string Id, string orgId)
        {
            var result = this.IQueryable().Where(p => p.Id == Id && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            return result;
        }

    }
}
