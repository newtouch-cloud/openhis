using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineAntibioticInfoRepo : RepositoryBase<SysMedicineAntibioticInfoEntity>, ISysMedicineAntibioticInfoRepo
    {
        public SysMedicineAntibioticInfoRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 提交抗生素类别信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Id</returns>
        public string SubmitForm(SysMedicineAntibioticInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.Id))
            {
                var dbEntity = this.FindEntity(p => p.Id == entity.Id && p.OrganizeId == entity.OrganizeId);
                //AppLogger.WriteEntityChangeRecordLog(dbEntity, entity, SysMedicineAntibioticInfoEntity.GetTableName(), entity.Id.ToString());
                dbEntity.kssLevel1TypeId = entity.kssLevel1TypeId;
                dbEntity.kssLevel2TypeId = entity.kssLevel2TypeId;
                dbEntity.qxjb = entity.qxjb ?? "";
                dbEntity.jlfwBegin = entity.jlfwBegin;
                dbEntity.jlfwEnd = entity.jlfwEnd;
                dbEntity.pcfwBegin = entity.pcfwBegin;
                dbEntity.pcfwEnd = entity.pcfwEnd;
                dbEntity.DDDnum = entity.DDDnum;
                dbEntity.DDDdw = entity.DDDdw;
                dbEntity.xj = entity.xj;
                dbEntity.zt = entity.zt;
                dbEntity.kssjldw = entity.kssjldw;
                dbEntity.kp = entity.kp ?? "";
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = entity.LastModifyTime;
                this.Update(dbEntity);
            }
            else
            {
                entity.qxjb = entity.qxjb ?? "";
                entity.kp = entity.kp ?? "";
                entity.Create(true);
                this.Insert(entity);
            }
            return entity.Id;
        }

        /// <summary>
        /// 通过Id获取药品抗生素信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public SysMedicineAntibioticInfoEntity GetKssInfo(string Id, string OrganizeId)
        {
            return this.FindEntity(p => p.Id == Id && p.OrganizeId == OrganizeId);
        }
    }
}
